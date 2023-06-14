using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElSalamPapermill.Domain.Entities
{
    public class Jobs
    {
        [Key]
        public int CandidateId { get; set; }
        [Required]
        public string CandidateFN { get; set; } = string.Empty;
        [Required]  
        public string CandidateLN { get; set;} = string.Empty;
        [EmailAddress]
        [Required]
        public string CandidateEmail { get; set; } = string.Empty;
        [Required]
        public string CandidateMobileNo { get; set; } = string.Empty;
        [Required]
        public string CandidateAddress { get; set; } = string.Empty;
        public string? Country { get; set; }
        [Required]
        public string CandidateJobTitle { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? CandidateCV { get; set; }
        public string? CandidateCVPath { get; set; }
        public string? CandidateMsg { get; set; }
        //public string UserId { get; set; } = string.Empty;
        //public IdentityUser? User { get; set; }
    }
}
