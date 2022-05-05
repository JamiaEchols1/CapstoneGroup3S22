using System;
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
        ///     Handles the Click event of the createTripButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void createTripButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.tripDal.CreateNewTrip(this.nameTextBox.Text, DateTime.Parse(this.startDatePicker.Text),
                    DateTime.Parse(this.endDatePicker.Text), LoggedUser.User.Id, this.descriptionTextBox.Text);

                MessageBox.Show("Trip creation was Successful!");

                NavigationService?.Navigate(this.backButton.NavUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Handles the Click event of the NavButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}