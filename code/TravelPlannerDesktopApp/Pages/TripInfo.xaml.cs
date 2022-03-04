using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

        private readonly WaypointDal _waypointDal;
        private readonly LodgingDal _lodgingDal;
        private readonly TransportationDal _transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TripInfo"/> class.
        /// </summary>
        public TripInfo()
        {
            this._waypointDal = new WaypointDal();
            this._transportationDal = new TransportationDal();
            this._lodgingDal = new LodgingDal();
            this.InitializeComponent();
            var waypointsAndTransportation = new List<object>();
            waypointsAndTransportation.AddRange(this._waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id));
            waypointsAndTransportation.AddRange(this._transportationDal.GetTransportationsByTrip(LoggedUser.SelectedTrip.Id));
            this.waypointsListBox.ItemsSource = waypointsAndTransportation;
            this.lodgingListBox.ItemsSource = this._lodgingDal.GetLodgings(LoggedUser.SelectedTrip.Id);
            //this.transportationListBox.ItemsSource = this._transportationDal.GetTransportationsByTrip(LoggedUser.selectedTrip.Id);
            //add to waypoints list box
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

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        private void WaypointsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemType = ObjectContext.GetObjectType(this.waypointsListBox.SelectedItem.GetType());

            if (selectedItemType == typeof(Waypoint))
            {
                LoggedUser.SelectedWaypoint = this.waypointsListBox.SelectedItem as Waypoint;
                if (LoggedUser.SelectedWaypoint != null)
                {
                    var waypointInfo = new WaypointInfo();
                    NavigationService?.Navigate(waypointInfo);
                }
            }
            else if (selectedItemType == typeof(Transportation))
            {
                LoggedUser.SelectedTransportation = this.waypointsListBox.SelectedItem as Transportation;
                if (LoggedUser.SelectedTransportation != null)
                {
                    var transportationInfo = new TransportationInfo(LoggedUser.SelectedTransportation);
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