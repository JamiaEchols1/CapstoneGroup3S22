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
        private WaypointDAL _waypointDal;
        private LodgingDAL _lodgingDal;
        private TransportationDAL _transportationDal;
        public TripInfo()
        {
            this._waypointDal = new WaypointDAL();
            this._transportationDal = new TransportationDAL();
            this._lodgingDal = new LodgingDAL();
            InitializeComponent();
            this.waypointsListBox.ItemsSource = this._waypointDal.GetWaypoints(LoggedUser.selectedTrip.Id);
            this.lodgingListBox.ItemsSource = this._lodgingDal.GetLodgings(LoggedUser.selectedTrip.Id);
            //this.transportationListBox.ItemsSource = this._transportationDal.GetTransportations(LoggedUser.selectedTrip.Id);
            //add to waypoints list box
            this.setSelectedTripText();
        }

        public void setSelectedTripText()
        {
            this.SelectedTripTextBlock.Text = "Selected Trip : " + LoggedUser.selectedTrip.Name;
            this.TripStartDateTextBlock.Text = "Start Date: " + LoggedUser.selectedTrip.StartDate.ToString("D");
            this.TripEndDateTextBlock.Text = "End Date: " + LoggedUser.selectedTrip.EndDate.ToString("D");
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

        private void LodgingListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.selectedLodging = this.lodgingListBox.SelectedItem as Lodging;
            if (LoggedUser.selectedLodging != null)
            {
                LodgingInfo lodgingInfo = new LodgingInfo();
                NavigationService.Navigate(lodgingInfo);
            }
        }
    }
}
