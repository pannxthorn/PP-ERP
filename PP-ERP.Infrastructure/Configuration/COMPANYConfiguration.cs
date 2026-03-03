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
    public class COMPANYConfiguration : IEntityTypeConfiguration<COMPANY>
    {
        public void Configure(EntityTypeBuilder<COMPANY> builder)
        {
            // Table name
            builder.ToTable(nameof(COMPANY));

            // Primary key
            builder.HasKey(f => f.COMPANY_ID);


            // Properties
            builder.Property(f => f.COMPANY_CODE)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(f => f.COMPANY_NAME)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(f => f.TAX_NO)
                .HasMaxLength(13);

            builder.Property(f => f.PHONE)
                .HasMaxLength(100);

            builder.Property(f => f.FAX)
                .HasMaxLength(100);

            builder.Property(f => f.EMAIL)
                .HasMaxLength(100);

            builder.Property(f => f.LINE)
                .HasMaxLength(100);

            builder.Property(f => f.FACEBOOK)
                .HasMaxLength(100);

            builder.Property(f => f.WEBSITE)
                .HasMaxLength(300);

            builder.Property(f => f.LOGO)
                .HasMaxLength(300);

            builder.Property(f => f.COMMENT)
                .HasMaxLength(4000);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index


            // Relationships
        }
    }
}
