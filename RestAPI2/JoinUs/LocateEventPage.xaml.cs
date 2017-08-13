using JoinUs.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace JoinUs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LocateEventPage : Page
    {
        public LocateEventPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((LocateEventsPageViewModel)DataContext).OnNavigatedTo(e);
        }

        private async void LocateEventMapControl1_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {
            await ((LocateEventsPageViewModel)DataContext).LocateEventMapControl1_MapTapped(sender,args);
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //((LocateEventsPageViewModel)DataContext).OnNavigatingFrom(e);
            base.OnNavigatingFrom(e);
        }
    }
}
