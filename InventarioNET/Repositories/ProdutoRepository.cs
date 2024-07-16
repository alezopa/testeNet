using TesteNET.Models;

namespace TesteNET.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext repositoryContext)
             : base(repositoryContext)
        {
        }
    }
}
