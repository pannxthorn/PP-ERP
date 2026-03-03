using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PP_ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Infrastructure.Configuration
{
    public class FLEX_ITEMConfiguration : IEntityTypeConfiguration<FLEX_ITEM>
    {
        public void Configure(EntityTypeBuilder<FLEX_ITEM> builder)
        {
            // Table name
            builder.ToTable(nameof(FLEX_ITEM));

            // Primary key
            builder.HasKey(f => f.FLEX_ITEM_ID);

            // Properties

            builder.Property(f => f.FLEX_ITEM_CODE)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.FLEX_ITEM_NAME)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index
            builder.HasIndex(f => f.FLEX_ITEM_CODE);

            // Relationships
            builder.HasOne(f => f.FLEX)
                .WithMany(f => f.FLEX_ITEMS)
                .HasForeignKey(f => f.FLEX_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
