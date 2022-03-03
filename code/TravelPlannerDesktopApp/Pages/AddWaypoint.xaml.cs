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
                if (this._waypointDal.GetOverlappingWaypoints(startDate, endDate).Count != 0)
                {
                    throw new Exception("Waypoint must not overlap with other waypoints");
                }
                if (this._transportationDal.GetOverlappingTransportation(startDate, endDate).Count != 0)
                {
                    throw new Exception("Waypoint must not overlap with transportations");
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
