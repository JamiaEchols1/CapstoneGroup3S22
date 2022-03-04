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

        private readonly WaypointDal _waypointDAL;
        private readonly TransportationDal transportationDal;
        private readonly Transportation transportation;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportationInfo" /> class.
        /// </summary>
        /// <param name="transportation">The transportation.</param>
        public TransportationInfo(Transportation transportation)
        {
            this.transportation = transportation;
            this._waypointDAL = new WaypointDal();
            this.transportationDal = new TransportationDal();
            this.InitializeComponent();
            this.startTimeTextBlock.Text = "Start Time: " + transportation.StartTime;
            this.endTimeTextBlock.Text = "End Time: " + transportation.EndTime;
            this.toTextBlock.Text = this._waypointDAL.GetWaypoint(transportation.ArrivingWaypointId).ToString();
            this.fromTextBlock.Text = this._waypointDAL.GetWaypoint(transportation.DepartingWaypointId).ToString();
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
                this.transportationDal.DeleteTransportation(this.transportation);

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