using StudentFlat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.Interfaces
{
    public interface IFlats
    {
        IEnumerable<Flat> AllFlats();
        IEnumerable<Flat> GetAval();
        Flat GetFlatsByStudentId(Guid studentId);
        IEnumerable<Flat> GetFlatsByOwnerId(Guid ownerId);
        Flat GetFlat(Guid flatId);
    }
}
