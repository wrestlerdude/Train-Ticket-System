using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: TwoStationsWindow
	 * Description: Controls to select two stations and then searches for any trains that run between them, displaying them
	 * Last Modified: 01/12/2018
	 */
    public partial class TwoStationsWindow : Window
    {
		//TwoStationsWindow constructor: Calls InitializeComponent()
        public TwoStationsWindow()
        {
            InitializeComponent();
        }

		//Button_Click: Searches for trains that run between given stations and displays them
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(station1Combo.Text) || string.IsNullOrEmpty(station2Combo.Text))
			{
				MessageBox.Show("Need a selection for both comboboxes!");
				return;
			}
			else if (station1Combo.Text == station2Combo.Text)
			{
				MessageBox.Show("Cannot both be the same!");
				return;
			}

			List<Train> filtered = new List<Train>();
			foreach(Train t in MainWindow.Trains)
			{
				//If the comboboxes are Edinburgh/London then any train suffices
				string[] initial = { "Edinburgh (Waverley)", "London (Kings Cross)" };
				if (initial.Contains(station1Combo.Text) && initial.Contains(station2Combo.Text))
					filtered.Add(t);
				else if (t is IIntermediate)
				{
					//Searches for any intermediates that match
					List<string> full = new List<string>(initial);
					full.AddRange(((IIntermediate)t).Intermediates);
					if (full.Contains(station1Combo.Text) && full.Contains(station2Combo.Text))
						filtered.Add(t);
				}
			}

			TrainShow window = new TrainShow(filtered);
			window.Show();
			Close();
		}
	}
}
