using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Models
{
    public class Inquiry : BaseEntity
    {
    
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string Fullname { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "please enter valid contact no.")]
        public string MobileNo { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "please enter valid contact no.")]
        public string AltPhoneNo { get; set; }

        [Required]
        [RegularExpression(@"[a-z0-9\\.]+@[a-z]+\.[a-z]{2,3}", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Address Line1 length can't be more than 20.")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Landmark length can't be more than 15.")]
        public string Landmark { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "please enter valid Pincode.")]
        public string Pincode { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string State { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "please enter valid Pincode.")]
        public int NoOfAttendee { get; set; }

        [Required]
        public string SlotType { get; set; }
        
        [Required]
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string Additionalnotes { get; set; }
        public string Status { get; set; }
        [Required]
        public long EventCategoryId { get; set; }
        public EventCategory EventCategory { get; set; }

        //public long CategoryServiceId { get; set; } 
        //public CategoryService CategoryService { get; set; }

        public virtual Event Event { get; set; }
    }
}
