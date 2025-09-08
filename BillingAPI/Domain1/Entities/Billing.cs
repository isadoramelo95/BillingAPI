using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Billing
    {
        public Customer Customer { get; set; }
        
        [Required]
        public Guid CustomerId { get; set; }
        public int Id { get; set; }
        public string Currency { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("lines")]
        public ICollection<BillingLine> BillingLines { get; set; }
    }
}
