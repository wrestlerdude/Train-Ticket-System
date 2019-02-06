using System;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan
	 * Class: BookingDecorator
	 * Description: Decorator pattern that adds extended functionality to the booking to
	 * allow fare calculation.
	 * Last Modified: 20/11/2018
	 */
	public class BookingDecorator
	{
		private Booking booking;
		private double fare = 0.00;

		//BookingDecorator Constructor: Sets object to extend and calls fare calculation
		public BookingDecorator(Booking booking)
		{
			this.booking = booking;
			CalculateFare();
		}
		
		/*
		CalculateFare(): Calculates booking fare by 
		Edinburgh – London: £50
		Between any 2 intermediate stations or any intermediate station and Edinburgh or London : £25
		First class £10 surcharge
		Sleeper £10 surcharge
		Sleeper cabin £20 surcharge
		*/
		private void CalculateFare()
		{
			string[] stations = { "Edinburgh (Waverley)", "London (Kings Cross)" };
			if ((booking.DepartureStation == stations[0] || booking.DepartureStation == stations[1])
				&& (booking.ArrivalStation == stations[0] || booking.ArrivalStation == stations[1]))
			{
				fare += 50;
			}
			else
				fare += 25;
			if (booking.FirstClass == true)
				fare += 10;
			if (booking.TrainObj.SleeperBerth == true)
			{
				fare += 10;
				if (booking.Cabin == true)
					fare += 20;
			}
		}

		//Fare: Getter and setter for booking fare, doesn't allow negative fares
		public double Fare
		{
			get => fare;
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("Fare cannot be below £0");
				fare = value;
			}
		}

	}
}
