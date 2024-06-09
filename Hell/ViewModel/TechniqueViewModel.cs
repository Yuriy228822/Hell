using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class TechniqueViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _тип;
        private int _годВыпуска;
        private string _состояние;
        private string _местоположение;

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

        public int ГодВыпуска
        {
            get => _годВыпуска;
            set
            {
                _годВыпуска = value;
                OnPropertyChanged(nameof(ГодВыпуска));
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

        public string Местоположение
        {
            get => _местоположение;
            set
            {
                _местоположение = value;
                OnPropertyChanged(nameof(Местоположение));
            }
        }

        public ICommand SaveCommand { get; }

        public TechniqueViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveTechniqueAsync);
        }

        private async Task SaveTechniqueAsync(object parameter)
        {
            string название = Название;
            string тип = Тип;
            int годВыпуска = ГодВыпуска;
            string состояние = Состояние;
            string местоположение = Местоположение;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Техника (Название, Тип, Год_выпуска, Состояние, Местоположение) " +
                           "VALUES (@Название, @Тип, @ГодВыпуска, @Состояние, @Местоположение)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Тип", тип);
                    command.Parameters.AddWithValue("@ГодВыпуска", годВыпуска);
                    command.Parameters.AddWithValue("@Состояние", состояние);
                    command.Parameters.AddWithValue("@Местоположение", местоположение);

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
            ГодВыпуска = 0;
            Состояние = string.Empty;
            Местоположение = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
