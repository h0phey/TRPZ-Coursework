using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentFlat.Interfaces;
using StudentFlat.Models;

namespace StudentFlat.Repository
{
    public class OwnerRepository : IOwners
    {
        private readonly AppDbContext appDbContent;

        public OwnerRepository(AppDbContext appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<Owner> AllOwners() => appDbContent.Owner.Include(p => p.Flats).ThenInclude(c => c.Students).ToList();

        public Owner GetOwner(Guid ownerId) => appDbContent.Owner.Include(p => p.Flats).ThenInclude(c => c.Students)
            .FirstOrDefault(v => v.id == ownerId);

        public List<Flat> GetFlats(Guid ownerId) => appDbContent.Owner.Include(p => p.Flats)
            .ThenInclude(c => c.Students)
            .FirstOrDefault(v => v.id.Equals(ownerId)).Flats;

        public Owner GetOwnerByUserId(Guid userId) => appDbContent.Owner.Include(p => p.Flats).ThenInclude(c => c.Students)
            .FirstOrDefault(v => v.UserId == userId);
    }
}
