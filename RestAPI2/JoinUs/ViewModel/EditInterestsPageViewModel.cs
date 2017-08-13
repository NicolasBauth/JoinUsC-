using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class EditInterestsPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ICommand _editInterestsCommand;
        private ICommand _goBackCommand;
        private AuthenticatedUser _currentUser;
        private List<Category> userInterests;
        private List<Category> allCategories;
        private List<GridCellModel> allCategoriesGrid;
        private ObservableCollection<GridCellModel> allCategoriesVisibleGrid;
        private List<Category> _chosenCategories;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            EditInterestPayload payload = (EditInterestPayload)e.Parameter;
            allCategories = payload.AllCategories;
            _currentUser = payload.CurrentUser;
            userInterests = _currentUser.Interests;

            allCategoriesGrid = new List<GridCellModel>();
            foreach (var category in allCategories)
            {
                GridCellModel elementToAdd = new GridCellModel(category);
                allCategoriesGrid.Add(elementToAdd);
            }
            foreach (var interest in userInterests)
            {
                foreach (var category in allCategoriesGrid)
                {
                    if (interest.Title == category.RepresentedCategory.Title)
                    {
                        category.IsChecked = true;
                        break;
                    }
                }
            }
            allCategoriesVisibleGrid = new ObservableCollection<GridCellModel>(allCategoriesGrid);
        }

        public ObservableCollection<GridCellModel> AllCategoriesVisibleGrid
        {
            get
            {
                return allCategoriesVisibleGrid;
            }
            set
            {
                allCategoriesVisibleGrid = value;
                RaisePropertyChanged("AllCategoriesVisibleGrid");
            }
        }

        public ICommand EditInterestsCommand
        {
            get
            {
                if (this._editInterestsCommand == null)
                {
                    this._editInterestsCommand = new RelayCommand(async () => await EditInterests());
                }
                return this._editInterestsCommand;
            }
        }

        public ICommand GoBackCommand
        {
            get
            {
                if (this._goBackCommand == null)
                {
                    this._goBackCommand = new RelayCommand(() => GoBack());
                }
                return this._goBackCommand;
            }
        }

        public void GoBack()
        {
            ProfilePagePayload payloadToSend = new ProfilePagePayload();
            payloadToSend.CurrentUser = _currentUser;
            _navigationService.NavigateTo("ProfilePage", payloadToSend);
        }

        public async Task EditInterests()
        {
            _chosenCategories = new List<Category>();
            foreach (var category in AllCategoriesVisibleGrid)
            {
                if (category.IsChecked)
                {
                    _chosenCategories.Add(category.RepresentedCategory);
                }
            }
            _currentUser = await CategoryDAO.UpdateUserInterests(_currentUser, _chosenCategories);
            ProfilePagePayload payloadToSend = new ProfilePagePayload();
            payloadToSend.CurrentUser = _currentUser;
            _navigationService.NavigateTo("ProfilePage", payloadToSend);
            ToastCenter.InformativeNotify("Intérêts édités", "Intérêts édités avec succès");
        }


        public EditInterestsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            _navigationService.NavigateTo("CreateEventPage", payload);
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }

    }

    public class GridCellModel
    {
        public Category RepresentedCategory { get; set; }
        public bool IsChecked { get; set; }
        public GridCellModel(Category representedCategory)
        {
            RepresentedCategory = representedCategory;
            IsChecked = false;
        }
    }
}
