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
            this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedWaypoint.Description;
            this.wbMaps.Source = new Uri("https://www.google.com/maps/place/" + FormatLocationString());
        }

        private string FormatLocationString()
        {
            string output = "";

            string[] locationParts = LoggedUser.SelectedWaypoint.Location.Split(' ');
            foreach (var part in locationParts)
            {
                output += part + "+";
            }

            
            output.Remove(output.Length-1);
            return output;
        }

        private void EditWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        /// <summary>
        /// Handles the OnClick event of the RemoveWaypointButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
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
        /// Handles the OnClick event of the BackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}