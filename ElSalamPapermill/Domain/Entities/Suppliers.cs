using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElSalamPapermill.Domain.Entities
{
    public class Suppliers
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string SupplierFN { get; set; } = string.Empty;
        [Required]  
        public string SupplierLN { get; set;} = string.Empty;
        [EmailAddress]
        [Required]
        public string SupplierEmail { get; set; } = string.Empty;
        [Required]
        public string SupplierMobileNo { get; set; } = string.Empty;
        [Required]
        public string SupplierAddress { get; set; } = string.Empty;
        public string? Country { get; set; }
        [Required]
        public string Material { get; set; } = string.Empty;
        public string? SupplierMsg { get; set; }
        //public string UserId { get; set; } = string.Empty;
        //public IdentityUser? User { get; set; }
    }
}
