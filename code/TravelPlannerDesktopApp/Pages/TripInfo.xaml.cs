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
    /// Interaction logic for Waypoints.xaml
    /// </summary>
    public partial class TripInfo : Page
    {
        public TripInfo()
        {
            InitializeComponent();
            this.waypointsListBox.ItemsSource = WaypointDAL.GetWaypoints(LoggedUser.selectedTrip.Id);
            this.setSelectedTripText();
        }

        public void setSelectedTripText()
        {
            this.SelectedTripTextBlock.Text = "Selected Trip : " + LoggedUser.selectedTrip.Name;
        }
        private void Grid_Click(object sender, RoutedEventArgs e)
        {

            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void WaypointsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.selectedWaypoint = this.waypointsListBox.SelectedItem as Waypoint;
            if (LoggedUser.selectedWaypoint != null)
            {
                WaypointInfo waypointInfo = new WaypointInfo();
                NavigationService.Navigate(waypointInfo);
            }
        }
    }
}
