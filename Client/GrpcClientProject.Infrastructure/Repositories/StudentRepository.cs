using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcClientProject.Domain.Models;
using GrpcClientProject.Domain.Repositories;
using GrpcClientProject.Infrastructure.Protos.v1;

namespace GrpcClientProject.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentService.StudentServiceClient _studentServiceClient;

        public StudentRepository(StudentService.StudentServiceClient studentServiceClient)
        {
            _studentServiceClient = studentServiceClient;
        }



        public async IAsyncEnumerable<StudentModel> GetAll([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var result = _studentServiceClient.GetAll(new Empty(), cancellationToken: cancellationToken);

            await foreach (var item in result.ResponseStream.ReadAllAsync(cancellationToken))
            {
                yield return new StudentModel
                {
                    Id = item.Id,
                    StudentNumber = item.StudentNumber,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Description = item.Description,
                    PhoneNumbers = new List<string>(item.PhoneNumbers),
                };
            }

        }
        public async Task<StudentModel> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _studentServiceClient.GetByIdAsync(new StudentId { Id = id }, cancellationToken: cancellationToken);
            return new StudentModel
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                StudentNumber = result.StudentNumber,
                Description = result.Description,
                PhoneNumbers = result.PhoneNumbers.Select(s => s).ToList()
            };
        }

        public async Task<IReadOnlyList<int>> Create(IEnumerable<StudentCreateModel> students, CancellationToken cancellationToken = default)
        {
            var request = _studentServiceClient.CreateStudent(cancellationToken: cancellationToken);
            foreach (var student in students)
            {
                var newStudent = new CreateStudentRequest
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    StudentNumber = student.StudentNumber,
                    Description = student.Description,
                    PhoneNumbers = { student.PhoneNumbers ?? new List<string>() }
                };

                await request.RequestStream.WriteAsync(newStudent, cancellationToken);
            }

            await request.RequestStream.CompleteAsync();
            
            var createdIds = new List<int>();
            await foreach (var responseStream in request.ResponseStream.ReadAllAsync(cancellationToken))
            {
                createdIds.Add(responseStream.Id);
            }
           
            return createdIds;
        }
        public async Task<bool> Update(StudentUpdateModel student, CancellationToken cancellationToken = default)
        {
            var result = await _studentServiceClient.UpdateStudentAsync(new UpdateStudentRequest
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description,
            }, cancellationToken: cancellationToken);
            return result.Value;
        }
        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _studentServiceClient.DeleteStudentAsync(new StudentId { Id = id }, cancellationToken: cancellationToken);
            return result.Value;
        }
    }
}
