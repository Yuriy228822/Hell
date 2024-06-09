using Hell.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Hell.ViewModel
{
    internal class Packaged_productsViewModel : INotifyPropertyChanged
    {
        private string _названиеПродукта;
        private int _количество;
        private string _типУпаковки;
        private string _ответственный;
        private DateTime _датаФасовки;

        public string НазваниеПродукта
        {
            get => _названиеПродукта;
            set
            {
                _названиеПродукта = value;
                OnPropertyChanged(nameof(НазваниеПродукта));
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

        public string ТипУпаковки
        {
            get => _типУпаковки;
            set
            {
                _типУпаковки = value;
                OnPropertyChanged(nameof(ТипУпаковки));
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

        public DateTime ДатаФасовки
        {
            get => _датаФасовки;
            set
            {
                _датаФасовки = value;
                OnPropertyChanged(nameof(ДатаФасовки));
            }
        }

        public ICommand SaveCommand { get; }

        public Packaged_productsViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePackagedProductAsync);
            ДатаФасовки = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SavePackagedProductAsync(object parameter)
        {
            string названиеПродукта = НазваниеПродукта;
            int количество = Количество;
            string типУпаковки = ТипУпаковки;
            string ответственный = Ответственный;
            DateTime датаФасовки = ДатаФасовки;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Фасованная_продукция (Название_продукта, Количество, Тип_упаковки, Ответственный, Дата_фасовки) " +
                           "VALUES (@НазваниеПродукта, @Количество, @ТипУпаковки, @Ответственный, @ДатаФасовки)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@НазваниеПродукта", названиеПродукта);
                    command.Parameters.AddWithValue("@Количество", количество);
                    command.Parameters.AddWithValue("@ТипУпаковки", типУпаковки);
                    command.Parameters.AddWithValue("@Ответственный", ответственный);
                    command.Parameters.AddWithValue("@ДатаФасовки", датаФасовки);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            НазваниеПродукта = string.Empty;
            Количество = 0;
            ТипУпаковки = string.Empty;
            Ответственный = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
