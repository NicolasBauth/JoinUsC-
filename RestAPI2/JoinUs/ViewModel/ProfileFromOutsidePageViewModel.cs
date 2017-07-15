using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class ProfileFromOutsidePageViewModel: ViewModelBase, INotifyPropertyChanged
    {
        /*private string _profileImagePath;
        private ObservableCollection<Category> _categories;
        private string _presentationString;
        private User _owner;
        private User _currentUser;
        private INavigationService _navigationService;
        private ICommand _goBackCommand;
        private ICommand _subscribeCommand;
        


        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ProfileFromOutsidePayload payload = (ProfileFromOutsidePayload)e.Parameter;
            Owner = payload.VisitedUser;
            _currentUser = payload.CurrentUser;
            Categories = new ObservableCollection<Category>(Owner.Interests);
            PresentationString = Owner.FirstName + " " + Owner.LastName + "," + Owner.Age + " ans";
            ProfileImagePath = Owner.ProfileImagePath;
        }
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged("Categories");
            }
        }
        public string ProfileImagePath
        {
            get { return _profileImagePath; }
            set
            {
                _profileImagePath = value;
                RaisePropertyChanged("ProfileImagePath");
            }
        }

        public string PresentationString
        {
            get { return _presentationString; }
            set
            {
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

        public ICommand GoBackCommand
        {
            get
            {
                if(this._goBackCommand == null)
                {
                    this._goBackCommand = new RelayCommand(() => GoBack());
                }
                return this._goBackCommand;
            }
            
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public ICommand SubscribeCommand
        {
            get
            {
                if(this._subscribeCommand == null)
                {
                    this._subscribeCommand = new RelayCommand(() => Subscribe());
                }
                return this._subscribeCommand;
            }
        }
        public void Subscribe()
        {

        }


        public ProfileFromOutsidePageViewModel(INavigationService navigationService)
        {
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
            _navigationService.NavigateTo("ProfilePage", Owner);
        }

        public void GoToSearchEvent()
        {
            _navigationService.NavigateTo("SearchEventPage", Owner);
        }

        public void GoToCreateEvent()
        {
            _navigationService.NavigateTo("CreateEventPage", Owner);
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        */
    }
}
