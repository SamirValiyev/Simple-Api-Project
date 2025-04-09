using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SimpleApiProjectUI.Models
{
    public class CategoryRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Definition is required")]
        public string? Definition { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public SelectList  Categories { get; set; }

    }
}
