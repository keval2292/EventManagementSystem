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
    public class Event : BaseEntity
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string EventName { get; set; }
        [Required]
        public string EventVenue { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "please enter only alphabets.")]
        public string OrganizerName { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "please enter valid contact no.")]
        public string OrganizerContact { get; set; }
        [Required]
        [RegularExpression(@"[a-z0-9\\.]+@[a-z]+\.[a-z]{2,3}", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string SlotType { get;set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime? Todate { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        
        public bool Ispaymentdone { get; set; }
        
        public string SelectedService { get; set; }

        ////Foreign key for Standard
        public Nullable<long> InquiryId { get; set; }
     
        public Inquiry Inquiry { get; set; }

        public long CategoryId { get; set; } = 0;
        public EventCategory Category { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<EventStaffWork> EventStaffWork { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Payment Payment { get; set; }

        [Required]
        public long OperatorId { get; set; }
        public Operator Operator { get; set; }

    }
}
