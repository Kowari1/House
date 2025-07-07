using House.Model;
using House.ViewModels.Base;

namespace House.ViewModels.Models
{
    /// <summary>
    /// ViewModel для отображения и редактирования сущности "Equipment" в UI.
    /// Содержит свойства с поддержкой уведомлений об изменениях для привязки к WPF.
    /// </summary>
    public class EquipmentViewModel : BindableViewModel<Equipment>
    {
        private Guid id;
        private string name;
        private EquipmentType type;
        private EquipmentStatus status;

        /// <summary>
        /// Идентификатор оборудования.
        /// </summary>
        public Guid Id
        {
            get => id;
            set => Set(ref id, value);
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name
        {
            get => name;
            set {
                if (Set(ref name, value) && !IsLoading)
                {
                    IsDirty = true;
                }
            }
        }

        /// <summary>
        /// Тип.
        /// </summary>
        public EquipmentType Type
        {
            get => type;
            set {
                if (Set(ref type, value) && !IsLoading)
                {
                    IsDirty = true;
                }
            }
        }

        
        /// <summary>
        /// Статус.
        /// </summary>
        public EquipmentStatus Status
        {
            get => status;
            set
            {
                if (Set(ref status, value) && !IsLoading)
                {
                    IsDirty = true;
                }
            }
        }

        public override void LoadFromModel(Equipment model)
        {
            IsLoading = true;

            Id = model.Id;
            Name = model.Name;
            Type = model.Type;
            Status = model.Status;

            IsLoading = false;
        }

        private bool isDirty;

        /// <summary>
        /// Хранит состояние того было изменено оборудование или нет.
        /// </summary>
        public bool IsDirty
        {
            get => isDirty;
            set
            {
                Set(ref isDirty, value);
                OnPropertyChanged(nameof(IsDirty));
            }
        }

        private bool isLoading;

        /// <summary>
        /// Состояние загрузки модели.
        /// </summary>
        public bool IsLoading
        {
            get => isLoading;
            set => Set(ref isLoading, value);
        }

        /// <summary>
        /// Загружает данные из модели "Equipment" во ViewModel.
        /// </summary>
        /// <param name="model">Модель оборудования из базы данных.</param>
        public override Equipment ToModel()
        {
            return new Equipment
            {
                Id = this.Id,
                Name = this.Name,
                Type = this.Type,
                Status = this.Status
            };
        }

        /// <summary>
        /// Используется для копирования класса с другой ссылкой.
        /// </summary>
        /// <returns></returns>
        public EquipmentViewModel Clone()
        {
            return new EquipmentViewModel
            {
                Id = this.Id,
                Name = this.Name,
                Type = this.Type,
                Status = this.Status,
                IsDirty = false
            };
        }

        /// <summary>
        /// Копирует поля для изменению в UI, предназначен для отката к предыдущей версии.
        /// </summary>
        /// <param name="source">Версия модели для отката.</param>
        public void CopyFrom(EquipmentViewModel source)
        {
            IsLoading = true;

            Id = source.Id;
            Name = source.Name;
            Type = source.Type;
            Status = source.Status;

            IsDirty = false;
            IsLoading = false;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Status));
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public EquipmentViewModel() { }

        /// <summary>
        /// Конструктор, инициализирующий ViewModel из модели "Equipment".
        /// </summary>
        /// <param name="model">Сущность "Equipment", которую необходимо обернуть.</param>
        public EquipmentViewModel(Equipment model)
        {
            LoadFromModel(model);
        }
    }
}
