using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Models
{
    public class Admin : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
