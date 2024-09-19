using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EMS.DB.Models
{
    public class Operator : BaseEntity
    {
        public Operator()
        {
            this.Events = new HashSet<Event>();
        }
        public string UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User User { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
