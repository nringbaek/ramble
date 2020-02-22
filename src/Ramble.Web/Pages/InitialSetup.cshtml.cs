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

            var createUserResult = await _userManager.CreateAsync(user, Setup.Password);
            if (!createUserResult.Succeeded)
                return Page();

            await _roleManager.CreateAsync(new RambleUserRoleEntity { Name = RambleConstants.Roles.Admin });
            await _roleManager.CreateAsync(new RambleUserRoleEntity { Name = RambleConstants.Roles.Editor });
            await _roleManager.CreateAsync(new RambleUserRoleEntity { Name = RambleConstants.Roles.Author });

            await _userManager.AddToRoleAsync(user, RambleConstants.Roles.Admin);

            return Redirect("/");
        }

        public class SetupForm
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}