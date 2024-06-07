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
    /// Логика взаимодействия для People.xaml
    /// </summary>
    public partial class People : Window
    {
        public People()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string имя = ИмяTextBox.Text;
            string фамилия = ФамилияTextBox.Text;
            string должность = ДолжностьTextBox.Text;
            string контактныеДанные = КонтактныеДанныеTextBox.Text;
            int опытРаботы = Convert.ToInt32(ОпытРаботыTextBox.Text);

            // Логика сохранения данных в базу данных

            MessageBox.Show("Данные сохранены!");
        }
    }
}
