using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.DAO;
using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JoinUs.ViewModel
{
    public class ProfilePageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _profileImagePath;
        private ObservableCollection<Category> _categories;
        private string _presentationString;
        private User _owner;
        private INavigationService _navigationService;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value;
                RaisePropertyChanged("Categories");
            }
        }
        public string ProfileImagePath
        {
            get { return _profileImagePath; }
            set { _profileImagePath = value;
                RaisePropertyChanged("ProfileImagePath");
            }
        }

        public string PresentationString
        {
            get { return _presentationString; }
            set {
                _presentationString = value;
                RaisePropertyChanged("PresentationString");
                    }
        }

        public User Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                RaisePropertyChanged("Owner");
            }
        }

        

        public ProfilePageViewModel(INavigationService navigationService)
        {
            Owner = UserDAO.GetCurrentUser();
            Categories = new ObservableCollection<Category>(CategoryDAO.GetAllCategories());
            PresentationString = Owner.FirstName + " " + Owner.LastName + "," + Owner.Age + " ans";
            ProfileImagePath = Owner.ProfileImagePath;
            _navigationService = navigationService;
        }

        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private bool _isPaneOpen;

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                RaisePropertyChanged("IsPaneOpen");
            }
        }



        public ICommand GoToProfileCommand
        {
            get
            {
                if (this._goToProfileCommand == null)
                {
                    this._goToProfileCommand = new RelayCommand(() => GoToProfile());
                }
                return this._goToProfileCommand;
            }
        }

        public ICommand GoToSearchEventCommand
        {
            get
            {
                if (this._goToSearchEventCommand == null)
                {
                    this._goToSearchEventCommand = new RelayCommand(() => GoToSearchEvent());
                }
                return this._goToSearchEventCommand;
            }
        }

        public ICommand GoToCreateEventCommand
        {
            get
            {
                if (this._goToCreateEventCommand == null)
                {
                    this._goToCreateEventCommand = new RelayCommand(() => GoToCreateEvent());
                }
                return this._goToCreateEventCommand;
            }
        }
        public ICommand CloseOpenPaneCommand
        {
            get
            {
                if (this._closeOpenPaneCommand == null)
                {
                    this._closeOpenPaneCommand = new RelayCommand(() => CloseOpenPane());
                }
                return this._closeOpenPaneCommand;
            }
        }


        public void GoToProfile()
        {
            _navigationService.NavigateTo("ProfilePage");
        }

        public void GoToSearchEvent()
        {
            _navigationService.NavigateTo("SearchEventPage");
        }

        public void GoToCreateEvent()
        {
            _navigationService.NavigateTo("CreateEventPage");
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
    }
}
