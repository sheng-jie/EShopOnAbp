using System;
using EShopOnAbp.Customers;
using EShopOnAbp.Vips;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace EShopOnAbp.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class EShopOnAbpDbContext :
        AbpDbContext<EShopOnAbpDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vip> Vips { get; set; }
        public DbSet<VipScoreRecord> VipScoreRecords { get; set; }

        public EShopOnAbpDbContext(DbContextOptions<EShopOnAbpDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(EShopOnAbpConsts.DbTablePrefix + "YourEntities", EShopOnAbpConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Customer>(c => { c.Property(c => c.Id).HasMaxLength(36); });


            builder.Entity<Vip>(v =>
            {
                v.Property(t => t.Id).HasMaxLength(36);
                v.Property(t => t.CustomerId).HasMaxLength(36);
                v.Property(t => t.CustomerId).IsRequired();
                v.HasIndex(t => t.CustomerId).IsUnique();
            });


            builder.Entity<VipScoreRecord>().HasOne<Vip>().WithMany(t => t.ScoreRecords)
                .HasForeignKey(t => t.VipId).IsRequired();

            builder.Entity<VipScoreRecord>(t =>
            {
                t.Property(v => v.VipScoreRecordStatus)
                    .HasMaxLength(16)
                    .HasConversion(
                        v => v.ToString(),
                        v => (VipScoreRecordStatusEnum) Enum.Parse(typeof(VipScoreRecordStatusEnum), v));

                t.Property(v => v.VipScoreRecordType)
                    .HasMaxLength(16)
                    .HasConversion(
                        v => v.ToString(),
                        v => (VipScoreRecordTypeEnum) Enum.Parse(typeof(VipScoreRecordTypeEnum), v));
            });
        }
    }
}