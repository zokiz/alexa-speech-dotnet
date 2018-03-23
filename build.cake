// Reference tools
#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=OctopusTools"

// Load custom scripts.
#load "local:?path=build/parameters.cake"
#load "local:?path=build/packages.cake"

// -------------------------------------------------------------------
// ARGUMENTS
// -------------------------------------------------------------------

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Debug");
var buildNumber = Argument("build_number", "0.0.0.0");

// -------------------------------------------------------------------
// PARAMETERS
// -------------------------------------------------------------------

Parameters parameters = Parameters.GetParameters(Context);
var BuildPackages = new List<BuildPackage>();
var DeployPackages = new List<DeployPackage>();

Setup(context =>
{
	var hiddenApiKeyNuGetServer = new string('*', parameters.NuGetArtifactsServer.ApiKey.Length - 4) + parameters.NuGetArtifactsServer.ApiKey.Substring(parameters.NuGetArtifactsServer.ApiKey.Length - 5, 4);
	var hiddenApiKeyNuGetDeploymentServer = new string('*', parameters.NuGetDeploymentArtifactsServer.ApiKey.Length - 4) + parameters.NuGetDeploymentArtifactsServer.ApiKey.Substring(parameters.NuGetDeploymentArtifactsServer.ApiKey.Length - 5, 4);
	var hiddenApiKeyOctopusServer = new string('*', parameters.OctopusDeploymentServer.ApiKey.Length - 4) + parameters.OctopusDeploymentServer.ApiKey.Substring(parameters.OctopusDeploymentServer.ApiKey.Length - 5, 4);

	// Print some of the available parameters
	Information("Is Local Build: {0}", parameters.IsLocalBuild);
	Information("Solution: {0}", parameters.SolutionName);
	Information("Build output directory: {0}", parameters.BuildOutputDirectory);
	Information("Build output test results directory: {0}", parameters.BuildOutputTestResultsDirectory);
	Information("Build output NuGet packages directory: {0}", parameters.BuildOutputNuGetDirectory);
	Information("Build output NuGet deployment packages directory: {0}", parameters.BuildOutputNuGetDeploymentDirectory);
	Information("NuGet server: {0} | (Key: {1})", parameters.NuGetArtifactsServer, hiddenApiKeyNuGetServer);
	Information("NuGet deployment server: {0} | (Key: {1})", parameters.NuGetDeploymentArtifactsServer, hiddenApiKeyNuGetDeploymentServer);
	Information("Octopus server: {0} | (Key: {1})", parameters.OctopusDeploymentServer, hiddenApiKeyOctopusServer);

	// Setup packages
	BuildPackages.Add(new BuildPackage
	{
		Name = "Alexa.Speech",
		Source = Directory("./Speech") + File("Alexa.Speech.nuspec"),
		OutputDirectory = parameters.BuildOutputNuGetDeploymentDirectory,
		ArtifactStore = parameters.NuGetDeploymentArtifactsServer
	});

	DeployPackages.Add(new DeployPackage
	{
		Name = "Alexa.Speech",
		Source = Directory("./Speech") + File("Alexa.Speech.nuspec"),
		ArtifactStore = parameters.NuGetDeploymentArtifactsServer,
		Deployer = parameters.OctopusDeploymentServer
	});
});

// -------------------------------------------------------------------
// TASKS: BUILD
// -------------------------------------------------------------------

Task("Build")
	.IsDependentOn("Restore")
	.IsDependentOn("Versioning")
	.Does(() =>
{
	var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
		DiagnosticOutput = false,
		NoRestore = true,
		Verbosity = DotNetCoreVerbosity.Minimal
    };
	DotNetCoreBuild(parameters.SolutionName, settings);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
	var projects = GetFiles("./**/*.csproj");
    foreach(var project in projects)
    {
		DotNetCoreRestore(project.ToString());
    }
});

Task("Clean")
    .Does(() =>
{
	CleanDirectory(parameters.BuildOutputDirectory);

	var projectsPaths = GetFiles("./**/*.csproj").Select(project => project.GetDirectory());
    foreach(var path in projectsPaths)
    {
        CleanDirectories(path + "/bin");
        CleanDirectories(path + "/obj");
    }
});

Task("Versioning")
	.Does(() =>
{
	CreateAssemblyInfo(parameters.CommonAssemblyInfoFile, new AssemblyInfoSettings
	{
		Version = buildNumber,
		FileVersion = buildNumber,
		Company = parameters.AssemblyInfoCompany,
		Product = parameters.AssemblyInfoProduct,
		Copyright = parameters.AssemblyInfoCopyright,
		ComVisible = false
	});
	Information("Common assembly file: {0}", parameters.CommonAssemblyInfoFile);
	Information("Assembly company: {0}", parameters.AssemblyInfoCompany);
	Information("Assembly product: {0}", parameters.AssemblyInfoProduct);
	Information("Assembly copyright: {0}", parameters.AssemblyInfoCopyright);
	Information("Created common assembly info file with version {0}.", buildNumber);
});

// -------------------------------------------------------------------
// TASKS: TESTS
// -------------------------------------------------------------------

Task("RunTests")
    .Does(() =>
{
	CreateDirectory(parameters.BuildOutputTestResultsDirectory);

	var assemblies = GetFiles("./**/bin/Release/**/*.Tests.dll");
	var args = new ProcessArgumentBuilder();
	foreach(var assembly in assemblies)
	{
		args.Append(assembly.ToString());
	}
	if (parameters.IsRunningOnTeamCity)
	{
		args.Append("-teamcity");
	}
	args.Append("-xml {0}", parameters.BuildOutputTestResultsDirectory + File("./tests-results.xml"));
	DotNetCoreExecute("./tools/xunit.runner.console.2.3.1/tools/netcoreapp2.0/xunit.console.dll", args, new DotNetCoreExecuteSettings
	{
		WorkingDirectory = Directory(assemblies.First().GetDirectory().ToString())
	});
});

// -------------------------------------------------------------------
// TASKS: PACKAGING
// -------------------------------------------------------------------

Task("BuildPackages")
	.IsDependentOn("PreBuildPackages")
	.Does(() =>
{
	CreateDirectory(parameters.BuildOutputNuGetDirectory);
	CreateDirectory(parameters.BuildOutputNuGetDeploymentDirectory);
	
	BuildPackages.ForEach((package) => 
	{
		PackageManager.CreatePackage(Context, buildNumber, package);
	});
});

Task("PreBuildPackages")
	.IsDependentOn("Build")
    .Does(() =>
{
	var settings = new DotNetCorePublishSettings
    {
        Configuration = configuration,
		DiagnosticOutput = false,
		NoRestore = true,
		Verbosity = DotNetCoreVerbosity.Minimal
    };
	DotNetCorePublish(parameters.SolutionName, settings);
});

Task("PublishPackages")
    .Does(() =>
{
	BuildPackages.ForEach((package) => 
	{
		PackageManager.PushPackage(Context, buildNumber, package);
	});
});

// -------------------------------------------------------------------
// TASKS: RELEASE AND DEPLOYMENT
// -------------------------------------------------------------------

Task("CreateReleases")
    .Does(() =>
{
	// Create and push OctopusDeploy releases.
	DeployPackages.ForEach((package) => 
	{
        Information("Creating release: {0}.", package.Name);
		ReleaseManager.CreateRelease(Context, buildNumber, package);
	});
});

RunTarget(target);
