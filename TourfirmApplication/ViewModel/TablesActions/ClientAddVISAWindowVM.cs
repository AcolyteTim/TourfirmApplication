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
    internal class ClientAddVISAWindowVM : BaseViewModel
    {
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

        public ClientAddVISAWindowVM(Client client)
        {
            ClientID = client.cl_id;
        }

        int clientID;
        public int ClientID
        {
            get { return clientID; }
            set
            {
                clientID = value;
                OnPropertyChanged(nameof(clientID));
            }
        }

        string countryID;
        public string CountryID
        {
            get { return countryID; }
            set
            {
                countryID = value;
                OnPropertyChanged(nameof(CountryID));
            }
        }

        public void AddVISAToClient()
        {
            try
            {
                if (CountryID != string.Empty)
                {
                    var convertedCountryID = Convert.ToInt32(CountryID);
                    OpenMessageWindow(DataWorker.AddVISAForClient(ClientID,convertedCountryID));
                }
            }
            catch (Exception ex)
            {
                OpenMessageWindow(ex.Message);
            }
        }

        private RelayCommand doCommand;
        public RelayCommand DoCommand
        {
            get
            {
                return doCommand ?? new RelayCommand(obj =>
                {
                    AddVISAToClient();
                }
                );
            }
        }
    }
}
