using System.ComponentModel.DataAnnotations;
using ElSalamPapermill.Domain.Enums;

namespace ElSalamPapermill.Domain.Entities
{
    public class OrderDetail
    {
        //Personal information
        [Key]
        public int OrderId { get; set; }
        public Guid OrderGuid { get; set; }
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? Website { get; set; }
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string ApplicantsName { get; set; } = string.Empty;
        [Required]
        public string ApplicantsPhone { get; set; } = string.Empty;

        //Shipping Address
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? DetailedAddress { get; set; }

        //Order information
        [Required]
        public string CardboardType { get; set; } = string.Empty;
        [Required]
        public string CutTypes { get; set; } = string.Empty;
        [Required]
        public decimal? LengthAndDiameter { get; set; }
        [Required]
        public decimal? Width { get; set; }
        [Required]
        public string QuantityType { get; set; } = string.Empty;
        [Required]
        public decimal? Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
