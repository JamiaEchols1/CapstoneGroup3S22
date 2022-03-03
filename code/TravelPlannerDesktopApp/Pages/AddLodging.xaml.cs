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
    /// Interaction logic for AddLodging.xaml
    /// </summary>
    public partial class AddLodging : Page
    {
        private LodgingDAL _lodgingDal;
        public AddLodging()
        {
            this._lodgingDal = new LodgingDAL();
            InitializeComponent();
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);

        }

        private void createLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Lodging newLodging = this._lodgingDal.CreateNewLodging(this.locationTextBox.Text, DateTime.Parse(this.startDatePicker.Text), DateTime.Parse(this.endDatePicker.Text), LoggedUser.selectedTrip.Id);

                MessageBox.Show("Lodging creation was Successful!");

                LoggedUser.selectedLodging = newLodging;
                NavigationService.Navigate(new LodgingInfo());
            }
            catch (Exception exception)
            {

                MessageBox.Show("Error Creating Logging. " + exception.Message);
            }
        }

        private void datePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.startDatePicker.Value != null && this.endDatePicker.Value != null)
            {
                this.overlappingListBox.ItemsSource = this._lodgingDal.GetOverlappingLodging(DateTime.Parse(this.startDatePicker.Text), DateTime.Parse(this.endDatePicker.Text));
            }
            if (this.overlappingListBox.Items.Count > 0)
            {
                this.overlappingListBox.Visibility = Visibility.Visible;
                this.overlappingLabel.Visibility = Visibility.Visible;
            }
            else
            {
                this.overlappingListBox.Visibility = Visibility.Collapsed;
                this.overlappingLabel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
