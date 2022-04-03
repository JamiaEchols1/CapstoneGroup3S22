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

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the selected transportation text
        ///     @precondition - LoggedUser.SelectedTransportation != null
        ///     @postcondition -  this.startTimeTextBlock.Text = "Start Time: " + LoggedUser.SelectedTransportation.StartTime;
        ///     this.endTimeTextBlock.Text = "End Time: " + LoggedUser.SelectedTransportation.EndTime;
        ///     this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedTransportation.Description;
        /// </summary>
        public void SetSelectedTransportText()
        {
            this.startTimeTextBlock.Text = "Start Time: " + LoggedUser.SelectedTransportation.StartTime;
            this.endTimeTextBlock.Text = "End Time: " + LoggedUser.SelectedTransportation.EndTime;
            this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedTransportation.Description;
            this.typeTextBlock.Text = "Type: " + LoggedUser.SelectedTransportation.Type;
            this.wbMaps.Source = new Uri("https://www.google.com/maps/dir/" + this.FormatLocationString());
        }

        private void editTransportationButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private string FormatLocationString()
        {
            var output = "";

            var originLocationParts = LoggedUser.SelectedTransportation.Origin.Split(' ');
            foreach (var part in originLocationParts)
            {
                output += part + "+";
            }

            output.Remove(output.Length - 1);

            output += "/";

            var destinationLocationParts = LoggedUser.SelectedTransportation.Destination.Split(' ');
            foreach (var part in destinationLocationParts)
            {
                output += part + "+";
            }

            output.Remove(output.Length - 1);

            return output;
        }

        /// <summary>
        ///     Handles the Click event of the removeTransportationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
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

        #endregion
    }
}