using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.Model;
using JoinUs.StaticServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class LocateEventsPageViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private INavigationService _navigationService;

        public LocateEventsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }


        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private ICommand _cancelCommand;
        private ICommand _validateCommand;
        private bool _isPaneOpen;
        private AuthenticatedUser _currentUser;
        private string _eventAddress;
        private Geopoint _mapCenter;
        private Geopoint _eventPosition;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            LocateEventPayload payload = (LocateEventPayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            _mapCenter = payload.UserCoordinates;
        }
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }
        public async Task LocateEventMapControl1_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {
            Geopoint tappedGeoPosition = args.Location;
            EventPosition = tappedGeoPosition;
            MapIcon eventPushpin = new MapIcon();
            eventPushpin.Location = tappedGeoPosition;
            eventPushpin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customPushpin.png"));
            eventPushpin.NormalizedAnchorPoint = new Point(0.5, 1.0);
            _eventAddress = await EventService.ReverseGeocodePoint(eventPushpin.Location);
            eventPushpin.Title = _eventAddress;
            if (!(sender.MapElements.Count == 0))
            {
                sender.MapElements.Clear();
            }
            sender.MapElements.Add(eventPushpin);

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
        public Geopoint EventPosition
        {
            get { return _eventPosition; }
            set
            {
                _eventPosition = value;
                RaisePropertyChanged("EventPosition");
            }
        }
        public Geopoint MapCenter
        {
            get
            {
                return _mapCenter;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if(this._cancelCommand == null)
                {
                    this._cancelCommand = new RelayCommand(() => Cancel());
                }
                return this._cancelCommand;
            }
        }
        public ICommand ValidateCommand
        {
            get
            {
                if(this._validateCommand == null)
                {
                    this._validateCommand = new RelayCommand(() => Validate());
                }
                return this._validateCommand;
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

        public void Cancel()
        {
            ToastCenter.InformativeNotify("Création annulée", "La création de l'évènement est annulée");
            _navigationService.NavigateTo("MainPage", _currentUser);
        }

        public void Validate()
        {
            if(_eventPosition == null)
            {
                ToastCenter.InformativeNotify("Localisation manquante", "Veuillez localiser votre évènement sur la carte en appuyant ou en cliquant dessus.");
            }
            else
            {
                CreateEventPagePayload payload = new CreateEventPagePayload();
                payload.CurrentUser = _currentUser;
                payload.EventLocation = new LocationInformation();
                payload.EventLocation.Location = _eventPosition;
                payload.EventLocation.Address = _eventAddress;
                _navigationService.NavigateTo("CreateEventPage", payload);
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
