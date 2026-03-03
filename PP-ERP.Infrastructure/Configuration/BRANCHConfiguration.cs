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
    public class BRANCHConfiguration : IEntityTypeConfiguration<BRANCH>
    {
        public void Configure(EntityTypeBuilder<BRANCH> builder)
        {
            // Table name
            builder.ToTable(nameof(BRANCH));

            // Primary key
            builder.HasKey(f => f.BRANCH_ID);

            // Properties
            builder.Property(f => f.BRANCH_CODE)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(f => f.BRANCH_NAME)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(f => f.PHONE)
                .HasMaxLength(100);

            builder.Property(f => f.EMAIL)
                .HasMaxLength(100);

            builder.Property(f => f.LINE)
                .HasMaxLength(100);

            builder.Property(f => f.FACEBOOK)
                .HasMaxLength(100);

            builder.Property(f => f.COMMENT)
                .HasMaxLength(4000);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index

            // Relationships
            builder.HasOne(f => f.COMPANY)
                .WithMany(f => f.BRANCHES)
                .HasForeignKey(f => f.COMPANY_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
