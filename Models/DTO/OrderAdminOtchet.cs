using System.ComponentModel.DataAnnotations;

namespace AcePalace.Models.DTO
{
    public class OrderAdminOtchet
    {
        public Order? Order { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
        public OrderDetail? OrderDetail { get; set; }    
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
        public Shedule? Shedule { get; set; }
        public IEnumerable<Shedule>? Shedules { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        [Display(Name = "Начальная дата")]
        public DateTime startTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        [Display(Name = "Конечная дата")]
        public DateTime finishTime { get; set; }
        
    }
}
