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
    /// Interaction logic for LodgingInfo.xaml
    /// </summary>
    public partial class LodgingInfo : Page
    {
        private LodgingDAL _lodgingDal;
        public LodgingInfo()
        {
            this._lodgingDal = new LodgingDAL();
            InitializeComponent();
            this.setSelectedLodgingText();
        }

        public void setSelectedLodgingText()
        {
            this.LocationTextBlock.Text = "Location: " + LoggedUser.selectedLodging.Location;
            this.StartDateTextBlock.Text = "Start Date: " + LoggedUser.selectedLodging.StartTime;
            this.EndDateTextBlock.Text = "End Date: " + LoggedUser.selectedLodging.EndTime;
        }

        private void editLodgingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this._lodgingDal.RemoveLodging(LoggedUser.selectedLodging);

                MessageBox.Show("Lodging Deletion was Successful!");

                NavigationService.Navigate(this.backButton.NavUri);
            }
            catch (Exception exception)
            {

                MessageBox.Show("Error Removing lodging. " + exception.Message);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;

            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
