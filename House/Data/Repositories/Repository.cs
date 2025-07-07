using Microsoft.EntityFrameworkCore;

namespace House.Data.Repositories
{
    /// <summary>
    /// Реализация интерфейса для работы с сущностями в бд.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        protected DbSet<T> _dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbSet.Remove(entity);

            await _db.SaveChangesAsync();
        }

        public async Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> values = _dbSet;

            return await values.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> values = _dbSet;

            return await values.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
