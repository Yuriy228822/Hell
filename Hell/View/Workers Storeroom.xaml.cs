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
    /// Логика взаимодействия для Workers_Storeroom.xaml
    /// </summary>
    public partial class Workers_Storeroom : Window
    {
        public Workers_Storeroom()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string имя = ИмяTextBox.Text;
            string фамилия = ФамилияTextBox.Text;
            string отчество = ОтчествоTextBox.Text;
            string должность = ДолжностьTextBox.Text;
            DateTime датаПриемаНаРаботу = ДатаПриемаНаРаботуDatePicker.SelectedDate ?? DateTime.Now;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
