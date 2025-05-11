using PaymentService.Application.Repositories;
using PaymentService.Domain.Entities;


namespace PaymentService.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        PaymentServiceContext _db;
        public PaymentRepository(PaymentServiceContext db) { 
            _db = db;
        }

        public bool SavePaymentDetails(PaymentDetail model)
        {
            _db.PaymentDetails.Add(model);
            _db.SaveChanges();
            return true;
        }
    }
}
