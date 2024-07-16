using TesteNET.Models;

namespace TesteNET.Repositories
{
    public class PedidosRepository : GenericRepository<Pedidos>, IPedidosRepository
    {
        public PedidosRepository(AppDbContext repositoryContext)
             : base(repositoryContext)
        {
        }
    }
}
