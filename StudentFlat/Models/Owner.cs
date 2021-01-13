using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.Models
{
    public class Owner
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<Flat> Flats { get; set; }
    }
}
