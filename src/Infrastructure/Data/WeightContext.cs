using Domain;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Data.AutoHistoryExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class WeightContext : DbContext
    {
        public WeightContext()
        {

        }
        public WeightContext(DbContextOptions<WeightContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();

            var dbConnectionString = $"server={Environment.GetEnvironmentVariable("ASPNETCORE_DB_SERVER")};port=3306;database={Environment.GetEnvironmentVariable("ASPNETCORE_DB_DATABASE")};user={Environment.GetEnvironmentVariable("ASPNETCORE_DB_USER")};password={Environment.GetEnvironmentVariable("ASPNETCORE_DB_USERPWD")};";

            if (string.IsNullOrEmpty(dbConnectionString))
                dbConnectionString = "server=localhost;port=3306;database=weightit;user=dbuser;password=dbuserpwd;";

            optionsBuilder.UseMySQL(dbConnectionString);
        }
        public DbSet<AutoHistory> AutoHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Plate> Plates { get; set; }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<ProductMeal> ProductMeals { get; set; }
        public DbSet<TemplateMeal> TemplateMeals { get; set; }
        public DbSet<TemplateMealProduct> TemplateMealProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.EnableAutoHistory<CustomAutoHistory>(o => { });
            #region Users

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(q => q.Id);

                entity.Property(q => q.FirstName).HasMaxLength(32);
                entity.Property(q => q.LastName).HasMaxLength(32);
                entity.Property(q => q.Username).HasMaxLength(32).IsRequired(true);
                entity.Property(q => q.Password).HasMaxLength(64).IsRequired(true);

                entity.Property(q => q.IsActive);
                entity.Property(q => q.IsDeleted);
                entity.Property(q => q.CreateDateTime);
            });

            #endregion

            #region Products

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(q => q.Id);
                entity.Property(q => q.IsFavorite);
                entity.Property(q => q.Name).IsRequired().HasMaxLength(100);
                entity.Property(q => q.Carbo).IsRequired();
                entity.Property(q => q.Fat).IsRequired();
                entity.Property(q => q.Protein).IsRequired();
                entity.Property(q => q.Manufacturer).IsRequired(false).HasMaxLength(100);
                entity.Property(q => q.Barcode).IsRequired(false).HasMaxLength(24);

                entity.HasOne(q => q.Creator).WithMany(q => q.Products).HasForeignKey(q => q.CreatorId).IsRequired(false);

                entity.HasMany(q => q.Meals).WithOne(q => q.Product);
            });

            modelBuilder.Entity<ProductMeal>(entity =>
            {
                entity.ToTable("ProductMeals");
                entity.HasKey(q => new { q.ProductId, q.MealId });

                entity.Property(q => q.CreateDateTime);
                entity.Property(q => q.ChangeDateTime);

                entity.HasOne(q => q.Product).WithMany(q => q.Meals).HasForeignKey(q => q.ProductId);
                entity.HasOne(q => q.Meal).WithMany(q => q.Products).HasForeignKey(q => q.MealId);
            });
            #endregion

            #region Meals

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meals");
                entity.HasKey(q => q.Id);

                entity.Property(q => q.Name).HasMaxLength(64);
                entity.Property(q => q.IsFavorite);

                entity.HasOne(q => q.Creator).WithMany(q => q.Meals).HasForeignKey(q => q.CreatorId).IsRequired(true);
                entity.HasMany(q => q.Products).WithOne(q => q.Meal);

                entity.Property(q => q.IsActive);
                entity.Property(q => q.IsDeleted);
                entity.Property(q => q.CreateDateTime);
                entity.Property(q => q.ChangeDateTime);
            });

            #endregion

            #region Plates

            modelBuilder.Entity<Plate>(entity =>
            {
                entity.ToTable("Plates");
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Name).IsRequired().HasMaxLength(100);
                entity.Property(q => q.Weight).IsRequired();

                entity.HasOne(q => q.Creator).WithMany(q => q.Plates).HasForeignKey(q => q.CreatorId).IsRequired(false);
            });


            #endregion

            #region TemplateMeal

            modelBuilder.Entity<TemplateMeal>(entity =>
            {
                entity.ToTable("TemplateMeals");
                entity.HasKey(q => q.Id);

                entity.Property(q => q.Name).HasMaxLength(64);

                entity.HasOne(q => q.Creator).WithMany(q => q.TemplateMeals).HasForeignKey(q => q.CreatorId).IsRequired(true);
                entity.HasMany(q => q.Products).WithOne(q => q.TemplateMeal);

                entity.Property(q => q.IsActive);
                entity.Property(q => q.IsDeleted);
                entity.Property(q => q.CreateDateTime);
                entity.Property(q => q.ChangeDateTime);
            });

            modelBuilder.Entity<TemplateMealProduct>(entity =>
            {
                entity.ToTable("TemplateMealProducts");
                entity.HasKey(q => new { q.ProductId, q.TemplateMealId });

                entity.Property(q => q.CreateDateTime);
                entity.Property(q => q.ChangeDateTime);

                entity.HasOne(q => q.Product).WithMany(q => q.TemplateMeals).HasForeignKey(q => q.ProductId);
                entity.HasOne(q => q.TemplateMeal).WithMany(q => q.Products).HasForeignKey(q => q.TemplateMealId);
            });

            #endregion

        }
        [Obsolete("Use method with 'ActionContext'", true)]
        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
        [Obsolete("Use method with 'ActionContext'", true)]
        public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        public int SaveChanges(ActionContext actionContext)
        {
            this.EnsureAutoHistory(() => new CustomAutoHistory()
            {
                Username = actionContext != null ? actionContext.Username : ""
            });

            var entries = ChangeTracker
             .Entries()
             .Where(e => e.Entity is BaseCreateEntity && (
                     e.State == EntityState.Added
                     || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseCreateEntity)entityEntry.Entity).ChangeDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseCreateEntity)entityEntry.Entity).CreateDateTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync(ActionContext actionContext)
        {
            this.EnsureAutoHistory(() => new CustomAutoHistory()
            {
                Username = actionContext != null ? actionContext.Username : ""
            });

            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is BaseCreateEntity && (
                       e.State == EntityState.Added
                       || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseCreateEntity)entityEntry.Entity).ChangeDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseCreateEntity)entityEntry.Entity).CreateDateTime = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
