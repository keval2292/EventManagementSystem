using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Service.Interface
{
    public interface IInquiryService
    {
        public List<Inquiry> GetLists();
        public void Insert(Inquiry customer);
        public void Delete(long id);
    }
}
