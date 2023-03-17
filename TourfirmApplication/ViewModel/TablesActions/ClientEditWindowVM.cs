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
    internal class ClientEditWindowVM :BaseViewModel
    {
        public ClientEditWindowVM(Client client)
        {
            ClientID = client.cl_id;
            SurnameTB = client.cl_surname;
            NameTB = client.cl_name;
            PatronymicTB = client.cl_patronymic;
            AddressID = client.a_id.ToString();
            TelephoneNum = client.cl_telNum;
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

        private int clientID;
        public int ClientID
        {
            get => clientID;
            set => clientID = value;
        }

        private string surnameTB;
        public string SurnameTB
        {
            get => surnameTB;
            set
            {
                surnameTB = value;
                OnPropertyChanged("SurnameTB");
            }
        }

        private string nameTB;
        public string NameTB
        {
            get => nameTB;
            set
            {
                nameTB = value;
                OnPropertyChanged("NameTB");
            }
        }

        private string patronymicTB;
        public string PatronymicTB
        {
            get => patronymicTB;
            set
            {
                patronymicTB = value;
                OnPropertyChanged("PatronymicTB");
            }
        }

        private string addressID;
        public string AddressID
        {
            get => addressID;
            set
            {
                addressID = value;
                OnPropertyChanged("AddressID");
            }
        }

        private string telephoneNum;
        public string TelephoneNum
        {
            get => telephoneNum;
            set
            {
                telephoneNum = value;
                OnPropertyChanged("TelephoneNum");
            }
        }

        public void EditPickedClient(int id, string surname, string name, string patronymic, string addressID, string telephoneNum)
        {
            try
            {
                if (surname != null && telephoneNum != null)
                {
                    var convertedAddress = Convert.ToInt32(addressID);
                    var charsToRemove = new string[] { "+", " ", "(", ")", "-" };
                    var convertedTelephoneNum = telephoneNum;
                    foreach (var c in charsToRemove)
                    {
                        telephoneNum = telephoneNum.Replace(c, string.Empty);
                    }
                    convertedTelephoneNum = Convert.ToInt64(telephoneNum).ToString("+# (###) ###-##-##");
                    OpenMessageWindow(DataWorker.EditClient(id, surname, name, patronymic, convertedAddress, convertedTelephoneNum));
                }
            }
            catch (Exception ex)
            {
                OpenMessageWindow(ex.Message);
            }
        }

        private RelayCommand editPickedClientCommand;
        public RelayCommand EditPickedClientCommand
        {
            get
            {
                return editPickedClientCommand ?? new RelayCommand(obj =>
                {
                    EditPickedClient(ClientID, SurnameTB, NameTB, PatronymicTB, AddressID, TelephoneNum);
                }
                );
            }
        }
    }
}
