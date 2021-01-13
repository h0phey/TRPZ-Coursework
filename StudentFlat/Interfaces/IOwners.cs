using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentFlat.Models;

namespace StudentFlat.Interfaces
{
    public interface IOwners
    {
        IEnumerable<Owner> AllOwners();
        Owner GetOwner(Guid ownerId);
        List<Flat> GetFlats(Guid ownerId);
        Owner GetOwnerByUserId(Guid userId);
    }
}
