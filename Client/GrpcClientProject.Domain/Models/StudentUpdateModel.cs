namespace GrpcClientProject.Domain.Models;

public record StudentUpdateModel
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Description { get; init; }
}