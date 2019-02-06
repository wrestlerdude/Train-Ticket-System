using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: BookingShow
	 * Description: To display a list of bookings in a window
	 * Last Modified: 29/11/2018
	 */
    public partial class BookingShow : Window
    {
		//BookingShow constructor: Takes in a List of Booking, binds the data and displays it
        public BookingShow(List<Booking> bookings)
        {
            InitializeComponent();
			bookingDataGrid.ItemsSource = bookings;
        }

		//DataGrid_OnAutoGeneratingColumn: Gets rid of the TrainObj column as it displays nothing
		private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.PropertyName == "TrainObj")
				e.Column = null;
		}
	}
}
