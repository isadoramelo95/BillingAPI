using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class BillingLine
    {
        public int Id { get; set; }
        public int BillingId { get; set; }
        public Billing Billing { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal SubTotal { get; set; }

    }
}
