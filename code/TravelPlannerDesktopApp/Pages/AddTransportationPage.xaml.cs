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
    ///     Interaction logic for AddTransportationPage.xaml
    /// </summary>
     public partial class AddTransportationPage : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;
        private readonly TransportationDal transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddTransportationPage" /> class.
        /// </summary>
        public AddTransportationPage()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.InitializeComponent();
            this.typeComboBox.ItemsSource = TransportationTypes.GetTypes();
            this.addTransportTitle.Content = "Add New Lodging: " + LoggedUser.SelectedTrip;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Click event of the createTransportationButton control.
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
        private void createTransportationButton_Click(object sender, RoutedEventArgs e)
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
                if (overlappingWaypoints.Count != 0)
                {
                    var message = overlappingWaypoints.Aggregate("The following overlapping waypoint(s) were found.\n",
                        (current, overlappedWaypoint) => current + (overlappedWaypoint + "\n"));

                    throw new Exception(message + "Transportation must not overlap with other waypoints");
                }

                var overlappingTransportations =
                    this.transportationDal.GetOverlappingTransportation(startDate, endDate);
                if (overlappingTransportations.Count != 0)
                {
                    var message = overlappingTransportations.Aggregate(
                        "The following overlapping transportation(s) were found.\n",
                        (current, transportation) => current + (transportation + "\n"));
     
                    throw new Exception(message + "Transportation must not overlap with transportations");
                }

                var newTransportation = this.transportationDal.CreateANewTransportation(LoggedUser.SelectedTrip.Id,
                    startDate, endDate, this.descriptionTextBox.Text, this.typeComboBox.SelectedItem.ToString(), this.orginLocationTextBox.Text, this.destinationLocationTextBox.Text);

                MessageBox.Show("Transportation creation was Successful!");
                LoggedUser.SelectedTransportation = newTransportation;
                NavigationService?.Navigate(new TransportationInfo());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating transportation. " + ex.Message);
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

            NavigationService?.Navigate(clickedButton.NavUri);
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
                waypointsAndTransportation.AddRange(this.waypointDal.GetOverlappingWaypoints(startDate, endDate));
                waypointsAndTransportation.AddRange(
                    this.transportationDal.GetOverlappingTransportation(startDate, endDate));
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
        }

        #endregion

    }
}