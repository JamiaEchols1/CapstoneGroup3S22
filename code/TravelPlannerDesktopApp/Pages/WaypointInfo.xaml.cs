using System;
using System.Web;
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
            this.setPageSize();
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
            this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedWaypoint.Description;
            this.wbMaps.Source = new Uri("https://www.google.com/maps/place/" + this.FormatLocationString());
        }

        private string FormatLocationString()
        {
            var output = "";
            var mapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            var waypointLocation = HttpUtility.UrlEncode(LoggedUser.SelectedWaypoint.Location);
            output = mapURL + "&q=" + waypointLocation;

            return output;
        }

        private void EditWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditWaypoint());
        }

        /// <summary>
        ///     Handles the OnClick event of the RemoveWaypointButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        ///     Handles the OnClick event of the BackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}