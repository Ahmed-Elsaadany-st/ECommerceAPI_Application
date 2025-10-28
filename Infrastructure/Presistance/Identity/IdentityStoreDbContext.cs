using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Identity
{
    public class IdentityStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
            builder.Entity<ApplicationUser>()
                       .HasOne(u => u.Address)
                       .WithOne(a => a.User)
                       .HasForeignKey<Address>(a => a.AppUserId);
        }
    }
}
