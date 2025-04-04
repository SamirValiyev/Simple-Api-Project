using System.ComponentModel.DataAnnotations;

namespace SimpleApiProjectUI.Models
{
    public class CategoryRequestModel
    {
        [Required(ErrorMessage = "Definition is required")]
        public string? Definition { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


    }
}
