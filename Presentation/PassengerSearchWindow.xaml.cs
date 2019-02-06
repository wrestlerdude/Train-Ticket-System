using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: PassengerSearchWindow
	 * Description: Controls to select a Train and then display passengers/parcels which take that train
	 * Last Modified: 29/11/2018
	 */
    public partial class PassengerSearchWindow : Window
    {
		//PassengerSearchWindow constructor: Adds train ids to the train combo box
        public PassengerSearchWindow()
        {
            InitializeComponent();
			foreach (Train t in MainWindow.Trains)
				trainCombo.Items.Add(t.TrainID);
        }

		//SearchBtn_Click: Filters bookings by train and displays bookings in a new window
		private void SearchBtn_Click(object sender, RoutedEventArgs e)
		{
			List<Booking> filtered = new List<Booking>();
			foreach (Booking b in MainWindow.Bookings)
				if (b.TrainID == trainCombo.Text)
					filtered.Add(b);

			BookingShow window = new BookingShow(filtered);
			window.Show();
			Close();
		}
	}
}
