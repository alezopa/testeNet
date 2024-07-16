using TesteNET.Models;

namespace TesteNET.Repositories
{
    public class UsuariosRepository : GenericRepository<Usuario>, IUsuariosRepository
    {
        public UsuariosRepository(AppDbContext repositoryContext)
             : base(repositoryContext)
        {
        }
    }
}
