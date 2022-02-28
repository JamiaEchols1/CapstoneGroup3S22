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
        public AddWaypoint()
        {
            this._waypointDal = new WaypointDAL();
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
                Waypoint newWaypoint = this._waypointDal.CreateNewWaypoint(this.locationTextBox.Text, DateTime.Parse(this.dateTimePicker.Text), LoggedUser.selectedTrip.Id);

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
