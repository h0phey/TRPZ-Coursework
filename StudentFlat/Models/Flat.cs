using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.Models
{
    public class Flat
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string shortDesc { get; set; }
        public string img { get; set; }
        public string longDesc { get; set; }
        public bool isAvail { get; set; }
        public ushort price { get; set; }
        public ushort capacity { get; set; }
        public short avPlaces => (short)(capacity - Students.Count);
        public Owner Owner { get; set; }
        public List<StudentRequest> StudentRequests { get; set; }
        public List<Student> Students { get; set; }
    }
}
