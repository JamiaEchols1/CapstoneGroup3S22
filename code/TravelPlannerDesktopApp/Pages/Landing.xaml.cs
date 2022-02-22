using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    /// Interaction logic for Landing.xaml
    /// </summary>
    public partial class Landing : Page
    {
        private TripDAL tripDAL = new TripDAL();

        public Landing()
        {
            InitializeComponent();
            this.sampleSetupText();
            this.tripsListBox.ItemsSource = tripDAL.GetTrips(LoggedUser.user.Id);
        }

        public void sampleSetupText()
        {
            this.WelcomeTextBlock.Text = "Welcome: " + LoggedUser.user.Username;
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {

            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            TripDetailsPage detailsPage = new TripDetailsPage((Trip) this.tripsListBox.SelectedItem);
            NavigationService.Navigate(detailsPage);
        }
    }
}
