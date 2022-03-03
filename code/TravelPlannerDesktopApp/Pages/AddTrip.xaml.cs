﻿using System;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for AddTrip.xaml
    /// </summary>
    public partial class AddTrip : Page
    {
        #region Data members

        private readonly TripDal tripDal = new TripDal();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddTrip" /> class.
        /// </summary>
        public AddTrip()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void createTripButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.tripDal.CreateNewTrip(this.nameTextBox.Text, DateTime.Parse(this.startDatePicker.Text),
                    DateTime.Parse(this.endDatePicker.Text), LoggedUser.User.Id);

                MessageBox.Show("Trip creation was Successful!");

                NavigationService?.Navigate(this.backButton.NavUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}