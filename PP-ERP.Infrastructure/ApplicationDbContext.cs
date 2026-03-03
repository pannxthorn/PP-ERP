using Microsoft.EntityFrameworkCore;
using PP_ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        #region [Entities]

        public DbSet<BRANCH> BRANCH { get; set; }
        public DbSet<COMPANY> COMPANY { get; set; }
        public DbSet<SYS_USER> USER { get; set; }
        public DbSet<FLEX> FLEX { get; set; }
        public DbSet<FLEX_ITEM> FLEX_ITEM { get; set; }

        #endregion [Entities]


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
