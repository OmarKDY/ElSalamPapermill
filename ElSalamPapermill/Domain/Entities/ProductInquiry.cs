using System.ComponentModel.DataAnnotations;

namespace ElSalamPapermill.Domain.Entities
{
    public class ProductInquiry
    {
        [Key]
        public int InquiryId { get; set; }
        [Required]
        public string ClientName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string ClientEmail { get; set; } = string.Empty;
        [Required]
        public string ProductType { get; set; } = string.Empty;
        [Required]
        public string ClientMobile { get; set; } = string.Empty;
        public string? ClientMsg { get; set; }

    }
}
