using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: TrainShow
	 * Description: Displays given list of trains on a form
	 * Last Modified: 23/11/2018
	 */
    public partial class TrainShow : Window
    {
		//TrainShow constructor: Takes in a list of trains and then displays them by adding to a listbox
		public TrainShow(List<Train> trains)
		{
			InitializeComponent();
			foreach (Train t in trains)
			{
				string item = $"{t.TrainID}, Depart: {t.Departure}, Destination: {t.Destination}, " +
							  $"{t.TimeToString()}, {t.DayToString()}, SleeperBerth: {t.SleeperBerth}, " +
							  $"First Class: {t.FirstClass}, {t.Type}";

				if (t is IIntermediate)
				{
					item += ", Intermediates: " + ((IIntermediate)t).IString();
				}
				trainListBox.Items.Add(item);
			}
        }
	}
}
