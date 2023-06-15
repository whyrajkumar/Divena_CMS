using System.ComponentModel.DataAnnotations;

namespace Divena_CMS.Models.ViewModels
{
    public class Login
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
