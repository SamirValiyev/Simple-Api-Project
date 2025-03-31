using System.ComponentModel.DataAnnotations;

namespace SimpleApiProjectUI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; } = null!;
    }
}
