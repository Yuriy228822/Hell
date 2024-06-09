using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class SuppliesViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _контактныеДанные;
        private string _репутация;
        private string _условияПоставки;
        private string _договор;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
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

        public string Репутация
        {
            get => _репутация;
            set
            {
                _репутация = value;
                OnPropertyChanged(nameof(Репутация));
            }
        }

        public string УсловияПоставки
        {
            get => _условияПоставки;
            set
            {
                _условияПоставки = value;
                OnPropertyChanged(nameof(УсловияПоставки));
            }
        }

        public string Договор
        {
            get => _договор;
            set
            {
                _договор = value;
                OnPropertyChanged(nameof(Договор));
            }
        }

        public ICommand SaveCommand { get; }

        public SuppliesViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveSupplierAsync);
        }

        private async Task SaveSupplierAsync(object parameter)
        {
            string название = Название;
            string контактныеДанные = КонтактныеДанные;
            string репутация = Репутация;
            string условияПоставки = УсловияПоставки;
            string договор = Договор;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Поставщики (Название, Контактные_данные, Репутация, Условия_поставки, Договор) " +
                           "VALUES (@Название, @КонтактныеДанные, @Репутация, @УсловияПоставки, @Договор)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@КонтактныеДанные", контактныеДанные);
                    command.Parameters.AddWithValue("@Репутация", репутация);
                    command.Parameters.AddWithValue("@УсловияПоставки", условияПоставки);
                    command.Parameters.AddWithValue("@Договор", договор);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            КонтактныеДанные = string.Empty;
            Репутация = string.Empty;
            УсловияПоставки = string.Empty;
            Договор = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
