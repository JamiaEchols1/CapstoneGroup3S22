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
    /// Interaction logic for AddWaypoint.xaml
    /// </summary>
    public partial class AddWaypoint : Page
    {
        private WaypointDAL _waypointDal;
        private TransportationDAL _transportationDal;
        public AddWaypoint()
        {
            this._waypointDal = new WaypointDAL();
            this._transportationDal = new TransportationDAL();
            InitializeComponent();
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);

        }

        private void CreateWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.Parse(this.startDateTimePicker.Text);
                DateTime endDate = DateTime.Parse(this.endDateTimePicker.Text);
                var overlappingWaypoints = this._waypointDal.GetOverlappingWaypoints(startDate, endDate);
                if (overlappingWaypoints.Count != 0)
                {
                    string message = "The following overlapping waypoint(s) were found.\n";
                    foreach (var waypoint in overlappingWaypoints)
                    {
                        message += waypoint + "\n";
                    }
                    throw new Exception(message + "Waypoint must not overlap with other waypoints");
                }

                var overlappingTransportations =
                    this._transportationDal.GetOverlappingTransportation(startDate, endDate);
                if (overlappingTransportations.Count != 0)
                {
                    string message = "The following overlapping transportation(s) were found.\n";
                    foreach (var transportation in overlappingTransportations)
                    {
                        message += transportation + "\n";
                    }
                    throw new Exception(message + "Waypoint must not overlap with transportations");
                }

                Waypoint newWaypoint = this._waypointDal.CreateNewWaypoint(this.locationTextBox.Text, startDate, endDate, LoggedUser.selectedTrip.Id);

                MessageBox.Show("Waypoint creation was Successful!");

                LoggedUser.selectedWaypoint = newWaypoint;
                NavigationService.Navigate(new WaypointInfo());
            }
            catch (Exception exception)
            {

                MessageBox.Show("Error Creating Waypoint. " + exception.Message);
            }
        }
    }
}
