using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ramble.Data.Models;

namespace Ramble.Web.Pages.Authentication
{
    public class SignoutModel : PageModel
    {
        private readonly SignInManager<RambleUserEntity> _signInManager;

        [BindProperty]
        public string ReturnUrl { get; set; }

        public SignoutModel(SignInManager<RambleUserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<ActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
        
            if (Url.IsLocalUrl(ReturnUrl))
                return Redirect(ReturnUrl);

            return Redirect("~/");
        }
    }
}