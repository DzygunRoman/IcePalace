using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcePalace.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int SheduleID { get; set; }
        [Required]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public int UnitPrice { get; set; }



        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата посещения")]
        public DateTime DateTime { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
        public IEnumerable<Product>? Details { get; set; }
    }
}
