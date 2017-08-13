using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using JoinUs.Model.UserDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class EventDescriptionPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private string _title;
        private string _address;
        private DateTime _eventDate;
        private string _description;
        private string _numberOfParticipantsString;
        private string _creatorString;
        private string _facebookUrl;
        private string _participateButtonText;
        private ICommand _goToFacebookCommand;
        private ICommand _participateCommand;
        private ICommand _goToCreatorProfileCommand;
        private ICommand _showEventLocationCommand;
        private ICommand _deleteEventCommand;

        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private ICommand _goBackCommand;
        private bool _isPaneOpen;
        private AuthenticatedUser _currentUser;
        private Event _eventToDetail;
        private bool _isCurrentUserCreator;
        private List<Category> _categoriesOfEvent;
        private ObservableCollection<Category> _categoriesDisplay;
        private bool _isUserAlreadyParticipating;

        public EventDescriptionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            EventDescriptionPayload payload = (EventDescriptionPayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            _eventToDetail = payload.EventToDisplay;
            _title = EventToDetail.Title;
            _address = EventToDetail.Address;
            _eventDate = EventToDetail.Date;
            _description = EventToDetail.Description;
            _categoriesOfEvent = EventToDetail.Categories;
            _categoriesDisplay = new ObservableCollection<Category>(_categoriesOfEvent);         
            int numberOfParticipants = EventToDetail.ParticipantsCount;
            _numberOfParticipantsString = numberOfParticipants + " personne(s) participe(nt)";
            _creatorString = "Créé par " + EventToDetail.CreatorFirstName +" "+EventToDetail.CreatorLastName;
            _isCurrentUserCreator = (EventToDetail.CreatorUsername == _currentUser.UserName);
            _facebookUrl = EventToDetail.UrlFacebook;
            _isUserAlreadyParticipating = payload.IsCurrentUserParticipating;
        }

        

        public ObservableCollection<Category> CategoriesDisplay
        {
            get
            {
                return _categoriesDisplay;
            }
            set
            {
                _categoriesDisplay = value;
                RaisePropertyChanged("CategoriesDisplay");
            }
        }
        public string AdminButtonsVisibility
        {
            get
            {
                if(_isCurrentUserCreator)
                {
                    return "Visible";
                }
                return "Collapsed";
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                RaisePropertyChanged("Adress");
            }
        }
        public string EventDate
        {
            get
            {
                string eventDateString="Le ";
                eventDateString += _eventDate.Day;
                eventDateString += " ";
                eventDateString += GetMonthName(_eventDate.Month);
                eventDateString += " de l'année ";
                eventDateString += _eventDate.Year;
                return eventDateString;
            }
            
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }


        public string NumberOfParticipantsString
        {
            get
            {
                return _numberOfParticipantsString;
            }
            set
            {
                _numberOfParticipantsString = value;
                RaisePropertyChanged("NumberOfParticipantsString");
            }
        }

        public string CreatorString
        {
            get
            {
                return _creatorString;
            }
            set
            {
                _creatorString = value;
                RaisePropertyChanged("CreatorString");
            }
        }

        public string FacebookUrl
        {
            get
            {
                return _facebookUrl;
            }
            set
            {
                _facebookUrl = value;
                RaisePropertyChanged("FacebookUrl");
            }
        }

        public string ParticipateButtonText
        {
            get
            {
                if(_isUserAlreadyParticipating)
                {
                    return "Se désinscrire";
                }
                else
                {
                    return "S'inscrire";
                }
            }
            
        }

        public Event EventToDetail
        {
            get
            {
                return _eventToDetail;
            }
            set
            {
                _eventToDetail = value;
                RaisePropertyChanged("EventToDetail");
            }
        }

        public ICommand GoToFacebookCommand
        {
            get
            {
                if(this._goToFacebookCommand == null)
                {
                    this._goToFacebookCommand = new RelayCommand(() => GoToFacebook());
                }
                return this._goToFacebookCommand;
            }
        }

        public async void GoToFacebook()
        {
            if (_facebookUrl != null)
            {
                Uri uri = null;
                if (!Uri.TryCreate(_facebookUrl, UriKind.Absolute, out uri) || null == uri)
                {
                    ToastCenter.InformativeNotify("L'évènement Facebook n'existe plus", "L'adresse de l'évènement facebook renseignée par le créateur n'existe plus");
                }
                else
                {
                    await Launcher.LaunchUriAsync(uri);
                }
            }
            else
            {
                ToastCenter.InformativeNotify("Pas d'évènement Facebook", "Il n'existe pas d'évènement Facebook pour cet évènement!");
            }
        }

        public ICommand ParticipateCommand
        {
            get
            {
                if(this._participateCommand == null)
                {
                    _participateCommand = new RelayCommand(async() => await Participate());
                }
                return _participateCommand;
            }
        }

        public async Task Participate()
        {
            if (DateTime.Compare(_eventToDetail.Date, DateTime.Now) > 0)
            {
                bool succeeded;
                if (_isUserAlreadyParticipating)
                {
                    succeeded = await UserDAO.CancelParticipationToEvent(_currentUser.UserName, _eventToDetail.DbId);
                }
                else
                {
                    succeeded = await UserDAO.ParticipateToEvent(_currentUser.UserName, _eventToDetail.DbId);
                }
                if (succeeded)
                {
                    _navigationService.GoBack();
                }
            }
            else
            {
                ToastCenter.InformativeNotify("Inscription/Désinscription impossible", "Malheureusement, il semble que vous soyez en retard à la fête... cet évènement est déjà passé!");
            }
        }

        public string GetMonthName(int indiceMois)
        {
            string monthName = "";
            switch(indiceMois)
            {
                case 1:
                    monthName = "Janvier";
                    break;
                case 2:
                    monthName = "Février";
                    break;
                case 3:
                    monthName = "Mars";
                    break;
                case 4:
                    monthName = "Avril";
                    break;
                case 5:
                    monthName = "Mai";
                    break;
                case 6:
                    monthName = "Juin";
                    break;
                case 7:
                    monthName = "Juillet";
                    break;
                case 8:
                    monthName = "Août";
                    break;
                case 9:
                    monthName = "Septembre";
                    break;
                case 10:
                    monthName = "Octobre";
                    break;
                case 11:
                    monthName = "Novembre";
                    break;
                case 12:
                    monthName = "Décembre";
                    break;
            }
            return monthName;
        }
        public ICommand GoToCreatorProfileCommand
        {
            get
            {
                if(_goToCreatorProfileCommand == null)
                {
                    _goToCreatorProfileCommand = new RelayCommand(async() =>await GoToCreatorProfile());
                }
                return _goToCreatorProfileCommand;
            }
            
        }

        public async Task GoToCreatorProfile()
        {
            ProfilePagePayload payloadToSend= new ProfilePagePayload();
            Model.User creatorProfile = await UserDAO.LoadUserProfileAsync(EventToDetail.CreatorUsername);
            payloadToSend.CurrentUser = _currentUser;
            payloadToSend.ProfileOwner = creatorProfile;
            _navigationService.NavigateTo("ProfilePage", payloadToSend);
        }

        public ICommand ShowEventLocationCommand
        {
            get
            {
                if(this._showEventLocationCommand == null)
                {
                    _showEventLocationCommand = new RelayCommand(() => ShowEventLocation());
                }
                return _showEventLocationCommand;
            }
        }
        public void ShowEventLocation()
        {
            EventPositionPagePayload payloadToSend = new EventPositionPagePayload();
            payloadToSend.CurrentUser = _currentUser;
            payloadToSend.Title = EventToDetail.Title;
            payloadToSend.Latitude = EventToDetail.Latitude;
            payloadToSend.Longitude = EventToDetail.Longitude;
            _navigationService.NavigateTo("EventPositionPage", payloadToSend);
        }

        public ICommand DeleteEventCommand
        {
            get
            {
                if(this._deleteEventCommand == null)
                {
                    _deleteEventCommand = new RelayCommand(async() =>await DeleteEvent());
                }
                return _deleteEventCommand;
            }
        }
        public async Task DeleteEvent()
        {
            if(!_isCurrentUserCreator)
            {
                ToastCenter.InformativeNotify("Pas les droits", "Vous ne pouvez pas supprimer un évènement dont vous n'êtes pas le créateur.");
            }
            else
            {
                bool succeeded = await EventDAO.DeleteEventAsync(EventToDetail.DbId);
                if(succeeded)
                {
                    ToastCenter.InformativeNotify("Suppression réussie", "La suppression de l'évènement s'est déroulée correctement.");
                    _navigationService.NavigateTo("MainPage", _currentUser);
                }
                else
                {
                    ToastCenter.InformativeNotify("Suppression échouée", "La suppression a échoué... tentez de rafraichir la page et vérifiez votre connexion internet.");
                }

            }
        }
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                RaisePropertyChanged("IsPaneOpen");
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
