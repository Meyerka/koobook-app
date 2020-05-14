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
        private readonly RoleManager<IdentityUser> _roleManager;

        public DbInitializer(KoobookDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityUser> roleManager)
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
            catch (Exception ex)
            {
            }

            if (_db.Roles.Any(r => r.Name == Utility.Admin)) return;
            

        }
    }
}
