using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hell.View
{
    /// <summary>
    /// Логика взаимодействия для Packing_stage.xaml
    /// </summary>
    public partial class Packing_stage : Window
    {
        public Packing_stage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string этап = ЭтапTextBox.Text;
            string описание = ОписаниеTextBox.Text;
            string времяВыполнения = ВремяВыполненияTextBox.Text;
            string ответственный = ОтветственныйTextBox.Text;
            DateTime дата = ДатаDatePicker.SelectedDate ?? DateTime.Now;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
