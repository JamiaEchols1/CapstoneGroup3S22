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
        /// Initializes a new instance of the <see cref="AddLodging"/> class.
        /// </summary>
        public AddLodging()
        {
            this.lodgingDal = new LodgingDal();
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        private void createLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newLodging = this.lodgingDal.CreateNewLodging(this.locationTextBox.Text,
                    DateTime.Parse(this.startDatePicker.Text), DateTime.Parse(this.endDatePicker.Text),
                    LoggedUser.SelectedTrip.Id);

                MessageBox.Show("Lodging creation was Successful!");

                LoggedUser.SelectedLodging = newLodging;
                NavigationService?.Navigate(new LodgingInfo());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Creating Logging. " + exception.Message);
            }
        }

        private void datePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
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

        #endregion
    }
}