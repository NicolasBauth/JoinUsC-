using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.Model;
using JoinUs.StaticServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class LocateCenterPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public LocateCenterPageViewModel(INavigationService navigationService)
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
        private Geopoint _scanCenter;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            LocateCenterPayload payload = (LocateCenterPayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            _mapCenter = payload.MapCenter;
        }
        
        public async Task LocateCenterMapControl1_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {
            Geopoint tappedGeoPosition = args.Location;
            _scanCenter = tappedGeoPosition;
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
                if (this._cancelCommand == null)
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
                if (this._validateCommand == null)
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
            ToastCenter.InformativeNotify("Localisation du centre annulée", "La localisation du centre de scan est annulée");
            _navigationService.NavigateTo("MainPage", _currentUser);
        }

        public void Validate()
        {
            if (_scanCenter == null)
            {
                ToastCenter.InformativeNotify("Localisation manquante", "Veuillez localiser votre évènement sur la carte en appuyant ou en cliquant dessus.");
            }
            else
            {
                SearchPagePayload payload = new SearchPagePayload();
                payload.CurrentUser = _currentUser;
                payload.ScanCenter = new LocationInformation();
                payload.ScanCenter.Address = _eventAddress;
                payload.ScanCenter.Location = _scanCenter;
                _navigationService.NavigateTo("SearchEventPage", payload);
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
