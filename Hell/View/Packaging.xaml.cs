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
    /// Логика взаимодействия для Packaging.xaml
    /// </summary>
    public partial class Packaging : Window
    {
        public Packaging()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string наименование = НаименованиеTextBox.Text;
            string материал = МатериалTextBox.Text;
            string структура = СтруктураTextBox.Text;
            string уровеньУпаковки = УровеньУпаковкиTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
