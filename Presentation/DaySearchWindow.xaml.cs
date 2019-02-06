using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: DaySearchWindow
	 * Description: Window that contains controls to select a date,
	 * which will be used to display trains which depart on that date.
	 * Last Modified: 30/11/2018
	 */
    public partial class DaySearchWindow : Window
    {
		//DaySearchWindow constructor: Calls InitializeComponent()
        public DaySearchWindow()
        {
            InitializeComponent();
        }

		//DatePicker_SelectedDateChanged: Filters trains by date selected and displays them
		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			List<Train> filtered = new List<Train>();
			foreach (Train t in MainWindow.Trains)
				if (t.DepartureDay == datePicker.SelectedDate.Value.Date)
					filtered.Add(t);

			TrainShow window = new TrainShow(filtered);
			window.Show();
			Close();
		}
	}
}
