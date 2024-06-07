﻿using System;
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
    /// Логика взаимодействия для finished_products.xaml
    /// </summary>
    public partial class finished_products : Window
    {
        public finished_products()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string название = НазваниеTextBox.Text;
            string тип = ТипTextBox.Text;
            int количество = Convert.ToInt32(КоличествоTextBox.Text);
            DateTime срокГодности = СрокГодностиDatePicker.SelectedDate ?? DateTime.Now;
            string местоположение = МестоположениеTextBox.Text;

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}