using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMS.DB.Models
{
    public class CategoryService : BaseEntity
    {
        [Required]
        [StringLength(15, ErrorMessage = "This field length can't be more than 15.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public String ServiceName{ get; set; }
        public virtual List<Inquiry> Inquiry { get; set; }
    }
}
