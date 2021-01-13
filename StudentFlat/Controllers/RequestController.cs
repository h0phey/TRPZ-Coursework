using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentFlat.Interfaces;
using StudentFlat.Models;

namespace StudentFlat.Controllers
{
    public class RequestController : Controller
    {
        private AppDbContext db;
        private IFlats allFlats;
        private IOwners allOwners;

        public RequestController(AppDbContext context, IFlats iFlats, IOwners iOwners)
        {
            db = context;
            allFlats = iFlats;
            allOwners = iOwners;
        }

        public async Task<IActionResult> RequestFlat(Guid flatId)
        {
            ViewData["Flat"] = allFlats.GetFlat(flatId);
            User user = db.Users.AsNoTracking().FirstOrDefault(u => u.Email == User.Identity.Name);
            StudentRequest studentRequest = new StudentRequest
            {
                StudentId = db.Student.AsNoTracking().FirstOrDefault(x => x.UserId == user.Id).id, 
                FlatId = flatId,
                Flat = db.Flat.AsNoTracking().FirstOrDefault(x => x.id == flatId),
                Student = db.Student.AsNoTracking().FirstOrDefault(x => x.UserId == user.Id)
            };
            var flat = allFlats.GetFlat(flatId); 
            flat.StudentRequests.Add(studentRequest);
            db.Flat.Update(flat);
            await db.SaveChangesAsync();
            return View("~/Views/Info/FlatInfo.cshtml");
        }

        public IActionResult OwnerFlats(Guid ownerId)
        {
            ViewData["Flat"] = allOwners.GetFlats(ownerId);
            return View("~/Views/Info/FlatInfo.cshtml");
        }

        [HttpGet]
        public IActionResult AddFlat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlat(Flat model)
        {
            if (ModelState.IsValid)
            {
                Flat flat = new Flat
                {
                    id = Guid.NewGuid(), Owner = db.Owner.FirstOrDefault(u => u.email == User.Identity.Name),
                    isAvail = true, name = model.name, shortDesc = model.shortDesc, longDesc = model.longDesc,
                    capacity = model.capacity, price = model.price
                };
                var owner = allOwners.GetOwnerByUserId(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Id"))
                    .Value));
                owner.Flats.Add(flat);
                db.Flat.Add(flat);
                await db.SaveChangesAsync();
                db.Owner.Update(owner);
                return RedirectToAction("AllFlats", "Flats");
            }
            else
                ModelState.AddModelError("", "Перевірте помилки.");
            return View(model);
        }

        public async Task<IActionResult> RequestsApply(Guid studentId, Guid flatId)
        {
            var request =
                db.StudentRequests.FirstOrDefault(x => x.FlatId.Equals(flatId) && x.StudentId.Equals(studentId));
            var flat = allFlats.GetFlat(request.FlatId);
            flat.Students.Add(request.Student);
            if (flat.avPlaces.Equals(0))
            {
                flat.isAvail = false;
            }
            db.Flat.Update(flat);
            request.Student.isSearching = false;
            db.Student.Update(request.Student);
            //db.StudentRequests.Remove(request);
            foreach (var req in db.StudentRequests.Where(x => x.StudentId.Equals(studentId)))
            {
                db.StudentRequests.Remove(req);
            }
            await db.SaveChangesAsync();
            ViewData["Flats"] = allFlats.AllFlats();
            return View("~/Views/Home/Flats.cshtml");
        }
        public async Task<IActionResult> RequestsDecline(Guid studentId, Guid flatId)
        {
            var request =
                db.StudentRequests.FirstOrDefault(x => x.FlatId.Equals(flatId) && x.StudentId.Equals(studentId));
            db.StudentRequests.Remove(request);
            await db.SaveChangesAsync();
            ViewData["Flats"] = allFlats.AllFlats();
            return View("~/Views/Home/Flats.cshtml");
        }
    }
}
