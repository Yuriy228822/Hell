using GalaSoft.MvvmLight;
using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class FieldActivitiesViewModel : ViewModelBase
    {
        private DateTime _дата;
        private string _типМероприятия;
        private string _имяОтветственного;
        private string _фамилияОтветственного;
        private int _продолжительность;
        private string _оборудование;

        public DateTime Дата
        {
            get => _дата;
            set
            {
                _дата = value;
                OnPropertyChanged(nameof(Дата));
            }
        }

        public string ТипМероприятия
        {
            get => _типМероприятия;
            set
            {
                _типМероприятия = value;
                OnPropertyChanged(nameof(ТипМероприятия));
            }
        }

        public string ИмяОтветственного
        {
            get => _имяОтветственного;
            set
            {
                _имяОтветственного = value;
                OnPropertyChanged(nameof(ИмяОтветственного));
            }
        }

        public string ФамилияОтветственного
        {
            get => _фамилияОтветственного;
            set
            {
                _фамилияОтветственного = value;
                OnPropertyChanged(nameof(ФамилияОтветственного));
            }
        }

        public int Продолжительность
        {
            get => _продолжительность;
            set
            {
                _продолжительность = value;
                OnPropertyChanged(nameof(Продолжительность));
            }
        }

        public string Оборудование
        {
            get => _оборудование;
            set
            {
                _оборудование = value;
                OnPropertyChanged(nameof(Оборудование));
            }
        }

        public ICommand SaveCommand { get; }

        public FieldActivitiesViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveFieldActivityAsync);
            Дата = DateTime.Now; // Установка текущей даты по умолчанию
        }

        private async Task SaveFieldActivityAsync(object parameter)
        {
            string имяОтветственного = ИмяОтветственного;
            string фамилияОтветственного = ФамилияОтветственного;
            string оборудование = Оборудование;

            // Проверяем наличие ответственного в таблице Люди
            if (!await IsPersonExists(имяОтветственного, фамилияОтветственного))
            {
                MessageBox.Show("Ответственный не найден в базе данных 'Люди'!");
                return;
            }

            // Проверяем наличие оборудования в таблице Оборудование
            if (!await IsEquipmentExists(оборудование))
            {
                MessageBox.Show("Оборудование не найдено в базе данных 'Оборудование'!");
                return;
            }

            // Сохраняем данные о полевом мероприятии в базу данных
            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Полевые_мероприятия (Дата, Тип_мероприятия, Имя_Ответственного, Фамилия_Ответственного, Продолжительность, Оборудование) " +
                           "VALUES (@Дата, @ТипМероприятия, @ИмяОтветственного, @ФамилияОтветственного, @Продолжительность, @Оборудование)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Дата", Дата);
                    command.Parameters.AddWithValue("@ТипМероприятия", ТипМероприятия);
                    command.Parameters.AddWithValue("@ИмяОтветственного", ИмяОтветственного);
                    command.Parameters.AddWithValue("@ФамилияОтветственного", ФамилияОтветственного);
                    command.Parameters.AddWithValue("@Продолжительность", Продолжительность);
                    command.Parameters.AddWithValue("@Оборудование", Оборудование);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            ClearFields();
        }

        private async Task<bool> IsPersonExists(string firstName, string lastName)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

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

        private async Task<bool> IsEquipmentExists(string equipment)
        {
            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "SELECT COUNT(*) FROM Оборудование WHERE Название = @Equipment";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Equipment", equipment);
                    int count = (int)await command.ExecuteScalarAsync();

                    return count > 0;
                }
            }
        }

        private void ClearFields()
        {
            Дата = DateTime.Now;
            ТипМероприятия = string.Empty;
            ИмяОтветственного = string.Empty;
            ФамилияОтветственного = string.Empty;
            Продолжительность = 0;
            Оборудование = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
