using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class ProductionViewModel : INotifyPropertyChanged
    {
        private string _названиеПродукта;
        private string _этапыПроизводства;
        private string _ответственный;
        private DateTime _датаНачала;
        private DateTime _датаЗавершения;

        public string НазваниеПродукта
        {
            get => _названиеПродукта;
            set
            {
                _названиеПродукта = value;
                OnPropertyChanged(nameof(НазваниеПродукта));
            }
        }

        public string ЭтапыПроизводства
        {
            get => _этапыПроизводства;
            set
            {
                _этапыПроизводства = value;
                OnPropertyChanged(nameof(ЭтапыПроизводства));
            }
        }

        public string Ответственный
        {
            get => _ответственный;
            set
            {
                _ответственный = value;
                OnPropertyChanged(nameof(Ответственный));
            }
        }

        public DateTime ДатаНачала
        {
            get => _датаНачала;
            set
            {
                _датаНачала = value;
                OnPropertyChanged(nameof(ДатаНачала));
            }
        }

        public DateTime ДатаЗавершения
        {
            get => _датаЗавершения;
            set
            {
                _датаЗавершения = value;
                OnPropertyChanged(nameof(ДатаЗавершения));
            }
        }

        public ICommand SaveCommand { get; }

        public ProductionViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveProductionAsync);
            ДатаНачала = DateTime.Now;
            ДатаЗавершения = DateTime.Now;
        }

        private async Task SaveProductionAsync(object parameter)
        {
            string названиеПродукта = НазваниеПродукта;
            string этапыПроизводства = ЭтапыПроизводства;
            string ответственный = Ответственный;
            DateTime датаНачала = ДатаНачала;
            DateTime датаЗавершения = ДатаЗавершения;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            if (!await CheckExistenceInDatabaseAsync(названиеПродукта, этапыПроизводства, ответственный, connectionString))
            {
                MessageBox.Show("Запись не найдена в базе данных.");
                return;
            }

            string query = "INSERT INTO Производство (Название_продукта, Этапы_производства, Ответственный, Дата_начала, Дата_завершения) " +
                           "VALUES (@Название, @Этапы, @Ответственный, @Дата_начала, @Дата_завершения)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", названиеПродукта);
                    command.Parameters.AddWithValue("@Этапы", этапыПроизводства);
                    command.Parameters.AddWithValue("@Ответственный", ответственный);
                    command.Parameters.AddWithValue("@Дата_начала", датаНачала);
                    command.Parameters.AddWithValue("@Дата_завершения", датаЗавершения);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            НазваниеПродукта = string.Empty;
            ЭтапыПроизводства = string.Empty;
            Ответственный = string.Empty;
            ДатаНачала = DateTime.Now;
            ДатаЗавершения = DateTime.Now;
        }

        private async Task<bool> CheckExistenceInDatabaseAsync(string названиеПродукта, string этапыПроизводства, string ответственный, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Производство WHERE Название_продукта = @Название AND Этапы_производства = @Этапы AND Ответственный = @Ответственный";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", названиеПродукта);
                    command.Parameters.AddWithValue("@Этапы", этапыПроизводства);
                    command.Parameters.AddWithValue("@Ответственный", ответственный);

                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}