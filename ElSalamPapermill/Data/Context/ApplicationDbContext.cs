using Microsoft.EntityFrameworkCore;
using ElSalamPapermill.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ElSalamPapermill.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        //public Suppliers Supplier { get; set; }
        //public Jobs Job { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }

    }
}