using Microsoft.EntityFrameworkCore;
using WebApiCatalogoMinimal.Models;

namespace WebApiCatalogoMinimal.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Categoria>().HasKey(c => c.CategoriaId);

            mb.Entity<Categoria>().Property(c => c.Nome)
                                  .HasMaxLength(100)
                                  .IsRequired();
            mb.Entity<Categoria>().Property(c => c.Descricao)
                                  .HasMaxLength(100)
                                  .IsRequired();

            mb.Entity<Produto>().HasKey(p => p.ProdutoId);
            mb.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(150);
            mb.Entity<Produto>().Property(p => p.Imagem).HasMaxLength(100);
            mb.Entity<Produto>().Property(p => p.Preco).HasPrecision(14, 2);

            mb.Entity<Produto>()
                .HasOne<Categoria>(c => c.Categoria)
                .WithMany(p => p.Produtos)
                .HasForeignKey(c => c.CategoriaId);
        }
    }
}
