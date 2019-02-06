using System;
using System.Collections.Generic;
using System.Windows;
using BusinessObjects;
using Microsoft.Win32;
using System.IO;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: MainWindow
	 * Description: Provides event handlers for the main window GUI
	 * Last Modified: 30/11/2018
	 */
	public partial class MainWindow : Window
	{
		//Trains: Runtime list of trains
		public static List<Train> Trains = new List<Train>();
		//Bookings: Runtuime List of Bookings
		public static List<Booking> Bookings = new List<Booking>();

		//MainWindow Constructor: Calls InitializeComponent()
		public MainWindow()
		{
			InitializeComponent();
		}

		//AddTrainBtnClick: Opens up a window to prompt for Train Details to add to Trains
		private void AddTrainBtn_Click(object sender, RoutedEventArgs e)
		{
			TrainDetails window = new TrainDetails();
			window.Show();
		}

		//SearchTrainTwoBtn_Click: Opens up a window to prompt for two stations to search for trains that run between them
		private void SearchTrainTwoBtn_Click(object sender, RoutedEventArgs e)
		{
			TwoStationsWindow window = new TwoStationsWindow();
			window.Show();
		}

		//AddBookingBtn_Click: Opens up a window to prompt for Booking Details to add to Bookings
		private void AddBookingBtn_Click(object sender, RoutedEventArgs e)
		{
			PassengerDetails window = new PassengerDetails();
			window.Show();
		}

		//CalculateFareBtn_Click: Opens up a window that displays all the fares for Bookings
		private void CalculateFareBtn_Click(object sender, RoutedEventArgs e)
		{
			CalculateWindow window = new CalculateWindow();
			window.Show();
		}

		//ShowTrainsBtn_Click: Opens up a window that displays all the Trains
		private void ShowTrainsBtn_Click(object sender, RoutedEventArgs e)
		{
			TrainShow window = new TrainShow(Trains);
			window.Show();
		}

		//SearchTrainDayBtn_Click: Opens up a window to prompt for a date that shows all the trains departing on said date
		private void SearchTrainDayBtn_Click(object sender, RoutedEventArgs e)
		{
			DaySearchWindow window = new DaySearchWindow();
			window.Show();
		}

		//SaveBtn_Click: Saves runtime Trains and Bookings as a file
		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog save_window = new SaveFileDialog();
			save_window.Filter = "DAT File|*.dat";
			save_window.Title = "Save Rail Network data";
			save_window.ShowDialog();

			if (!string.IsNullOrEmpty(save_window.FileName))
			{
				try
				{
					TrainFacade.SerializePair(new Combined(Trains, Bookings), (FileStream)save_window.OpenFile());
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}

			MessageBox.Show("Saved!");
		}

		//LoadBtn_Click: Loads Trains and Bookings from a file and adjust TrainFactory
		private void LoadBtn_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog load_window = new OpenFileDialog();
			load_window.Filter = "DAT File|*.dat";
			load_window.Title = "Load Rail Network data";
			load_window.ShowDialog();

			Combined loaded = null;
			if (!string.IsNullOrEmpty(load_window.FileName))
			{
				try
				{
					loaded = TrainFacade.DeserializePair((FileStream)load_window.OpenFile());
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}
			else
				return;
            
            Trains = loaded.Trains;
			Bookings = loaded.Bookings;
			//TrainFactory needs to be rebuilt because TrainFactory context is not serialized
			TrainFactory.Records = new Dictionary<string, List<string>>();
            if (Bookings.Count == 0)
                foreach (Train t in Trains)
                    TrainFactory.Records.Add(t.TrainID, new List<string>());
            else
			    foreach (Booking b in Bookings)
			    {
				    if (!TrainFactory.Records.ContainsKey(b.TrainID))
					    TrainFactory.Records.Add(b.TrainID, new List<string> { $"{b.Coach}{b.Seat.ToString()}" });
				    else
					    TrainFactory.Records[b.TrainID].Add(b.Coach + b.Seat.ToString());
			    }

			MessageBox.Show("Loaded!");
		}

		//ShowBookingsBtn_Click: Opens a window that displays all the Bookings
		private void ShowBookingsBtn_Click(object sender, RoutedEventArgs e)
		{
			BookingShow window = new BookingShow(Bookings);
			window.Show();
		}

		//SearchPassengerTrainBtn_Click: Opens a window to prompt for a train id and then displays all the parcels/passengers on said train
		private void SearchPassengerTrainBtn_Click(object sender, RoutedEventArgs e)
		{
			PassengerSearchWindow window = new PassengerSearchWindow();
			window.Show();
		}
	}
}
