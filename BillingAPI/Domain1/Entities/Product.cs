namespace Domain.Entities
{
    public class Product
    {
        public required Guid Id { get; set; }
        public required string NomeDoProduto { get; set; }

        public ICollection<BillingLine> BillingLines { get; set; }
    }
}
