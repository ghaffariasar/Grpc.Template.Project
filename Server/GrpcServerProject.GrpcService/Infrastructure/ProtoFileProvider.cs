namespace GrpcServerProject.GrpcService.Infrastructure;

public class ProtoFileProvider
{
    private readonly string _protosPath;

    public ProtoFileProvider(IWebHostEnvironment webHostEnvironment)
    {
        var contentRootPath = webHostEnvironment.ContentRootPath;
        _protosPath = $"{contentRootPath}/protos";
    }


    public Dictionary<string, IEnumerable<string?>> GetAll()
    {
        return Directory.GetDirectories($"{_protosPath}").
            Select(c => new
            {
                version = c,
                protos = Directory.GetFiles(c).Select(Path.GetFileName)
            }).ToDictionary(c => Path.GetRelativePath("protos", c.version), c => c.protos);
    }

    public async Task<string> GetContent(int version, string protoName)
    {
        var filePath = $"{_protosPath}/v{version}/{protoName}";
        var exists = File.Exists(filePath);
        return exists ? await File.ReadAllTextAsync(filePath) : string.Empty;
    }

    public string GetPath(int version, string protoName)
    {
        var filePath = $"{_protosPath}/v{version}/{protoName}";
        var exists = File.Exists(filePath);

        return exists ? filePath : string.Empty;

    }
}