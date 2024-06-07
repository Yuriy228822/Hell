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
    /// Логика взаимодействия для Packaged_products.xaml
    /// </summary>
    public partial class Packaged_products : Window
    {
        public Packaged_products()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string названиеПродукта = НазваниеПродуктаTextBox.Text;
            int количество = Convert.ToInt32(КоличествоTextBox.Text);
            string типУпаковки = ТипУпаковкиTextBox.Text;
            DateTime датаФасовки = ДатаФасовкиDatePicker.SelectedDate ?? DateTime.Now;
            int срокГодности = Convert.ToInt32(СрокГодностиTextBox.Text);

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
