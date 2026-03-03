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
    public class FLEXConfiguration : IEntityTypeConfiguration<FLEX>
    {
        public void Configure(EntityTypeBuilder<FLEX> builder)
        {
            // Table name
            builder.ToTable(nameof(FLEX));

            // Primary key
            builder.HasKey(f => f.FLEX_ID);

            // Properties
            builder.Property(f => f.FLEX_CODE)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.FLEX_NAME)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index
            builder.HasIndex(f => f.FLEX_CODE);

            // Relationships
        }
    }
}
