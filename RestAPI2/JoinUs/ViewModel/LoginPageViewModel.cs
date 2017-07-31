using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JoinUs.ViewModel
{
    public class LoginPageViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private string _password;
        private string _login;
        private INavigationService _navigationService;
        private ICommand _loginCommand;
        private ICommand _registerCommand;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
            }

        }

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanExecuteLoginCommand()
        {
            if (Password != null && Login != null)
            {
                return true;
            }
            return false;
        }

        public ICommand LoginCommand
        {
            get
            {
                if (this._loginCommand == null)
                {
                    this._loginCommand = new RelayCommand(async () => await CommitLogin());
                }
                return _loginCommand;
            }
        }

        public async Task CommitLogin()
        {
            if (CanExecuteLoginCommand())
            {
                string tryToastTitle = "Tentative de Connexion...";
                string tryToastText = "Tentative de Connexion pour l'utilisateur " + Login;
                ToastCenter.InformativeNotify(tryToastTitle, tryToastText);
                AuthenticatedUser currentUser = await UserDAO.AuthenticateUser(Login, Password);
                if (currentUser != null)
                {
                    _navigationService.NavigateTo("MainPage", currentUser);
                }
                else
                {
                    string errorToastTitle = "Erreur de Login";
                    string errorToastText = "Mauvaise combinaison de mot de passe/nom d'utilisateur";
                    ToastCenter.InformativeNotify(errorToastTitle, errorToastText);
                }
            }
            else
            {
                string errorToastTitle = "Erreur de Login";
                string errorToastText = "Veuillez renseigner votre Login et votre mot de passe";
                ToastCenter.InformativeNotify(errorToastTitle, errorToastText);
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                if (this._registerCommand == null)
                {
                    this._registerCommand = new RelayCommand(() => Register());
                }
                return this._registerCommand;
            }
        }

        public void Register()
        {
            _navigationService.NavigateTo("RegisterPage");
        }

    }
}
