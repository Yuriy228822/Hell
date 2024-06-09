using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class ChemicalsViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _назначение;
        private DateTime _датаПроизводства;
        private int _срокГодности;
        private string _производитель;
        private int _кладовая;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Назначение
        {
            get => _назначение;
            set
            {
                _назначение = value;
                OnPropertyChanged(nameof(Назначение));
            }
        }

        public DateTime ДатаПроизводства
        {
            get => _датаПроизводства;
            set
            {
                _датаПроизводства = value;
                OnPropertyChanged(nameof(ДатаПроизводства));
            }
        }

        public int СрокГодности
        {
            get => _срокГодности;
            set
            {
                _срокГодности = value;
                OnPropertyChanged(nameof(СрокГодности));
            }
        }

        public string Производитель
        {
            get => _производитель;
            set
            {
                _производитель = value;
                OnPropertyChanged(nameof(Производитель));
            }
        }

        public int Кладовая
        {
            get => _кладовая;
            set
            {
                _кладовая = value;
                OnPropertyChanged(nameof(Кладовая));
            }
        }

        public ICommand SaveCommand { get; }

        public ChemicalsViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveChemicalAsync);
            ДатаПроизводства = DateTime.Now;  // Установка текущей даты по умолчанию
        }

        private async Task SaveChemicalAsync(object parameter)
        {
            string название = Название;
            string назначение = Назначение;
            DateTime датаПроизводства = ДатаПроизводства;
            int срокГодности = СрокГодности;
            string производитель = Производитель;
            int кладовая = Кладовая;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            if (!await CheckWarehouseExistsAsync(кладовая, connectionString))
            {
                MessageBox.Show("Склад с указанным ID не существует.");
                return;
            }

            string query = "INSERT INTO Химикаты (Название, Назначение, Дата_производства, Срок_годности, Производитель, Кладовая) " +
                           "VALUES (@Название, @Назначение, @ДатаПроизводства, @СрокГодности, @Производитель, @Кладовая)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Назначение", назначение);
                    command.Parameters.AddWithValue("@ДатаПроизводства", датаПроизводства);
                    command.Parameters.AddWithValue("@СрокГодности", срокГодности);
                    command.Parameters.AddWithValue("@Производитель", производитель);
                    command.Parameters.AddWithValue("@Кладовая", кладовая);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }
        private void Clear()
        {
            Название = string.Empty;
            Назначение = string.Empty;
            ДатаПроизводства = DateTime.Now;
            СрокГодности = 0;
            Производитель = string.Empty;
        }

        private async Task<bool> CheckWarehouseExistsAsync(int warehouseId, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Кладовая WHERE ID = @WarehouseId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WarehouseId", warehouseId);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
