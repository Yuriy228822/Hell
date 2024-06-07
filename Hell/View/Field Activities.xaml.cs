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
    /// Логика взаимодействия для Field_Activities.xaml
    /// </summary>
    public partial class Field_Activities : Window
    {
        public Field_Activities()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime дата = ДатаDatePicker.SelectedDate ?? DateTime.Now;
            string типМероприятия = ТипМероприятияTextBox.Text;
            string ответственный = ОтветственныйTextBox.Text;
            int продолжительность = Convert.ToInt32(ПродолжительностьTextBox.Text);
            string оборудование = ОборудованиеTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
