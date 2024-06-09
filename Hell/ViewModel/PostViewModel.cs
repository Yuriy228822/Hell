using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class PostViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _описание;
        private string _обязанности;
        private string _требования;
        private decimal _заработнаяПлата;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Описание
        {
            get => _описание;
            set
            {
                _описание = value;
                OnPropertyChanged(nameof(Описание));
            }
        }

        public string Обязанности
        {
            get => _обязанности;
            set
            {
                _обязанности = value;
                OnPropertyChanged(nameof(Обязанности));
            }
        }

        public string Требования
        {
            get => _требования;
            set
            {
                _требования = value;
                OnPropertyChanged(nameof(Требования));
            }
        }

        public decimal ЗаработнаяПлата
        {
            get => _заработнаяПлата;
            set
            {
                _заработнаяПлата = value;
                OnPropertyChanged(nameof(ЗаработнаяПлата));
            }
        }

        public ICommand SaveCommand { get; }

        public PostViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePostAsync);
        }

        private async Task SavePostAsync(object parameter)
        {
            string название = Название;
            string описание = Описание;
            string обязанности = Обязанности;
            string требования = Требования;
            decimal заработнаяПлата = ЗаработнаяПлата;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Должность (Название, Описание, Обязанности, Требования, Заработная_плата) " +
                           "VALUES (@Название, @Описание, @Обязанности, @Требования, @ЗаработнаяПлата)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Описание", описание);
                    command.Parameters.AddWithValue("@Обязанности", обязанности);
                    command.Parameters.AddWithValue("@Требования", требования);
                    command.Parameters.AddWithValue("@ЗаработнаяПлата", заработнаяПлата);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Должность сохранена!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Описание = string.Empty;
            Обязанности = string.Empty;
            Требования = string.Empty;
            ЗаработнаяПлата = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
