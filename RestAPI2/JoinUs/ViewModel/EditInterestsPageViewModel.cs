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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class EditInterestsPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /*private INavigationService _navigationService;
        private ICommand _editInterestsCommand;
        private ICommand _goBackCommand;
        private User _currentUser;
        private List<Category> userInterests;
        private List<GridCellModel> userInterestsToConvert;
        private ObservableCollection<GridCellModel> _interestsList;
        private List<Category> _chosenCategories;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentUser = (User)e.Parameter;
            userInterests = (List<Category>)CategoryDAO.GetAllCategories();
            userInterestsToConvert = new List<GridCellModel>();
            foreach (var interest in userInterests)
            {
                GridCellModel elementToAdd = new GridCellModel(interest);
                userInterestsToConvert.Add(elementToAdd);
            }
            _interestsList = new ObservableCollection<GridCellModel>(userInterestsToConvert);
        }

        public ObservableCollection<GridCellModel> InterestsList
        {
            get
            {
                return _interestsList;
            }
            set
            {
                _interestsList = value;
                RaisePropertyChanged("InterestsList");
            }
        }
        
        public ICommand EditInterestsCommand
        {
            get
            {
                if(this._editInterestsCommand == null)
                {
                    this._editInterestsCommand = new RelayCommand(() => EditInterests());
                }
                return this._editInterestsCommand;
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
            _navigationService.NavigateTo("ProfilePage", _currentUser);
        }

        public void EditInterests()
        {
            _chosenCategories = new List<Category>();
            foreach(var category in _interestsList)
            {
                if(category.IsChecked)
                {
                    _chosenCategories.Add(category.RepresentedCategory);
                }
            }
            CategoryDAO.UpdateUserInterests(_currentUser, _chosenCategories);
            _navigationService.NavigateTo("ProfilePage",_currentUser);
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

    public class GridCellModel
    {
        /*public Category RepresentedCategory { get; set; }
        public bool IsChecked { get; set; }
        public GridCellModel(Category representedCategory)
        {
            RepresentedCategory = representedCategory;
            IsChecked = true;
        }*/
    }
}
