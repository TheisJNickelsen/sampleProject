using System;
using System.Collections.Generic;
using SampleSolution.Data.Contexts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SampleSolution.Data.Contexts
{
    /*Migrations
             1) Change object or add tables
             2) From Package Manager Console:
                 - Add-Migration <name>
                 - Update-Database
            */

    public class SomeDataContext : IdentityDbContext<ApplicationUser>
    {

        public SomeDataContext(DbContextOptions<SomeDataContext> options)
            : base(options)
        { }

        public SomeDataContext() : base(new DbContextOptions<SomeDataContext>()) { }

        public virtual DbSet<SomeData> SomeData { get; set; }
        public virtual DbSet<BusinessUser> BusinessUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SomeData>(e => {
                e.Property(x => x.BusinessUserId).IsRequired();
            });
        }
    }
}
