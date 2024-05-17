using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Persistence
{
    public class DataSeedingInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeedingInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task UserAndRoleSeedAsync()
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if(_userManager.FindByEmailAsync("opeyemiadebayo@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "opeyemiadebayo@gmail.com",
                    Email = "opeyemiadebayo@gmail.com"
                };
                IdentityResult identityResult = _userManager.CreateAsync(user,"Password1@").Result;
                if(identityResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user,"Admin").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("johndoe@example.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "johndoe@example.com",
                    Email = "johndoe@example.com"
                };
                IdentityResult identityResult = _userManager.CreateAsync(user, "Password1@").Result;
                if (identityResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("janesmith@example.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "janesmith@example.com",
                    Email = "janesmith@example.com"
                };
                IdentityResult identityResult = _userManager.CreateAsync(user, "Password1@").Result;
                if (identityResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("michaeljohnson@example.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "michaeljohnson@example.com",
                    Email = "michaeljohnson@example.com"
                };
                IdentityResult identityResult = _userManager.CreateAsync(user, "Password1@").Result;
                //No role assigned to Mr Michael Johnson.
            }
        }
    }
}
