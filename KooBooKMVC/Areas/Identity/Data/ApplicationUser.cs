using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public string FullName { get; set; }



    }
}
