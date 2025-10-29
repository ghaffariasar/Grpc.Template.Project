using GrpcServerProject.Domain.Models;
using GrpcServerProject.Infrastructure.Entities;

namespace GrpcServerProject.Infrastructure.Mappers
{
    public static class StudentMapper
    {
        public static StudentModel ToModel(this Student student)
        {
            return new StudentModel
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description,
                PhoneNumbers = student.PhoneNumbers.Select(s => s.Value).ToList()
            };
        }

        public static Student ToModel(this StudentModel student)
        {
            return new Student
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description,
                PhoneNumbers = student.PhoneNumbers?.Select(s => new PhoneNumber {Value = s}).ToList() ?? new List<PhoneNumber>()
            };
        }

        public static Student ToModel(this StudentCreateModel student)
        {
            return new Student
            {
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description,
                PhoneNumbers = student.PhoneNumbers?.Select(s => new PhoneNumber { Value = s }).ToList() ?? new List<PhoneNumber>()
            };
        }

    }
}
