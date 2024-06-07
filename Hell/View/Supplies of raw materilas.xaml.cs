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
    /// Логика взаимодействия для Supplies_of_raw_materilas.xaml
    /// </summary>
    public partial class Supplies_of_raw_materilas : Window
    {
        public Supplies_of_raw_materilas()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime датаПоставки = ДатаПоставкиDatePicker.SelectedDate ?? DateTime.Now;
            int количество = Convert.ToInt32(КоличествоTextBox.Text);
            string поставщик = ПоставщикTextBox.Text;
            string типСырья = ТипСырьяTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
