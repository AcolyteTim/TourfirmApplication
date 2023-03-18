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
using TourfirmApplication.ViewModel.RoleScreenVM;
using TourfirmApplication.Model;

namespace TourfirmApplication.View.UserScreens
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            using (TourfirmEntities db = new TourfirmEntities())
            {
                User_ user = db.User_.FirstOrDefault(el => el.u_id == 1);
                user.u_isActive = false;
            }
            Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => !x.IsVisible).ShowDialog();
        }
    }
}
