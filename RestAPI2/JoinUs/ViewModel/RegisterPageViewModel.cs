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
using Windows.UI.Xaml;

namespace JoinUs.ViewModel
{
    public class RegisterPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /*private INavigationService _navigationService;
        private ICommand _commitCommand;
        private ICommand _goBackCommand;
        private string _firstName;
        private string _lastName;
        private string _eMail;
        private string _userName;
        private DateTimeOffset _birthdate;
        private string _password;
        private string _passwordConfirmed;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public string PasswordConfirmed
        {
            get
            {
                return _passwordConfirmed;
            }
            set
            {
                _passwordConfirmed = value;
                RaisePropertyChanged("PasswordConfirmed");
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string EMail
        {
            get
            {
                return _eMail;
            }
            set
            {
                _eMail = value;
                RaisePropertyChanged("EMail");
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }

        }

        public DateTimeOffset BirthDate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                RaisePropertyChanged("BirthDate");
            }

        }
        

        


        public RegisterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand CommitCommand
        {
            get
            {
                if(this._commitCommand == null)
                {
                    _commitCommand = new RelayCommand(() => Commit());
                }
                return _commitCommand;
            }
        }

        public void Commit()
        {
            if (CanTryToCommit())
            {
                User userToRegister = new User();
                userToRegister.Birthdate = BirthDate.Date;
                userToRegister.Email = EMail;
                userToRegister.FirstName = FirstName;
                userToRegister.LastName = LastName;
                userToRegister.UserName = UserName;
                userToRegister.Password = PasswordConfirmed;
                if (!UserDAO.RegisterUser(userToRegister))
                {
                    
                    string toastTitle = "Utilisateur déjà existant";
                    string toastDescription = "Le pseudo que vous avez rentré existe déjà. Veuillez en choisir un autre.";
                    ToastCenter.InformativeNotify(toastTitle, toastDescription);
                }
                else
                {
                    UserDAO.RegisterUser(userToRegister);
                    _navigationService.NavigateTo("LoginPage");
                    string toastTitle = "Inscription réussie";
                    string toastDescription = "Vous avez bien été inscrit! Félicitations! Veuillez maintenant vous connecter.";
                    ToastCenter.InformativeNotify(toastTitle, toastDescription);
                    
                }
            }
            else
            {
                string toastTitle = "Formulaire incorrect";
                string toastDescription = "Tous les champs doivent être remplis. Vérifiez que les deux mots de passe entrés sont identiques.";
                ToastCenter.InformativeNotify(toastTitle, toastDescription);
            }           
        }

        public bool CanTryToCommit()
        {
            if( FirstName!=null && LastName!=null && EMail!=null && BirthDate!=null && UserName!=null && Password!=null && PasswordConfirmed!=null && Password == PasswordConfirmed)
            {
                return true;
            }
            return false;
        }

        public ICommand GoBackCommand
        {
            get
            {
                if(this._goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(() => GoBack());
                }
                return _goBackCommand;
            }
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
        */
    }
}
