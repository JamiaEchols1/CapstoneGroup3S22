using System;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for TransportationInfo.xaml
    /// </summary>
    public partial class TransportationInfo : Page
    {
        #region Data members

        private readonly WaypointDal waypointDal;
        private readonly TransportationDal transportationDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportationInfo" /> class.
        /// </summary>
        public TransportationInfo()
        {
            this.waypointDal = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.InitializeComponent();
            this.SetSelectedTransportText();
        }

        /// <summary>
        /// Sets the selected transportation text
        /// @precondition - LoggedUser.SelectedTransportation != null
        /// @postcondition -  this.startTimeTextBlock.Text = "Start Time: " + LoggedUser.SelectedTransportation.StartTime;
        ///                   this.endTimeTextBlock.Text = "End Time: " + LoggedUser.SelectedTransportation.EndTime;
        ///                   this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedTransportation.Description;
        /// </summary>
        public void SetSelectedTransportText()
        {
            this.startTimeTextBlock.Text = "Start Time: " + LoggedUser.SelectedTransportation.StartTime;
            this.endTimeTextBlock.Text = "End Time: " + LoggedUser.SelectedTransportation.EndTime;
            this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedTransportation.Description;
            this.typeTextBlock.Text = "Type: " + LoggedUser.SelectedTransportation.Type;
        }

        #endregion

        #region Methods

        private void editTransportationButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void removeTransportationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.transportationDal.DeleteTransportation(LoggedUser.SelectedTransportation);

                MessageBox.Show("Transportation Deletion was Successful!");

                NavigationService?.Navigate(this.backButton.NavUri);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Removing transportation. " + exception.Message);
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}