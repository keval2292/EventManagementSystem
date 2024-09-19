using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EMS.DB.Repository.Interface
{
    public interface IInquiryRepository
    {
        public List<Inquiry> GetLists();
        public void Insert(Inquiry customer);
        public void Delete(long id);
    }
}
