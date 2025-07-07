using House.Model;
using House.ViewModels.Models;

namespace House.Mappers
{
    public static class EquipmentMapper
    {
        public static void MapToEntity(EquipmentViewModel viewModel, Equipment entity)
        {
            entity.Name = viewModel.Name;
            entity.Type = viewModel.Type;
            entity.Status = viewModel.Status;
        }
    }
}
