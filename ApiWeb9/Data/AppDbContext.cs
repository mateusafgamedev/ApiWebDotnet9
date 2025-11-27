using ApiWeb9.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb9.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}
