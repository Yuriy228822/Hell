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
    /// Логика взаимодействия для Packing.xaml
    /// </summary>
    public partial class Packing : Window
    {
        public Packing()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string названиеПродукта = НазваниеПродуктаTextBox.Text;
            double вес = Convert.ToDouble(ВесTextBox.Text);
            DateTime датаФасовки = ДатаФасовкиDatePicker.SelectedDate ?? DateTime.Now;
            int количество = Convert.ToInt32(КоличествоTextBox.Text);
            string рабочий = РабочийTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
