using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _адрес;
        private string _контактныеДанные;
        private string _времяРаботы;
        private string _запасы;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Адрес
        {
            get => _адрес;
            set
            {
                _адрес = value;
                OnPropertyChanged(nameof(Адрес));
            }
        }

        public string КонтактныеДанные
        {
            get => _контактныеДанные;
            set
            {
                _контактныеДанные = value;
                OnPropertyChanged(nameof(КонтактныеДанные));
            }
        }

        public string ВремяРаботы
        {
            get => _времяРаботы;
            set
            {
                _времяРаботы = value;
                OnPropertyChanged(nameof(ВремяРаботы));
            }
        }

        public string Запасы
        {
            get => _запасы;
            set
            {
                _запасы = value;
                OnPropertyChanged(nameof(Запасы));
            }
        }

        public ICommand SaveCommand { get; }

        public ShopViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveShopAsync);
        }

        private async Task SaveShopAsync(object parameter)
        {
            string название = Название;
            string адрес = Адрес;
            string контактныеДанные = КонтактныеДанные;
            string времяРаботы = ВремяРаботы;
            string запасы = Запасы;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Магазин (Название, Адрес, Контактные_данные, Время_работы, Запасы) " +
                           "VALUES (@Название, @Адрес, @КонтактныеДанные, @ВремяРаботы, @Запасы)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Адрес", адрес);
                    command.Parameters.AddWithValue("@КонтактныеДанные", контактныеДанные);
                    command.Parameters.AddWithValue("@ВремяРаботы", времяРаботы);
                    command.Parameters.AddWithValue("@Запасы", запасы);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Адрес = string.Empty;
            КонтактныеДанные = string.Empty;
            ВремяРаботы = string.Empty;
            Запасы = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
