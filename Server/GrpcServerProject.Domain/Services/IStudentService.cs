using GrpcServerProject.Domain.Models;

namespace GrpcServerProject.Domain.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetAllAsync();
        Task<StudentModel?> GetByIdAsync(int id);


        Task<int> CreateAsync(StudentCreateModel student);
        Task<bool> UpdateAsync(StudentUpdateModel student);
        Task<bool> DeleteAsync(int id);
    }
}
