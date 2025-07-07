using House.Data.Repositories;
using House.Mappers;
using House.Model;
using House.ViewModels.Models;
using Microsoft.EntityFrameworkCore;

namespace House.Data
{
    /// <summary>
    /// Реализация интерфейса для работы с сущностью "Оборудования".
    /// </summary>
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        private AppDbContext _db;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="db">Контекст бд. для работы с сущностью</param>
        public EquipmentRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение оборудования в зависимости от статуса.
        /// </summary>
        /// <param name="status">Статус оборудования.</param>
        /// <returns>Список оборудования с определенный статусом.</returns>
        public async Task<List<Equipment>> GetByStatusAsync(EquipmentStatus status)
        {
            return await _db.Equipments.Where(e => e.Status == status).ToListAsync();
        }

        /// <summary>
        /// Используется для апдейта отслеживаемой модели EF core.
        /// </summary>
        /// <param name="id">Id модели для апдейта.</param>
        /// <param name="viewModel">Модель из которой будут переносится изменения.</param>
        public async Task UpdateViewModelAsync(EquipmentViewModel viewModel)
        {
            var entity = await _dbSet.FindAsync(viewModel.Id);

            if (entity != null)
            {
                EquipmentMapper.MapToEntity(viewModel, entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
