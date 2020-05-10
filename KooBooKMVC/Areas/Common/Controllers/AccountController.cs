using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooBooKMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace KooBooKMVC.Controllers
{
    [Area("Common")]
    public class AccountController : Controller
    {
        private readonly KoobookDbContext _context;
        public AccountController(KoobookDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}