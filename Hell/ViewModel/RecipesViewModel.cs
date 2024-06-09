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
    public class RecipesViewModel : INotifyPropertyChanged
    {
        private string _название;
        private string _описание;
        private string _ингредиенты;
        private string _этапыПриготовления;
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

        public string Ингредиенты
        {
            get => _ингредиенты;
            set
            {
                _ингредиенты = value;
                OnPropertyChanged(nameof(Ингредиенты));
            }
        }

        public string ЭтапыПриготовления
        {
            get => _этапыПриготовления;
            set
            {
                _этапыПриготовления = value;
                OnPropertyChanged(nameof(ЭтапыПриготовления));
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

        public RecipesViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveRecipeAsync);
        }

        private async Task SaveRecipeAsync(object parameter)
        {
            string название = Название;
            string описание = Описание;
            string ингредиенты = Ингредиенты;
            string этапыПриготовления = ЭтапыПриготовления;
            string ответственный = Ответственный;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Рецепты (Название, Описание, Ингредиенты, Этапы_приготовления, Ответственный) " +
                           "VALUES (@Название, @Описание, @Ингредиенты, @ЭтапыПриготовления, @Ответственный)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Описание", описание);
                    command.Parameters.AddWithValue("@Ингредиенты", ингредиенты);
                    command.Parameters.AddWithValue("@ЭтапыПриготовления", этапыПриготовления);
                    command.Parameters.AddWithValue("@Ответственный", ответственный);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Данные сохранены!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Описание = string.Empty;
            Ингредиенты = string.Empty;
            ЭтапыПриготовления = string.Empty;
            Ответственный = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
