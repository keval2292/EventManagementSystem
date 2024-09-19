using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EMS.DB.Models
{
    public class EventStaffWork: BaseEntity 
    {
        [Required]
        public String Service { get; set; }
        [Required]
        public String Description { get; set; }

        public String Status { get; set; }
        [Required]
        public long StaffId { get; set; }
      
        public Staff Staff { get; set; }
        [Required]
        public long EventId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Event Event { get; set; }


    }
}
