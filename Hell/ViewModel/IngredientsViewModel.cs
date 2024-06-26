﻿using Hell.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hell.ViewModel
{
    public class IngredientsViewModel : INotifyPropertyChanged
    {
        private string _название;
        private int _количество;
        private string _качество;
        private string _поставщик;
        private int _срокГодности;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
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

        public string Качество
        {
            get => _качество;
            set
            {
                _качество = value;
                OnPropertyChanged(nameof(Качество));
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

        public int СрокГодности
        {
            get => _срокГодности;
            set
            {
                _срокГодности = value;
                OnPropertyChanged(nameof(СрокГодности));
            }
        }

        public ICommand SaveCommand { get; }

        public IngredientsViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveIngredientAsync);
        }

        private async Task SaveIngredientAsync(object parameter)
        {
            string название = Название;
            int количество = Количество;
            string качество = Качество;
            string поставщик = Поставщик;
            int срокГодности = СрокГодности;

            string connectionString = @"Data Source=(local);Initial Catalog=Pivo;Integrated Security=True";

            string query = "INSERT INTO Ингредиенты (Название, Количество, Качество, Поставщик, Срок_годности) " +
                           "VALUES (@Название, @Количество, @Качество, @Поставщик, @СрокГодности)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Название", название);
                    command.Parameters.AddWithValue("@Количество", количество);
                    command.Parameters.AddWithValue("@Качество", качество);
                    command.Parameters.AddWithValue("@Поставщик", поставщик);
                    command.Parameters.AddWithValue("@СрокГодности", срокГодности);

                    await command.ExecuteNonQueryAsync();
                }
            }

            MessageBox.Show("Ингредиент сохранен!");
            Clear();
        }

        private void Clear()
        {
            Название = string.Empty;
            Количество = 0;
            Качество = string.Empty;
            Поставщик = string.Empty;
            СрокГодности = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
