using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPlannerDesktopApp.Controls;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Pages
{
    /// <summary>
    /// Interaction logic for AddTrip.xaml
    /// </summary>
    public partial class AddTrip : Page
    {
        private TripDAL tripDAL = new TripDAL();
        public AddTrip()
        {
            InitializeComponent();
        }


        private void createTripButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                tripDAL.CreateNewTrip(this.nameTextBox.Text, DateTime.Parse(this.StartDatePicker.Text), DateTime.Parse(this.endDatePicker.Text), LoggedUser.user.Id);

                MessageBox.Show("Trip creation was Successful!");

                NavigationService.Navigate(this.backButton.NavUri);
            }
            catch (Exception exception)
            {

                MessageBox.Show("Error Creating trip. " + exception.Message);
            }
            
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);

        }
    }
}
