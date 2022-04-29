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
using TravelPlannerLibrary.Models;

namespace TravelPlannerDesktopApp.Controls
{
    /// <summary>
    /// Interaction logic for LocationDialog.xaml
    /// </summary>
    public partial class LocationDialog : Window
    {
        private Transportation transportation;

        public LocationDialog(String origin, string detination)
        {
            this.transportation = transportation;
            InitializeComponent();
            this.wbMaps.Source = new Uri("https://www.google.com/maps/dir/?api=1&" + this.FormatLocationString(origin,detination));
        }

        
        private string FormatLocationString(string origin, string destination)
        {
            var output = "origin=";

            var originLocationParts = origin.Split(' ');
            foreach (var part in originLocationParts)
            {
                output += part + "+";
            }

            output.Remove(output.Length - 1);

            output += "&destination=";

            var destinationLocationParts = destination.Split(' ');
            foreach (var part in destinationLocationParts)
            {
                output += part + "+";
            }

            output.Remove(output.Length - 1);

            output += "&travelmode=" + transportation.Type.ToLower();

            return output;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
