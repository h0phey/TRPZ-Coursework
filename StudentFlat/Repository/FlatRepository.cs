using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentFlat.Interfaces;
using StudentFlat.Models;

namespace StudentFlat.Repository
{
    public class FlatRepository : IFlats
    {
        private readonly AppDbContext appDbContent;

        public FlatRepository(AppDbContext appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<Flat> AllFlats() => appDbContent.Flat.Include(c => c.Students).Include(p => p.Owner)
            .Include(f => f.StudentRequests).ThenInclude(x => x.Flat).Include(f => f.StudentRequests)
            .ThenInclude(o => o.Student);

        public IEnumerable<Flat> GetAval() => appDbContent.Flat.Where(p => p.isAvail).Include(c => c.Students)
            .Include(p => p.Owner).Include(f => f.StudentRequests).ThenInclude(x => x.Flat)
            .Include(f => f.StudentRequests).ThenInclude(o => o.Student);

        public Flat GetFlatsByStudentId(Guid studentId) => appDbContent.Flat.FirstOrDefault(x => x.Students.Any(s =>s.id.Equals(studentId)));

        public IEnumerable<Flat> GetFlatsByOwnerId(Guid ownerId) => appDbContent.Flat.Where(p=>p.Owner.id.Equals(ownerId)).Include(c => c.Students)
            .Include(p => p.Owner).Include(f => f.StudentRequests).ThenInclude(x => x.Flat)
            .Include(f => f.StudentRequests).ThenInclude(o => o.Student);

        public Flat GetFlat(Guid flatId) => appDbContent.Flat.Include(c => c.Students).Include(p => p.Owner)
            .Include(f => f.StudentRequests).Include(f => f.StudentRequests)
            .ThenInclude(o => o.Student).FirstOrDefault(p => p.id == flatId);
    }
}
