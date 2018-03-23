#load "./servers.cake"

public class PackageManager
{
	public static void CreatePackage(ICakeContext context, string buildNumber, BuildPackage package, string configuration = "Release")
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        context.NuGetPack(package.Source, new NuGetPackSettings
        {
            Version = buildNumber,
            RequireLicenseAcceptance = false,
            Symbols = false,
            NoPackageAnalysis = true,
            OutputDirectory = package.OutputDirectory,
            ArgumentCustomization = args => args.Append($"-Prop Configuration={configuration}")
        });
    }

	public static void PushPackage(ICakeContext context, string buildNumber, BuildPackage package)
	{
	    if (context == null)
	    {
	        throw new ArgumentNullException(nameof(context));
	    }
        var filePath = package.OutputDirectory.CombineWithFilePath(new FilePath($"{package.Name}.{buildNumber}.nupkg"));
	    context.NuGetPush(filePath, new NuGetPushSettings
	    {
	        Source = package.ArtifactStore.Url,
	        ApiKey = package.ArtifactStore.ApiKey,
	        Verbosity = NuGetVerbosity.Detailed
	    });
	    context.Log.Write(Verbosity.Normal, LogLevel.Information, "NuGet package {0} pushed to {1}.", filePath, package.ArtifactStore.Url);
	}
}

public class ReleaseManager
{
	public static void CreateRelease(ICakeContext context, string buildNumber, DeployPackage package)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        context.OctoCreateRelease(package.Name, new CreateReleaseSettings
        {
            Server = package.Deployer.Url,
            ApiKey = package.Deployer.ApiKey,
            ReleaseNumber = buildNumber,
            DefaultPackageVersion = buildNumber,
            EnableServiceMessages = true,
            WaitForDeployment = true
        });
        context.Log.Write(Verbosity.Normal, LogLevel.Information, "Created release {0} of {1}.", buildNumber, package.Name);
    }

	public static void CreateReleaseAndDeploy(ICakeContext context, string buildNumber, DeployPackage package, string environment)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        context.OctoCreateRelease(package.Name, new CreateReleaseSettings
        {
            Server = package.Deployer.Url,
            ApiKey = package.Deployer.ApiKey,
            DeployTo = environment,
            Channel = environment,
            ReleaseNumber = buildNumber,
            DefaultPackageVersion = buildNumber,
            EnableServiceMessages = true,
            WaitForDeployment = true
        });
        context.Log.Write(Verbosity.Normal, LogLevel.Information, "Created release {0} of {1} on environment {2}.", buildNumber, package.Name, environment);
    }

	public static void DeleteRelease(ICakeContext context, string buildNumber, DeployPackage package)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        context.Log.Write(Verbosity.Normal, LogLevel.Information, "Deleted release {0} of {1}.", buildNumber, package.Name);
    }

	public static void DeleteTemporaryReleases(ICakeContext context, DeployPackage package, string machineName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        context.Log.Write(Verbosity.Normal, LogLevel.Information, "Deleted {0} releases for #{1}.", package.Name, machineName);
    }

	public static void DeployLocaly(ICakeContext context, DeployPackage package)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        DateTime currentTime = DateTime.Now;
        string buildNumber = $"0.{currentTime.Day}.{currentTime.Hour}{currentTime.Minute}.{currentTime.Second}";
        context.Log.Write(Verbosity.Normal, LogLevel.Information, "Deploy {0} of {1} to {2}...", buildNumber, package.Name, Environment.MachineName);
    }
}

public class BuildPackage
{
    public string Name { get; set; }

    public FilePath Source { get; set; }

    public DirectoryPath OutputDirectory { get; set; }

    public NugetServer ArtifactStore { get; set; }
}

public class DeployPackage
{
    public string Name { get; set; }

    public FilePath Source { get; set; }

    public NugetServer ArtifactStore { get; set; }

    public OctopusServer Deployer { get; set; }
}
