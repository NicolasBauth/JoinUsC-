using JoinUs.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace JoinUs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((MainPageViewModel)DataContext).OnNavigatedTo(e);
        }



    }
}
