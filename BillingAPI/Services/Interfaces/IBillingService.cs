using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBillingService
    {
        Task<List<Billing>> CreateBilling(Billing billing);
        Billing? GetBillingById(int id);
        Task<string> UpdateBillingCurrency(Billing billing);
        Task<string> DeleteBilling(int id);
    }
}
