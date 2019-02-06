using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessObjects;
using System.Globalization;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: TrainDetails
	 * Description: GUI to enter details of train to add to Trains
	 * Last Modified: 29/11/2018
	 */
	public partial class TrainDetails : Window
	{
		//TrainDetails constructor: Calls InitializeComponent()
		public TrainDetails()
		{
			InitializeComponent();
		}

		//AddBtn_Click: Attempts to add train details entered to Trains
		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			TrainFactory factory = TrainFactory.Instance();
			Train train;
			List<string> intermediates = new List<string>();

			//Iterates over all the controls in the form, if it's a checkbox, check if its checked, if so add it to the list of intermediates
			foreach (var ctrl in detailsGrid.Children)
				if (ctrl.GetType() == typeof(CheckBox))
					if (((CheckBox)ctrl).IsChecked.Value)
						intermediates.Add(((CheckBox)ctrl).Content.ToString());

			try
			{
				train = factory.CreateTrain(idTxtBox.Text, typeCombo.Text,
											departCombo.Text, destCombo.Text, bool.Parse(firstclassCombo.Text), intermediates,
											TimeSpan.ParseExact(timeTxtBox.Text, @"hh\:mm", CultureInfo.InvariantCulture),
											dayDatePick.SelectedDate.Value.Date);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				successTxtBlk.Foreground = Brushes.Red;
				successTxtBlk.Text = "Failure, Train was not added.";
				return;
			}
			MainWindow.Trains.Add(train);
			successTxtBlk.Foreground = Brushes.Green;
			successTxtBlk.Text = "Success! Train was added.";
			MessageBox.Show("Added!");
		}
	}
}
