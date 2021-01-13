using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentFlat.Interfaces;
using StudentFlat.Models;
using StudentFlat.ViewModels;

namespace StudentFlat.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db;
        private IFlats allFlats;
        private IOwners allOwners;
        private IStudents allStudents;
        public AccountController(AppDbContext context, IFlats iFlats, IStudents iStudents, IOwners iOwners)
        {
            db = context;
            allFlats = iFlats;
            allOwners = iOwners;
            allStudents = iStudents;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email, user.Role, user.Id,
                        (user.Role)
                            ? db.Owner.FirstOrDefaultAsync(u => u.UserId == user.Id).Result.name
                            : db.Student.FirstOrDefaultAsync(u => u.UserId == user.Id).Result.name);

                    return RedirectToAction("AllFlats", "Flats");
                }
                ModelState.AddModelError("", "Некорректні логін та(або) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    User user1 = new User {Id = Guid.NewGuid(), Email = model.Email, Password = model.Password, Role = model.Role};
                    if (model.Role)
                    {
                        Owner owner = new Owner
                        {
                            id = Guid.NewGuid(), name = model.Name, email = model.Email, UserId = user1.Id,
                            phone = model.Phone
                        };
                        db.Owner.Add(owner);
                    }
                    else
                    {
                        Student student = new Student
                        {
                            id = Guid.NewGuid(), name = model.Name, email = model.Email, UserId = user1.Id,
                            phone = model.Phone, isSearching = true
                        };
                        db.Student.Add(student);
                    }
                    db.Users.Add(user1);
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email, model.Role, user1.Id, model.Name);

                    return RedirectToAction("AllFlats", "Flats");
                }
                else
                    ModelState.AddModelError("", "Користувач з такою адресою вже існує");
            }
            return View(model);
        }

        private async Task Authenticate(string userName, bool role, Guid userId, String name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim("Role", role.ToString()),
                new Claim("Id", userId.ToString()),
                new Claim("Name", name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult UserPage()
        {
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (Boolean.Parse((ReadOnlySpan<char>) User.Claims.FirstOrDefault(x => x.Type == "Role").Value))
            {
                ViewData["Owner"] = allOwners.GetOwnerByUserId(user.Id);
                ViewData["Flats"] = allFlats.GetFlatsByOwnerId(allOwners.GetOwnerByUserId(user.Id).id);
                return View("~/Views/Account/OwnerPage.cshtml");
            }
            else
            {
                ViewData["Flat"] = allFlats.GetFlatsByStudentId(allStudents.GetStudentByUserId(user.Id).id);
                ViewData["Student"] = allStudents.GetStudentByUserId(user.Id);
                return View("~/Views/Account/StudentPage.cshtml");
            }
        }
    }
}
