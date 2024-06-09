using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        private string _имя;
        private string _фамилия;
        private string _должность;
        private string _контактныеДанные;
        private int _опытРаботы;

        public string Имя
        {
            get => _имя;
            set
            {
                _имя = value;
                OnPropertyChanged(nameof(Имя));
            }
        }

        public string Фамилия
        {
            get => _фамилия;
            set
            {
                _фамилия = value;
                OnPropertyChanged(nameof(Фамилия));
            }
        }

        public string Должность
        {
            get => _должность;
            set
            {
                _должность = value;
                OnPropertyChanged(nameof(Должность));
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

        public int ОпытРаботы
        {
            get => _опытРаботы;
            set
            {
                _опытРаботы = value;
                OnPropertyChanged(nameof(ОпытРаботы));
            }
        }

        public ICommand SaveCommand { get; }

        public PeopleViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePersonAsync);
        }

        private async Task SavePersonAsync(object parameter)
        {
            string имя = Имя;
            string фамилия = Фамилия;
            string должность = Должность;
            string контактныеДанные = КонтактныеДанные;
            int опытРаботы = ОпытРаботы;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            if (!await CheckPositionExistsAsync(должность, connectionString))
            {
                MessageBox.Show("Должность не найдена в базе данных.");
                return;
            }

            string query = "INSERT INTO Люди (Имя, Фамилия, Должность, Контактные_данные, Опыт_работы) " +
                           "VALUES (@Имя, @Фамилия, @Должность, @КонтактныеДанные, @ОпытРаботы)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Имя", имя);
                    command.Parameters.AddWithValue("@Фамилия", фамилия);
                    command.Parameters.AddWithValue("@Должность", должность);
                    command.Parameters.AddWithValue("@КонтактныеДанные", контактныеДанные);
                    command.Parameters.AddWithValue("@ОпытРаботы", опытРаботы);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private async Task<bool> CheckPositionExistsAsync(string position, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Должность WHERE Название = @Position";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Position", position);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        private void Clear()
        {
            Имя = string.Empty;
            Фамилия = string.Empty;
            Должность = string.Empty;
            КонтактныеДанные = string.Empty;
            ОпытРаботы = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
