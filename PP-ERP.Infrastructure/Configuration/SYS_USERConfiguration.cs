using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP_ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Infrastructure.Configuration
{
    public class SYS_USERConfiguration : IEntityTypeConfiguration<SYS_USER>
    {
        public void Configure(EntityTypeBuilder<SYS_USER> builder)
        {
            // Table name
            builder.ToTable(nameof(SYS_USER));

            // Primary key
            builder.HasKey(f => f.USER_ID);

            // Properties

            builder.Property(f => f.USERNAME)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.PASSWORD)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(f => f.COMMENT)
                .HasMaxLength(4000);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Relationships
            builder.HasOne(f => f.COMPANY)
                .WithMany()
                .HasForeignKey(f => f.COMPANY_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.BRANCH)
                .WithMany()
                .HasForeignKey(f => f.BRANCH_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
