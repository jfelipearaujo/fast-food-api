﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240106005907_ChangedNullableEmail")]
    partial class ChangedNullableEmail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.ClientAggregate.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<string>("DocumentId")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StatusUpdatedAt")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<string>("TrackId")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Observation")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("orders_items", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("payments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("ProductCategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.ToTable("product_categories", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasPrecision(7)
                        .HasColumnType("timestamp(7) with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("stocks", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ClientAggregate.Client", b =>
                {
                    b.OwnsOne("Domain.Entities.ClientAggregate.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("ClientId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)");

                            b1.HasKey("ClientId");

                            b1.ToTable("clients");

                            b1.WithOwner()
                                .HasForeignKey("ClientId");
                        });

                    b.Navigation("FullName")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.HasOne("Domain.Entities.ClientAggregate.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Domain.Entities.OrderAggregate.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ProductAggregate.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.ProductAggregate.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(7, 2)
                                .HasColumnType("numeric(7,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("orders_items");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("Order");

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Payment", b =>
                {
                    b.HasOne("Domain.Entities.OrderAggregate.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.ProductAggregate.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("PaymentId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(7, 2)
                                .HasColumnType("numeric(7,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)");

                            b1.HasKey("PaymentId");

                            b1.ToTable("payments");

                            b1.WithOwner()
                                .HasForeignKey("PaymentId");
                        });

                    b.Navigation("Order");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.Product", b =>
                {
                    b.HasOne("Domain.Entities.ProductAggregate.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.ProductAggregate.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(7, 2)
                                .HasColumnType("numeric(7,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.Stock", b =>
                {
                    b.HasOne("Domain.Entities.ProductAggregate.Product", "Product")
                        .WithOne("Stock")
                        .HasForeignKey("Domain.Entities.ProductAggregate.Stock", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.ClientAggregate.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.Product", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Stock")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ProductAggregate.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
