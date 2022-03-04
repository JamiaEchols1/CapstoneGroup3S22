using System;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for WaypointInfo.xaml
    /// </summary>
    public partial class WaypointInfo : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaypointInfo" /> class.
        /// </summary>
        public WaypointInfo()
        {
            this.waypointDal = new WaypointDal();
            this.InitializeComponent();
            this.SetSelectedWaypointText();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the text for the waypoint info
        ///     @precondition - LoggedUSer != null
        ///     @postcondition - LocationTextBlock.Text = "Location: " + LoggedUser.selectedWaypoint.Location;
        ///     TimeTextBlock.Text = "Start Time: " + LoggedUser.selectedWaypoint.StartDateTime;
        ///     EndDateTextBlock.Text = "End Time: " + LoggedUser.selectedWaypoint.EndDateTime;
        /// </summary>
        public void SetSelectedWaypointText()
        {
            this.locationTextBlock.Text = "Location: " + LoggedUser.SelectedWaypoint.Location;
            this.timeTextBlock.Text = "Start Time: " + LoggedUser.SelectedWaypoint.StartDateTime;
            this.endDateTextBlock.Text = "End Time: " + LoggedUser.SelectedWaypoint.EndDateTime;
        }

        private void EditWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void RemoveWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.waypointDal.RemoveWaypoint(LoggedUser.SelectedWaypoint);

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
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(clickedButton.NavUri);
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}