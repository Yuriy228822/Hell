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
    /// Логика взаимодействия для Field.xaml
    /// </summary>
    public partial class Field : Window
    {
        public Field()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string название = НазваниеTextBox.Text;
            string местоположение = МестоположениеTextBox.Text;
            decimal площадь = Convert.ToDecimal(ПлощадьTextBox.Text);
            string типПочвы = ТипПочвыTextBox.Text;
            decimal урожайность = Convert.ToDecimal(УрожайностьTextBox.Text);

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
