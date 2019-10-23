using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ramble.Data.Models;

namespace Ramble.Web.Pages
{
    public class InitialSetupModel : PageModel
    {
        private readonly UserManager<RambleUserEntity> _userManager;
        private readonly RoleManager<RambleUserRoleEntity> _roleManager;

        [BindProperty]
        public SetupForm Setup { get; set; }

        public InitialSetupModel(UserManager<RambleUserEntity> userManager, RoleManager<RambleUserRoleEntity> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new RambleUserEntity
            {
                Email = Setup.Email,
                UserName = Setup.Email
            };

            var role = new RambleUserRoleEntity
            {
                Name = "Admin"
            };

            var r1 = await _userManager.CreateAsync(user, Setup.Password);
            if (!r1.Succeeded)
                return Page();

            var r2 = await _roleManager.CreateAsync(role);
            var r3 = await _userManager.AddToRoleAsync(user, "Admin");

            return Redirect("/");
        }

        public class SetupForm
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}