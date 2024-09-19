using EMS.DB;
using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Repository
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {

        #region Property
        private IRepository<Payment> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public PaymentRepository(IRepository<Payment> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion

        public void Delete(long id)
        {
            //Payment Payments = _appDbContext.Payments.FirstOrDefault(c => c.Id.Equals(id));
            //_repository.Remove(Payments);
            //_repository.SaveChanges();

            using AppDbContext _myContext = base.GetContext();
            Payment Payments = _myContext.Payments.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.Payments.Remove(Payments);
            _myContext.SaveChanges();
        }
        public void Update(Payment PaymentModel)
        {
            //_repository.Update(PaymentModel);
            using AppDbContext _myContext = base.GetContext();
            if (PaymentModel.Id > 0)
            {
                _myContext.Payments.Update(PaymentModel);
                _myContext.SaveChanges();
            }
        }
        public Payment GetById(long id)
        {
            //return _repository.GetById(id);
            using AppDbContext _myContext = base.GetContext();
            if (id > 0)
            {
                //_myContext.Payments.Update(PaymentModel);
                //_myContext.SaveChanges();
                return _myContext.Payments.FirstOrDefault(x => x.Id == id);
            }

            return null;
        }

        public List<Payment> GetPaymentList()
        {
            //return _appDbContext.Payments
            //        .Include(x => x.Event).ToList();
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Payments.Include(x => x.Event).ToList();
        }
        //_repository.GetAll();

        public List<Payment> GetPaymentListByEventId(long eventId)
        {
            //return _appDbContext.Payments
            //        .Include(x => x.Event).Where(x => x.EventId == eventId).ToList();

            using AppDbContext _myContext = base.GetContext();
            return _myContext.Payments.Include(x => x.Event).Where(x => x.EventId == eventId).ToList();
        }
        public void Insert(Payment PaymentModel)
        {
            //if (PaymentModel.Id is 0)
            //    _repository.Insert(PaymentModel);
            //else _repository.Update(PaymentModel);

            using AppDbContext _myContext = base.GetContext();
            if (PaymentModel.Id is 0)
                _myContext.Payments.Add(PaymentModel);
            else
                _myContext.Payments.Update(PaymentModel);

            _myContext.SaveChanges();
        }

        public Payment GetByEventId(long eventId)
        {
            //return _appDbContext.Payments.FirstOrDefault(x => x.EventId == eventId);
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Payments.Include(x => x.Event).FirstOrDefault(x => x.EventId == eventId);
        }
    }
}

