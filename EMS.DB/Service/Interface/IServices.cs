using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Service.Interface
{
    public interface IServices
    {
        public List<Services> GetList();
        public void Insert(Services ServiceModel);
        public void Delete(long id);
    }
}
