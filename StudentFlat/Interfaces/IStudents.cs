using StudentFlat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.Interfaces
{
    public interface IStudents
    {
        IEnumerable<Student> AllStudents();
        Student GetStudent(Guid studentId);
        Student GetStudentByUserId(Guid userId);
    }
}
