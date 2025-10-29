using GrpcServerProject.Domain.Models;

namespace GrpcServerProject.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentModel>> GetAllAsync();
        Task<StudentModel?> GetByIdAsync(int id);


        Task<int> CreateAsync(StudentCreateModel student);
        Task<int> UpdateAsync(StudentUpdateModel student);
        Task<int> DeleteAsync(int id);
    }
}
