using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using JoinUs.Model.EventDTOs;
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
    public class EventListPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public EventListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private bool _isPaneOpen;
        private AuthenticatedUser _currentUser;
        private List<EventShortDTO> _eventsToDisplay;
        public ObservableCollection<EventShortDTO> eventsList;
        private EventShortDTO _selectedEvent;
        private ICommand _goToDescriptionOfEventCommand;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            EventListPayload payload= (EventListPayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            _eventsToDisplay = payload.EventsToDisplay;
            eventsList = new ObservableCollection<EventShortDTO>(_eventsToDisplay);
            SelectedEvent = null;
            
        }

        public ObservableCollection<EventShortDTO> EventsList
        {
            get
            {
                return eventsList;
            }
            set
            {
                eventsList = value;
                RaisePropertyChanged("EventsList");
            }
        }

        public EventShortDTO SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                RaisePropertyChanged("SelectedEvent");
            }
        }

        public ICommand GoToDescriptionOfEventCommand
        {
            get
            {
                if(_goToDescriptionOfEventCommand == null)
                {
                    _goToDescriptionOfEventCommand = new RelayCommand(async() =>await GoToDescriptionOfEvent());
                }
                return _goToDescriptionOfEventCommand;
            }
        }

        public async Task GoToDescriptionOfEvent()
        {
            if (SelectedEvent != null)
            {
                EventDescriptionPayload payloadToSend = new EventDescriptionPayload();
                Event describedEvent = await EventDAO.GetFullEventDescription(_selectedEvent.Id);
                if (describedEvent == null)
                {
                    ToastCenter.InformativeNotify("Erreur: évènement non existant", "Nous éprouvons des difficultés à charger les détails de l'évènement. L'évènement a peut-être été supprimé, ou votre connexion internet a été interrompue. Tentez de recharger la page.");
                }
                else
                {
                    payloadToSend.CurrentUser = _currentUser;
                    payloadToSend.EventToDisplay = describedEvent;
                    payloadToSend.IsCurrentUserParticipating = await UserDAO.IsUserParticipating(_currentUser.UserName, SelectedEvent.Id);
                    _navigationService.NavigateTo("EventDescriptionPage", payloadToSend);
                }
            }
            else
            {
                ToastCenter.InformativeNotify("Sélectionnez un évènement", "Si vous souhaitez consulter les détails d'un évènement, sélectionnez le dans la liste avant d'appuyer sur le bouton");
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
