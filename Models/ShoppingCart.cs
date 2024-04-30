using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcePalace.Models
{
    
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public IEnumerable<CartDetail>? CartDetails { get; set; }

    }
}
