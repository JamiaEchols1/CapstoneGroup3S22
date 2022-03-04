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
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    /// Interaction logic for AddTransportationPage.xaml
    /// </summary>
    public partial class AddTransportationPage : Page
    {
        private WaypointDAL _waypointDal;
        private TransportationDAL _transportationDal;
        public AddTransportationPage()
        {
            this._waypointDal = new WaypointDAL();
            this._transportationDal = new TransportationDAL();
            InitializeComponent();
            this.arrivingWaypointComboBox.ItemsSource = this._waypointDal.GetWaypoints(LoggedUser.selectedTrip.Id).Where(w => w.StartDateTime >= LoggedUser.selectedWaypoint.EndDateTime);

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                Waypoint waypoint = (Waypoint)this.arrivingWaypointComboBox.SelectedItem;

                DateTime startDate = LoggedUser.selectedWaypoint.EndDateTime;
                DateTime endTime = waypoint.StartDateTime;
                var overlappingWaypoints = this._waypointDal.GetOverlappingWaypoints(startDate, endTime);
                if (overlappingWaypoints.Count != 0)
                {
                    string message = "The following overlapping waypoint(s) were found.\n";
                    foreach (var overlappedWaypoint in overlappingWaypoints)
                    {
                        message += overlappedWaypoint + "\n";
                    }
                    throw new Exception(message + "Transportation must not overlap with other waypoints");
                }

                var overlappingTransportations =
                    this._transportationDal.GetOverlappingTransportation(startDate, endTime);
                if (overlappingTransportations.Count != 0)
                {
                    string message = "The following overlapping transportation(s) were found.\n";
                    foreach (var transportation in overlappingTransportations)
                    {
                        message += transportation + "\n";
                    }
                    throw new Exception(message + "Transportation must not overlap with transportations");
                }

                this._transportationDal.CreateANewTransportation(LoggedUser.selectedWaypoint.Id, waypoint.Id, LoggedUser.selectedTrip.Id, startDate, endTime, descriptionTextBox.Text);
                WaypointInfo waypointInfo = new WaypointInfo();
                NavigationService.Navigate(waypointInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            WaypointInfo waypointInfo = new WaypointInfo();
            NavigationService.Navigate(waypointInfo);
        }
    }
}
