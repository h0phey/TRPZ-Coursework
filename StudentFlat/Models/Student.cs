using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.Models
{
    public class Student
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool isSearching { get; set; }
        public List<StudentRequest> StudentRequests { get; set; }
    }
}
