using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model.UserDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JoinUs.ViewModel
{
    public class RegisterPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ICommand _commitCommand;
        private ICommand _goBackCommand;
        private string _firstName;
        private string _lastName;
        private string _eMail;
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
                if (this._commitCommand == null)
                {
                    _commitCommand = new RelayCommand(async () => await Commit());
                }
                return _commitCommand;
            }
        }

        public async Task Commit()
        {
            if (CanTryToCommit())
            {
                RegisterFormDTO userToRegister = new RegisterFormDTO();
                userToRegister.Birthdate = BirthDate.Date;
                userToRegister.Email = EMail;
                userToRegister.FirstName = FirstName;
                userToRegister.LastName = LastName;
                userToRegister.Password = PasswordConfirmed;
                userToRegister.ConfirmPassword = PasswordConfirmed;
                bool formConfirmation = await UserDAO.RegisterUser(userToRegister);
                if (!formConfirmation)
                {

                    string toastTitle = "Utilisateur déjà existant";
                    string toastDescription = "Le pseudo que vous avez rentré existe déjà. Veuillez en choisir un autre.";
                    ToastCenter.InformativeNotify(toastTitle, toastDescription);
                }
                else
                {
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
            if (FirstName != null && LastName != null && EMail != null && BirthDate != null && Password != null && PasswordConfirmed != null && Password == PasswordConfirmed)
            {

            }
            return false;
        }

        public ICommand GoBackCommand
        {
            get
            {
                if (this._goBackCommand == null)
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

    }
}
