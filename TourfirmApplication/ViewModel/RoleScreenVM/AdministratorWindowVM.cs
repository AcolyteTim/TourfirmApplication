using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourfirmApplication.Model;
using TourfirmApplication.View.TablesActions;


namespace TourfirmApplication.ViewModel.RoleScreenVM
{
    class AdministratorWindowVM : BaseViewModel
    {
        // Window openning method
        private void SetWindowPostionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        // Addresses
        private ObservableCollection<Address_> addressList = DataWorker.GetAllAddress();
        public ObservableCollection<Address_> AddressList
        {
            get => addressList;
            set
            {
                addressList = value;
                OnPropertyChanged("AddressList");
            }
        }

        private Address_ selectedAddressRow;
        public Address_ SelectedAddressRow
        {
            get => selectedAddressRow;
            set
            {
                selectedAddressRow = value;
                OnPropertyChanged("SelectedAddressRow");
            }
        }

        // Clients
        private ObservableCollection<Client> _filteredClients = DataWorker.GetAllClient();
        public ObservableCollection<Client> FilteredClients
        {
            get { return _filteredClients; }
            set
            {
                _filteredClients = value;
                OnPropertyChanged("FilteredClients");
            }
        }

        private Client selectedClientRow;
        public Client SelectedClientRow
        {
            get => selectedClientRow;
            set
            {
                selectedClientRow = value;
                OnPropertyChanged("SelectedClientRow");
            }
        }

        private string clientIDTextBoxText;
        public string ClientIDTextBoxText
        {
            get => clientIDTextBoxText;
            set
            {
                clientIDTextBoxText = value;
                OnPropertyChanged("ClientIDTextBoxText");
            }
        }

        private ObservableCollection<Country> clientVISAList;
        public ObservableCollection<Country> ClientVISAList
        {
            get { return clientVISAList; }
            set
            {
                clientVISAList = value;
                OnPropertyChanged("ClientVISAList");
            }
        }

        private Country selectedClientVISARow;
        public Country SelectedClientVISARow
        {
            get => selectedClientVISARow;
            set
            {
                selectedClientVISARow = value;
                OnPropertyChanged("SelectedClientVISARow");
            }
        }

        public void GetVISAOfPickedClientByID(string textFromTextBoxWithID)
        {
            try
            {
                if (textFromTextBoxWithID != String.Empty)
                {
                    ClientVISAList = DataWorker.GetAllClientVISAs(Convert.ToInt32(textFromTextBoxWithID));
                }
            }
            catch(Exception ex) {  }
        }

        private RelayCommand getClientVISA;
        public RelayCommand GetClientVISA
        {
            get
            {
                return getClientVISA ?? new RelayCommand(obj =>
                {
                    GetVISAOfPickedClientByID(ClientIDTextBoxText);
                }
                );
            }
        }      

        private string clientSearchTextBoxText;
        public string ClientSearchTextBoxText
        {
            get => clientSearchTextBoxText;
            set
            {
                clientSearchTextBoxText = value;
                OnPropertyChanged("ClientSearchTextBoxText");
            }
        }

        public void FilterClients()
        { 
            var filteredResult = DataWorker.GetAllClientsWithFilter(ClientSearchTextBoxText);
            _filteredClients.Clear();
            if (filteredResult.Count > 0)
            {
                foreach (var a in filteredResult)
                {
                    _filteredClients.Add(a);
                }
            }
            else
            {
                Client cl = new Client();
                cl.cl_surname = "NO RESULTS"; 
                _filteredClients.Add(cl);
            }
        }

        private RelayCommand getFilteredClients;
        public RelayCommand GetFilteredClients
        {
            get
            {
                return getFilteredClients ?? new RelayCommand(obj =>
                {
                    FilterClients();
                }
                );
            }
        }


        // Countries
        private ObservableCollection<Country> countryList = DataWorker.GetAllCountry();
        public ObservableCollection<Country> CountryList
        {
            get => countryList;
            set
            {
                countryList = value;
                OnPropertyChanged("CountryList");
            }
        }
       
