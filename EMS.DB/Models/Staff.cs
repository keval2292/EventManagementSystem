using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EMS.DB.Models
{
    public class Staff : BaseEntity
    {
        [Required]
        public long StaffService { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User User { get; set; }
    }
}
