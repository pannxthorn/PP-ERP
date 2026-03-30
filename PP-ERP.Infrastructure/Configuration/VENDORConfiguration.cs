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
    public class VENDORConfiguration : IEntityTypeConfiguration<VENDOR>
    {
        public void Configure(EntityTypeBuilder<VENDOR> builder)
        {
            // Table name
            builder.ToTable(nameof(VENDOR));

            // Primary key
            builder.HasKey(f => f.VENDOR_ID);

            // Properties
            builder.Property(f => f.VENDOR_CODE)
                .HasMaxLength(50);

            builder.Property(f => f.VENDOR_NAME)
                .HasMaxLength(300);

            builder.Property(f => f.VENDOR_NAME_ENG)
                .HasMaxLength(300);

            builder.Property(f => f.VENDOR_FULLNAME)
                .HasMaxLength(300);

            builder.Property(f => f.VENDOR_FULLNAME_ENG)
                .HasMaxLength(300);

            builder.Property(f => f.COMMENTS)
                .HasMaxLength(4000);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index
            builder.HasIndex(f => f.VENDOR_CODE);

            // Relationships
            builder.HasOne(f => f.TITLE)
                .WithMany()
                .HasForeignKey(f => f.TITLE_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
