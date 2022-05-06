using System;
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
        ///     Initializes a new instance of the <see cref="TripInfo" /> class.
        /// </summary>
        public TripInfo()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.lodgingDal = new LodgingDal();
            this.InitializeComponent();
            this.setPageSize();
            this.SetListSources();
            this.SetSelectedTripText();
        }

        #endregion

        #region Methods

        private void setPageSize()
        {
            this.pageGrid.Width = this.Width;
            this.pageGrid.Height = this.Height;
            Application.Current.MainWindow.Height = this.Height;
            Application.Current.MainWindow.Width = this.Width;
            Application.Current.MainWindow.MinWidth = this.MinWidth;
            Application.Current.MainWindow.MinHeight = this.MinHeight;
            Application.Current.MainWindow.MaxHeight = this.MaxHeight;
            Application.Current.MainWindow.MaxWidth = this.MaxWidth;
        }

        /// <summary>
        ///     Sets the trip info text
        ///     @precondition - LoggedUser != null
        ///     @postcondition - SelectedTripTextBlock.Text = "Selected Trip : " + LoggedUser.selectedTrip.Name;
        ///     TripStartDateTextBlock.Text = "Start Date: " + LoggedUser.selectedTrip.StartDate.ToString("D");
        ///     TripEndDateTextBlock.Text = "End Date: " + LoggedUser.selectedTrip.EndDate.ToString("D");
        /// </summary>
        public void SetSelectedTripText()
        {
            this.selectedTripTextBlock.Text = "Selected Trip : " + LoggedUser.SelectedTrip.Name;
            this.tripStartDateTextBlock.Text = "Start Date: " + LoggedUser.SelectedTrip.StartDate.ToString("D");
            this.tripEndDateTextBlock.Text = "End Date: " + LoggedUser.SelectedTrip.EndDate.ToString("D");
            this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedTrip.Description;
        }

        /// <summary>
        ///     Sets the waypoint transport source.
        ///     @precondition - LoggedUser.SelectedTrip != null
        ///     @postcondition - waypointsAndTransportListBox contains all waypoints and transportation for the selected trip
        ///     lodgingListBox contains all lodging for the selected trip
        /// </summary>
        public void SetListSources()
        {
            var waypointsAndTransportation = new List<TripItem>();
            var waypoints = this.waypointDal.GetWaypoints(LoggedUser.SelectedTrip.Id);
            var transport = this.transportationDal.GetTransportationsByTrip(LoggedUser.SelectedTrip.Id);

            foreach (var item in waypoints)
            {
                item.StartDate = item.StartDateTime;
                item.EndDate = item.EndDateTime;
                item.ItemType = TripItem.TripItemType.Waypoint;
                item.ItemLocation = item.Location;
            }

            foreach (var item in transport)
            {
                item.StartDate = item.StartTime;
                item.EndDate = item.EndTime;
                item.ItemType = TripItem.TripItemType.Transportation;
                item.ItemLocation = $"Origin: {item.Origin},{Environment.NewLine}Destination: {item.Destination}";
            }

            waypointsAndTransportation.AddRange(waypoints);
            waypointsAndTransportation.AddRange(transport);
            this.waypointsAndTransportDataGrid.ItemsSource = waypointsAndTransportation.OrderBy(x => x.StartDate);
            this.lodgingDataGrid.ItemsSource = this.lodgingDal.GetLodgings(LoggedUser.SelectedTrip.Id);
        }

        /// <summary>
        ///     Handles the Click event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            if (clickedButton != null)
            {
                NavigationService?.Navigate(clickedButton.NavUri);
            }
            
        }

        /// <summary>
        ///     Handles the OnSelectionChanged event of the WaypointsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void WaypointsAndTransportDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemType =
                ObjectContext.GetObjectType(this.waypointsAndTransportDataGrid.SelectedItem.GetType());

            if (selectedItemType == typeof(Waypoint))
            {
                LoggedUser.SelectedWaypoint = this.waypointsAndTransportDataGrid.SelectedItem as Waypoint;
                if (LoggedUser.SelectedWaypoint != null)
                {
                    var waypointInfo = new WaypointInfo();
                    NavigationService?.Navigate(waypointInfo);
                }
            }
            else if (selectedItemType == typeof(Transportation))
            {
                LoggedUser.SelectedTransportation = this.waypointsAndTransportDataGrid.SelectedItem as Transportation;
                if (LoggedUser.SelectedTransportation != null)
                {
                    var transportationInfo = new TransportationInfo();
                    NavigationService?.Navigate(transportationInfo);
                }
            }
        }

        /// <summary>
        ///     Handles the OnSelectionChanged event of the LodgingListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void LodgingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.SelectedLodging = this.lodgingDataGrid.SelectedItem as Lodging;
            if (LoggedUser.SelectedLodging != null)
            {
                var lodgingInfo = new LodgingInfo();
                NavigationService?.Navigate(lodgingInfo);
            }
        }

        #endregion
    }
}