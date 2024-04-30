using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcePalace.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int ShoppingCartId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int ProductID { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Цена")]
        public int UnitPrice { get; set; }   
        public Product? Product { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
       
    }
}
