using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMS.DB.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(30, ErrorMessage = "This field length can't be more than 30.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "please enter valid contact no.")]
        public string ContactNo { get; set; }

        [Required]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Password Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters")]
        public string Password { get; set; }

        [Required]
        public string Useraddress { get; set; }

        [Required]
        public string Userrole { get; set; }

        public DateTime UserJoiningDate { get; set; } = DateTime.Today;

        public bool IsActive { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Staff Staffs { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual Operator Operators { get; set; }
    }
}
