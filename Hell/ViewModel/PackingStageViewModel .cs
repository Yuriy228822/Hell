using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class PackingStageViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _описание;
        private string _оборудование;
        private int _продолжительность;
        private string _ответственный;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public string Описание
        {
            get => _описание;
            set
            {
                _описание = value;
                OnPropertyChanged(nameof(Описание));
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

        public int Продолжительность
        {
            get => _продолжительность;
            set
            {
                _продолжительность = value;
                OnPropertyChanged(nameof(Продолжительность));
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

        public ICommand SaveCommand { get; }

        public PackingStageViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SavePackingStageAsync);
        }

        private async Task SavePackingStageAsync(object parameter)
        {
            string название = Название;
            string описание = Описание;
            string оборудование = Оборудование;
            int продолжительность = Продолжительность;
            string ответственный = Ответственный;

            string connectionString = @"Data Source=(local);Initial Catalog=YourDatabase;Integrated Security=True";

            string query = "INSERT INTO Этап_фасовки (Название_этапа, Описание, Оборудование, Продолжительность, Ответственный) " +
                           "VALUES (@Название, @Описание, @Оборудование, @Продолжительность, @Ответственный)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Название", название);
                        command.Parameters.AddWithValue("@Описание", описание);
                        command.Parameters.AddWithValue("@Оборудование", оборудование);
                        command.Parameters.AddWithValue("@Продолжительность", продолжительность);
                        command.Parameters.AddWithValue("@Ответственный", ответственный);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                MessageBox.Show("Данные сохранены!");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void Clear()
        {
            Название = string.Empty;
            Описание = string.Empty;
            Оборудование = string.Empty;
            Продолжительность = 0;
            Ответственный = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
