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
            this.SampleSetupText();
            this.tripsListBox.ItemsSource = this.tripDal.GetTrips(LoggedUser.User.Id);
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

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        private void TripsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoggedUser.SelectedTrip = this.tripsListBox.SelectedItem as Trip;
            if (LoggedUser.SelectedTrip != null)
            {
                var tripInfo = new TripInfo();
                NavigationService?.Navigate(tripInfo);
            }
        }

        #endregion
    }
}