using System;
using System.Linq;
using System.Collections.Generic;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan
	 * Class: Booking
	 * Description: Details the booking for a passenger/parcel onboard a set train.
	 * Last Modified: 30/11/2018
	 */
	[Serializable]
	public class Booking
	{
		private string name, id, departure_station, arrival_station;
		private bool first_class, cabin;
		private char coach;
		private int seat;
		private Train train;
		
		public Booking() { }

		//Booking constructor: Initializes all the neccesary details + a private train object.
		public Booking(Train train, string name, string departure, string arrival, bool first_class, bool cabin, char coach, int seat)
		{
			this.train = train;
			Name = name;
			id = train.TrainID;
			DepartureStation = departure;
			ArrivalStation = arrival;
			FirstClass = first_class;
			Cabin = cabin;
			Coach = coach;
			Seat = seat;
		}

		//TrainObj: Getter and setter for train object
		public Train TrainObj { get => train; set => train = value; }
		
		//Name: Getter and setter, checks if value is empty
		public string Name
		{
			get => name;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("Name must not be blank!");
				else
					name = value;
			}
		}
		//TrainID: Getter and setter, sets id for train affliated
		public string TrainID
		{
			get => id;
			set => id = value;
		}
		//DepartureStation: Getter and setter for departure location, checks if it is a valid station
		public string DepartureStation
		{
			get => departure_station;
			set
			{
				List<string> stations = new List<string>{"Edinburgh (Waverley)", "London (Kings Cross)"};
                if (train is IIntermediate)
                    stations.AddRange(((IIntermediate)train).Intermediates);
				//If station is not Edinburgh/London and station doesn't match train's intermediates available then throw an exception
				if (!stations.Contains(value))
					throw new ArgumentOutOfRangeException("Not a valid departure station.");
				departure_station = value;
			}
		}
		//ArrivalStation: Getter and setter for arrival location, check if it is a valid station
		public string ArrivalStation
		{
			get => arrival_station;
			set
			{
				List<string> stations = new List<string>{"Edinburgh (Waverley)", "London (Kings Cross)"};
                if (train is IIntermediate)
                    stations.AddRange(((IIntermediate)train).Intermediates);
                //Does not allow depart and arrival to be the same
                if (value == departure_station)
					throw new ArgumentOutOfRangeException("Arrival cannot be the same as departure!");
				else if (!stations.Contains(value))
					throw new ArgumentOutOfRangeException("Train does not have that arrival station!");
				arrival_station = value;
			}
		}
		//FirstClass: Getter and setter for first class status, checks if first class can actually be true
		public bool FirstClass
		{
			get => first_class;
			set
			{
				if (value == true && train.FirstClass == false)
					throw new ArgumentOutOfRangeException("Booking cannot be first class because train doesn't offer it.");
				first_class = value;
			}
		}
		//Cabin: Getter and setter for cabin status, checks if cabin can actually exist
		public bool Cabin
		{
			get => cabin;
			set
			{
				if (value == true && train.SleeperBerth == false)
					throw new ArgumentOutOfRangeException("Booking cannot have cabin because train doesn't offer it.");
				cabin = value;
			}
		}
		//Coach: Getter and setter for coach section, checks if coach character is in range of A-H
		public char Coach
		{
			get => coach;
			set
			{
				List<char> range = "ABCDEFGH".ToList();
				if (!range.Contains(value))
					throw new ArgumentOutOfRangeException("Coach must be A-H.");
				coach = value;
			}
		}
		//Seat: Getter and setter for seat number within coach, checks if value is in range of 1-60
		public int Seat
		{
			get => seat;
			set
			{
				if (value < 1 || value > 60)
					throw new ArgumentOutOfRangeException("Seat number must be 1-60");
				seat = value;
			}
		}
	}
}
