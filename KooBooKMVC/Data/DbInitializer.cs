using KooBooKMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly KoobookDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(KoobookDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }
       

            if (_db.Roles.Any(r => r.Name == Utility.Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(Utility.Admin)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "karlerik.meyer@gmail.com",
                Email = "karlerik.meyer@gmail.com",
                EmailConfirmed = true,
            }, "Admin123*").GetAwaiter().GetResult();

            IdentityUser user = _db.Users.Where(u => u.Email == "karlerik.meyer@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, Utility.Admin).GetAwaiter().GetResult();
        }
    }
}
