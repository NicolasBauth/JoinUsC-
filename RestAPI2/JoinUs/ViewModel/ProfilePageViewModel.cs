using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class ProfilePageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _profileImagePath;
        private ObservableCollection<Category> _categories;
        private string _presentationString;
        private User _owner;
        private AuthenticatedUser _currentUser;
        private INavigationService _navigationService;
        private ICommand _goToEditInterestsCommand;
        private ICommand _browseEventsCreatedCommand;
        private ICommand _browseEventsParticipatingCommand;

        public ICommand BrowseEventsCreatedCommand
        {
            get
            {
                if (_browseEventsCreatedCommand == null)
                {
                    _browseEventsCreatedCommand = new RelayCommand(async () => await BrowseEventsCreated());
                }
                return _browseEventsCreatedCommand;
            }
        }
        public ICommand BrowseEventsParticipatingCommand
        {
            get
            {
                if (_browseEventsParticipatingCommand == null)
                {
                    _browseEventsParticipatingCommand = new RelayCommand(async () => await BrowseEventsParticipating());
                }
                return _browseEventsParticipatingCommand;
            }
        }
        public bool IsCurrentUserOwnerOfProfile
        {
            get
            {
                return (_currentUser.UserName == _owner.UserName);
            }
        }
        public string AdminButtonsVisibility
        {
            get
            {
                if(IsCurrentUserOwnerOfProfile)
                {
                    return "Visible";
                }
                return "Collapsed";
            }
        }
        public string CreatedEventsButtonText
        {
            get
            {
                if(IsCurrentUserOwnerOfProfile)
                {
                    return "Vos évènements";
                }
                return "Ses évènements";
            }
        }
        public string ParticipatingEventsButtonText
        {
            get
            {
                if(IsCurrentUserOwnerOfProfile)
                {
                    return "Vos participations";
                }
                return "Ses participations";
            }
        }

        public async Task BrowseEventsCreated()
        {
            List<EventShortDTO> eventsToDisplay = await EventDAO.GetAllEventsCreatedByUser(Owner.UserName);
            if(eventsToDisplay.Count != 0)
            {
                EventListPayload payloadToSend = new EventListPayload(_currentUser, eventsToDisplay);
                _navigationService.NavigateTo("EventListPage", payloadToSend);
            }
            else
            {
                ToastCenter.InformativeNotify("Pas d'évènements", "Aucun évènement créé n'est associé à ce profil. Si vous êtes certain qu'il s'agit d'une erreur, vérifiez votre connexion internet.");
            }       
        }

        public async Task BrowseEventsParticipating()
        {
            List<EventShortDTO> eventsToDisplay = await EventDAO.GetAllEventsUserParticipates(Owner.UserName);
            if (eventsToDisplay.Count != 0)
            {
                EventListPayload payloadToSend = new EventListPayload(_currentUser, eventsToDisplay);
                _navigationService.NavigateTo("EventListPage", payloadToSend);
            }
            else
            {
                ToastCenter.InformativeNotify("Pas d'évènements", "Aucune participation ne correspond à ce profil. Si vous êtes certain qu'il s'agit d'une erreur, vérifiez votre connexion internet.");
            }
        }

        public ICommand GoToEditInterestsCommand
        {
            get
            {
                if (_goToEditInterestsCommand == null)
                {
                    _goToEditInterestsCommand = new RelayCommand(async () => await GoToEditInterests());
                }
                return _goToEditInterestsCommand;
            }
        }

        public async Task GoToEditInterests()
        {
            if (IsCurrentUserOwnerOfProfile)
            {
                List<Category> allCategories = await CategoryDAO.GetAllCategories();
                EditInterestPayload payload = new EditInterestPayload();
                payload.AllCategories = allCategories;
                payload.CurrentUser = _currentUser;
                _navigationService.NavigateTo("EditInterestsPage", payload);
            }
            else
            {
                ToastCenter.InformativeNotify("Action refusée", "Seul le propriétaire du profil peut modifier les intérêts.");
            }
        }


        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ProfilePagePayload payload = (ProfilePagePayload)e.Parameter;
            if (payload.ProfileOwner == null)
            {
                User profile = new Model.User();
                profile.Birthdate = payload.CurrentUser.BirthDate;
                profile.FirstName = payload.CurrentUser.FirstName;
                profile.Interests = payload.CurrentUser.Interests;
                profile.LastName = payload.CurrentUser.LastName;
                profile.ProfileImagePath = payload.CurrentUser.ProfileImagePath;
                profile.UserName = payload.CurrentUser.UserName;
                Owner = profile;
            }
            else
            {
                Owner = payload.ProfileOwner;
            }
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



        public ProfilePageViewModel(INavigationService navigationService)
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
            ProfilePagePayload payloadToSend = new ProfilePagePayload();
            payloadToSend.CurrentUser = _currentUser;
            _navigationService.NavigateTo("ProfilePage", payloadToSend);
        }

        public void GoToSearchEvent()
        {
            SearchPagePayload payloadToSend = new SearchPagePayload();
            payloadToSend.CurrentUser = _currentUser;
            _navigationService.NavigateTo("SearchEventPage", payloadToSend);
        }

        public void GoToCreateEvent()
        {
            CreateEventPagePayload payload = new CreateEventPagePayload();
            payload.CurrentUser = _currentUser;
            _navigationService.NavigateTo("CreateEventPage", payload);
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }

    }
}
