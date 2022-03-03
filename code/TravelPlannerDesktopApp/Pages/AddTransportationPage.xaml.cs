using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for AddTransportationPage.xaml
    /// </summary>
    public partial class AddTransportationPage : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;
        private readonly TransportationDal transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTransportationPage"/> class.
        /// </summary>
        public AddTransportationPage()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.InitializeComponent();
            this.arrivingWaypointComboBox.ItemsSource = this.waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id)
                                                            .Where(w => w.StartDateTime >=
                                                                        LoggedUser.SelectedWaypoint.EndDateTime);
        }

        #endregion

        #region Methods

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var waypoint = (Waypoint)this.arrivingWaypointComboBox.SelectedItem;

                var startDate = LoggedUser.SelectedWaypoint.EndDateTime;
                var endTime = waypoint.StartDateTime;
                var overlappingWaypoints = this.waypointDal.GetOverlappingWaypoints(startDate, endTime);
                if (overlappingWaypoints.Count != 0)
                {
                    var message = overlappingWaypoints.Aggregate("The following overlapping waypoint(s) were found.\n", (current, overlappedWaypoint) => current + (overlappedWaypoint + "\n"));

                    throw new Exception(message + "Transportation must not overlap with other waypoints");
                }

                var overlappingTransportations =
                    this.transportationDal.GetOverlappingTransportation(startDate, endTime);
                if (overlappingTransportations.Count != 0)
                {
                    var message = overlappingTransportations.Aggregate("The following overlapping transportation(s) were found.\n", (current, transportation) => current + (transportation + "\n"));

                    throw new Exception(message + "Transportation must not overlap with transportations");
                }

                this.transportationDal.CreateANewTransportation(LoggedUser.SelectedWaypoint.Id, waypoint.Id,
                    LoggedUser.SelectedTrip.Id, startDate, endTime, this.descriptionTextBox.Text);
                var waypointInfo = new WaypointInfo();
                NavigationService?.Navigate(waypointInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var waypointInfo = new WaypointInfo();
            NavigationService?.Navigate(waypointInfo);
        }

        #endregion
    }
}