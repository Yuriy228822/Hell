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
    /// Логика взаимодействия для Receipts.xaml
    /// </summary>
    public partial class Receipts : Window
    {
        public Receipts()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int номерЧека = Convert.ToInt32(НомерЧекаTextBox.Text);
            DateTime дата = ДатаDatePicker.SelectedDate ?? DateTime.Now;
            double сумма = Convert.ToDouble(СуммаTextBox.Text);
            string кассир = КассирTextBox.Text;
            string магазин = МагазинTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
