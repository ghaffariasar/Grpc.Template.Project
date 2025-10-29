namespace GrpcServerProject.Infrastructure.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public required string StudentNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Description { get; set; }


        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
    }
}
