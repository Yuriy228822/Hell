using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace Hell.ViewModel
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _тип;
        private string _производитель;
        private DateTime _датаПокупки;
        private string _состояние;
        
        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Тип
        {
            get => _тип;
            set
            {
                _тип = value;
                OnPropertyChanged(nameof(Тип));
            }
        }

        public string Производитель
        {
            get => _производитель;
            set
            {
                _производитель = value;
                OnPropertyChanged(nameof(Производитель));
            }
        }

        public DateTime ДатаПокупки
        {
            get => _датаПокупки;
            set
            {
                _датаПокупки = value;
                OnPropertyChanged(nameof(ДатаПокупки));
            }
        }

        public string Состояние
        {
            get => _состояние;
            set
            {
                _состояние = value;
                OnPropertyChanged(nameof(Состояние));
            }
        }

        public ICommand SaveCommand { get; }

        public EquipmentViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveEquipmentAsync);
            ДатаПокупки = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveEquipmentAsync(object parameter)
        {
            string название = Название;
            string тип = Тип;
            string производитель = Производитель;
            DateTime датаПокупки = ДатаПокупки;
            string состояние = Состояние;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Оборудование (Название, Тип, Производитель, Дата_покупки, Состояние) " +
                           "VALUES (@Название, @Тип, @Производитель, @ДатаПокупки, @Состояние)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Тип", тип);
                    command.Parameters.AddWithValue("@Производитель", производитель);
                    command.Parameters.AddWithValue("@ДатаПокупки", датаПокупки);
                    command.Parameters.AddWithValue("@Состояние", состояние);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
           ClearFields();
        }

        private void ClearFields()
        {
            Название = string.Empty;
            Тип = string.Empty;
            Производитель = string.Empty;
            ДатаПокупки = DateTime.Now;
            Состояние = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}
