using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AcePalace.Models
{
    public class Scates : Product
    {


        [Display(Name = "Размер")]
        public int? Size { get; set; }


    }
}
