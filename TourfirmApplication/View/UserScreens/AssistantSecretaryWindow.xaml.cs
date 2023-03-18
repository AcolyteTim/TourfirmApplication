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

namespace TourfirmApplication.View.UserScreens
{
    /// <summary>
    /// Логика взаимодействия для AssistantSecretaryWindow.xaml
    /// </summary>
    public partial class AssistantSecretaryWindow : Window
    {
        public AssistantSecretaryWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => !x.IsVisible).ShowDialog();
        }
    }
}
