using GrpcClientProject.Domain.Models;
using GrpcClientProject.Domain.Repositories;
using GrpcClientProject.Domain.Services;
using Microsoft.Extensions.Logging;

namespace GrpcClientProject.Application.Services
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

        public async Task<IEnumerable<StudentModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = new List<StudentModel>();
            await foreach (var student in _studentRepository.GetAll(cancellationToken).WithCancellation(cancellationToken))
            {
                result.Add(student);
            }
            return result;
        }
        public async Task<StudentModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _studentRepository.GetById(id, cancellationToken);
        }

        public async Task<IReadOnlyList<int>> CreateAsync(IEnumerable<StudentCreateModel> students, CancellationToken cancellationToken = default)
        {
            var ids = await _studentRepository.Create(students, cancellationToken);
            if (ids.Count > 0)
            {
                _logger.LogInformation("{Count} students created: {Ids}", ids.Count, string.Join(",", ids));
            }
            return ids;
        }
        public async Task<bool> UpdateAsync(StudentUpdateModel student, CancellationToken cancellationToken = default)
        {
            return await _studentRepository.Update(student, cancellationToken);
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _studentRepository.Delete(id, cancellationToken);
            return result;
        }
    }
}
