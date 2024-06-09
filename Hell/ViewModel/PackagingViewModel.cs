using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class PackagingViewModel : INotifyPropertyChanged
    {
        private string _наименование;
        private string _материал;
        private string _структура;
        private string _уровеньУпаковки;

        public string Наименование
        {
            get => _наименование;
            set
            {
                _наименование = value;
                OnPropertyChanged(nameof(Наименование));
            }
        }

        public string Материал
        {
            get => _материал;
            set
            {
                _материал = value;
                OnPropertyChanged(nameof(Материал));
            }
        }

        public string Структура
        {
            get => _структура;
            set
            {
                _структура = value;
                OnPropertyChanged(nameof(Структура));
            }
        }

        public string УровеньУпаковки
        {
            get => _уровеньУпаковки;
            set
            {
                _уровеньУпаковки = value;
                OnPropertyChanged(nameof(УровеньУпаковки));
            }
        }

        public ICommand SaveCommand { get; }

        public PackagingViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePackagingAsync);
        }

        private async Task SavePackagingAsync(object parameter)
        {
            string наименование = Наименование;
            string материал = Материал;
            string структура = Структура;
            string уровеньУпаковки = УровеньУпаковки;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Упаковка (Наименование, Материал, Структура, Уровень_упаковки) " +
                           "VALUES (@Наименование, @Материал, @Структура, @УровеньУпаковки)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Наименование", наименование);
                    command.Parameters.AddWithValue("@Материал", материал);
                    command.Parameters.AddWithValue("@Структура", структура);
                    command.Parameters.AddWithValue("@УровеньУпаковки", уровеньУпаковки);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Наименование = string.Empty;
            Материал = string.Empty;
            Структура = string.Empty;
            УровеньУпаковки = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
