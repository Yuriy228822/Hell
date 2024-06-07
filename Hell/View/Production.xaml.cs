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
    /// Логика взаимодействия для Production.xaml
    /// </summary>
    public partial class Production : Window
    {
        public Production()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string названиеПродукта = НазваниеПродуктаTextBox.Text;
            string этапыПроизводства = ЭтапыПроизводстваTextBox.Text;
            string ответственный = ОтветственныйTextBox.Text;
            DateTime датаНачала = ДатаНачалаDatePicker.SelectedDate ?? DateTime.Now;
            DateTime датаЗавершения = ДатаЗавершенияDatePicker.SelectedDate ?? DateTime.Now;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
