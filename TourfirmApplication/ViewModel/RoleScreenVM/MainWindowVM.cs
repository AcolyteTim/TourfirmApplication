using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourfirmApplication.Model;
using TourfirmApplication.View.TablesActions;
using TourfirmApplication.View;
using TourfirmApplication.View.UserScreens;

namespace TourfirmApplication.ViewModel.RoleScreenVM
{
    internal class MainWindowVM : BaseViewModel
    {
        private uint attemps = 0;
        public uint Attemps
        {
            get { return attemps; }
            set { attemps= value;
                OnPropertyChanged("Attemps");
            }
        }

        private User_ user;
        public User_ User { get; set; }

        private String captchaGenerated;
        public String CaptchaGenerated
        { 
            get => captchaGenerated;
            set { 
                captchaGenerated = value;
                OnPropertyChanged(nameof(CaptchaGenerated));
            }
        }

        private bool captchaVisibility = false;
        public bool CaptchaVisibility
        {
            get { return captchaVisibility; }
            set
            {
                captchaVisibility = value;
                OnPropertyChanged(nameof(CaptchaVisibility));
            }
        }

        private string loginTextTB;
        public string LoginTextTB
        {
            get { return loginTextTB; }
            set
            {
                loginTextTB = value;
                OnPropertyChanged(nameof(LoginTextTB));
            }
        }

        private string passwordTextTB;
        public string PasswordTextTB
        {
            get { return passwordTextTB; }
            set
            {
                passwordTextTB = value;
                OnPropertyChanged(nameof(PasswordTextTB));
            }
        }

        private string captchaTextTB;
        public string CaptchaTextTB
        {
            get { return captchaTextTB; }
            set
            {
                captchaTextTB = value;
                OnPropertyChanged(nameof(CaptchaTextTB));
            }
        }

        public string GenerateCaptcha()
        {
            return Captcha.Generate(6);
        }

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

        public void LoginAndOpenUserView()
        {
            try
            {
                if (LoginTextTB != String.Empty && PasswordTextTB != String.Empty)
                {
                    if (Attemps < 2)
                    {
                        User = DataWorker.GetUserByLoginAndPassword(LoginTextTB, PasswordTextTB);
                        if (User == null)
                        {
                            Attemps++;
                            if (Attemps == 2)
                            {
                                CaptchaGenerated = GenerateCaptcha();
                                CaptchaVisibility = true;
                            }
                            OpenMessageWindow("There is no such login and password combination");
                            return;
                        }
                    }
                    else
                    {
                        if (CaptchaTextTB != String.Empty)
                        {
                            if (CaptchaTextTB == CaptchaGenerated)
                            {
                                User = DataWorker.GetUserByLoginAndPassword(LoginTextTB, PasswordTextTB);
                                if (User == null)
                                {
                                    CaptchaGenerated = GenerateCaptcha();
                                    OpenMessageWindow("There is no such login and password combination");
                                    return;
                                }
                               
                            }
                            else
                            {
                                CaptchaGenerated = GenerateCaptcha();
                                OpenMessageWindow("Wrong captcha!");
                                return;
                            }
                        }
                    }

                    if (User != null)
                    {
                        Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsVisible).Hide();
                        switch (Convert.ToInt32(User.ut_id))
                        {
                            case 0:
                                {
                                    var adminWin = new AdministratorWindow();
                                    SetWindowPostionAndOpen(adminWin);
                                }
                                break;

                            case 1:
                                {
                                    var HRWin = new HRWindow();
                                    SetWindowPostionAndOpen(HRWin);
                                }
                                break;

                            case 2:
                                {
                                    var salesManagerWin = new SalesManagerWindow();
                                    SetWindowPostionAndOpen(salesManagerWin);
                                }
                                break;

                            case 3:
                                {
                                    var assistantSecretaryWin = new AssistantSecretaryWindow();
                                    SetWindowPostionAndOpen(assistantSecretaryWin);
                                }
                                break;
                        }
                    }
                    
                }
            }
            catch(Exception ex) { OpenMessageWindow(ex.Message); }
        }

        RelayCommand loginCommand;
        public RelayCommand LoginCommand 
        {
            get
            {
                return loginCommand ?? new RelayCommand(obj =>
                {
                    LoginAndOpenUserView();
                }
                );
            }
        }


    }
}
