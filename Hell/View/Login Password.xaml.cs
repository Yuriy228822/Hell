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
    /// Логика взаимодействия для Login_Password.xaml
    /// </summary>
    public partial class Login_Password : Window
    {
        public Login_Password()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string логин = ЛогинTextBox.Text;
            string пароль = ПарольPasswordBox.Password;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
