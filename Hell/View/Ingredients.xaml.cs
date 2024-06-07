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
    /// Логика взаимодействия для Ingredients.xaml
    /// </summary>
    public partial class Ingredients : Window
    {
        public Ingredients()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string название = НазваниеTextBox.Text;
            string тип = ТипTextBox.Text;
            int количество = Convert.ToInt32(КоличествоTextBox.Text);
            DateTime срокГодности = СрокГодностиDatePicker.SelectedDate ?? DateTime.Now;
            string поставщик = ПоставщикTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
