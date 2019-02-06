using System;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan 
	 * Class: Train
	 * Decription: This abstract class is meant to be the general basis of which the
	 * Stopping, Sleeper and Express Trains derive from. Not all properties have their validation or constructs sets defined here
	 * as this is defined within the derived classes.
	 * Last Modified: 30/11/2018
	 */
	[Serializable]
	public abstract class Train
	{
		private string id, departure, destination;
		protected string type;
		protected bool sleeper_berth;
		protected TimeSpan departure_time;
		private DateTime departure_day;
		private bool first_class;
		
		public Train() { }

		//Train Constructor: Sets all the properties which are consistent for any derived classes.
		public Train(string id, string departure, string destination, bool first_class, TimeSpan departure_time, DateTime departure_day)
		{
			TrainID = id;
			Departure = departure;
			Destination = destination;
			FirstClass = first_class;
			DepartureTime = departure_time;
			DepartureDay = departure_day;
		}

		//TimeToString(): Returns the departure time in a formatted string.
		public string TimeToString() => departure_time.ToString(@"hh\:mm");

		//DayToString(): Returns the Date part from the DateTime departure date property
		public string DayToString() => departure_day.ToShortDateString();
		
		//TrainID: Getter and Setter for train id, checks if value set is a 4 character long string
		public string TrainID
		{
			get => id;
			set
			{
				if (value.Length != 4)
					throw new ArgumentOutOfRangeException("Not a 4 character code.");
				id = value;
			}
		}
		//Departure: Getter and setter for departure station, checks if value is a valid station.
		public string Departure
		{
			get => departure;
			set
			{
				if (value != "Edinburgh (Waverley)" && value != "London (Kings Cross)")
					throw new ArgumentOutOfRangeException("Departure Not Edinburgh (Waverley) OR London (Kings Cross).");
				departure = value;
			}
		}
		//Destination: Getter and setter for destination station, checks if value is a valid station and that it isn't already the departure
		public string Destination
		{
			get => destination;
			set
			{
				if (value == departure || (value != "Edinburgh (Waverley)" && value != "London (Kings Cross)"))
					throw new ArgumentOutOfRangeException("Destination Mismatch / Not Edinburgh (Waverley) OR London (Kings Cross).");
				destination = value;
			}
		}

		//Most of these do not require specific validation, or are validated and set conditioned through other means
		//Type: Getter and setter for train type
		public string Type { get => type; set => type = value; }
		//DepartureTime: Getter and setter for train departure time, virtual to allow override for SleeperTrain
		public virtual TimeSpan DepartureTime { get => departure_time; set => departure_time = value; }
		//DepartureDay: Getter and setter for train depature date
		public DateTime DepartureDay { get => departure_day; set => departure_day = value; }
		//SleeperBerth: Getter and setter for if Train offers overnight/cabin utilities
		public bool SleeperBerth { get => sleeper_berth; set => sleeper_berth = value; }
		//FirstClass: Getter and Setter for if Train offers first class utilities
		public bool FirstClass{ get => first_class; set => first_class = value; }
	}
}
