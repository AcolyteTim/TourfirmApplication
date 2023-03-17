using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourfirmApplication.Model;
using TourfirmApplication.View.TablesActions;

namespace TourfirmApplication.ViewModel.TablesActions
{
    internal class AddressEditWindowVM : BaseViewModel
    {
        public AddressEditWindowVM(Address_ pickedAddress)
        {
            AddressID = pickedAddress.a_id;
            LocalityTB = pickedAddress.a_locality;
            StreetTB = pickedAddress.a_street;
            BuildingTB = pickedAddress.a_building;
        }

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

        private int addressID;
        public int AddressID
        {
            get => addressID;
            set => addressID = value;
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

        public void EditPickedAddress(int id, string locality, string street, string building)
        {
            try
            {
                if (locality != null && street != null && building != null)
                {
                    OpenMessageWindow(DataWorker.EditAddress(id,locality, street, building));
                }
            }
            catch (Exception ex)
            {
                OpenMessageWindow(ex.Message);
            }
        }

        private RelayCommand editPickedAddressCommand;
        public RelayCommand EditPickedAddressCommand
        {
            get
            {
                return editPickedAddressCommand ?? new RelayCommand(obj =>
                {
                    EditPickedAddress(AddressID, LocalityTB, StreetTB, BuildingTB);
                }
                );
            }
        }
    }
}
