using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AcePalace.Models
{
    public class Shedule
    {
        public int SheduleID { get; set; }

        [Display(Name = "Вид занятий")]
        public string? SheduleName { get; set; }

        [Display(Name = "Тренер")]
        public string? Trener { get; set; }

        [Display(Name = "Время проведения")]

        [DataType(DataType.DateTime)]
      //  [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }



    }
}
