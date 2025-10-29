using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServerProject.Domain.Models;
using GrpcServerProject.Domain.Services;
using GrpcServerProject.GrpcService;

namespace GrpcServerProject.GrpcService.Services
{
    public class StudentGrpcService : StudentService.StudentServiceBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentGrpcService> _logger;
        public StudentGrpcService(IStudentService studentService, ILogger<StudentGrpcService> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }


        public override async Task GetAll(Empty request, IServerStreamWriter<StudentResponse> responseStream, ServerCallContext context)
        {
            var result = await _studentService.GetAllAsync();

            foreach (var model in result)
            {
                await responseStream.WriteAsync(new StudentResponse
                {
                    Id = model.Id,
                    StudentNumber = model.StudentNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Description = model.Description,
                    PhoneNumbers = { model.PhoneNumbers?? new List<string>()}
                });
            }

            await Task.CompletedTask;
        }
        public override async Task<StudentResponse> GetById(StudentId request, ServerCallContext context)
        {
            var serverResult = await _studentService.GetByIdAsync(request.Id);
            if (serverResult == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Student with id {request.Id} dose not found."));
            
            return new StudentResponse(new StudentResponse
            {
                Id = serverResult.Id,
                StudentNumber = serverResult.StudentNumber,
                FirstName = serverResult.FirstName,
                LastName = serverResult.LastName,
                Description = serverResult.Description,
                PhoneNumbers = { serverResult.PhoneNumbers ?? new List<string>() }
            });
        }

        public override async Task CreateStudent(IAsyncStreamReader<CreateStudentRequest> requestStream, IServerStreamWriter<StudentId> responseStream, ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                var serviceResult = await _studentService.CreateAsync(new StudentCreateModel
                {
                    StudentNumber = request.StudentNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Description = request.Description,
                    PhoneNumbers = new List<string>(request.PhoneNumbers)
                });

                await responseStream.WriteAsync(new StudentId { Id = serviceResult });
            }

            await Task.CompletedTask;
        }
        public override async Task<BoolValue> UpdateStudent(UpdateStudentRequest request, ServerCallContext context)
        {
            var serviceResult = await _studentService.UpdateAsync(new StudentUpdateModel
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Description = request.Description,
            });
            return await Task.FromResult(new BoolValue { Value = serviceResult });
        }
        public override async Task<BoolValue> DeleteStudent(StudentId requestStream, ServerCallContext context)
        {
            var serviceResult = await _studentService.DeleteAsync(requestStream.Id);
            return await Task.FromResult(new BoolValue { Value = serviceResult });
        }
    }
}
