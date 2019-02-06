using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: CalculateWindow
	 * Description: Displays all the fares for bookings in runtime.
	 * Last Modified: 29/11/2018
	 */
    public partial class CalculateWindow : Window
    {
		//CalculateWindow constructor: Displays booking details and fares by adding to listbox
        public CalculateWindow()
        {
            InitializeComponent();
			foreach (Booking booking in MainWindow.Bookings)
			{
				BookingDecorator decorator = new BookingDecorator(booking);
				calculateListBox.Items.Add($"{booking.TrainID} {booking.Coach}{booking.Seat} £{decorator.Fare} {booking.Name}");
			}
        }
	}
}
