using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ramble.Data.Models;

namespace Ramble.Web.Pages.Authentication
{
    public class SigninModel : PageModel
    {
        private readonly SignInManager<RambleUserEntity> _signInManager;

        [Required]
        [MinLength(1)]
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberLogin { get; set; }

        [DataType(DataType.Text)]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        public SigninModel(SignInManager<RambleUserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _signInManager.PasswordSignInAsync(Email, Password, RememberLogin, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);

                return Redirect("~/");
            }
            else
                ModelState.AddModelError(string.Empty, "Invalid sign in credentials.");

            return Page();
        }
    }
}