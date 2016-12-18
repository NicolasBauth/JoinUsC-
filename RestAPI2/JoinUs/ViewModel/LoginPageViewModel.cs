using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
            set {        
                    _password = value;
                    RaisePropertyChanged("Password");
            }
        }
        public string Login
        {
            get { return _login; }
            set {
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
            if(Password!=null && Login!=null)
            {
                return true;
            }
            return false;
        }

        public ICommand LoginCommand
        {
            get
            {
                if(this._loginCommand == null)
                {
                    this._loginCommand = new RelayCommand(() => CommitLogin());
                }
                return _loginCommand;
            }
        }

        public void CommitLogin()
        {
            if (CanExecuteLoginCommand())
            {
                if (UserDAO.AuthenticateUser(Login, Password))
                {
                    User CurrentUser = UserDAO.GetUserByUserName(Login);
                    _navigationService.NavigateTo("MainPage", CurrentUser);
                }
                else
                {
                    string toastTitle = "Erreur de Login";
                    string toastText = "Mauvaise combinaison de mot de passe/nom d'utilisateur";
                    ToastCenter.InformativeNotify(toastTitle, toastText);
                }
            }
            else
            {
                string toastTitle = "Erreur de Login";
                string toastText = "Veuillez renseigner votre Login et votre mot de passe";
                ToastCenter.InformativeNotify(toastTitle, toastText);
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                if(this._registerCommand == null)
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
