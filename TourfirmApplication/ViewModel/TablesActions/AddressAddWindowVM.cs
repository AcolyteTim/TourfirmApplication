using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourfirmApplication.Model;
using TourfirmApplication.View.TablesActions;

namespace TourfirmApplication.ViewModel.TablesActions
{
    internal class AddressAddWindowVM : BaseViewModel
    {
        string a;

        // Window openning method
        private void SetWindowPostionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void OpenMessageWindow(string text)
        {
            var messageWindow = new MessageWindow(text);
            SetWindowPostionAndOpen(messageWindow);
        }


        private string localityTB;
        public string LocalityTB
        {
            get => localityTB;
            set
            {
                localityTB = value;
                OnPropertyChanged("LocalityTB");
            }
        }

        private string streetTB;
        public string StreetTB
        {
            get => streetTB;
            set
            {
                streetTB = value;
                OnPropertyChanged("StreetTB");
            }
        }

        private string buildingTB;
        public string BuildingTB
        {
            get => buildingTB;
            set
            {
                buildingTB = value;
                OnPropertyChanged("BuildingTB");
            }
        }

        public void CreateNewAddress(string locality, string street, string building)
        {
            try
            {
                if (locality != null && street != null && building != null)
                {
                    OpenMessageWindow(DataWorker.CreateAddress(locality, street, building));
                }
            }
            catch(Exception ex) 
            {
                OpenMessageWindow(ex.Message);
            }
        }

        private RelayCommand createAddress;
        public RelayCommand CreateAddress
        {
            get
            {
                return createAddress ?? new RelayCommand(obj =>
                {
                    CreateNewAddress(LocalityTB,StreetTB,BuildingTB);
                }
                );
            }
        }

    }
}
