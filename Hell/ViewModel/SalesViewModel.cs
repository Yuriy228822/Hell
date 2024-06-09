using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class SalesViewModel : INotifyPropertyChanged
    {
        private string _продукт;
        private int _количество;
        private decimal _цена;
        private DateTime _дата;
        private string _покупатель;
        private int _магазинId;
        private int _чекId;

        public string Продукт
        {
            get => _продукт;
            set
            {
                _продукт = value;
                OnPropertyChanged(nameof(Продукт));
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

        public decimal Цена
        {
            get => _цена;
            set
            {
                _цена = value;
                OnPropertyChanged(nameof(Цена));
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

        public string Покупатель
        {
            get => _покупатель;
            set
            {
                _покупатель = value;
                OnPropertyChanged(nameof(Покупатель));
            }
        }

        public int МагазинId
        {
            get => _магазинId;
            set
            {
                _магазинId = value;
                OnPropertyChanged(nameof(МагазинId));
            }
        }

        public int ЧекId
        {
            get => _чекId;
            set
            {
                _чекId = value;
                OnPropertyChanged(nameof(ЧекId));
            }
        }

        public ICommand SaveCommand { get; }

        public SalesViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveSalesAsync);
            Дата = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveSalesAsync(object parameter)
        {
            string продукт = Продукт;
            int количество = Количество;
            decimal цена = Цена;
            DateTime дата = Дата;
            string покупатель = Покупатель;
            int магазинId = МагазинId;
            int чекId = ЧекId;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            if (!await CheckShopExistsAsync(магазинId, connectionString))
            {
                MessageBox.Show("Магазин с указанным ID не существует.");
                return;
            }

            if (!await CheckReceiptExistsAsync(чекId, connectionString))
            {
                MessageBox.Show("Чек с указанным ID не существует.");
                return;
            }

            string query = "INSERT INTO Продажи (Продукт, Количество, Цена, Дата, Покупатель, МагазинId, ЧекId) " +
                           "VALUES (@Продукт, @Количество, @Цена, @Дата, @Покупатель, @МагазинId, @ЧекId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Продукт", продукт);
                    command.Parameters.AddWithValue("@Количество", количество);
                    command.Parameters.AddWithValue("@Цена", цена);
                    command.Parameters.AddWithValue("@Дата", дата);
                    command.Parameters.AddWithValue("@Покупатель", покупатель);
                    command.Parameters.AddWithValue("@МагазинId", магазинId);
                    command.Parameters.AddWithValue("@ЧекId", чекId);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Продукт = string.Empty;
            Количество = 0;
            Цена = 0;
            Дата = DateTime.Now;
            Покупатель = string.Empty;
            МагазинId = 0;
            ЧекId = 0;
        }

        private async Task<bool> CheckShopExistsAsync(int shopId, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Магазин WHERE ID = @ShopId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ShopId", shopId);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        private async Task<bool> CheckReceiptExistsAsync(int receiptId, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Чеки WHERE ID = @ReceiptId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiptId", receiptId);
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

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object> _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<object, Task> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async void Execute(object parameter)
        {
            _isExecuting = true;
            try
            {
                await _execute(parameter);
            }
            finally
            {
                _isExecuting = false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
