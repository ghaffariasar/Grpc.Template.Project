using GrpcClientProject.Domain.Models;

namespace GrpcClientProject.Domain.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<StudentModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);


        Task<IReadOnlyList<int>> CreateAsync(IEnumerable<StudentCreateModel> students, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(StudentUpdateModel student, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
