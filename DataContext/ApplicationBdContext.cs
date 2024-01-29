using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<ProdutoModel> Produtos { get; set; }

    }

}


