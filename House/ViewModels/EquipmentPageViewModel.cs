using House.Data.Repositories;
using House.Infrastructure;
using House.Model;
using House.ViewModels.Base;
using House.ViewModels.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace House.ViewModels
{
    /// <summary>
    /// ViewModel для управления списком оборудования.
    /// </summary>
    public class EquipmentPageViewModel : ViewModel
    {
        private readonly IEquipmentRepository _equipmentRepository;

        /// <summary>
        /// Коллекция оборудования, отображаемая в UI.
        /// </summary>
        public ObservableCollection<EquipmentViewModel> Equipments { get; } = new();

        private EquipmentViewModel selectedEquipment;
        private EquipmentViewModel previousEquipment;

        /// <summary>
        /// Текущее выбранное оборудование.
        /// </summary>
        public EquipmentViewModel SelectedEquipment
        {
            get => selectedEquipment;
            set
            {
                if (selectedEquipment != null && selectedEquipment.IsDirty)
                {
                    bool confirm = AskSaveChanges(selectedEquipment);

                    if (confirm)
                    {
                        previousEquipment = selectedEquipment.Clone();

                        _ = UpdateAsync();
                        selectedEquipment.IsDirty = false;
                    }
                    else
                    {
                        selectedEquipment.CopyFrom(previousEquipment);
                        selectedEquipment.IsDirty = false;
                        return;
                    }
                }

                if (Set(ref selectedEquipment, value))
                {
                    previousEquipment = value?.Clone();
                }
            }
        }

        #region Команды

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand UpdateCommand { get; }

        #endregion

        /// <summary>
        /// Конструктор с внедрением репозитория оборудования.
        /// </summary>
        public EquipmentPageViewModel(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;

            AddCommand = new LambdaCommand(_ => Add());
            DeleteCommand = new LambdaCommand(async _ => await DeleteAsync(), _ => SelectedEquipment != null);
            SaveCommand = new LambdaCommand(async _ => await SaveAsync(), _ => Equipments.Any());
            UpdateCommand = new LambdaCommand(async _ => await UpdateAsync(), _ => SelectedEquipment != null);
            LoadAsync();
        }

        /// <summary>
        /// Загружает оборудование из базы данных.
        /// </summary>
        private async Task LoadAsync()
        {
            Equipments.Clear();

            var list = await _equipmentRepository.GetAllAsync();

            foreach (var eq in list)
                Equipments.Add(new EquipmentViewModel(eq));
        }

        /// <summary>
        /// Добавляет новое оборудование.
        /// </summary>
        private void Add()
        {
            var newItem = new EquipmentViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Новое оборудование",
                Type = EquipmentType.Принтер,
                Status = EquipmentStatus.НаСкладе
            };

            Equipments.Add(newItem);
            SelectedEquipment = newItem;
        }

        /// <summary>
        /// Удаляет выбранное оборудование.
        /// </summary>
        private async Task DeleteAsync()
        {
            if (SelectedEquipment == null)
            {
                return;
            }

            await _equipmentRepository.DeleteAsync(SelectedEquipment.Id);

            Equipments.Remove(SelectedEquipment);

            SelectedEquipment = null;
        }

        /// <summary>
        /// Сохраняет изменения в БД.
        /// </summary>
        public async Task SaveAsync()
        {
            foreach (var eq in Equipments)
            {
                var exists = await _equipmentRepository.GetByIdAsync(eq.Id);

                if (exists != null)
                {
                    await _equipmentRepository.UpdateViewModelAsync(eq);
                }  
                else
                {
                    await _equipmentRepository.AddAsync(eq.ToModel());
                }
            }

            await LoadAsync();
        }

        /// <summary>
        /// Уведомляет о том сохранить измененное оборудования при переключении
        /// на другое, если не было предварительного сохранения изменений.
        /// </summary>
        /// <param name="eq"></param>
        /// <returns></returns>
        private bool AskSaveChanges(EquipmentViewModel eq)
        {
            var result = MessageBox.Show(
                "Вы внесли изменения в текущее оборудование. Сохранить перед переключением?",
                "Несохранённые изменения",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            return result == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Изменяет оборудование.
        /// </summary>
        private async Task UpdateAsync()
        {
            if (SelectedEquipment == null)
            {
                return;
            }

            SelectedEquipment.IsDirty = false;

            var exists = await _equipmentRepository.GetByIdAsync(selectedEquipment.Id);

            if (exists != null)
            {
                await _equipmentRepository.UpdateViewModelAsync(selectedEquipment);
            }
            else
            {
                await _equipmentRepository.AddAsync(selectedEquipment.ToModel());
            }
        }
    }
}
