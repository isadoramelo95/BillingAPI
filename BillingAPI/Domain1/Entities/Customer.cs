namespace Domain.Entities
{
    public class Customer
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }

        public ICollection<Billing> Billings { get; set; }
    }
}
