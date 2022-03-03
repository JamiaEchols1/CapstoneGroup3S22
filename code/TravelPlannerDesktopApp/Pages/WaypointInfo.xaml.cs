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
    /// Interaction logic for WaypointInfo.xaml
    /// </summary>
    public partial class WaypointInfo : Page
    {
        private WaypointDAL _waypointDal;

        public WaypointInfo()
        {
            this._waypointDal = new WaypointDAL();
            InitializeComponent();
            this.setSelectedWaypointText();
        }

        public void setSelectedWaypointText()
        {
            this.LocationTextBlock.Text = "Location: " + LoggedUser.selectedWaypoint.Location;
            this.TimeTextBlock.Text = "Start Time: " + LoggedUser.selectedWaypoint.StartDateTime;
            this.EndDateTextBlock.Text = "End Time: " + LoggedUser.selectedWaypoint.EndDateTime;
        }

        private void EditWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this._waypointDal.RemoveWaypoint(LoggedUser.selectedWaypoint);

                MessageBox.Show("Waypoint Deletion was Successful!");

                NavigationService.Navigate(this.backButton.NavUri);
            }
            catch (Exception exception)
            {

                MessageBox.Show("Error Removing waypoint. " + exception.Message);
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
