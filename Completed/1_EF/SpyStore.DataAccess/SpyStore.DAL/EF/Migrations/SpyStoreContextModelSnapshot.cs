using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SpyStore.DAL.EF;

namespace spystore.dal.EF.Migrations
{
    [DbContext(typeof(SpyStoreContext))]
    partial class SpyStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpyStore.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Categories","Store");
                });

            modelBuilder.Entity("SpyStore.Models.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FullName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasName("IX_Customers");

                    b.ToTable("Customers","Store");
                });

            modelBuilder.Entity("SpyStore.Models.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("ShipDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders","Store");
                });

            modelBuilder.Entity("SpyStore.Models.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("LineItemTotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("money")
                        .HasComputedColumnSql("[Quantity]*[UnitCost]");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails","Store");
                });

            modelBuilder.Entity("SpyStore.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 3800);

                    b.Property<bool>("IsFeatured");

                    b.Property<string>("ModelName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ModelNumber")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ProductImage")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("ProductImageLarge")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("ProductImageThumb")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("money");

                    b.Property<int>("UnitsInStock");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products","Store");
                });

            modelBuilder.Entity("SpyStore.Models.Entities.ShoppingCartRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity")
                        .HasDefaultValue(1);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("Id", "ProductId")
                        .HasName("IX_ShoppingCart");

                    b.ToTable("ShoppingCartRecords","Store");
                });

            modelBuilder.Entity("SpyStore.Models.Entities.Order", b =>
                {
                    b.HasOne("SpyStore.Models.Entities.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpyStore.Models.Entities.OrderDetail", b =>
                {
                    b.HasOne("SpyStore.Models.Entities.Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpyStore.Models.Entities.Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpyStore.Models.Entities.Product", b =>
                {
                    b.HasOne("SpyStore.Models.Entities.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpyStore.Models.Entities.ShoppingCartRecord", b =>
                {
                    b.HasOne("SpyStore.Models.Entities.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpyStore.Models.Entities.Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
