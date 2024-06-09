using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class WorkersStoreroomViewModel : INotifyPropertyChanged
    {
        private string _имя;
        private string _фамилия;
        private string _должность;
        private string _графикРаботы;
        private string _контактныеДанные;

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

        public string ГрафикРаботы
        {
            get => _графикРаботы;
            set
            {
                _графикРаботы = value;
                OnPropertyChanged(nameof(ГрафикРаботы));
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

        public ICommand SaveCommand { get; }

        public WorkersStoreroomViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveWorkerAsync);
        }

        private async Task SaveWorkerAsync(object parameter)
        {
            string имя = Имя;
            string фамилия = Фамилия;
            string должность = Должность;
            string графикРаботы = ГрафикРаботы;
            string контактныеДанные = КонтактныеДанные;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Рабочие_Кладовая (Имя, Фамилия, Должность, График_работы, Контактные_данные) " +
                           "VALUES (@Имя, @Фамилия, @Должность, @ГрафикРаботы, @КонтактныеДанные)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Имя", имя);
                    command.Parameters.AddWithValue("@Фамилия", фамилия);
                    command.Parameters.AddWithValue("@Должность", должность);
                    command.Parameters.AddWithValue("@ГрафикРаботы", графикРаботы);
                    command.Parameters.AddWithValue("@КонтактныеДанные", контактныеДанные);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Имя = string.Empty;
            Фамилия = string.Empty;
            Должность = string.Empty;
            ГрафикРаботы = string.Empty;
            КонтактныеДанные = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
