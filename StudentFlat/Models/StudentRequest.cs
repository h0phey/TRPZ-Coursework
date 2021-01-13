using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.Models
{
    public class StudentRequest
    {
        public Guid StudentId { get; set; }
        public Guid FlatId { get; set; }
        public Flat Flat { get; set; }
        public Student Student { get; set; }
    }
}
