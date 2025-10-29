using GrpcClientProject.Domain.Models;

namespace GrpcClientProject.Domain.Repositories
{
    public interface IStudentRepository
    {
        IAsyncEnumerable<StudentModel> GetAll(CancellationToken cancellationToken = default);
        Task<StudentModel> GetById(int id, CancellationToken cancellationToken = default);


        Task<IReadOnlyList<int>> Create(IEnumerable<StudentCreateModel> students, CancellationToken cancellationToken = default);
        Task<bool> Update(StudentUpdateModel student, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
