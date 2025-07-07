using House.Model;
using System.Globalization;
using System.Windows.Data;

namespace House.Converters
{
    /// <summary>
    /// Преобразует EquipmentStatus в строку для отображения в UI.
    /// </summary>
    public class EquipmentStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EquipmentStatus status)
            {
                return status switch
                {
                    EquipmentStatus.ВПользовании => "В пользовании",
                    EquipmentStatus.НаСкладе => "На складе",
                    EquipmentStatus.НаРемонте => "На ремонте",
                    _ => status.ToString()
                };
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                "В пользовании" => EquipmentStatus.ВПользовании,
                "На складе" => EquipmentStatus.НаСкладе,
                "На ремонте" => EquipmentStatus.НаРемонте,
                _ => Binding.DoNothing
            };
        }
    }
}