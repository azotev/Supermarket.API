using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly sql_storeContext _context;

        public BaseRepository(sql_storeContext context)
        {
            _context = context;
        }
    }
}
