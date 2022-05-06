using System;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for LodgingInfo.xaml
    /// </summary>
    public partial class LodgingInfo : Page
    {
        #region Data members

        private readonly LodgingDal lodgingDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LodgingInfo" /> class.
        /// </summary>
        public LodgingInfo()
        {
            this.lodgingDal = new LodgingDal();
            this.InitializeComponent();
            this.SetSelectedLodgingText();
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
        ///     Sets the text for lodging info
        ///     @precondition - LoggedUser != null
        ///     @postcondition - LocationtextBlock.Text == "Location: " + LoggedUser.selectedLodging.Location;
        ///     StartDateTextBlock.Text = "Start Date: " + LoggedUser.selectedLodging.StartTime;
        ///     EndDateTextBlock.Text = "End Date: " + LoggedUser.selectedLodging.EndTime;
        /// </summary>
        public void SetSelectedLodgingText()
        {
            this.locationTextBlock.Text = "Location: " + LoggedUser.SelectedLodging.Location;
            this.descriptionTextBlock.Text = "Description: " + LoggedUser.SelectedLodging.Description;
            this.startDateTextBlock.Text = "Start Date: " + LoggedUser.SelectedLodging.StartTime;
            this.endDateTextBlock.Text = "End Date: " + LoggedUser.SelectedLodging.EndTime;
            this.mapWebBrowser.Source = new Uri("https://www.google.com/maps/place/" + this.FormatLocationString());
        }

        private string FormatLocationString()
        {
            var output = "";
            var mapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyDJEezkTFgj0PAnzJQJVVEhfZbpUmH27s0";
            var Location = HttpUtility.UrlEncode(LoggedUser.SelectedLodging.Location);
            output = mapURL + "&q=" + Location;

            return output;
        }

        private void editLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditLodging());
        }

        /// <summary>
        ///     Handles the Click event of the removeLodgingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void removeLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.lodgingDal.RemoveLodging(LoggedUser.SelectedLodging);

                MessageBox.Show("Lodging Deletion was Successful!");

                NavigationService?.Navigate(this.backButton.NavUri);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Removing lodging. " + exception.Message);
            }
        }

        /// <summary>
        ///     Handles the Click event of the backButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}