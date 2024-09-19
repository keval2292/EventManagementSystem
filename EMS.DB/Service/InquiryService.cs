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
    public class InquiryService : IInquiryService
    {

        #region Property
        private IRepository<Inquiry> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public InquiryService(IRepository<Inquiry> repository, AppDbContext appDbContext)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion

        public List<Inquiry> GetLists() => _repository.GetAll();

        public void Insert(Inquiry inquiry)
        {
            if (inquiry.Id is 0) _repository.Insert(inquiry);
            else _repository.Update(inquiry);
        }

        public void Delete(long id)
        {
            Inquiry inquiry = _appDbContext.InquiryList.FirstOrDefault(c => c.Id.Equals(id));
            _repository.Remove(inquiry);
            _repository.SaveChanges();
        }
    }
}
