using Hell.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class PantryViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _название;
        private string _местоположение;
        private int _вместимость;
        private string _ответственный;
        private string _запасы;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
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

        public int Вместимость
        {
            get => _вместимость;
            set
            {
                _вместимость = value;
                OnPropertyChanged(nameof(Вместимость));
            }
        }

        public string Ответственный
        {
            get => _ответственный;
            set
            {
                _ответственный = value;
                OnPropertyChanged(nameof(Ответственный));
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

        public PantryViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePantryAsync);
        }

        private async Task SavePantryAsync(object parameter)
        {
            string название = Название;
            string местоположение = Местоположение;
            int вместимость = Вместимость;
            string ответственный = Ответственный;
            string запасы = Запасы;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Кладовая (Название_склада, Местоположение, Вместимость, Ответственный, Запасы) " +
                           "VALUES (@Название, @Местоположение, @Вместимость, @Ответственный, @Запасы)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Местоположение", местоположение);
                    command.Parameters.AddWithValue("@Вместимость", вместимость);
                    command.Parameters.AddWithValue("@Ответственный", ответственный);
                    command.Parameters.AddWithValue("@Запасы", запасы);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные о кладовой сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Местоположение = string.Empty;
            Вместимость = 0;
            Ответственный = string.Empty;
            Запасы = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
