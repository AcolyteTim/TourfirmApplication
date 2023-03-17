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
    internal class ClientAddWindowVM : BaseViewModel
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

        public void CreateNewClient(string surname, string name, string patronymic, string addressID, string telephoneNum)
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
                    OpenMessageWindow(DataWorker.CreateClient(surname, name, patronymic, convertedAddress, convertedTelephoneNum));
                }
            }
            catch (Exception ex)
            {
                OpenMessageWindow(ex.Message);
            }
        }

        private RelayCommand createClient;
        public RelayCommand CreateClient
        {
            get
            {
                return createClient ?? new RelayCommand(obj =>
                {
                    CreateNewClient(SurnameTB, NameTB, PatronymicTB,AddressID,TelephoneNum);
                }
                );
            }
        }
    }
}
