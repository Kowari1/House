using House.Model;
using House.ViewModels.Models;

namespace House.Data.Repositories
{
    /// <summary>
    /// Интерфейс для работы с сущностью "Оборудования".
    /// </summary>
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        /// <summary>
        /// Получение оборудования в зависимости от статуса.
        /// </summary>
        /// <param name="status">Статус оборудования.</param>
        /// <returns></returns>
        Task<List<Equipment>> GetByStatusAsync(EquipmentStatus status);

        /// <summary>
        /// Используется для апдейта отслеживаемой модели EF core.
        /// </summary>
        /// <param name="viewModel">Модель из которой будут переносится изменения.</param>
        /// <returns></returns>
        Task UpdateViewModelAsync(EquipmentViewModel viewModel);
    }
}
