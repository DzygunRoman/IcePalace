using System.ComponentModel.DataAnnotations;

namespace AcePalace.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "Название")]
        public string? categoryName { get; set; }
        [Display(Name = "Описание")]
        public string? categoryDescription { get; set; }
        [Display(Name = "Слаг")]
        public string? Slug { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
