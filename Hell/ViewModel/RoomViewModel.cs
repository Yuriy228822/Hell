using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _тип;
        private decimal _площадь;
        private string _местоположение;
        private string _состояние;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Тип
        {
            get => _тип;
            set
            {
                _тип = value;
                OnPropertyChanged(nameof(Тип));
            }
        }

        public decimal Площадь
        {
            get => _площадь;
            set
            {
                _площадь = value;
                OnPropertyChanged(nameof(Площадь));
            }
        }

        public string Местоположение
        {
            get => _местоположение;
            set
            {
                _местоположение = value;
                OnPropertyChanged(nameof(Местоположение));
            }
        }

        public string Состояние
        {
            get => _состояние;
            set
            {
                _состояние = value;
                OnPropertyChanged(nameof(Состояние));
            }
        }

        public ICommand SaveCommand { get; }

        public RoomViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveRoomAsync);
        }

        private async Task SaveRoomAsync(object parameter)
        {
            string название = Название;
            string тип = Тип;
            decimal площадь = Площадь;
            string местоположение = Местоположение;
            string состояние = Состояние;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Помещение (Название, Тип, Площадь, Местоположение, Состояние) " +
                           "VALUES (@Название, @Тип, @Площадь, @Местоположение, @Состояние)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Тип", тип);
                    command.Parameters.AddWithValue("@Площадь", площадь);
                    command.Parameters.AddWithValue("@Местоположение", местоположение);
                    command.Parameters.AddWithValue("@Состояние", состояние);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Тип = string.Empty;
            Площадь = 0;
            Местоположение = string.Empty;
            Состояние = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
