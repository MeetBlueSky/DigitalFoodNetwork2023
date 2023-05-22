
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

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Category>(entity =>
            {

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.ChildCategory)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<CategoryProductBase>(entity =>
            {

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryProductBase)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_CategoryProductBase");

                entity.HasOne(d => d.ProductBase)
                    .WithMany(p => p.CategoryProductBase)
                    .HasForeignKey(d => d.ProductBaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductBase_CategoryProductBase");
            });

            modelBuilder.Entity<City>(entity =>
            {

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Country_City");
            });

            modelBuilder.Entity<County>(entity =>
            {

                entity.HasOne(d => d.City)
                    .WithMany(p => p.County)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_County");
            });

            modelBuilder.Entity<Company>(entity =>
            {

                //entity.HasOne(d => d.CompanyType)
                //    .WithMany(p => p.Company)
                //    .HasForeignKey(d => d.CompanyTypeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_CompanyType_Company");

                //entity.HasOne(d => d.OfficialCountry)
                //    .WithMany(p => p.OfficialCompany)
                //    .HasForeignKey(d => d.OfficialCountryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Country_CompanyOfficial");

                //entity.HasOne(d => d.OfficialCity)
                //    .WithMany(p => p.OfficialCompany)
                //    .HasForeignKey(d => d.OfficialCityId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_City_CompanyOfficial");

                //entity.HasOne(d => d.OfficialCounty)
                //    .WithMany(p => p.OfficialCompany)
                //    .HasForeignKey(d => d.OfficialCountyId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_County_CompanyOfficial");

                //entity.HasOne(d => d.MapCountry)
                //    .WithMany(p => p.MapCompany)
                //    .HasForeignKey(d => d.MapCountryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Country_CompanyMap");

                //entity.HasOne(d => d.MapCity)
                //    .WithMany(p => p.MapCompany)
                //    .HasForeignKey(d => d.MapCityId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_City_CompanyMap");

                //entity.HasOne(d => d.MapCounty)
                //    .WithMany(p => p.MapCompany)
                //    .HasForeignKey(d => d.MapCountyId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_County_CompanyMap");
            });

            modelBuilder.Entity<CompanyImage>(entity =>
            {

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyImage)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_CompanyImage");
            });
             

            modelBuilder.Entity<ProductCompany>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCompany)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_ProductCompany");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_ProductCompany");

                entity.HasOne(d => d.ProductBase)
                    .WithMany(p => p.ProductCompany)
                    .HasForeignKey(d => d.ProductBaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductBase_ProductCompany");
            });

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

            modelBuilder.Entity<UserUrunler>(entity =>
            {
                entity.HasOne(d => d.Company)
                   .WithMany(p => p.UserUrunler)
                   .HasForeignKey(d => d.CompanyId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Company_UserUrunler");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserUrunler)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserUrunler");
            });


            modelBuilder.Entity<Message>(entity =>
            {

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.FromUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Message"); 
            });


        }



    }
}
