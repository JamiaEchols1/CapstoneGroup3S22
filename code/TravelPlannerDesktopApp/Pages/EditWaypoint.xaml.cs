using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for EditWaypoint.xaml
    /// </summary>
    public partial class EditWaypoint : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;
        private readonly TransportationDal transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditWaypoint" /> class.
        /// </summary>
        public EditWaypoint()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.InitializeComponent();
            this.editWaypointTitle.Content = "Edit A New Waypoint: " + LoggedUser.SelectedTrip;
            this.descriptionTextBox.Text = LoggedUser.SelectedWaypoint.Description;
            this.locationTextBox.Text = LoggedUser.SelectedWaypoint.Location;
            this.startDateTimePicker.Value = LoggedUser.SelectedWaypoint.StartDate;
            this.endDateTimePicker.Value = LoggedUser.SelectedWaypoint.EndDateTime;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Click event of the BackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        /// <summary>
        ///     Handles the OnClick event of the CreateWaypointButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.Exception">
        ///     Must enter a start date!
        ///     or
        ///     Must enter an end date!
        ///     or
        ///     or
        /// </exception>
        private void EditWaypointButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.startDateTimePicker.Text == null)
                {
                    throw new Exception("Must enter a start date!");
                }

                if (this.endDateTimePicker.Text == null)
                {
                    throw new Exception("Must enter an end date!");
                }

                var startDate = DateTime.Parse(this.startDateTimePicker.Text);
                var endDate = DateTime.Parse(this.endDateTimePicker.Text);
                var overlappingWaypoints = this.waypointDal.GetOverlappingWaypoints(startDate, endDate);
                overlappingWaypoints.RemoveAll(x => x.Id == LoggedUser.SelectedWaypoint.Id);

                if (overlappingWaypoints.Count != 0)
                {
                    var message = overlappingWaypoints.Aggregate("The following overlapping waypoint(s) were found.\n",
                        (current, waypoint) => current + (waypoint + "\n"));

                    throw new Exception(message + "Waypoint must not overlap with other waypoints");
                }

                var overlappingTransportations =
                    this.transportationDal.GetOverlappingTransportation(startDate, endDate);
                if (overlappingTransportations.Count != 0)
                {
                    var message = overlappingTransportations.Aggregate(
                        "The following overlapping transportation(s) were found.\n",
                        (current, transportation) => current + (transportation + "\n"));

                    throw new Exception(message + "Waypoint must not overlap with transportations");
                }

                LoggedUser.SelectedWaypoint = this.waypointDal.EditWaypoint(this.locationTextBox.Text, startDate, endDate,
                    this.descriptionTextBox.Text);

                MessageBox.Show("Waypoint edit was Successful!");

                NavigationService?.Navigate(new WaypointInfo());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error editing Waypoint. " + exception.Message);
            }
        }

        /// <summary>
        ///     Handles the ValueChanged event of the datePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="object" /> instance containing the event data.</param>
        private void datePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.startDateTimePicker.Value != null && this.endDateTimePicker.Value != null)
            {
                var startDate = DateTime.Parse(this.startDateTimePicker.Text);
                var endDate = DateTime.Parse(this.endDateTimePicker.Text);
                var waypointsAndTransportation = new List<object>();
                waypointsAndTransportation.AddRange(this.transportationDal.GetOverlappingTransportation(startDate, endDate));
                var overlappingWaypoints = this.waypointDal.GetOverlappingWaypoints(startDate, endDate);
                overlappingWaypoints.RemoveAll(x => x.Id == LoggedUser.SelectedWaypoint.Id);


                waypointsAndTransportation.AddRange(overlappingWaypoints);
                this.overlappingListBox.ItemsSource = waypointsAndTransportation;
            }

            if (this.overlappingListBox.Items.Count > 0)
            {
                this.overlappingListBox.Visibility = Visibility.Visible;
                this.overlappingLabel.Visibility = Visibility.Visible;
            }
            else
            {
                this.overlappingListBox.Visibility = Visibility.Collapsed;
                this.overlappingLabel.Visibility = Visibility.Collapsed;
            }

            #endregion
        }
    }
}