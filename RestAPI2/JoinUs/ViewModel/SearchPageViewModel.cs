using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.DAO;
using JoinUs.Model;
using JoinUs.StaticServices;
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
    public class SearchPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /*private INavigationService _navigationService;

        public SearchPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private ICommand _goToFoundEventListCommand;
        private ObservableCollection<CategoriesCheckBoxListViewModel> _categories;
        private User _currentUser;
        private int _searchRadius;
        private string _city;
        private string _tags;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentUser = (User)e.Parameter;
            
            List<Category> userInterests = (List<Category>)_currentUser.Interests;
            List<CategoriesCheckBoxListViewModel> categories = new List<CategoriesCheckBoxListViewModel>();
            foreach (var category in userInterests)
            {
                categories.Add(new CategoriesCheckBoxListViewModel(category.Title));
            }
            Categories = new ObservableCollection<CategoriesCheckBoxListViewModel>(categories);
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

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                RaisePropertyChanged("City");
            }
        }

        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                RaisePropertyChanged("Tags");
            }
        }


        public ICommand GoToFoundEventListCommand
        {
            get
            {
                if(_goToFoundEventListCommand == null)
                {
                    _goToFoundEventListCommand = new RelayCommand(() => GoToFoundEventList());
                }
                return _goToFoundEventListCommand;
            }
        }

        public void GoToFoundEventList()
        {
            List<string> categoriesNameToSearch = new List<string>();
            foreach(var category in Categories)
            {
                if(category.IsChecked)
                {
                    categoriesNameToSearch.Add(category.CategoryName);
                }
            }
            List<Category> categoriesToSearch = new List<Category>();
            foreach(var categoryName in categoriesNameToSearch)
            {
                categoriesToSearch.Add(new Category(categoryName,CategoryService.getIdOfCategoryName(categoryName)));
            }
            List<Event> matchingEvents = EventDAO.GetEventsAround(SearchRadius, categoriesToSearch);
            EventListPayload payloadToSend = new EventListPayload(_currentUser, matchingEvents);
            _navigationService.NavigateTo("EventListPage", payloadToSend);
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
        */
    }
}
