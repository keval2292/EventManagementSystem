using EMS.DB.Models;
using EMS.DB.Service.Interface;
using EMS.DB.unitofwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Service
{

    public class Service : IServices
    {
        private IRepository<Services> _repository;
        private AppDbContext _appDbContext;
        public Service(IRepository<Services> repository, AppDbContext appDbContext)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }

        public List<Services> GetList() => _repository.GetAll();

        public void Insert(Services serviceModel)
        {
            _repository.Insert(serviceModel);
            
        }
        public void Delete(long id)
        {
            Services services = _appDbContext.ServicesList.FirstOrDefault(c => c.Id.Equals(id));
            _repository.Remove(services);
            _repository.SaveChanges();
        }

       
    }
}
