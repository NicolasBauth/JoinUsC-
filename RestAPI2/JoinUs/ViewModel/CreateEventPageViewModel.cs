using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class CreateEventPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _nameText;
        private DateTimeOffset _date;
        private TimeSpan _time;
        private string _descriptionText;
        private ObservableCollection<CategoriesCheckBoxListViewModel> _interests;
        private string _facebookLink;
        private ICommand _createCommand;
        private ICommand _locateEventCommand;
        private INavigationService _navigationService;
        private AuthenticatedUser _currentUser;
        private Geopoint _eventLocation;
        private string _eventAddress;
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CreateEventPagePayload payload = (CreateEventPagePayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            if(payload.EventLocation == null)
            {
                if(_eventLocation == null)
                {
                    EventAddress = "Pas d'adresse";
                }
            }
            else
            {
                _eventLocation = payload.EventLocation.Location;
                EventAddress = payload.EventLocation.Address;
            }
            List<Category> userInterests = (List<Category>)_currentUser.Interests;
            List<CategoriesCheckBoxListViewModel> categories = new List<CategoriesCheckBoxListViewModel>();
            foreach (var category in userInterests)
            {
                categories.Add(new CategoriesCheckBoxListViewModel(category.Title));
            }
            Interests = new ObservableCollection<CategoriesCheckBoxListViewModel>(categories);
        }

        public string NameText
        {
            get { return _nameText; }
            set
            {
                _nameText = value;
                RaisePropertyChanged("NameText");
            }
        }

        public string LocateButtonText
        {
            get
            {
                if(_eventLocation == null)
                {
                    return "Localisez!";
                }
                else
                {
                    return "Modifier...";
                }
            }
        }
        public string EventAddress
        {
            get
            {
                return _eventAddress;
            }
            set
            {
                _eventAddress = value;
                RaisePropertyChanged("EventAddress");
            }
        }

        public DateTimeOffset Date
        {
            get
            {
                if(DateTimeOffset.Compare(_date,DateTimeOffset.Now)<0)
                {
                    return DateTimeOffset.Now;
                }
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }
        
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged("Time");
            }
        }

        public string DescriptionText
        {
            get { return _descriptionText; }
            set
            {
                _descriptionText = value;
                RaisePropertyChanged("DescriptionText");
            }
        }

        public string FacebookLink
        {
            get { return _facebookLink; }
            set
            {
                _facebookLink = value;
                RaisePropertyChanged("FacebookLink");
            }
        }

        

        public ObservableCollection<CategoriesCheckBoxListViewModel> Interests
        {
            get { return _interests; }
            set
            {
                _interests = value;
                RaisePropertyChanged("Interests");
            }
        }

        public ICommand CreateCommand
        {
            get
            {
                if (this._createCommand == null)
                {
                    this._createCommand = new RelayCommand(async () => await CreateEvent());
                }
                return this._createCommand;
            }
        }

        public ICommand LocateEventCommand
        {
            get
            {
                if (this._locateEventCommand == null)
                {
                    this._locateEventCommand = new RelayCommand(() =>LocateEvent());
                }
                return this._locateEventCommand;
            }
        }
        public CreateEventPageViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
        }

        private bool CanExecute()
        {
            int selectedCategoriesCount = 0;
            foreach (var category in Interests)
            {
                if(category.IsChecked)
                {
                    selectedCategoriesCount++;
                }
            }
            if (NameText != null && Date != null && Time != null
                && DescriptionText != null && _eventLocation != null && selectedCategoriesCount != 0)
            {
                if (DateTimeOffset.Compare(_date, DateTimeOffset.Now) < 0)
                {
                    ToastCenter.InformativeNotify("Date de l'évènement dans le passé", "Il semble que la date de votre évènement soit dans le passé...");
                    return false;
                }
                    if (_facebookLink != null)
                    {
                        Uri uriResult;
                        bool result = Uri.TryCreate(_facebookLink, UriKind.Absolute, out uriResult) && (Uri.CheckSchemeName(uriResult.Scheme));
                        if (result)
                        {
                            if (uriResult.Host == "www.facebook.com")
                            {
                                return true;
                            }
                            else
                            {
                                ToastCenter.InformativeNotify("URL invalide", "Le lien de l'évènement facebook spécifié n'est pas un lien facebook...");
                                return false;
                            }
                        }
                        else
                        {
                            ToastCenter.InformativeNotify("URL invalide", "l'URL facebook spécifiée n'est pas une URL valide.");
                            return false;
                        }
                    }
                    return true;
            }
            ToastCenter.InformativeNotify("Mauvais formulaire", "Des champs obligatoires ne sont pas remplis. Seuls le lien facebook est optionnel. N'oubliez pas de cocher au moins une catégorie.");
            return false;
        }
        private async Task CreateEvent()
        {
            Event eventToCreate = new Event();

            if (CanExecute())
            {
                List<string> selectedCategories = new List<string>();
                List<Category> categoriesToCommit = new List<Category>();
                foreach (var categoryCheckBox in Interests)
                {
                    if (categoryCheckBox.IsChecked)
                    {
                        selectedCategories.Add(categoryCheckBox.CategoryName);
                    }
                }
                if (selectedCategories.Count() == 0)
                {
                    categoriesToCommit.Add(new Category("Autres"));
                }
                else
                {
                    int index = 0;
                    foreach (var categoryName in selectedCategories)
                    {
                        categoriesToCommit.Add(new Category(selectedCategories.ElementAt(index)));
                        index++;
                    }
                }
                eventToCreate.Address = _eventAddress;
                eventToCreate.Latitude = _eventLocation.Position.Latitude;
                eventToCreate.Longitude = _eventLocation.Position.Longitude;
                eventToCreate.Categories = categoriesToCommit;
                eventToCreate.Description = DescriptionText;
                eventToCreate.Title = NameText;
                eventToCreate.UrlFacebook = FacebookLink;
                DateTime dateAndTimeOfEvent;
                DateTime dateOfEvent = Date.Date;
                TimeSpan timeOfEvent = Time;
                dateAndTimeOfEvent = dateOfEvent.Add(Time);
                eventToCreate.Date = dateAndTimeOfEvent;
                bool result = await EventDAO.CreateEventAsync(eventToCreate, _currentUser);
                if (result)
                {
                    _navigationService.NavigateTo("MainPage", _currentUser);
                    ToastCenter.InformativeNotify("Succès de l'enregistrement!", "L'évènement a été créé avec succès!");
                    NameText = null;
                    DescriptionText = null;
                    FacebookLink = null;

                }
                else
                {
                    ToastCenter.InformativeNotify("Oups!", "Quelque chose s'est mal passé lors de la tentative de création de l'évènement. Vérifiez votre connexion internet.");
                }
            }
            

        }
        public async void LocateEvent()
        {
            try
            {
                BasicGeoposition mapcenterPosition = new BasicGeoposition();
                mapcenterPosition.Latitude = 50.40;
                mapcenterPosition.Longitude = 4.45;
                Geopoint mapCenter = new Geopoint(mapcenterPosition);
                LocateEventPayload locateEventPayload = new LocateEventPayload();
                locateEventPayload.CurrentUser = _currentUser;
                locateEventPayload.UserCoordinates = mapCenter;
                _navigationService.NavigateTo("LocateEventPage", locateEventPayload);
            }
            catch (UnauthorizedAccessException)
            {
                ToastCenter.InformativeNotify("Fermeture de l'application", "Cette application nécessite d'accéder à votre position pour fonctionner correctement. Dans la fenêtre qui vient de s'ouvrir, veuillez activer la géolocalisation pour l'application JoinUs");
                await Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
            }
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
            payload.EventLocation = new LocationInformation();
            payload.EventLocation.Address = _eventAddress;
            payload.EventLocation.Location = _eventLocation;
            _navigationService.NavigateTo("CreateEventPage", payload);
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }

    }
    public class CategoriesCheckBoxListViewModel
    {
        public string CategoryName { get; set; }
        public bool IsChecked { get; set; }

        public CategoriesCheckBoxListViewModel(string categoryName)
        {
            CategoryName = categoryName;
            IsChecked = false;
        }

    }

}







