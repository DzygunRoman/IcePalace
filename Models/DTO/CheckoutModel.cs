using System.ComponentModel.DataAnnotations;

namespace AcePalace.Models.DTO
{
    public class CheckoutModel
    {
        public Shedule? Shedule { get; set; }
        public IEnumerable<Shedule>? Shedules { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(30)]
        public string? Email { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }


    }
}


