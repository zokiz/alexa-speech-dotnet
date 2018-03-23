#load "./servers.cake"

public class Parameters
{
	private const string SOLUTION_NAME = "Alexa.sln";
	private const string COMMON_ASSEMBLY_INFO_FILENAME = "CommonAssemblyInfo.cs";
	private const string ASSEMBLY_INFO_COMPANY = "Zoran Zlatanov";
	private const string ASSEMBLY_INFO_PRODUCT = "Alexa Speech for .NET";
	private const string ASSEMBLY_INFO_COPYRIGHT = "Copyright (c) {0} Zoran Zlatanov, All rights reserved.";
	private const string BUILD_OUTPUT_DIRECTORY_NAME = "BuildOutput";
	private const string BUILD_OUTPUT_NUGET_DIRECTORY_NAME = "nuget-server";
	private const string BUILD_OUTPUT_NUGET_DEPLOYMENT_DIRECTORY_NAME = "nuget-deployment-server";
	private const string BUILD_OUTPUT_TEST_RESULTS_DIRECTORY_NAME = "TestResults";
	private const string NUGET_ARTIFACTS_SERVER_URL_VARIABLE_NAME = "nuget_server";
	private const string NUGET_ARTIFACTS_SERVER_APIKEY_VARIABLE_NAME = "nuget_server_api_key";
	private const string NUGET_DEPLOYMENT_ARTIFACTS_SERVER_URL_VARIABLE_NAME = "nuget_deployment_server";
	private const string NUGET_DEPLOYMENT_ARTIFACTS_SERVER_APIKEY_VARIABLE_NAME = "nuget_deployment_server_api_key";
	private const string OCTOPUS_SERVER_URL_VARIABLE_NAME = "octopus_server";
	private const string OCTOPUS_SERVER_APIKEY_VARIABLE_NAME = "octopus_server_api_key";
	private const string TEAMCITY_VERSION_KEY = "TEAMCITY_VERSION";

	public string SolutionName { get; set; }
	public FilePath CommonAssemblyInfoFile { get; set; }
	public string AssemblyInfoCompany { get; set; }
	public string AssemblyInfoProduct { get; set; }
	public string AssemblyInfoCopyright { get; set; }
	public bool IsLocalBuild { get; private set; }
    public bool IsRunningOnUnix { get; private set; }
    public bool IsRunningOnWindows { get; private set; }
	public DirectoryPath BuildOutputDirectory { get; private set; }
	public DirectoryPath BuildOutputNuGetDirectory { get; private set; }
	public DirectoryPath BuildOutputNuGetDeploymentDirectory { get; private set; }
	public DirectoryPath BuildOutputTestResultsDirectory { get; private set; }
	public NugetServer NuGetArtifactsServer { get; private set; }
	public NugetServer NuGetDeploymentArtifactsServer { get; private set; }
	public OctopusServer OctopusDeploymentServer { get; private set; }
    public bool IsRunningOnTeamCity { get; private set; }

	public static Parameters GetParameters(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        var buildSystem = context.BuildSystem();
		
        return new Parameters {
			SolutionName = SOLUTION_NAME,
			CommonAssemblyInfoFile = new FilePath(COMMON_ASSEMBLY_INFO_FILENAME),
			AssemblyInfoCompany = ASSEMBLY_INFO_COMPANY,
			AssemblyInfoProduct = ASSEMBLY_INFO_PRODUCT,
			AssemblyInfoCopyright = string.Format(ASSEMBLY_INFO_COPYRIGHT, DateTime.Now.Year),
			BuildOutputDirectory = new DirectoryPath(BUILD_OUTPUT_DIRECTORY_NAME),
			BuildOutputNuGetDirectory = new DirectoryPath(BUILD_OUTPUT_DIRECTORY_NAME).Combine(new DirectoryPath(BUILD_OUTPUT_NUGET_DIRECTORY_NAME)),
			BuildOutputNuGetDeploymentDirectory = new DirectoryPath(BUILD_OUTPUT_DIRECTORY_NAME).Combine(new DirectoryPath(BUILD_OUTPUT_NUGET_DEPLOYMENT_DIRECTORY_NAME)),
			BuildOutputTestResultsDirectory = new DirectoryPath(BUILD_OUTPUT_DIRECTORY_NAME).Combine(new DirectoryPath(BUILD_OUTPUT_TEST_RESULTS_DIRECTORY_NAME)),
			NuGetArtifactsServer = new NugetServer(context.EnvironmentVariable(NUGET_ARTIFACTS_SERVER_URL_VARIABLE_NAME), context.EnvironmentVariable(NUGET_ARTIFACTS_SERVER_APIKEY_VARIABLE_NAME)),
			NuGetDeploymentArtifactsServer = new NugetServer(context.EnvironmentVariable(NUGET_DEPLOYMENT_ARTIFACTS_SERVER_URL_VARIABLE_NAME), context.EnvironmentVariable(NUGET_DEPLOYMENT_ARTIFACTS_SERVER_APIKEY_VARIABLE_NAME)),
			OctopusDeploymentServer = new OctopusServer(context.EnvironmentVariable(OCTOPUS_SERVER_URL_VARIABLE_NAME), context.EnvironmentVariable(OCTOPUS_SERVER_APIKEY_VARIABLE_NAME)),
            IsLocalBuild = buildSystem.IsLocalBuild,
            IsRunningOnUnix = context.IsRunningOnUnix(),
            IsRunningOnWindows = context.IsRunningOnWindows(),
			IsRunningOnTeamCity = string.IsNullOrWhiteSpace(context.EnvironmentVariable(TEAMCITY_VERSION_KEY)) ? false : true
        };
    }
}
