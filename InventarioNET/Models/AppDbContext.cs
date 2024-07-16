using Microsoft.EntityFrameworkCore;

namespace TesteNET.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Pedidos> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.PedidoId);

                entity.Property(e => e.NumPedido);

                entity.Property(e => e.Descricao).HasMaxLength(150);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EnderecoEntrega)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Numero);


                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50);
            });

                OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
