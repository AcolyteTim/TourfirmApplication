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
using TourfirmApplication.Model;
using TourfirmApplication.ViewModel.TablesActions;

namespace TourfirmApplication.View.TablesActions
{
    /// <summary>
    /// Логика взаимодействия для ClientEditWindow.xaml
    /// </summary>
    public partial class ClientEditWindow : Window
    {
        public ClientEditWindow(Client client)
        {
            InitializeComponent();
            DataContext = new ClientEditWindowVM(client);

        }
    }
}
