using MotosAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotosAPI.Models.BillingPayment
{
    [Table("BillingPayment", Schema = "public")]
    public class BillingPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public string PaymentId { get; set; } = "";

        [Required]
        public string Method { get; set; } = "";

        [Required]
        public string CardType { get; set; } = "";

        [Required]
        public string Bank { get; set; } = "";

        [Required]
        public double Amount { get; set; }

        [Required]
        public string Currency { get; set; } = "";

        [Required]
        public string AccountNo { get; set; } = "";

        [Required]
        public long TimeInitiate { get; set; }

        [Required]
        public long TimeSuccess { get; set; }

        [Required]
        public long TimeFailed { get; set; }

        [Required]
        public string Note { get; set; } = "";

        [Required]
        public string FullName { get; set; } = "";

        [Required]
        public string Brand { get; set; } = "";

        [Required]
        public string MOdel { get; set; } = "";

        [Required]
        public string Phone { get; set; } = "";

        [Required]
        public string AddressLong { get; set; } = "";

        [Required]
        public bool IsPartialPayment { get; set; }

        [Required]
        public string Status { get; set; } = "";
        
        [Required]
        public long TimeCreated { get; set; } = TimeOperation.GetUnixTime();

        [Required]
        public long TimeUpdated { get; set; } = TimeOperation.GetUnixTime();

        [Required]
        public long TimeDeleted { get; set; } = 0;
    }
}
