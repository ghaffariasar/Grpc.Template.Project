using GrpcServerProject.Domain.Models;
using GrpcServerProject.Domain.Repositories;
using GrpcServerProject.Domain.Services;
using Microsoft.Extensions.Logging;

namespace GrpcServerProject.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<StudentModel>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<StudentModel?> GetByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(StudentCreateModel student)
        {
            var result = await _studentRepository.CreateAsync(student);
            _logger.LogDebug("Student Created {StudentNumber}", result);
            return result;
        }

        public async Task<bool> UpdateAsync(StudentUpdateModel student)
        {
            var result = await _studentRepository.UpdateAsync(student);
            _logger.LogDebug("Student Updated {StudentNumber}", result);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var result= await _studentRepository.DeleteAsync(id);
           return result == 1;
        }
    }
}
