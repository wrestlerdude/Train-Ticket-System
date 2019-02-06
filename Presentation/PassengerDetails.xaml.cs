using System;
using System.Windows;
using System.Windows.Media;
using BusinessObjects;

namespace Presentation
{
	/*
	 * Author: Raish Allan
	 * Class: PassengerDetails
	 * Description: GUI to enter details of parcel/passenger to add to Bookings
	 * Last Modified: 29/11/2018
	 */
    public partial class PassengerDetails : Window
    {
		//PassengerDetails constructor: Fills the seat combobox with the values 1 to 60
        public PassengerDetails()
        {
            InitializeComponent();
			for (int i = 1; i <= 60; i++)
				seatCombo.Items.Add(i);
        }

		//AddBtn_Click: Attempts to add booking details entered to Bookings
		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			TrainFactory factory = TrainFactory.Instance();
			Booking booking;
			//Returns a train which matches the id entered on the form
			try
			{
                //This needs to be done otherwise Find() returns first item in list.
                if (string.IsNullOrEmpty(idTxtBox.Text))
                    throw new ArgumentNullException("ID cannot be empty!");
                Train train = MainWindow.Trains.Find(x => x.TrainID.Contains(idTxtBox.Text));
                if (train == null)
					throw new ArgumentNullException("Train to book on doesn't exist!");

				booking = factory.CreateBooking(train, nameTxtBox.Text, departCombo.Text, arrivalCombo.Text,
												bool.Parse(firstclassCombo.Text), bool.Parse(cabinCombo.Text),
												coachCombo.Text[0], (int)seatCombo.SelectedValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Failure();
				return;
			}
			
			BookingDecorator decorator = new BookingDecorator(booking);
			//Opens a message box displaying fare and asking for user's confirmation
			MessageBoxResult result = MessageBox.Show($"Are you sure? The price will be £{decorator.Fare}", "Price Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.No)
			{
				Failure();
                TrainFactory.Records[booking.TrainID].RemoveAt(TrainFactory.Records[booking.TrainID].Count-1);
				return;
			}

			//Actually adds booking to Bookings
			MainWindow.Bookings.Add(booking);
			successTxtBlk.Foreground = Brushes.Green;
			successTxtBlk.Text = "Success! Booking was added.";
			MessageBox.Show("Added!");
		}

		//Failure(): Made to cut down on a few lines of code, self explanatory
		private void Failure()
		{
			successTxtBlk.Foreground = Brushes.Red;
			successTxtBlk.Text = "Failure, Booking was not added.";
		}
	}
}
