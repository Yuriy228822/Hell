using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class FinishedProductionViewModel : INotifyPropertyChanged
    {
        private string _названиеПродукта;
        private int _количество;
        private string _качество;
        private DateTime _датаПроизводства;
        private int _срокГодности;

        public string НазваниеПродукта
        {
            get => _названиеПродукта;
            set
            {
                _названиеПродукта = value;
                OnPropertyChanged(nameof(НазваниеПродукта));
            }
        }

        public int Количество
        {
            get => _количество;
            set
            {
                _количество = value;
                OnPropertyChanged(nameof(Количество));
            }
        }

        public string Качество
        {
            get => _качество;
            set
            {
                _качество = value;
                OnPropertyChanged(nameof(Качество));
            }
        }

        public DateTime ДатаПроизводства
        {
            get => _датаПроизводства;
            set
            {
                _датаПроизводства = value;
                OnPropertyChanged(nameof(ДатаПроизводства));
            }
        }

        public int СрокГодности
        {
            get => _срокГодности;
            set
            {
                _срокГодности = value;
                OnPropertyChanged(nameof(СрокГодности));
            }
        }

        public ICommand SaveCommand { get; }

        public FinishedProductionViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveFinishedProductionAsync);
            ДатаПроизводства = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveFinishedProductionAsync(object parameter)
        {
            string названиеПродукта = НазваниеПродукта;
            int количество = Количество;
            string качество = Качество;
            DateTime датаПроизводства = ДатаПроизводства;
            int срокГодности = СрокГодности;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Готовая_продукция (Название_продукта, Количество, Качество, Дата_производства, Срок_годности) " +
                           "VALUES (@НазваниеПродукта, @Количество, @Качество, @ДатаПроизводства, @СрокГодности)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@НазваниеПродукта", названиеПродукта);
                    command.Parameters.AddWithValue("@Количество", количество);
                    command.Parameters.AddWithValue("@Качество", качество);
                    command.Parameters.AddWithValue("@ДатаПроизводства", датаПроизводства);
                    command.Parameters.AddWithValue("@СрокГодности", срокГодности);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            НазваниеПродукта = string.Empty;
            Количество = 0;
            Качество = string.Empty;
            ДатаПроизводства = DateTime.Now;
            СрокГодности = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
