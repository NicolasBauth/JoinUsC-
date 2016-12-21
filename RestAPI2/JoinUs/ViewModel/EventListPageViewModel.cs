using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
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
        private User _currentUser;
        private List<Event> _eventsToDisplay;
        public ObservableCollection<Event> eventsList;
        private Event _selectedEvent;
        private ICommand _goToDescriptionOfEventCommand;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            EventListPayload payload= (EventListPayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            _eventsToDisplay = payload.EventsToDisplay;
            eventsList = new ObservableCollection<Event>(_eventsToDisplay);
        }

        public ObservableCollection<Event> EventsList
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

        public Event SelectedEvent
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
                    _goToDescriptionOfEventCommand = new RelayCommand(() => GoToDescriptionOfEvent());
                }
                return _goToDescriptionOfEventCommand;
            }
        }

        public void GoToDescriptionOfEvent()
        {
            if (SelectedEvent != null)
            {
                EventDescriptionPayload payloadToSend = new EventDescriptionPayload(_currentUser, SelectedEvent);
                _navigationService.NavigateTo("EventDescriptionPage", payloadToSend);
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
            _navigationService.NavigateTo("ProfilePage",_currentUser);
        }

        public void GoToSearchEvent()
        {
            _navigationService.NavigateTo("SearchEventPage",_currentUser);
        }

        public void GoToCreateEvent()
        {
            _navigationService.NavigateTo("CreateEventPage",_currentUser);
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
    }
}
