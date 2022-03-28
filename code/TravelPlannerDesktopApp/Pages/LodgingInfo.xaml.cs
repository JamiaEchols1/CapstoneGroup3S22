using System;
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
        }

        #endregion

        #region Methods

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
            this.mapWebBrowser.Source = new Uri("https://www.google.com/maps/place/" + FormatLocationString());
        }

        private string FormatLocationString()
        {
            string output = "";

            string[] locationParts = LoggedUser.SelectedLodging.Location.Split(' ');
            foreach (var part in locationParts)
            {
                output += part + "+";
            }


            output.Remove(output.Length - 1);
            return output;
        }

        private void editLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}