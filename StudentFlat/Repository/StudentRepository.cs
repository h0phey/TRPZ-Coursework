using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentFlat.Interfaces;
using StudentFlat.Models;

namespace StudentFlat.Repository
{
    public class StudentRepository : IStudents
    {
        private readonly AppDbContext appDbContent;

        public StudentRepository(AppDbContext appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<Student> AllStudents() => appDbContent.Student.Include(c => c.StudentRequests)
            .ThenInclude(o => o.Flat).Include(c => c.StudentRequests).ThenInclude(o => o.Student);

        public Student GetStudent(Guid studentId) => appDbContent.Student.Include(c => c.StudentRequests)
            .ThenInclude(o => o.Flat).Include(c => c.StudentRequests).ThenInclude(o => o.Student)
            .FirstOrDefault(c => c.id == studentId);

        public Student GetStudentByUserId(Guid userId) => appDbContent.Student.Include(c => c.StudentRequests)
            .ThenInclude(o => o.Flat).Include(c => c.StudentRequests).ThenInclude(o => o.Student)
            .FirstOrDefault(v => v.UserId == userId);
    }
}
