using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMS.DB.Models
{
    public class Payment : BaseEntity
    {
        
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string PaymentMode { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "please enter amount.")]
        public string TotalAmount { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "please enter amount.")]
        public string ReceivedAmount { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "please enter amount.")]
        public string RemainingAmount { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "please enter amount.")]
        public string Transactionid { get; set; }

        public string Description { get; set; }

        [Required]
        public long EventId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Event Event { get; set; }

        [Required]
        public string UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User User { get; set; }

    }
}
