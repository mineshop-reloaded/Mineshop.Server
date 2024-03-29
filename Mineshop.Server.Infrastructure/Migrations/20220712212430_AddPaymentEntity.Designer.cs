// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MineshopContext))]
    [Migration("20220712212430_AddPaymentEntity")]
    partial class AddPaymentEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.CategoryEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<Guid>("ServerIdentifier")
                        .HasColumnType("uuid");

                    b.HasKey("Identifier");

                    b.HasIndex("ServerIdentifier");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.PaymentEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MinecraftPlayer")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<int>("PaymentGateway")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.HasKey("Identifier");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.PaymentProductEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PaymentIdentifier")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductIdentifier")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Identifier");

                    b.HasIndex("PaymentIdentifier");

                    b.HasIndex("ProductIdentifier");

                    b.ToTable("PaymentProducts");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.ProductEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryIdentifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Identifier");

                    b.HasIndex("CategoryIdentifier");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.ServerEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Identifier");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.CategoryEntity", b =>
                {
                    b.HasOne("Mineshop.Server.Domain.Domains.ServerEntity", "Server")
                        .WithMany()
                        .HasForeignKey("ServerIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.PaymentProductEntity", b =>
                {
                    b.HasOne("Mineshop.Server.Domain.Domains.PaymentEntity", "Payment")
                        .WithMany("Products")
                        .HasForeignKey("PaymentIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mineshop.Server.Domain.Domains.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.ProductEntity", b =>
                {
                    b.HasOne("Mineshop.Server.Domain.Domains.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Mineshop.Server.Domain.Domains.PaymentEntity", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
