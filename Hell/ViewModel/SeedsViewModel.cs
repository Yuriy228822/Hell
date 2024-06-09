using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class SeedsViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _сорт;
        private string _производитель;
        private DateTime _датаПокупки;
        private int _срокГодности;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Сорт
        {
            get => _сорт;
            set
            {
                _сорт = value;
                OnPropertyChanged(nameof(Сорт));
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

        public SeedsViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveSeedAsync);
            ДатаПокупки = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveSeedAsync(object parameter)
        {
            string название = Название;
            string сорт = Сорт;
            string производитель = Производитель;
            DateTime датаПокупки = ДатаПокупки;
            int срокГодности = СрокГодности;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Семена (Название, Сорт, Производитель, Дата_покупки, Срок_годности) " +
                           "VALUES (@Название, @Сорт, @Производитель, @ДатаПокупки, @СрокГодности)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Сорт", сорт);
                    command.Parameters.AddWithValue("@Производитель", производитель);
                    command.Parameters.AddWithValue("@ДатаПокупки", датаПокупки);
                    command.Parameters.AddWithValue("@СрокГодности", срокГодности);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Сорт = string.Empty;
            Производитель = string.Empty;
            ДатаПокупки = DateTime.Now;
            СрокГодности = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
