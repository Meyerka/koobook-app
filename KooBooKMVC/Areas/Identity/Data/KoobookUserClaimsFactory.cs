using KooBooKMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KooBooKMVC.Areas.Identity.Data
{
    public class KoobookUserClaimsFactory:
        UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public KoobookUserClaimsFactory(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options
            ): base (userManager, roleManager, options)
        {

        }

        protected override async Task<ClaimsIdentity>
            GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FullName",
                user.FullName));
            identity.AddClaim(new Claim("RegistrationDate",
                user.RegistrationDate.ToShortDateString()));
            return identity;
        }
    }
}
