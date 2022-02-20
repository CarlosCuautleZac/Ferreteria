using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ferreteria.Models
{
    public partial class ferreteriaContext : DbContext
    {
        public ferreteriaContext()
        {
        }

        public ferreteriaContext(DbContextOptions<ferreteriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Seccion> Seccions { get; set; }
        public virtual DbSet<Vwdepartamentosordenado> Vwdepartamentosordenados { get; set; }
        public virtual DbSet<Vwlistadoentre500y100> Vwlistadoentre500y100s { get; set; }
        public virtual DbSet<Vwlistadomenoralpromedio> Vwlistadomenoralpromedios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=ferreteria;password=root;user=root;port=3307", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.5-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1");

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("departamento");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Sku)
                    .HasName("PRIMARY");

                entity.ToTable("producto");

                entity.HasIndex(e => e.IdSeccion, "FK_producto_1");

                entity.Property(e => e.Sku)
                    .HasColumnType("bigint(20) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.IdSeccion).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Precio).HasPrecision(10, 2);

                entity.HasOne(d => d.IdSeccionNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdSeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkProductoSeccion");
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.ToTable("seccion");

                entity.HasIndex(e => e.IdDepartamento, "fkSeccionDepto_idx");

                entity.HasIndex(e => e.IdSeccionPrincipal, "kfSeccionPrincipal_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdDepartamento).HasColumnType("int(10) unsigned");

                entity.Property(e => e.IdSeccionPrincipal).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Seccions)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("fkSeccionDepto");
            });

            modelBuilder.Entity<Vwdepartamentosordenado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwdepartamentosordenados");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Vwlistadoentre500y100>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwlistadoentre500y100");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("marca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sku)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("sku");
            });

            modelBuilder.Entity<Vwlistadomenoralpromedio>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwlistadomenoralpromedio");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.IdSeccion).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Precio).HasPrecision(10, 2);

                entity.Property(e => e.Sku).HasColumnType("bigint(20) unsigned");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
