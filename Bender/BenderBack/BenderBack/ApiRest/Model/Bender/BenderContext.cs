using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRest.Model.Bender
{
    public partial class BenderContext : DbContext
    {
        public BenderContext()
        {
        }

        public BenderContext(DbContextOptions<BenderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Combo> Combos { get; set; } = null!;
        public virtual DbSet<Combostockexist> Combostockexists { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductHasCombo> ProductHasCombos { get; set; } = null!;
        public virtual DbSet<ProductHasCombosHasCartum> ProductHasCombosHasCarta { get; set; } = null!;
        public virtual DbSet<ProductHasOrder> ProductHasOrders { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionDB.BenderConnectionString, ServerVersion.Parse("8.0.19-mysql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.Idbranch)
                    .HasName("PRIMARY");

                entity.ToTable("branch");

                entity.Property(e => e.Idbranch).HasColumnName("idbranch");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Tablequantity).HasColumnName("tablequantity");
            });

            modelBuilder.Entity<Combo>(entity =>
            {
                entity.HasKey(e => e.Idcombo)
                    .HasName("PRIMARY");

                entity.ToTable("combo");

                entity.Property(e => e.Idcombo).HasColumnName("idcombo");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Namecombo)
                    .HasMaxLength(45)
                    .HasColumnName("namecombo");
            });

            modelBuilder.Entity<Combostockexist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("combostockexist");

                entity.Property(e => e.CombosIdcombos).HasColumnName("combos_idcombos");

                entity.Property(e => e.Namecombo)
                    .HasMaxLength(45)
                    .HasColumnName("namecombo");

                entity.Property(e => e.ProductoIdproducto).HasColumnName("producto_idproducto");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Idinvoice)
                    .HasName("PRIMARY");

                entity.ToTable("invoice");

                entity.Property(e => e.Idinvoice).HasColumnName("idinvoice");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Idorder).HasColumnName("idorder");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Idtable).HasColumnName("idtable");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Paymentmethod)
                    .HasMaxLength(45)
                    .HasColumnName("paymentmethod");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.Idmenu)
                    .HasName("PRIMARY");

                entity.ToTable("menu");

                entity.Property(e => e.Idmenu).HasColumnName("idmenu");

                entity.Property(e => e.Idproductcombo).HasColumnName("idproductcombo");

                entity.Property(e => e.Idstock).HasColumnName("idstock");

                entity.Property(e => e.Price)
                    .HasPrecision(5, 2)
                    .HasColumnName("price");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Idorder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.Property(e => e.Idorder).HasColumnName("idorder");

                entity.Property(e => e.CombosIdcombos).HasColumnName("combos_idcombos");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.MesaIdmesa).HasColumnName("mesa_idmesa");

                entity.Property(e => e.MesaSucursalIdsucursal).HasColumnName("mesa_sucursal_idsucursal");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Idproduct)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.InvoiceIdinvoice).HasColumnName("invoice_idinvoice");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasMaxLength(45)
                    .HasColumnName("price");

                entity.Property(e => e.Supplier)
                    .HasMaxLength(45)
                    .HasColumnName("supplier");
            });

            modelBuilder.Entity<ProductHasCombo>(entity =>
            {
                entity.HasKey(e => e.Idproductocombo)
                    .HasName("PRIMARY");

                entity.ToTable("product_has_combo");

                entity.Property(e => e.Idproductocombo).HasColumnName("idproductocombo");

                entity.Property(e => e.CombosIdcombos).HasColumnName("combos_idcombos");

                entity.Property(e => e.ProductoIdproducto).HasColumnName("producto_idproducto");
            });

            modelBuilder.Entity<ProductHasCombosHasCartum>(entity =>
            {
                entity.HasKey(e => e.ProductHasComboIdproductcombo)
                    .HasName("PRIMARY");

                entity.ToTable("product_has_combos_has_carta");

                entity.Property(e => e.ProductHasComboIdproductcombo).HasColumnName("product_has_combo_idproductcombo");

                entity.Property(e => e.MenuIdmenu).HasColumnName("menu_idmenu");

                entity.Property(e => e.ProductHasComboComboIdcombo).HasColumnName("product_has_combo_combo_idcombo");

                entity.Property(e => e.ProductHasComboProductIdproduct).HasColumnName("product_has_combo_product_idproduct");
            });

            modelBuilder.Entity<ProductHasOrder>(entity =>
            {
                entity.HasKey(e => e.ProductHasOrderIdproductorder)
                    .HasName("PRIMARY");

                entity.ToTable("product_has_order");

                entity.Property(e => e.ProductHasOrderIdproductorder).HasColumnName("product_has_order_idproductorder");

                entity.Property(e => e.OrderIdorder).HasColumnName("order_idorder");

                entity.Property(e => e.ProductIdproduct).HasColumnName("product_idproduct");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.IdPurchase)
                    .HasName("PRIMARY");

                entity.ToTable("purchase");

                entity.Property(e => e.IdPurchase).HasColumnName("idPurchase");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Nitsupplier)
                    .HasMaxLength(45)
                    .HasColumnName("nitsupplier");

                entity.Property(e => e.ProductIdproduct).HasColumnName("product_idproduct");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(45)
                    .HasColumnName("quantity");

                entity.Property(e => e.Supplier)
                    .HasMaxLength(45)
                    .HasColumnName("supplier");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Idrol)
                    .HasName("PRIMARY");

                entity.ToTable("rol");

                entity.Property(e => e.Idrol).HasColumnName("idrol");

                entity.Property(e => e.Rolname)
                    .HasMaxLength(45)
                    .HasColumnName("rolname");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Idstock)
                    .HasName("PRIMARY");

                entity.ToTable("stock");

                entity.Property(e => e.Idstock).HasColumnName("idstock");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ProductIdproduct).HasColumnName("product_idproduct");

                entity.Property(e => e.Stock1).HasColumnName("stock");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.Idtable)
                    .HasName("PRIMARY");

                entity.ToTable("table");

                entity.Property(e => e.Idtable).HasColumnName("idtable");

                entity.Property(e => e.Availability)
                    .HasMaxLength(45)
                    .HasColumnName("availability");

                entity.Property(e => e.BranchIdbranch).HasColumnName("branch_idbranch");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.Property(e => e.Iduser)
                    .ValueGeneratedNever()
                    .HasColumnName("iduser");

                entity.Property(e => e.BranchIdbranch).HasColumnName("branch_idbranch");

                entity.Property(e => e.Names)
                    .HasMaxLength(45)
                    .HasColumnName("names");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password");

                entity.Property(e => e.RolIdrol).HasColumnName("rol_idrol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
