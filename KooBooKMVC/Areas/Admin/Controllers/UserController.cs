using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KooBooKMVC.Migrations;
using KooBooKMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KooBooKMVC.Areas.Admin
{
    [Authorize(Roles = Utility.Admin)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserData _userData;
        private readonly KoobookDbContext _db;
        public UserController(IUserData userData, KoobookDbContext db)
        {
            _userData = userData;
            _db = db;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            return View(_userData.GetAll(u => u.Id != claims.Value));        }
    }
}