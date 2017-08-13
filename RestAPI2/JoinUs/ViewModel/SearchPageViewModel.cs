using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using JoinUs.StaticServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class SearchPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public SearchPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private ICommand _launchScanCommand;
        private ICommand _scanFromGivenPositionCommand;
        private ObservableCollection<CategoriesCheckBoxListViewModel> _categories;
        private AuthenticatedUser _currentUser;
        private int _searchRadius;
        private Geopoint _scanCenter;
        private string _address;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            SearchPagePayload payload =(SearchPagePayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            if (!(payload.ScanCenter == null))
            {
                ScanCenter = payload.ScanCenter.Location;
                Address = payload.ScanCenter.Address;
            }
            
            
             List<Category> userInterests = _currentUser.Interests;
             List<CategoriesCheckBoxListViewModel> categories = new List<CategoriesCheckBoxListViewModel>();
             foreach (var category in userInterests)
             {
                categories.Add(new CategoriesCheckBoxListViewModel(category.Title));
             }
             Categories = new ObservableCollection<CategoriesCheckBoxListViewModel>(categories);
            
        }
        
        public Geopoint ScanCenter
        {
            get
            {
                return _scanCenter;
            }
            set
            {
                _scanCenter = value;
                RaisePropertyChanged("ScanCenter");
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
                RaisePropertyChanged("Address");
            }
        }
        public ObservableCollection<CategoriesCheckBoxListViewModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged("Categories");
            }
        }
        

        public int SearchRadius
        {
            get
            {
                return _searchRadius;
            }
            set
            {
                _searchRadius = value;
                RaisePropertyChanged("SearchRadius");
            }
        }
        public string FeatureDescription
        {
            get
            {
                return "Sélectionnez le rayon de scan (en km) \n puis les catégories que vous voulez utiliser \n comme filtres.\n  Si vous ne sélectionnez pas de catégorie,\n aucun filtre ne sera appliqué.\nCliquez sur le bouton ci-dessous pour\n définir le centre du scan.";
            }
        }
        public string LocateButtonTitle
        {
            get
            {
                if(_scanCenter == null)
                {
                    return "Définir le centre du scan";
                }
                return "Modifier le centre du scan";
            }
        }

        public ICommand ScanFromGivenPositionCommand
        {
            get
            {
                if (this._scanFromGivenPositionCommand == null)
                {
                    this._scanFromGivenPositionCommand = new RelayCommand(async() => await ScanFromGivenPosition());
                }
                return this._scanFromGivenPositionCommand;
            }
        }

        public async Task ScanFromGivenPosition()
        {
            try
            {
                BasicGeoposition mapcenterPosition = new BasicGeoposition();
                mapcenterPosition.Latitude = 50.40;
                mapcenterPosition.Longitude = 4.45;
                Geopoint mapCenter = new Geopoint(mapcenterPosition);
                LocateCenterPayload payloadToSend = new LocateCenterPayload();
                payloadToSend.CurrentUser = _currentUser;
                payloadToSend.MapCenter = mapCenter;
                
                _navigationService.NavigateTo("LocateCenterPage", payloadToSend);
            }
            catch(UnauthorizedAccessException)
            {
                ToastCenter.InformativeNotify("Veuillez accepter la localisation", "Cette application nécessite d'accéder à votre position pour fonctionner correctement. Veuillez autoriser l'application JoinUs à accéder à votre position dans la fenêtre qui vient de s'ouvrir.");
                await Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
            }
        }

        public ICommand LaunchScanCommand
        {
            get
            {
                if(this._launchScanCommand == null)
                {
                    this._launchScanCommand = new RelayCommand(async() => await LaunchScan());
                }
                return this._launchScanCommand;
            }
        }
        public async Task LaunchScan()
         {
            if (_scanCenter == null)
            {
                ToastCenter.InformativeNotify("Scan impossible", "Vous ne pouvez pas lancer le scan maintenant : définissez d'abord le centre du scan");
            }
            else
            {
                if(SearchRadius == 0)
                {
                    SearchRadius = 1;
                }
                List<string> selectedCategoriesNames = new List<string>();
                foreach(var categoryCheckBox in _categories)
                {
                    if(categoryCheckBox.IsChecked)
                    {
                        selectedCategoriesNames.Add(categoryCheckBox.CategoryName);
                    }
                }
                List<EventShortDTO> foundEvents = await EventDAO.GetEventsAroundPoint(_scanCenter, _searchRadius,selectedCategoriesNames);
                if (foundEvents.Count == 0)
                {
                    ToastCenter.InformativeNotify("Pas d'évènement trouvé", "Il semble qu'aucun évènement ne se trouve dans cette zone...");
                }
                else
                {
                    EventListPayload payloadToSend = new EventListPayload(_currentUser, foundEvents);
                    _navigationService.NavigateTo("EventListPage", payloadToSend);
                }
            }

        }
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
            _navigationService.NavigateTo("SearchEventPage",payloadToSend);
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
