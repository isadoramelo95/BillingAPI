using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateBilling(Billing billing);
        Billing? GetBillingById(int id);
        Task<string> UpdateBillingCurrency(Billing billing);
        Task DeleteBilling(int id);
    }
}
