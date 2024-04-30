using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcePalace.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Display(Name = "Дата создания")]          
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        [Required]
        [EmailAddress]
        [MaxLength(30)]
        [Display(Name = "Электронный адрес")]
        public string? Email { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата посещения")]
        public DateTime DateTime { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
    }
}
