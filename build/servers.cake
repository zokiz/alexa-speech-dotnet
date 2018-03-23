public class NugetServer
{
    public string Url { get; set; }

    public string ApiKey { get; set; }

    public NugetServer(string url, string apiKey)
    {
        Url = url;
        ApiKey = apiKey;
    }

	public override string ToString()
    {
        base.ToString();
        return Url;
    }
}

public class OctopusServer
{
    public string Url { get; set; }

    public string ApiKey { get; set; }

    public OctopusServer(string url, string apiKey)
    {
        Url = url;
        ApiKey = apiKey;
    }

	public override string ToString()
    {
        base.ToString();
        return Url;
    }
}

public class DatabaseServer
{
	public string Name { get; set; }

	public int? PortNumber { get; set; }

	public DatabaseServer(string name, int? portNumber = null)
    {
        Name = name;
        PortNumber = portNumber;
    }

	public override string ToString()
    {
        base.ToString();
        return PortNumber.HasValue ? $"{Name},{PortNumber}" : Name;
    }
}