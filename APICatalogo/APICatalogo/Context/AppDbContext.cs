using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class AppDbContext : DbContext
    {
        //representa uma seção com o banco de dados

        //define um contexto, que depois registra como serviço
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //mapea as entidades
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
