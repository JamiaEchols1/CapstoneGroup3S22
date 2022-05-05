using System;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for AddLodging.xaml
    /// </summary>
    public partial class AddLodging : Page
    {
        #region Data members

        private readonly LodgingDal lodgingDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddLodging" /> class.
        /// </summary>
        public AddLodging()
        {
            this.lodgingDal = new LodgingDal();
            this.InitializeComponent();
            this.setPageSize();
            this.setTripInfo();
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

        private void setTripInfo()
        {
            this.tripName.Content = LoggedUser.SelectedTrip.Name;
            this.tripStart.Content = LoggedUser.SelectedTrip.StartDate;
            this.tripEnd.Content = LoggedUser.SelectedTrip.EndDate;
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

        /// <summary>
        ///     Handles the Click event of the createLodgingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.Exception">
        ///     Must enter a start date!
        ///     or
        ///     Must enter an end date!
        /// </exception>
        private void createLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.startDatePicker.Text == null)
                {
                    throw new Exception("Must enter a start date!");
                }

                if (this.endDatePicker.Text == null)
                {
                    throw new Exception("Must enter an end date!");
                }

                var newLodging = this.lodgingDal.CreateNewLodging(this.locationTextBox.Text,
                    DateTime.Parse(this.startDatePicker.Text), DateTime.Parse(this.endDatePicker.Text),
                    LoggedUser.SelectedTrip.Id, this.descriptionTextBox.Text);

                MessageBox.Show("Lodging creation was Successful!");

                LoggedUser.SelectedLodging = newLodging;
                NavigationService?.Navigate(new LodgingInfo());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Creating Logging. " + exception.Message);
            }
        }

        /// <summary>
        ///     Handles the ValueChanged event of the datePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="object" /> instance containing the event data.</param>
        private void datePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (this.startDatePicker.Value != null && this.endDatePicker.Value != null)
                {
                    this.overlappingListBox.ItemsSource =
                        this.lodgingDal.GetOverlappingLodging(DateTime.Parse(this.startDatePicker.Text),
                            DateTime.Parse(this.endDatePicker.Text));
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
            catch (Exception exception)
            {
                MessageBox.Show("Error updating the date");
            }
        }

        #endregion
    }
}