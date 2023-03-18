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

namespace TourfirmApplication.View.UserScreens
{
    /// <summary>
    /// Логика взаимодействия для SalesManagerWindow.xaml
    /// </summary>
    public partial class SalesManagerWindow : Window
    {
        public SalesManagerWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => !x.IsVisible).ShowDialog();
        }
    }
}
