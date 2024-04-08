﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookShelfHaven6Ice2.Models;

public partial class BookShelfHavenContext : DbContext
{
    public BookShelfHavenContext()
    {
    }

    public BookShelfHavenContext(DbContextOptions<BookShelfHavenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Checkout> Checkouts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LoginAttempt> LoginAttempts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=BookShelfHaven;Encrypt=False;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Admin__536C85E5BA0E9720");

            entity.ToTable("Admin");

            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B75EFDB3B7");

            entity.ToTable("Cart");

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductNames).HasMaxLength(100);
            entity.Property(e => e.UserId)
                .HasMaxLength(200)
                .HasColumnName("UserID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__ProductId__52593CB8");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Cart__Username__5165187F");
        });

        modelBuilder.Entity<Checkout>(entity =>
        {
            entity.HasKey(e => e.CheckoutId).HasName("PK__Checkout__E07EF5FC25882C91");

            entity.ToTable("Checkout");

            entity.Property(e => e.CheckoutDateTime).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Checkouts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductId");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Checkouts)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_CustomerId");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Customer__536C85E58A2B9640");

            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        modelBuilder.Entity<LoginAttempt>(entity =>
        {
            entity.HasKey(e => e.AttemptId).HasName("PK__LoginAtt__891A68E6A87D19BD");

            entity.Property(e => e.AttemptDateTime).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.LoginAttempts)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__LoginAtte__Usern__4CA06362");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD04447732");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductNames).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
