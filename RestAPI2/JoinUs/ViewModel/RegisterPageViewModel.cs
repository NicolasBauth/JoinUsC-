using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model.UserDTOs;
using JoinUs.StaticServices;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
                if (DateTimeOffset.Compare(_birthdate, DateTimeOffset.Now) < 0)
                {
                    return DateTimeOffset.Now;
                }
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                RaisePropertyChanged("BirthDate");
            }
        }

        public string PasswordWarning
        {
            get
            {
                return "Note : votre mot de passe doit contenir au moins :\n-Un caractère spécial;\n-Une majuscule;\n-Une minuscule;\n-Un chiffre\n il doit également être au minimum\n composé de 6 caractères (maximum 100)";
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
                userToRegister.Birthdate = _birthdate.Date;
                userToRegister.Email = EMail;
                userToRegister.FirstName = FirstName;
                userToRegister.LastName = LastName;
                userToRegister.Password = PasswordConfirmed;
                userToRegister.ConfirmPassword = PasswordConfirmed;
                bool formConfirmation = await UserDAO.RegisterUser(userToRegister);
                if (formConfirmation)
                {
                    _navigationService.NavigateTo("LoginPage");
                    string toastTitle = "Inscription réussie";
                    string toastDescription = "Vous avez bien été inscrit! Félicitations! Veuillez maintenant vous connecter.";
                    ToastCenter.InformativeNotify(toastTitle, toastDescription);
                }
                else
                {
                    ToastCenter.InformativeNotify("Erreur,Impossible de vous enregistrer. ", "Il est possible que cette adresse mail soit déjà associée à un compte. Vérifiez également que votre mot de passe est convenable.");
                }
            }
            
        }

        public bool CanTryToCommit()
        {
            if(FirstName == null)
            {
                ToastCenter.InformativeNotify("Prénom absent", "Votre prénom est requis.");
                return false;
            }
            foreach(char c in FirstName)
            {
                if(!char.IsLetter(c) && c!=' ' && c!='-')
                {
                    ToastCenter.InformativeNotify("Prénom invalide", "Votre prénom ne peut être composé que de lettres, d'espaces et de traits d'union.");
                    return false;
                }
            }
            if (LastName == null)
            {
                ToastCenter.InformativeNotify("Nom absent", "Votre nom de famille est requis.");
                return false;
            }
            foreach (char c in LastName)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-')
                {
                    ToastCenter.InformativeNotify("Nom de famille invalide", "Votre nom de famille ne peut être composé que de lettres, d'espaces et de traits d'union.");
                    return false;
                }
            }
            if(EMail == null)
            {
                ToastCenter.InformativeNotify("e-mail absent", "Votre e-mail est requis.");
                return false;
            }
            var emailCheck = new EmailAddressAttribute();
            if(!emailCheck.IsValid(EMail))
            {
                ToastCenter.InformativeNotify("e-mail incorrect", "L'adresse e-mail que vous avez rentré n'est pas valide. Veuillez réessayer.");
                return false;
            }
            if(Password == null)
            {
                ToastCenter.InformativeNotify("Mot de passe absent", "Vous n'avez pas fourni de mot de passe.");
                return false;
            }
            if(PasswordConfirmed == null)
            {
                ToastCenter.InformativeNotify("Confirmation du mot de passe absente", "Vous n'avez pas confirmé votre mot de passe.");
                return false;
            }
            if(Password != PasswordConfirmed)
            {
                ToastCenter.InformativeNotify("Mots de passe différents", "Votre mot de passe et confirmation de mot de passe sont différents.");
                return false;
            }
            if(_birthdate == null)
            {
                ToastCenter.InformativeNotify("Date de naissance absente", "Veuillez renseigner une date de naissance");
                return false;
            }
            if(DateTimeOffset.Compare(_birthdate,DateTimeOffset.Now) >= 0)
            {
                ToastCenter.InformativeNotify("Date de naissance", "La date de naissance spécifiée est dans le futur...");
                return false;
            }
            if(UserService.CalculateAge(_birthdate.DateTime) < 12)
            {
                ToastCenter.InformativeNotify("Vous êtes trop jeune", "Vous devez être agé d'au moins 12 ans pour utiliser l'application.");
                return false;
            }
            return true;
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
