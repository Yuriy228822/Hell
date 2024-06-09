using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class ReceiptsViewModel : INotifyPropertyChanged
    {
        private int _номерЧека;
        private DateTime _дата;
        private decimal _сумма;
        private string _продукты;
        private string _покупатель;
        private string _кассир;
        private string _магазин;

        public int НомерЧека
        {
            get => _номерЧека;
            set
            {
                _номерЧека = value;
                OnPropertyChanged(nameof(НомерЧека));
            }
        }

        public DateTime Дата
        {
            get => _дата;
            set
            {
                _дата = value;
                OnPropertyChanged(nameof(Дата));
            }
        }

        public decimal Сумма
        {
            get => _сумма;
            set
            {
                _сумма = value;
                OnPropertyChanged(nameof(Сумма));
            }
        }

        public string Продукты
        {
            get => _продукты;
            set
            {
                _продукты = value;
                OnPropertyChanged(nameof(Продукты));
            }
        }

        public string Покупатель
        {
            get => _покупатель;
            set
            {
                _покупатель = value;
                OnPropertyChanged(nameof(Покупатель));
            }
        }

        public string Кассир
        {
            get => _кассир;
            set
            {
                _кассир = value;
                OnPropertyChanged(nameof(Кассир));
            }
        }

        public string Магазин
        {
            get => _магазин;
            set
            {
                _магазин = value;
                OnPropertyChanged(nameof(Магазин));
            }
        }

        public ICommand SaveCommand { get; }

        public ReceiptsViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveReceiptAsync);
            Дата = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveReceiptAsync(object parameter)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            if (!await CheckCashierExistsAsync(_кассир, connectionString))
            {
                MessageBox.Show("Кассир с указанным именем не существует.");
                return;
            }

            if (!await CheckStoreExistsAsync(_магазин, connectionString))
            {
                MessageBox.Show("Магазин с указанным названием не существует.");
                return;
            }

            string query = "INSERT INTO Чеки (Номер_чека, Дата, Сумма, Продукты, Покупатель, Кассир, Магазин) " +
                           "VALUES (@НомерЧека, @Дата, @Сумма, @Продукты, @Покупатель, @Кассир, @Магазин)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@НомерЧека", _номерЧека);
                    command.Parameters.AddWithValue("@Дата", _дата);
                    command.Parameters.AddWithValue("@Сумма", _сумма);
                    command.Parameters.AddWithValue("@Продукты", _продукты);
                    command.Parameters.AddWithValue("@Покупатель", _покупатель);
                    command.Parameters.AddWithValue("@Кассир", _кассир);
                    command.Parameters.AddWithValue("@Магазин", _магазин);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            НомерЧека = 0;
            Дата = DateTime.Now;
            Сумма = 0;
            Продукты = string.Empty;
            Покупатель = string.Empty;
            Кассир = string.Empty;
            Магазин = string.Empty;
        }

        private async Task<bool> CheckCashierExistsAsync(string cashierName, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Люди WHERE Имя = @CashierName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CashierName", cashierName);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        private async Task<bool> CheckStoreExistsAsync(string storeName, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Магазин WHERE Название = @StoreName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StoreName", storeName);
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
