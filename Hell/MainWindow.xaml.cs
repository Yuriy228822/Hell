using Hell.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hell
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenПоляWindow_Click(object sender, RoutedEventArgs e)
        {
            Field window = new Field();
            window.Show();
        }

        private void OpenПолевыеМероприятияWindow_Click(object sender, RoutedEventArgs e)
        {
            Field_Activities window = new Field_Activities();
            window.Show();
        }

        private void OpenСырьеWindow_Click(object sender, RoutedEventArgs e)
        {
            Raw_materials window = new Raw_materials();
            window.Show();    
        }

        private void OpenЛюдиWindow_Click(object sender, RoutedEventArgs e)
        {
            People window = new People();
            window.Show();
        }

        private void OpenПроизводствоWindow_Click(object sender, RoutedEventArgs e)
        {
            Production window = new Production();
            window.Show();
        }

        private void OpenХимикатыWindow_Click(object sender, RoutedEventArgs e)
        {
            Chemicals window = new Chemicals();
            window.Show();
        }

        private void OpenПоставкиСырьеWindow_Click(object sender, RoutedEventArgs e)
        {
            Supplies_of_raw_materilas window = new Supplies_of_raw_materilas();
            window.Show();
        }

        private void OpenКладоваяWindow_Click(object sender, RoutedEventArgs e)
        {
            Pantry window = new Pantry();
            window.Show();
        }

        private void OpenМагазинWindow_Click(object sender, RoutedEventArgs e)
        {
            Shop window = new Shop();
            window.Show();
        }

        private void OpenТехникаWindow_Click(object sender, RoutedEventArgs e)
        {
            Technique window = new Technique();
            window.Show();
        }

        private void OpenПоставщикиWindow_Click(object sender, RoutedEventArgs e)
        {
            Suppliers window = new Suppliers();
            window.Show();
        }

        private void OpenОборудованиеWindow_Click(object sender, RoutedEventArgs e)
        {
            Equipment window = new Equipment();
            window.Show();
        }

        private void OpenИнгредиентыWindow_Click(object sender, RoutedEventArgs e)
        {
            Ingredients window = new Ingredients();
            window.Show();
        }

        private void OpenГотоваяПродукцияWindow_Click(object sender, RoutedEventArgs e)
        {
            finished_products window = new finished_products();
            window.Show();
        }

        private void OpenСеменаWindow_Click(object sender, RoutedEventArgs e)
        {
            Seeds window = new Seeds();
            window.Show();
        }

        private void OpenРабочиеКладоваяWindow_Click(object sender, RoutedEventArgs e)
        {
            Workers_Storeroom window = new Workers_Storeroom();
            window.Show();
        }

        private void OpenРецептыWindow_Click(object sender, RoutedEventArgs e)
        {
            Recipes window = new Recipes();
            window.Show();
        }

        private void OpenФасовкаWindow_Click(object sender, RoutedEventArgs e)
        {
            Packing window = new Packing();
            window.Show();
        }

        private void OpenЭтапФасовкиWindow_Click(object sender, RoutedEventArgs e)
        {
            Packing_stage window = new Packing_stage();
            window.Show();
        }

        private void OpenПомещениеWindow_Click(object sender, RoutedEventArgs e)
        {
            Room window = new Room();
            window.Show();
        }

        private void OpenЧекиWindow_Click(object sender, RoutedEventArgs e)
        {
            Receipts window = new Receipts();
            window.Show();
        }

        private void OpenПродажиWindow_Click(object sender, RoutedEventArgs e)
        {
            Sales window = new Sales();
            window.Show();
        }

        private void OpenДолжностьWindow_Click(object sender, RoutedEventArgs e)
        {
            Post window = new Post();
            window.Show();
        }

        private void OpenФасованнаяПродукцияWindow_Click(object sender, RoutedEventArgs e)
        {
            Packaged_products window = new Packaged_products();
            window.Show();
        }

        private void OpenУпаковкаWindow_Click(object sender, RoutedEventArgs e)
        {
            Packaging window = new Packaging();
            window.Show();
        }

        private void OpenЛогинПарольWindow_Click(object sender, RoutedEventArgs e)
        {
            Login_Password window = new Login_Password();
            window.Show();
        }
    }
}

