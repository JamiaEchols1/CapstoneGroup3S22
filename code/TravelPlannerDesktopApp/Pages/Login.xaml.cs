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
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

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