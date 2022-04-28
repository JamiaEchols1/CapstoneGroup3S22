using System;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for EditLodging.xaml
    /// </summary>
    public partial class EditLodging : Page
    {
        #region Data members

        private readonly LodgingDal lodgingDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditLodging" /> class.
        /// </summary>
        public EditLodging()
        {
            this.lodgingDal = new LodgingDal();
            this.InitializeComponent();
            this.editLodgingTitle.Content = "Edit Lodging: " + LoggedUser.SelectedLodging;
            this.descriptionTextBox.Text = LoggedUser.SelectedLodging.Description;
            this.locationTextBox.Text = LoggedUser.SelectedLodging.Location;
            this.startDatePicker.Value = LoggedUser.SelectedLodging.StartTime;
            this.endDatePicker.Value = LoggedUser.SelectedLodging.EndTime;
        }

        #endregion

        #region Methods

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
        ///     Handles the Click event of the editLodgingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.Exception">
        ///     Must enter a start date!
        ///     or
        ///     Must enter an end date!
        /// </exception>
        private void editLodgingButton_Click(object sender, RoutedEventArgs e)
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

                LoggedUser.SelectedLodging = this.lodgingDal.EditLodging(this.locationTextBox.Text,
                     DateTime.Parse(this.startDatePicker.Text), DateTime.Parse(this.endDatePicker.Text),
                      this.descriptionTextBox.Text);
                MessageBox.Show("Lodging edit was Successful!");
                
                NavigationService?.Navigate(new LodgingInfo());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error editing Logging. " + exception.Message);
            }
        }

        /// <summary>
        ///     Handles the ValueChanged event of the datePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="object" /> instance containing the event data.</param>
        private void datePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.startDatePicker.Value != null && this.endDatePicker.Value != null)
            {
                var overlappingList = this.lodgingDal.GetOverlappingLodging(DateTime.Parse(this.startDatePicker.Text),
                        DateTime.Parse(this.endDatePicker.Text));
                overlappingList.RemoveAll(x => x.Id == LoggedUser.SelectedLodging.Id);

                this.overlappingListBox.ItemsSource = overlappingList;
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