        private Country selectedCountryRow;
        public Country SelectedCountryRow
        {
            get => selectedCountryRow;
            set
            {
                selectedCountryRow = value;
                OnPropertyChanged("SelectedCountryRow");
            }
        }

        // Employees
        private ObservableCollection<Employee> employeeList = DataWorker.GetAllEmployee();
        public ObservableCollection<Employee> EmployeeList
        {
            get => employeeList;
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private Employee selectedEmployeeRow;
        public Employee SelectedEmployeeRow
        {
            get => selectedEmployeeRow;
            set
            {
                selectedEmployeeRow = value;
                OnPropertyChanged("SelectedEmployeeRow");

            }
        }

        // Employee positions
        private ObservableCollection<Employee_position> empPositionList = DataWorker.GetAllEmployeePosition();
        public ObservableCollection<Employee_position> EmpPositionList
        {
            get => empPositionList;
            set
            {
                empPositionList = value;
                OnPropertyChanged("EmpPositionList");
            }
        }

        private Employee_position selectedEmpPositionRow;
        public Employee_position SelectedEmpPositionRow
        {
            get => selectedEmpPositionRow;
            set
            {
                selectedEmpPositionRow = value;
                OnPropertyChanged("SelectedEmpPositionRow");

            }
        }

        // Routes
        private ObservableCollection<Route_> routeList = DataWorker.GetAllRoute();
        public ObservableCollection<Route_> RouteList
        {
            get => routeList;
            set
            {
                routeList = value;
                OnPropertyChanged("RouteList");
            }
        }

        private Route_ selectedRouteRow;
        public Route_ SelectedRouteRow
        {
            get => selectedRouteRow;
            set
            {
                selectedRouteRow = value;
                OnPropertyChanged("SelectedRouteRow");
            }
        }

        // Sales
        private ObservableCollection<Sale> saleList = DataWorker.GetAllSale();
        public ObservableCollection<Sale> SaleList
        {
            get => saleList;
            set
            {
                saleList = value;
                OnPropertyChanged("SaleList");
            }
        }

        private Sale selectedSaleRow;
        public Sale SelectedSaleRow
        {
            get => selectedSaleRow;
            set
            {
                selectedSaleRow = value;
            }
        }

        private string saleIDTextBoxText;
        public string SaleIDTextBoxText
        {
            get => saleIDTextBoxText;
            set
            {
                saleIDTextBoxText = value;
                OnPropertyChanged("SaleIDTextBoxText");
            }
        }

        private ObservableCollection<Trip> saleTripList;
        public ObservableCollection<Trip> SaleTripList
        {
            get { return saleTripList; }
            set
            {
                saleTripList = value;
                OnPropertyChanged("SaleTripList");
            }
        }

        private Trip selectedSaleTripRow;
        public Trip SelectedSaleTripRow
        {
            get => selectedSaleTripRow;
            set
            {
                selectedSaleTripRow = value;
            }
        }

        public void GetTripOfPickedSaleByID(string textFromTextBoxWithID)
        {
            try
            {
                if (textFromTextBoxWithID != String.Empty)
                {
                    SaleTripList = DataWorker.GetAllSaleTrips(Convert.ToInt32(textFromTextBoxWithID));
                }
            }
            catch { }
        }

        private RelayCommand getSaleTrip;
        public RelayCommand GetSaleTrip
        {
            get
            {
                return getSaleTrip ?? new RelayCommand(obj =>
                {
                    GetTripOfPickedSaleByID(SaleIDTextBoxText);
                }
                );
            }
        }

        // Trips
        private ObservableCollection<Trip> tripList = DataWorker.GetAllTrip();
        public ObservableCollection<Trip> TripList
        {
            get => tripList;
            set
            {
                tripList = value;
                OnPropertyChanged("TripList");
            }
        }

        private Trip selectedTripRow;
        public Trip SelectedTripRow
        {
            get => selectedTripRow;
            set
            {
                selectedTripRow = value;
            }
        }

        private string tripIDTextBoxText;
        public string TripIDTextBoxText
        {
            get => tripIDTextBoxText;
            set
            {
                tripIDTextBoxText = value;
                OnPropertyChanged("TripIDTextBoxText");
            }
        }

        private ObservableCollection<Goal> tripGoalList;
        public ObservableCollection<Goal> TripGoalList
        {
            get { return tripGoalList; }
            set
            {
                tripGoalList = value;
                OnPropertyChanged("TripGoalList");
            }
        }

        private Goal selectedTripGoalRow;
        public Goal SelectedTripGoalRow
        {
            get => selectedTripGoalRow;
            set
            {
                selectedTripGoalRow = value;
            }
        }

        public void GetGoalOfPickedTripByID(string textFromTextBoxWithID)
        {
            try
            {
                if (textFromTextBoxWithID != String.Empty)
                {
                    TripGoalList = DataWorker.GetAllTripGoals(Convert.ToInt32(textFromTextBoxWithID));
                }
            }
            catch { }
        }

        private RelayCommand getTripGoal;
        public RelayCommand GetTripGoal
        {
            get
            {
                return getTripGoal ?? new RelayCommand(obj =>
                {
                    GetGoalOfPickedTripByID(TripIDTextBoxText);
                }
                );
            }
        }

        // Users
        private ObservableCollection<User_> userList = DataWorker.GetAllUser();
        public ObservableCollection<User_> UserList
        {
            get => userList;
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        private User_ selectedUserRow;
        public User_ SelectedUserRow
        {
            get => selectedUserRow;
            set
            {
                selectedUserRow = value;
                OnPropertyChanged("SelectedUserRow");
            }
        }

        // User types
        private ObservableCollection<User_type> userTypeList = DataWorker.GetAllUserType();
        public ObservableCollection<User_type> UserTypeList
        {
            get => userTypeList;
            set
            {
                userTypeList = value;
                OnPropertyChanged("UserTypeList");
            }
        }

        private User_type selectedUserTypeRow;
        public User_type SelectedUserTypeRow
        {
            get => selectedUserTypeRow;
            set
            {
                selectedUserTypeRow = value;
                OnPropertyChanged("SelectedUserTypeRow");
            }
        }

        // Address BUTTONS
        private void OpenAddressAddWindow()
        { 
            var addressAddWindow = new AddressAddWindow();
            SetWindowPostionAndOpen(addressAddWindow);
            AddressList = DataWorker.GetAllAddress();
        }
        private RelayCommand addressAdd;
        public RelayCommand AddressAdd
        {
            get
            {
                return addressAdd ?? new RelayCommand(obj =>
                {
                    OpenAddressAddWindow();
                }
                );
            }
        }
        private void OpenAddressEditWindow(Address_ selectedAddressRow)
        {
            if (selectedAddressRow != null)
            {
                var addressEditWindow = new AddressEditWindow(selectedAddressRow);
                SetWindowPostionAndOpen(addressEditWindow);
                AddressList = DataWorker.GetAllAddress();
            }
        }
        private RelayCommand addressEdit;
        public RelayCommand AddressEdit
        {
            get
            {
                return addressEdit ?? new RelayCommand(obj =>
                {
                    OpenAddressEditWindow(SelectedAddressRow);
                }
                );
            }
        }
        private void DeleteAddress()
        { 
            if (selectedAddressRow != null )
            {
                var msgBoxResult = MessageBox.Show("Do you really want to delete picked item?", "Deleting address", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (msgBoxResult == MessageBoxResult.OK) {
                    DataWorker.DeleteAddress(SelectedAddressRow.a_id);
                    AddressList = DataWorker.GetAllAddress();
                }
            }
        }
        private RelayCommand addressDelete;
        public RelayCommand AddressDelete 
        {
            get
            {
                return addressDelete ?? new RelayCommand(obj =>
                {
                    DeleteAddress();
                }
                );
            }
        }

        // Client BUTTONS
        private void OpenClientAddWindow()
        {
            var clientAddWindow = new ClientAddWindow();
            SetWindowPostionAndOpen(clientAddWindow);
            FilteredClients = DataWorker.GetAllClient();
        }
        private RelayCommand clientAdd;
        public RelayCommand ClientAdd
        {
            get
            {
                return clientAdd ?? new RelayCommand(obj =>
                {
                    OpenClientAddWindow();
                }
                );
            }
        }

        private void OpenClientEditWindow(Client selectedClientRow)
        {
            if (selectedClientRow != null)
            {
                var clientEditWindow = new ClientEditWindow(selectedClientRow);
                SetWindowPostionAndOpen(clientEditWindow);
                FilteredClients = DataWorker.GetAllClient();
            }
        }
        private RelayCommand clientEdit;
        public RelayCommand ClientEdit
        {
            get
            {
                return clientEdit ?? new RelayCommand(obj =>
                {
                    OpenClientEditWindow(SelectedClientRow);
                }
                );
            }
        }

        private void DeleteСlient()
        {
            if (selectedClientRow != null)
            {
                var msgBoxResult = MessageBox.Show("Do you really want to delete picked item?", "Deleting client", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (msgBoxResult == MessageBoxResult.OK)
                {
                    DataWorker.DeleteClient(SelectedClientRow.cl_id);
                    FilteredClients = DataWorker.GetAllClient();
                }
            }
        }
        private RelayCommand clientDelete;
        public RelayCommand ClientDelete
        {
            get
            {
                return clientDelete ?? new RelayCommand(obj =>
                {
                    DeleteСlient();
                }
                );
            }
        }

        private void OpenClientVISAAddWindow()
        {
            if (SelectedClientRow != null)
            {
                var clientAddVISAWindow = new ClientAddVISAWindow(SelectedClientRow);
                SetWindowPostionAndOpen(clientAddVISAWindow);
                ClientVISAList = DataWorker.GetAllClientVISAs(Convert.ToInt32(ClientIDTextBoxText));
            }
        }
        private RelayCommand clientVISAAdd;
        public RelayCommand ClientVISAAdd
        {
            get
            {
                return clientVISAAdd ?? new RelayCommand(obj =>
                {
                    OpenClientVISAAddWindow();
                }
                );
            }
        }

        private void RemoveСlientVISA()
        {
            if (SelectedClientVISARow != null && SelectedClientRow != null)
            {
                var msgBoxResult = MessageBox.Show("Do you really want to remove picked item?", "Removing picked VISA", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (msgBoxResult == MessageBoxResult.OK)
                {
                    var convertedClientRow = Convert.ToInt32(SelectedClientRow.cl_id);
                    DataWorker.DeleteVISAFromClient(convertedClientRow, SelectedClientVISARow.c_id);
                    ClientVISAList = DataWorker.GetAllClientVISAs(Convert.ToInt32(ClientIDTextBoxText));
                }
            }
        }
        private RelayCommand clientVISARemove;
        public RelayCommand ClientVISARemove
        {
            get
            {
                return clientVISARemove ?? new RelayCommand(obj =>
                {
                    RemoveСlientVISA();
                }
                );
            }
        }

        //Employee BUTTONS
        void ChangePicture()
        {
            if (SelectedEmployeeRow != null)
            {
                try
                {
                    var dialog = new OpenFileDialog
                    {
                        Filter = "Images (*.png, *.jpeg, *.jpg)|*.png;*.jpeg;*.jpg",
                        Multiselect = false,
                        CheckFileExists = true,
                        CheckPathExists = true,
                        ValidateNames = true
                    };

                    if (dialog.ShowDialog() != true)
                    {
                        return;
                    }

                    var imagePath = dialog.FileName;
                    var imageData = File.ReadAllBytes(imagePath);

                    DataWorker.EditEmployeeImage(Convert.ToInt32(SelectedEmployeeRow.e_id), imageData);
                    EmployeeList = DataWorker.GetAllEmployee();
                }
                catch (Exception ex) { }
            }
        }

        private RelayCommand addImageToEmployee;
        public RelayCommand AddImageToEmployee
        {
            get
            {
                return addImageToEmployee ?? new RelayCommand(obj =>
                {
                    ChangePicture();
                }
                );
            }
        }

    }
}
