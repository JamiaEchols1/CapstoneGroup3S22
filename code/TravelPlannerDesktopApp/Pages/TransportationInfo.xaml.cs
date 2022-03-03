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
    /// Interaction logic for TransportationInfo.xaml
    /// </summary>
    public partial class TransportationInfo : Page
    {
        private WaypointDAL _waypointDAL;
        private TransportationDAL _transportationDAL;
        private Transportation transportation;
        public TransportationInfo(Transportation transportation)
        {
            this.transportation = transportation;
            this._waypointDAL = new WaypointDAL();
            this._transportationDAL = new TransportationDAL();
            InitializeComponent();
            this.startTimeTextBlock.Text = "Start Time: " + transportation.StartTime.ToString();
            this.endTimeTextBlock.Text = "End Time: " + transportation.EndTime.ToString();
            this.toTextBlock.Text = this._waypointDAL.GetWaypoint(transportation.ArrivingWaypointId).ToString();
            this.fromTextBlock.Text = this._waypointDAL.GetWaypoint(transportation.DepartingWaypointId).ToString();
        }

        private void editTransportationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeTransportationButton_Click(object sender, RoutedEventArgs e)
        {
            this._transportationDAL.DeleteTransportation(this.transportation);
            TripInfo tripInfo = new TripInfo();
            NavigationService.Navigate(tripInfo);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
