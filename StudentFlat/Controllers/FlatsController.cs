using Microsoft.AspNetCore.Mvc;
using StudentFlat.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace StudentFlat.Controllers
{
    public class FlatsController : Controller
    {
        public readonly IFlats allFlats;
        public readonly IStudents AllStudents;
        private AppDbContext db;

        public FlatsController(IFlats iFlats, AppDbContext context, IStudents iStudents)
        {
            allFlats = iFlats;
            AllStudents = iStudents;
            db = context;
        }

        public IActionResult Flats()
        {
            ViewData["Flats"] = allFlats.GetAval();
            return View("~/Views/Home/Flats.cshtml");
        }

        [Authorize]
        public IActionResult AllFlats()
        {
            ViewData["Flats"] = allFlats.AllFlats();
            return View("~/Views/Home/AllFlats.cshtml");
        }

        public IActionResult GetFlat(Guid flatId)
        {
            ViewData["Flat"] = allFlats.GetFlat(flatId);
            try
            {
                ViewData["Student"] =
                    AllStudents.GetStudentByUserId(
                        Guid.Parse(User.Claims.FirstOrDefault(a => a.Type.Equals("Id")).Value));

            }
            catch { }
            return View("~/Views/Info/FlatInfo.cshtml");
        }
    }
}
