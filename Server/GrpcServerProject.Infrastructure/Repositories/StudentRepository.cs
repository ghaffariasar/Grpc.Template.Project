using GrpcServerProject.Domain.Models;
using GrpcServerProject.Domain.Repositories;
using GrpcServerProject.Infrastructure.Entities;
using GrpcServerProject.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GrpcServerProject.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentModel>> GetAllAsync()
        {
            var result = await _context.Students.Include(p => p.PhoneNumbers).AsNoTracking().Select(p => p.ToModel()).ToListAsync();

            return result;
        }

        public async Task<StudentModel?> GetByIdAsync(int id)
        {
            var result = await _context.Students.Include(p => p.PhoneNumbers).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return result?.ToModel();
        }

        public async Task<int> CreateAsync(StudentCreateModel student)
        {
            var studentEntity = student.ToModel();

            await _context.AddAsync(studentEntity);
            await _context.SaveChangesAsync();
            return studentEntity.Id;
        }

        public async Task<int> UpdateAsync(StudentUpdateModel student)
        {
            var studentEntity = new Student
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description,
                StudentNumber = string.Empty
            };

            _context.Entry(studentEntity).Property(p => p.FirstName).IsModified = true;
            _context.Entry(studentEntity).Property(p => p.LastName).IsModified = true;
            _context.Entry(studentEntity).Property(p => p.Description).IsModified = true;

            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var studentEntity = await _context.Students.FindAsync(id);
            if (studentEntity == null)
                return 0;

            _context.Students.Remove(studentEntity);
            return await _context.SaveChangesAsync();
        }
    }
}
