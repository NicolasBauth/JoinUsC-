using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.Model;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class EventPositionPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        public EventPositionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private ICommand _goBackCommand;
        private bool _isPaneOpen;
        private AuthenticatedUser _currentUser;
        private string _eventTitle;
        private Geopoint _eventLocation;
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            EventPositionPagePayload payload = (EventPositionPagePayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            EventTitle = payload.Title;
            BasicGeoposition eventPosition = new BasicGeoposition();
            eventPosition.Latitude = payload.Latitude;
            eventPosition.Longitude = payload.Longitude;
            EventLocation = new Geopoint(eventPosition);
            
        }
        public void ShowPushpinOnMap(Windows.UI.Xaml.Controls.Maps.MapControl sender)
        {
            MapIcon eventPushpin = new MapIcon();
            eventPushpin.Location = EventLocation;
            eventPushpin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customPushpin.png"));
            eventPushpin.NormalizedAnchorPoint = new Point(0.5, 1.0);
            eventPushpin.Title = EventTitle;
            if (!(sender.MapElements.Count == 0))
            {
                sender.MapElements.Clear();
            }
            sender.MapElements.Add(eventPushpin);
        }




        public string EventTitle
        {
            get
            {
                return _eventTitle;
            }
            set
            {
                _eventTitle = value;
                RaisePropertyChanged("EventTitle");
            }
        }
        
        public Geopoint EventLocation
        {
            get
            {
                return _eventLocation;
            }
            set
            {
                _eventLocation = value;
                RaisePropertyChanged("EventLocation");
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

















