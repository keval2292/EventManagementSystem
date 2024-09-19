using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Models
{
    public class EventCategory : BaseEntity
    {

        [Required]
        [StringLength(15, ErrorMessage = "This field length can't be more than 15.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public String CategoryName { get; set; }
        public virtual List<Inquiry> InquiryList { get; set; }
    }
}
