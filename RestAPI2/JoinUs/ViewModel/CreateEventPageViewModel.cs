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
        private string _tagsText;
        private ICommand _createCommand;
        private ICommand _locateEventCommand;
        private INavigationService _navigationService;
        private AuthenticatedUser _currentUser;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentUser = (AuthenticatedUser)e.Parameter;
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



        public DateTimeOffset Date
        {
            get { return _date; }
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

        public string TagsText
        {
            get { return _tagsText; }
            set
            {
                _tagsText = value;
                RaisePropertyChanged("TagsText");
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
                    this._createCommand = new RelayCommand(() => LocateEvent());
                }
                return this._createCommand;
            }
        }
        public CreateEventPageViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
        }

        private bool CanExecute()
        {
            if (NameText != null && Date != null && Time != null
                && DescriptionText != null)
            {
                return true;
            }
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
                eventToCreate.Address = "77,Rue des Carmes, Namur";
                eventToCreate.Latitude = 50.467521;
                eventToCreate.Longitude = 4.863455;
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
                }
                else
                {
                    ToastCenter.InformativeNotify("Oups!", "Quelque chose s'est mal passé lors de la tentative de création de l'évènement. Vérifiez votre connexion internet et l'exactitude du formulaire.");
                }
            }
            else
            {
                ToastCenter.InformativeNotify("Mauvais formulaire", "Des champs obligatoires ne sont pas remplis. Seuls les tags, le lien facebook et les catégories sont optionnels.");
            }

        }
        public void LocateEvent()
        {
            _navigationService.NavigateTo("LocateEventPage", _currentUser);
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
            _navigationService.NavigateTo("ProfilePage", _currentUser);
        }

        public void GoToSearchEvent()
        {
            _navigationService.NavigateTo("SearchEventPage", _currentUser);
        }

        public void GoToCreateEvent()
        {
            _navigationService.NavigateTo("CreateEventPage", _currentUser);
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







