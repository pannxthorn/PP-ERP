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
    public class PURCHASE_ORDERConfiguration : IEntityTypeConfiguration<PURCHASE_ORDER>
    {
        public void Configure(EntityTypeBuilder<PURCHASE_ORDER> builder)
        {
            // Table name
            builder.ToTable(nameof(PURCHASE_ORDER));

            // Primary key
            builder.HasKey(f => f.PURCHASE_ORDER_ID);

            // Properties
            builder.Property(f => f.PURCHASE_ORDER_NO)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(f => f.VENDOR_CODE)
                .HasMaxLength(50);

            builder.Property(f => f.VENDOR_FULLNAME)
                .HasMaxLength(300);

            builder.Property(f => f.VAT_RATE)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.SUB_TOTAL)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.SUB_TOTAL_FC)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.DISCOUNT)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.DISCOUNT_FC)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_AFTER_DISCOUNT)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_AFTER_DISCOUNT_FC)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_BEFORE_VAT)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_BEFORE_VAT_FC)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_BEFORE_VAT_BASE)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.VAT)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_AFTER_VAT)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PRICE_AFTER_VAT_FC)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.GRAND_TOTAL)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.GRAND_TOTAL_FC)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.DOCUMENT_STATUS_CODE)
                .HasMaxLength(50);

            builder.Property(f => f.DOCUMENT_STATUS_NAME)
                .HasMaxLength(300);

            builder.Property(f => f.DOCUMENT_STATUS_REASON)
                .HasMaxLength(4000);

            builder.Property(f => f.REMARK)
                .HasMaxLength(4000);

            builder.Property(f => f.COMMENTS)
                .HasMaxLength(4000);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index
            builder.HasIndex(f => f.PURCHASE_ORDER_NO);

            // Relationships
            builder.HasOne(f => f.COMPANY)
                .WithMany()
                .HasForeignKey(f => f.COMPANY_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.BRANCH)
                .WithMany()
                .HasForeignKey(f => f.BRANCH_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.VENDOR)
                .WithMany()
                .HasForeignKey(f => f.VENDOR_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.DOCUMENT_STATUS)
                .WithMany()
                .HasForeignKey(f => f.DOCUMENT_STATUS_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
