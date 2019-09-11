using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ef2SqlLib
{
    public partial class PrsDb7Context : DbContext
    {
        public PrsDb7Context()
        {
        }

        public PrsDb7Context(DbContextOptions<PrsDb7Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<RequestLines> RequestLines { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=PrsDb7;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasIndex(e => e.PartNbr)
                    .HasName("UQ__Products__DAFC0C1E4271F077")
                    .IsUnique();

                entity.Property(e => e.Price).HasDefaultValueSql("((10))");

                entity.Property(e => e.Unit).HasDefaultValueSql("('Each')");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Vendor__5441852A");
            });

            modelBuilder.Entity<RequestLines>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RequestLines)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestLi__Produ__5DCAEF64");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestLines)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestLi__Reque__5CD6CB2B");
            });

            modelBuilder.Entity<Requests>(entity =>
            {
                entity.Property(e => e.DeliveryMode).HasDefaultValueSql("('PickUp')");

                entity.Property(e => e.Status).HasDefaultValueSql("('NEW')");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Requests__UserId__59FA5E80");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__536C85E4539824FB")
                    .IsUnique();
            });

            modelBuilder.Entity<Vendors>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Vendors__A25C5AA726F9572A")
                    .IsUnique();
            });
        }
    }
}
