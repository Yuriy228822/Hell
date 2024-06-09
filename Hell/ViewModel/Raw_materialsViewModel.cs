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
    public class Raw_materialsViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _качество;
        private int _количество;
        private DateTime _дата_получения;
        private string _поставщик;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Качество
        {
            get => _качество;
            set
            {
                _качество = value;
                OnPropertyChanged(nameof(Качество));
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

        public DateTime Дата_получения
        {
            get => _дата_получения;
            set
            {
                _дата_получения = value;
                OnPropertyChanged(nameof(Дата_получения));
            }
        }

        public string Поставщик
        {
            get => _поставщик;
            set
            {
                _поставщик = value;
                OnPropertyChanged(nameof(Поставщик));
            }
        }

        public ICommand SaveCommand { get; }

        public Raw_materialsViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveRawMaterialAsync);
            Дата_получения = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveRawMaterialAsync(object parameter)
        {
            string название = Название;
            string качество = Качество;
            int количество = Количество;
            DateTime дата_получения = Дата_получения;
            string поставщик = Поставщик;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Сырье (Название, Качество, Количество, Дата_получения, Поставщик) " +
                           "VALUES (@Название, @Качество, @Количество, @Дата_получения, @Поставщик)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Качество", качество);
                    command.Parameters.AddWithValue("@Количество", количество);
                    command.Parameters.AddWithValue("@Дата_получения", дата_получения);
                    command.Parameters.AddWithValue("@Поставщик", поставщик);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные о сырье сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Качество = string.Empty;
            Количество = 0;
            Дата_получения = DateTime.Now;
            Поставщик = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
