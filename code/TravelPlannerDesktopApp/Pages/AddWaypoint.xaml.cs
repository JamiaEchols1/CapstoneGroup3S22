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
        public AddWaypoint()
        {
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
                //TimeSpan.Parse(this.timePicker.Text);
                //var ts = this.timePicker.Value;
                //var time = new TimeSpan(0, ts.Value.Hour, ts.Value.Minute, ts.Value.Second, ts.Value.Millisecond);
                Waypoint newWaypoint = WaypointDAL.CreateNewWaypoint(this.locationTextBox.Text, new TimeSpan(1), LoggedUser.selectedTrip.Id);

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
