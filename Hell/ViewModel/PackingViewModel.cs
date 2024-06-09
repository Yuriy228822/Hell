using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class PackingViewModel : INotifyPropertyChanged
    {
        private string _productName;
        private int _quantity;
        private string _packagingType;
        private string _responsible;
        private DateTime _packingDate;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string PackagingType
        {
            get => _packagingType;
            set
            {
                _packagingType = value;
                OnPropertyChanged(nameof(PackagingType));
            }
        }

        public string Responsible
        {
            get => _responsible;
            set
            {
                _responsible = value;
                OnPropertyChanged(nameof(Responsible));
            }
        }

        public DateTime PackingDate
        {
            get => _packingDate;
            set
            {
                _packingDate = value;
                OnPropertyChanged(nameof(PackingDate));
            }
        }

        public ICommand SaveCommand { get; }

        public PackingViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePackingAsync);
            PackingDate = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SavePackingAsync(object parameter)
        {
            // Получаем данные из свойств модели представления
            string productName = ProductName;
            int quantity = Quantity;
            string packagingType = PackagingType;
            string responsible = Responsible;
            DateTime packingDate = PackingDate;

            // Строка подключения к базе данных
            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            // Проверка наличия типа упаковки и ответственного в базе данных
            if (!await CheckPackagingTypeExistsAsync(packagingType, connectionString))
            {
                MessageBox.Show("Указанный тип упаковки не существует.");
                return;
            }

            string[] responsibleNames = responsible.Split(' ');
            if (responsibleNames.Length != 2)
            {
                MessageBox.Show("Введите имя и фамилию ответственного через пробел.");
                return;
            }

            if (!await CheckResponsibleExistsAsync(responsibleNames[0], responsibleNames[1], connectionString))
            {
                MessageBox.Show("Указанный ответственный не существует.");
                return;
            }

            // SQL-запрос для вставки данных о фасовке в базу данных
            string query = "INSERT INTO Фасовка (Название_продукта, Количество, Тип_упаковки, Ответственный, Дата_фасовки) " +
                           "VALUES (@ProductName, @Quantity, @PackagingType, @Responsible, @PackingDate)";

            // Создаем подключение к базе данных и выполняем запрос асинхронно
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметры к запросу
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@PackagingType", packagingType);
                    command.Parameters.AddWithValue("@Responsible", responsible);
                    command.Parameters.AddWithValue("@PackingDate", packingDate);

                    // Выполняем запрос без получения результата
                    await command.ExecuteNonQueryAsync();
                }
            }

            // После сохранения данных выводим сообщение и очищаем свойства модели
            MessageBox.Show("Данные о фасовке сохранены!");
            Clear();
        }


        private async Task<bool> CheckPackagingTypeExistsAsync(string packagingType, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Упаковка WHERE Наименование = @PackagingType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PackagingType", packagingType);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        private async Task<bool> CheckResponsibleExistsAsync(string firstName, string lastName, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Люди WHERE Имя = @FirstName AND Фамилия = @LastName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }


        // Метод для очистки свойств модели
        private void Clear()
        {
            ProductName = string.Empty;
            Quantity = 0;
            PackagingType = string.Empty;
            Responsible = string.Empty;
            PackingDate = DateTime.Now;
        }

        // Реализация интерфейса INotifyPropertyChanged для уведомления об изменениях свойств
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
