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
using System.Windows.Shapes;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp
{
    /// <summary>
    /// Interaction logic for AddTripPage.xaml
    /// </summary>
    public partial class AddTripPage : Window
    {
        public AddTripPage()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            TripDAL.CreateNewTrip(this.nameTextBox.Text, DateTime.Parse(this.startDatePicker.Text), DateTime.Parse(this.endDatePicker.Text), LoggedUser.user.Id);
            Home home = new Home();
            Hide();
            home.Show();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            Hide();
            home.Show();
        }
    }
}
