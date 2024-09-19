using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Repository.Interface
{
    public interface IPaymentRepository
    {
        public List<Payment> GetPaymentList();
        public void Insert(Payment PaymentModel);
        public void Update(Payment PaymentModel);
        public void Delete(long id);
        public Payment GetById(long id);
        public List<Payment> GetPaymentListByEventId(long eventId);
        public Payment GetByEventId(long eventId);
      
    }
}
