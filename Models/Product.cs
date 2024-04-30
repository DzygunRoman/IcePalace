using AcePalace.Models;
using System.ComponentModel.DataAnnotations;

namespace AcePalace.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Display(Name = "Цена")]
        public int price { get; set; }

        [Display(Name = "Фото")]
        public string? Photo { get; set; }

        [Display(Name = "Слаг")]
        public string? Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 2")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Доступность")]
        public bool available { get; set; }
        [Display(Name = "Категория")]
        public int CategoryID { get; set; }
        [Display(Name = "Категория")]
        public Category? Category { get; set; }     
    }
}
