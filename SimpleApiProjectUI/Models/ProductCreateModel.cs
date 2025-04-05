using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleApiProjectUI.Models
{
    public class ProductCreateModel
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
    }
}
