using System.Linq.Expressions;

namespace House.Data.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с сущностями.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получает все сущности.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получает сущность по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает множество сущностей по фильтру.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Получает сущность по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Изменение сущности.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Удаление сущности.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
