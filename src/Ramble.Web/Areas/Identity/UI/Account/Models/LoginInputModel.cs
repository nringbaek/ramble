using System.ComponentModel.DataAnnotations;

namespace Ramble.Web.Areas.Identity.UI.Account.Models
{
    public class LoginInputModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
