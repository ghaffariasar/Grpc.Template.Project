namespace GrpcClientProject.Domain.Models
{
    public record StudentModel
    {
        public int Id { get; init; }
        public required string StudentNumber { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public string? Description { get; init; }


        public List<string>? PhoneNumbers { get; init; }
    }
}
