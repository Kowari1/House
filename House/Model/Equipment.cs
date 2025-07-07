
namespace House.Model
{
    /// <summary>
    /// Оборудование.
    /// </summary>
    public class Equipment
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Equipment() { }

        /// <summary>
        /// Конструктор c параметрами.
        /// </summary>
        /// <param name="name">Наименование.</param>
        /// <param name="type">Тип.</param>
        /// <param name="status">Статус.</param>
        public Equipment(string name, EquipmentType type, EquipmentStatus status)
        {
            Name = name;
            Type = type;
            Status = status;
        }

        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public EquipmentType Type { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public EquipmentStatus Status { get; set; }
    }

    public static class EnumHelper
    {
        /// <summary>
        /// Получение всех типов оборудования.
        /// </summary>
        public static Array EquipmentTypeValues => Enum.GetValues(typeof(EquipmentType));

        /// <summary>
        /// Получение всех статусов оборудования.
        /// </summary>
        public static Array EquipmentStatusValues => Enum.GetValues(typeof(EquipmentStatus));
    }

    #region Enums
    /// <summary>
    /// Типы оборудования.
    /// </summary>
    public enum EquipmentType
    {
        Принтер,
        Сканер,
        Монитор
    }

    /// <summary>
    /// Статусы оборудования.
    /// </summary>
    public enum EquipmentStatus
    {
        ВПользовании,
        НаСкладе,
        НаРемонте
    }
    #endregion
}
