using MyLibrary.LogicClasses;
using MyLibrary.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLibrary.ViewModel
{
    public class LoginWindowViewModel : BaseViewModel
    {
        private string password;
        private string login;
        private string error;
        private Person person;

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(obj =>
                  {
                      LoginUser();
                  }));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private void CloseWindowAndOpenNew(Window window)
        {
            WindowCollection windows = Application.Current.Windows;
            window.Show();
            if (windows != null)
            {
                windows[0].Close();
            }
        }

        public void LoginUser()
        {
            try
            {  
                Password password = new Password(Password);
                Person person = DataBaseController.Login(Login, password.HeshPassword);

                switch ((Role)person.IdRole)
                {
                    
                    case Role.Reader:
                        ReaderWindow readerWindow = new ReaderWindow(person);
                        CloseWindowAndOpenNew(readerWindow);
                        break;
                    case Role.Librarian:
                        LibrarianWindow librarianWindow = new LibrarianWindow(person);
                        CloseWindowAndOpenNew(librarianWindow);
                        break;
                }
            }
            catch (Exception ex)
            {
                Error = "Что-то пошло не так. " + ex.Message;
                person = null;
            }

        }
    }
}
