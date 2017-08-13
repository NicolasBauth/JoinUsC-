using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<ProfilePageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
            SimpleIoc.Default.Register<CreateEventPageViewModel>();
            SimpleIoc.Default.Register<EditInterestsPageViewModel>();
            SimpleIoc.Default.Register<LocateEventsPageViewModel>();
            SimpleIoc.Default.Register<EventDescriptionPageViewModel>();
            SimpleIoc.Default.Register<EventListPageViewModel>();
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<RegisterPageViewModel>();
            SimpleIoc.Default.Register<EventPositionPageViewModel>();
            SimpleIoc.Default.Register<LocateCenterPageViewModel>();

            NavigationService navigationPages = new NavigationService();
            
            SimpleIoc.Default.Register<INavigationService>(()=>navigationPages);
            navigationPages.Configure("CreateEventPage", typeof(CreateEventPage));
            navigationPages.Configure("EditInterestsPage", typeof(EditInterestsPage));
            navigationPages.Configure("EventDescriptionPage", typeof(EventDescriptionPage));
            navigationPages.Configure("EventListPage", typeof(EventListPage));
            navigationPages.Configure("LocateEventPage", typeof(LocateEventPage));
            navigationPages.Configure("LoginPage", typeof(LoginPage));
            navigationPages.Configure("MainPage", typeof(MainPage));
            navigationPages.Configure("ProfilePage", typeof(ProfilePage));
            navigationPages.Configure("SearchEventPage", typeof(SearchPage));
            navigationPages.Configure("RegisterPage", typeof(RegisterPage));
            navigationPages.Configure("EventPositionPage", typeof(EventPositionPage));
            navigationPages.Configure("LocateCenterPage", typeof(LocateCenterPage));
        }
        public MainPageViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }
        public CreateEventPageViewModel CreateEvent
        {
            get { return ServiceLocator.Current.GetInstance<CreateEventPageViewModel>(); }
        }
        public ProfilePageViewModel UserProfile
        {
            get { return ServiceLocator.Current.GetInstance<ProfilePageViewModel>(); }
        }
        public SearchPageViewModel SearchEvent
        {
            get { return ServiceLocator.Current.GetInstance<SearchPageViewModel>(); }
        }
        public EditInterestsPageViewModel EditInterests
        {
            get { return ServiceLocator.Current.GetInstance<EditInterestsPageViewModel>(); }
        }
        public LocateEventsPageViewModel LocateEvent
        {
            get { return ServiceLocator.Current.GetInstance<LocateEventsPageViewModel>(); }
        }
        public EventDescriptionPageViewModel EventDescription
        {
            get { return ServiceLocator.Current.GetInstance<EventDescriptionPageViewModel>(); }
        }
        public EventListPageViewModel EventList
        {
            get { return ServiceLocator.Current.GetInstance<EventListPageViewModel>(); }
        }
        public LoginPageViewModel LoginPage
        {
            get { return ServiceLocator.Current.GetInstance<LoginPageViewModel>(); }
        }

        public RegisterPageViewModel RegisterPage
        {
            get { return ServiceLocator.Current.GetInstance<RegisterPageViewModel>(); }
        }  
        public EventPositionPageViewModel EventPosition
        {
            get { return ServiceLocator.Current.GetInstance<EventPositionPageViewModel>(); }
        } 

        public LocateCenterPageViewModel LocateCenter
        {
            get { return ServiceLocator.Current.GetInstance<LocateCenterPageViewModel>(); }
        }
    }
}
