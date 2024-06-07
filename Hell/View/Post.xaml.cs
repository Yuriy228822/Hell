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
    /// Логика взаимодействия для Post.xaml
    /// </summary>
    public partial class Post : Window
    {
        public Post()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string названиеДолжности = НазваниеДолжностиTextBox.Text;
            string описание = ОписаниеTextBox.Text;
            string обязанности = ОбязанностиTextBox.Text;
            string требования = ТребованияTextBox.Text;
            decimal заработнаяПлата = Convert.ToDecimal(ЗаработнаяПлатаTextBox.Text);

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
