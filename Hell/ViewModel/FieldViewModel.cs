using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hell.Model;
using RelayCommand = Hell.Model.RelayCommand;

namespace Hell.ViewModel
{
    public class FieldViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _местоположение;
        private decimal _площадь;
        private string _типПочвы;
        private decimal _урожайность;

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

        public decimal Площадь
        {
            get => _площадь;
            set
            {
                _площадь = value;
                OnPropertyChanged(nameof(Площадь));
            }
        }

        public string ТипПочвы
        {
            get => _типПочвы;
            set
            {
                _типПочвы = value;
                OnPropertyChanged(nameof(ТипПочвы));
            }
        }

        public decimal Урожайность
        {
            get => _урожайность;
            set
            {
                _урожайность = value;
                OnPropertyChanged(nameof(Урожайность));
            }
        }

        public ICommand SaveCommand { get; }

        public FieldViewModel()
        {
            SaveCommand = new RelayCommand(SaveField);
        }

        private void SaveField(object parameter)
        {
            // Получение данных из свойств ViewModel
            string название = Название;
            string местоположение = Местоположение;
            decimal площадь = Площадь;
            string типПочвы = ТипПочвы;
            decimal урожайность = Урожайность;

            // Строка подключения к вашей базе данных
            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            // Создание SQL-запроса для вставки данных
            string query = "INSERT INTO Поля (Название, Местоположение, Площадь, Тип_почвы, Урожайность) " +
                           "VALUES (@Название, @Местоположение, @Площадь, @ТипПочвы, @Урожайность)";

            // Создание объекта подключения к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие подключения
                connection.Open();

                // Создание объекта команды SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавление параметров к команде SQL
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Местоположение", местоположение);
                    command.Parameters.AddWithValue("@Площадь", площадь);
                    command.Parameters.AddWithValue("@ТипПочвы", типПочвы);
                    command.Parameters.AddWithValue("@Урожайность", урожайность);

                    // Выполнение SQL-запроса
                    command.ExecuteNonQuery();
                }
            }

            // Оповещение пользователя об успешном сохранении данных
            MessageBox.Show("Данные сохранены!");
            Clear();
        }
        private void Clear()
        {
            Название = string.Empty;
            Местоположение = string.Empty;
            Площадь = 0;
            ТипПочвы = string.Empty;
            Урожайность = 0;
        }
        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
