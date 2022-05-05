using System.Windows;
using System.Windows.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    ///     Interaction logic for checkLoginCredentials.xaml
    /// </summary>
    public partial class Login : Page
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Login" /> class.
        /// </summary>
        public Login()
        {
            this.InitializeComponent();
            this.setPageSize();
        }

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

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the login.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void handleLogin(object sender, RoutedEventArgs e)
        {
            var loginDal = new LoginDal();
            var username = this.usernameTextBox.Text;
            var password = this.passwordTextBox.Password;
            var loggedUser = loginDal.CheckLoginCredentials(username, password);

            if (loggedUser != null)
            {
                LoggedUser.User = loggedUser;
                var landingPage = new Landing();
                NavigationService?.Navigate(landingPage);
            }
            else
            {
                MessageBox.Show("Login failed! No user found.");
            }
        }

        /// <summary>
        ///     Handles the Click event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var loginDal = new LoginDal();
            var username = this.usernameTextBox.Text;
            var password = this.passwordTextBox.Password;
            var loggedUser = loginDal.CheckLoginCredentials(username, password);

            if (loggedUser != null)
            {
                LoggedUser.User = loggedUser;
                var landingPage = new Landing();
                NavigationService?.Navigate(landingPage);
            }
        }

        #endregion
    }
}