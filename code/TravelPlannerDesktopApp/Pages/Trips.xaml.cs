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
    /// Interaction logic for Trips.xaml
    /// </summary>
    public partial class Trips : Page
    {
        public Trips()
        {
            InitializeComponent();
            this.tripsListBox.ItemsSource = TripDAL.GetTrips(LoggedUser.user.Id);
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {

            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void TripsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.selectedTrip = this.tripsListBox.SelectedItem as Trip;
            if (LoggedUser.selectedTrip != null)
            {
                TripInfo tripInfo = new TripInfo();
                NavigationService.Navigate(tripInfo);
            }
            
        }
    }
}
