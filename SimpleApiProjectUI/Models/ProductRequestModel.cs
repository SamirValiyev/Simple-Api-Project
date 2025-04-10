using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SimpleApiProjectUI.Models
{
    public class ProductRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Name must be full")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Stock must be full")]
        public int Stock { get; set; }
        [Required(ErrorMessage ="Price must be full")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="CategoryId must be full")]
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
    }
}
