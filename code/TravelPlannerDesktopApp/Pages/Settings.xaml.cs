using System.Windows;
using System.Windows.Controls;
using TravelPlannerDesktopApp.Controls;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton.NavUri);
        }

        #endregion
    }
}