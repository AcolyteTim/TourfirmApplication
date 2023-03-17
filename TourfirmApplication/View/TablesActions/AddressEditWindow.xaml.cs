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
using TourfirmApplication.Model;
using TourfirmApplication.ViewModel.TablesActions;

namespace TourfirmApplication.View.TablesActions
{
    /// <summary>
    /// Логика взаимодействия для AddressEditWindow.xaml
    /// </summary>
    public partial class AddressEditWindow : Window
    {
        public AddressEditWindow(Address_ address)
        {
            DataContext = new AddressEditWindowVM(address);
            InitializeComponent();
        }
    }
}
