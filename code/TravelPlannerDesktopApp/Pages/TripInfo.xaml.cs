using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for Waypoints.xaml
    /// </summary>
    public partial class TripInfo : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;
        private readonly LodgingDal lodgingDal;
        private readonly TransportationDal transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TripInfo"/> class.
        /// </summary>
        public TripInfo()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.lodgingDal = new LodgingDal();
            this.InitializeComponent();
            this.SetListSources();
            this.SetSelectedTripText();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the trip info text
        /// @precondition - LoggedUser != null
        /// @postcondition - SelectedTripTextBlock.Text = "Selected Trip : " + LoggedUser.selectedTrip.Name;
        ///                  TripStartDateTextBlock.Text = "Start Date: " + LoggedUser.selectedTrip.StartDate.ToString("D");
        ///                  TripEndDateTextBlock.Text = "End Date: " + LoggedUser.selectedTrip.EndDate.ToString("D");
        /// </summary>
        public void SetSelectedTripText()
        {
            this.selectedTripTextBlock.Text = "Selected Trip : " + LoggedUser.SelectedTrip.Name;
            this.tripStartDateTextBlock.Text = "Start Date: " + LoggedUser.SelectedTrip.StartDate.ToString("D");
            this.tripEndDateTextBlock.Text = "End Date: " + LoggedUser.SelectedTrip.EndDate.ToString("D");
        }

        /// <summary>
        /// Sets the waypoint transport source.
        /// @precondition - LoggedUser.SelectedTrip != null
        /// @postcondition - waypointsAndTransportListBox contains all waypoints and transportation for the selected trip
        ///                  lodgingListBox contains all lodging for the selected trip
        /// </summary>
        public void SetListSources()
        {
            var waypointsAndTransportation = new List<TripItem>();
            var waypoints = this.waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id);
            var transport = this.transportationDal.GetTransportationsByTrip(LoggedUser.SelectedTrip.Id);

            foreach (var item in waypoints)
            {
                item.StartDate = item.StartDateTime;
            }
            foreach (var item in transport)
            {
                item.StartDate = item.StartTime;
            }
            waypointsAndTransportation.AddRange(waypoints);
            waypointsAndTransportation.AddRange(transport);
            this.waypointsAndTransportListBox.ItemsSource = waypointsAndTransportation.OrderBy(x => x.StartDate);
            this.lodgingListBox.ItemsSource = this.lodgingDal.GetLodgings(LoggedUser.SelectedTrip.Id);
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        private void WaypointsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemType = ObjectContext.GetObjectType(this.waypointsAndTransportListBox.SelectedItem.GetType());

            if (selectedItemType == typeof(Waypoint))
            {
                LoggedUser.SelectedWaypoint = this.waypointsAndTransportListBox.SelectedItem as Waypoint;
                if (LoggedUser.SelectedWaypoint != null)
                {
                    var waypointInfo = new WaypointInfo();
                    NavigationService?.Navigate(waypointInfo);
                }
            }
            else if (selectedItemType == typeof(Transportation))
            {
                LoggedUser.SelectedTransportation = this.waypointsAndTransportListBox.SelectedItem as Transportation;
                if (LoggedUser.SelectedTransportation != null)
                {
                    var transportationInfo = new TransportationInfo();
                    NavigationService?.Navigate(transportationInfo);
                }
            }
        }

        private void LodgingListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.SelectedLodging = this.lodgingListBox.SelectedItem as Lodging;
            if (LoggedUser.SelectedLodging != null)
            {
                var lodgingInfo = new LodgingInfo();
                NavigationService?.Navigate(lodgingInfo);
            }
        }

        #endregion
    }
}