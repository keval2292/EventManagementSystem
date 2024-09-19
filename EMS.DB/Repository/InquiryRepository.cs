using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace EMS.DB.Repository
{
    public class InquiryRepository : BaseRepository, IInquiryRepository
    {

        #region Property
        private IRepository<Inquiry> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public InquiryRepository(IRepository<Inquiry> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion

        //public List<Inquiry> GetLists() => _repository.GetAll();
        public List<Inquiry> GetLists()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Inquiries.Where(x=>x.IsActive==false).ToList();
            //_appDbContext.Events.Include(x => x.Category).ToList();
            //return _appDbContext.Events.Include(x => x.Inquiry).ToList();
        }
        public void Insert(Inquiry inquiry)
        {
            //if (inquiry.Id is 0) _repository.Insert(inquiry);
            //else _repository.Update(inquiry);

            using AppDbContext _myContext = base.GetContext();
            if (inquiry.Id is 0)
                _myContext.Inquiries.Add(inquiry);
            else
                _myContext.Inquiries.Update(inquiry);

            _myContext.SaveChanges();
        }

        public void Delete(long id)
        {
            //Inquiry inquiry = _appDbContext.Inquiries.FirstOrDefault(c => c.Id.Equals(id));
            //_repository.Remove(inquiry);
            //_repository.SaveChanges();

            using AppDbContext _myContext = base.GetContext();
            Inquiry inquiry = _myContext.Inquiries.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.Inquiries.Remove(inquiry);
            _myContext.SaveChanges();
        }
    }
}
