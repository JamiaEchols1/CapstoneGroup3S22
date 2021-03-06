using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for Landing.xaml
    /// </summary>
    public partial class Landing : Page
    {
        #region Data members

        private readonly TripDal tripDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Landing" /> class.
        /// </summary>
        public Landing()
        {
            this.tripDal = new TripDal();
            this.InitializeComponent();
            this.setPageSize();
            this.SampleSetupText();
            this.tripsDataGrid.ItemsSource = this.tripDal.GetTrips(LoggedUser.User.Id);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the welcome text for tha landing page
        ///     @precondition - LoggedUser != null
        ///     @postcondition - WelcomeTextBlock.Text == "Welcome: " + LoggedUser.user.Username;
        /// </summary>
        public void SampleSetupText()
        {
            this.welcomeTextBlock.Text = "Welcome: " + LoggedUser.User.Username;
        }

        /// <summary>
        ///     Handles the Click event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

/*        /// <summary>
        ///     Handles the OnSelectionChanged event of the TripsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void TripsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.SelectedTrip = this.tripsListBox.SelectedItem as Trip;
            if (LoggedUser.SelectedTrip != null)
            {
                var tripInfo = new TripInfo();
                NavigationService?.Navigate(tripInfo);
            }
        }*/

        #endregion

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
        /// Deletes the selected trip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            LoggedUser.SelectedTrip = this.tripsDataGrid.SelectedItem as Trip;

            if (LoggedUser.SelectedTrip != null)
            {
                MessageBoxButton buttons = MessageBoxButton.YesNo;

                string message = "Are you sure you want to delete this trip?";
                string caption = LoggedUser.SelectedTrip.Name;

                var result = MessageBox.Show(message, caption, buttons);

                if (result == MessageBoxResult.Yes)
                {
                    this.tripDal.RemoveTrip(LoggedUser.SelectedTrip.Id);
                    this.tripsDataGrid.ItemsSource = this.tripDal.GetTrips(LoggedUser.User.Id);

                    var clickedButton = e.OriginalSource as NavButton;

                    NavigationService?.Navigate(clickedButton.NavUri);
                }
            } else
            {
                MessageBox.Show("Must select a trip to delete");
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            LoggedUser.SelectedTrip = this.tripsDataGrid.SelectedItem as Trip;

            if (LoggedUser.SelectedTrip != null)
            {
                var clickedButton = e.OriginalSource as NavButton;

                NavigationService?.Navigate(clickedButton.NavUri);
            }
        }

    }
}