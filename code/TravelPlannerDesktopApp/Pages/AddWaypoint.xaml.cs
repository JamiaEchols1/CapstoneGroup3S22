﻿using System;
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
    ///     Interaction logic for AddWaypoint.xaml
    /// </summary>
    public partial class AddWaypoint : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;
        private readonly TransportationDal transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddWaypoint" /> class.
        /// </summary>
        public AddWaypoint()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        private void CreateWaypointButton_OnClick(object sender, RoutedEventArgs e)
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

                var newWaypoint = this.waypointDal.CreateNewWaypoint(this.locationTextBox.Text, startDate, endDate,
                    LoggedUser.SelectedTrip.Id, this.descriptionTextBox.Text);

                MessageBox.Show("Waypoint creation was Successful!");

                LoggedUser.SelectedWaypoint = newWaypoint;
                NavigationService?.Navigate(new WaypointInfo());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Creating Waypoint. " + exception.Message);
            }
        }

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

            #endregion
        }
    }
}