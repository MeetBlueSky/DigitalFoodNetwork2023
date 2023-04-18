
using Microsoft.EntityFrameworkCore;
using DFN2023.Entities.EF;
using System;

namespace DFN2023.Infrastructure.Context
{
    public abstract class BaseContext<DCT> : DbContext where DCT : DbContext
    {
        protected BaseContext(DbContextOptions<DCT> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }



        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryProductBase> CategoryProductBase { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyImage> CompanyImage { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<ProductBase> ProductBase { get; set; }
        public virtual DbSet<ProductCompany> ProductCompany { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<StaticContentGrupPage> StaticContentGrupPage { get; set; }
        public virtual DbSet<StaticContentGrupTemp> StaticContentGrupTemp { get; set; }
        public virtual DbSet<StaticContentPage> StaticContentPage { get; set; }
        public virtual DbSet<StaticContentTemp> StaticContentTemp { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserUrunler> UserUrunler { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            //modelBuilder.Entity<Category>(entity =>
            //{

            //    entity.HasOne(d => d.ParentCategory)
            //        .WithMany(p => p.ChildCategory)
            //        .HasForeignKey(d => d.ParentId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Category_Category");
            //});

            modelBuilder.Entity<City>(entity =>
            {

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<County>(entity =>
            {

                entity.HasOne(d => d.City)
                    .WithMany(p => p.County)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_County_City");
            });

            //modelBuilder.Entity<Boats>(entity =>
            //{

            //    entity.HasOne(d => d.Brands)
            //        .WithMany(p => p.Boats)
            //        .HasForeignKey(d => d.BrandId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Boat_Brands");

            //    entity.HasOne(d => d.ModelGroup)
            //        .WithMany(p => p.Boats)
            //        .HasForeignKey(d => d.ModelId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Boat_ModelGroup");

            //    entity.HasOne(d => d.StokTypes)
            //        .WithMany(p => p.Boats)
            //        .HasForeignKey(d => d.StokTypeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Boat_StockType");
            //});


            

            modelBuilder.Entity<StaticContentPage>(entity =>
            {

                entity.HasOne(d => d.StaticContentGrupPage)
                .WithMany(p => p.StaticContentPage)
                .HasForeignKey(d => d.GrupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaticContentGrupPage_StaticContentPage");

                entity.HasOne(d => d.StaticContentTemp)
                                     .WithMany(p => p.StaticContentPage)
                                     .HasForeignKey(d => d.TempId)
                                     .OnDelete(DeleteBehavior.ClientSetNull)
                                     .HasConstraintName("FK_StaticContentTemp_StaticContentPage");


                //entity.HasIndex(d => d.Slug).IsUnique();


            });


            modelBuilder.Entity<StaticContentGrupPage>(entity =>
            {

                entity.HasOne(d => d.StaticContentGrupTemp)
                    .WithMany(p => p.StaticContentGrupPage)
                    .HasForeignKey(d => d.GrupTempId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaticContentGrupTemp_StaticContentGrupPage");

            });


        }
    }
}
