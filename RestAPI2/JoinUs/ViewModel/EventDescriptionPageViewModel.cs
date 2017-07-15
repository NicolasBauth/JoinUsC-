using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JoinUs.AppToastCenter;
using JoinUs.DAO;
using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Navigation;

namespace JoinUs.ViewModel
{
    public class EventDescriptionPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /*private INavigationService _navigationService;
        private string _title;
        private string _adress;
        private DateTime _eventDate;
        private string _description;
        private string _tagsString;
        private string _firstCategoryImagePath;
        private string _numberOfParticipantsString;
        private string _creatorString;
        private string _facebookUrl;
        private string _participateButtonText;
        private ICommand _goToFacebookCommand;
        private ICommand _participateCommand;
        private ICommand _goToCreatorProfileCommand;
        public EventDescriptionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private ICommand _goToProfileCommand;
        private ICommand _goToSearchEventCommand;
        private ICommand _goToCreateEventCommand;
        private ICommand _closeOpenPaneCommand;
        private bool _isPaneOpen;
        private Model.User _currentUser;
        private Event _eventToDetail;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            EventDescriptionPayload payload = (EventDescriptionPayload)e.Parameter;
            _currentUser = payload.CurrentUser;
            _eventToDetail = payload.EventToDisplay;
            _title = EventToDetail.Title;
            _adress = EventToDetail.Adress;
            _eventDate = EventToDetail.Date;
            _description = EventToDetail.Description;
            _tagsString = "";
            if (EventToDetail.Categories != null)
            {
                Category firstCategory = EventToDetail.Categories.ElementAt(0);
                _firstCategoryImagePath = firstCategory.ImagePath;
            }
            if (EventToDetail.Tags!=null)
            {
                List<Tag> tags = (List<Tag>)EventToDetail.Tags;


                foreach (var tag in tags)
                {
                    _tagsString += tag.Name;
                }
            }
            int numberOfParticipants = EventToDetail.Participants.Count();
            _numberOfParticipantsString = numberOfParticipants + " personnes participent";
            _creatorString = "Créé par " + EventToDetail.Creator.UserName;
            _facebookUrl = EventToDetail.UrlFacebook;
            
            
        }

        public string FirstCategoryImagePath
        {
            get
            {
                return _firstCategoryImagePath;
            }
            set
            {
                _firstCategoryImagePath = value;
                RaisePropertyChanged("FirstCategoryImagePath");
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                RaisePropertyChanged("Adress");
            }
        }
        public DateTime EventDate
        {
            get
            {
                return _eventDate;
            }
            set
            {
                _eventDate = value;
                RaisePropertyChanged("EventDate");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string TagsString
        {
            get
            {
                return _tagsString;
            }
            set
            {
                _tagsString = value;
                RaisePropertyChanged("TagsString");
            }
        }

        public string NumberOfParticipantsString
        {
            get
            {
                return _numberOfParticipantsString;
            }
            set
            {
                RaisePropertyChanged("NumberOfParticipantsString");
            }
        }

        public string CreatorString
        {
            get
            {
                return _creatorString;
            }
            set
            {
                _creatorString = value;
                RaisePropertyChanged("CreatorString");
            }
        }

        public string FacebookUrl
        {
            get
            {
                return _facebookUrl;
            }
            set
            {
                _facebookUrl = value;
                RaisePropertyChanged("FacebookUrl");
            }
        }

        public string ParticipateButtonText
        {
            get
            {
                if(CheckIfUserIsAlreadyParticipating())
                {
                    _participateButtonText = "Se désinscrire";
                }
                else
                {
                    _participateButtonText = "S'inscrire";
                }
                return _participateButtonText;
            }
            set
            {
                _participateButtonText = value;
                RaisePropertyChanged("ParticipateButtonText");
            }
        }

        public Event EventToDetail
        {
            get
            {
                return _eventToDetail;
            }
            set
            {
                _eventToDetail = value;
                RaisePropertyChanged("EventToDetail");
            }
        }

        public ICommand GoToFacebookCommand
        {
            get
            {
                if(this._goToFacebookCommand == null)
                {
                    this._goToFacebookCommand = new RelayCommand(() => GoToFacebook());
                }
                return this._goToFacebookCommand;
            }
        }

        public async void GoToFacebook()
        {
            if (_facebookUrl != null)
            {
                Uri uri = null;
                if (!Uri.TryCreate(_facebookUrl, UriKind.Absolute, out uri) || null == uri)
                {
                    ToastCenter.InformativeNotify("L'évènement Facebook n'existe plus", "L'adresse de l'évènement facebook renseignée par le créateur n'existe plus");
                }
                else
                {
                    await Launcher.LaunchUriAsync(uri);
                }
            }
            else
            {
                ToastCenter.InformativeNotify("Pas d'évènement Facebook", "Il n'existe pas d'évènement Facebook pour cet évènement!");
            }
        }

        public ICommand ParticipateCommand
        {
            get
            {
                if(this._participateCommand == null)
                {
                    _participateCommand = new RelayCommand(() => Participate());
                }
                return _participateCommand;
            }
        }

        public void Participate()
        {
            if(CheckIfUserIsAlreadyParticipating())
            {
                EventDAO.KickEvent(_currentUser, EventToDetail);
                ToastCenter.InformativeNotify("Désinscription", "Vous avez été bien désinscrit de l'évènement.");
                ParticipateButtonText = "S'inscrire";
            }
            else
            {
                EventDAO.JoinEvent(_currentUser, EventToDetail);
                ToastCenter.InformativeNotify("Inscription", "Vous avez bien été inscrit à l'évènement");
                ParticipateButtonText = "Se désinscrire";
            }
        }

        public ICommand GoToCreatorProfileCommand
        {
            get
            {
                if(_goToCreatorProfileCommand == null)
                {
                    _goToCreatorProfileCommand = new RelayCommand(() => GoToCreatorProfile());
                }
                return _goToCreatorProfileCommand;
            }
            
        }

        public void GoToCreatorProfile()
        {
            ProfileFromOutsidePayload payloadToSend= new ProfileFromOutsidePayload(_currentUser, EventToDetail.Creator);
            _navigationService.NavigateTo("ProfileFromOutsidePage", payloadToSend);
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

        public bool CheckIfUserIsAlreadyParticipating()
        {
            foreach(var participant in EventToDetail.Participants)
            {
                if(_currentUser.UserName == participant.UserName)
                {
                    return true;
                }
            }
            return false;
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
            _navigationService.NavigateTo("CreateEventPage", _currentUser);
        }

        public void CloseOpenPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        */
    }
}
