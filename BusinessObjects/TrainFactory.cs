using System;
using System.Collections.Generic;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan
	 * Class: TrainFactory
	 * Description: Singleton + Factory pattern, allows creation and unique id validation for Booking and Train classes.
	 * Last Modified: 23/11/2018
	 */
	public class TrainFactory
	{
		private static TrainFactory instance;
		//Records: Every train id has a list of seats booked with it
		public static Dictionary<string, List<String>> Records = new Dictionary<string, List<string>>();

		//Instance(): Keeps track and sets a single possible instance of TrainFactory
		public static TrainFactory Instance()
		{
			if (instance == null)
				instance = new TrainFactory();

			return instance;
		}

		//CreateTrain(): Creates derived train objects with validation to check if train id already exists
		public Train CreateTrain(string id, string type, string departure, string destination,
			 bool first_class, List<string> intermediates, TimeSpan time, DateTime day)
		{
			Train t;
			if (Records.ContainsKey(id))
					throw new InvalidOperationException("Train already exists!");

			if (type == "Stopping")
				t = new StopperTrain(id, departure, destination, first_class, time, day, intermediates);
			else if (type == "Express")
				t = new ExpressTrain(id, departure, destination, first_class, time, day);
			else if (type == "Sleeper")
				t = new SleeperTrain(id, departure, destination, first_class, time, day, intermediates);
			else
				throw new ArgumentOutOfRangeException("Not a valid train type!");

			Records.Add(id, new List<string>());
			return t;
		}

		//CreateBooking(): Creates booking objects with validation to check if coach+seat already taken
		public Booking CreateBooking(Train train, string name, string departure, string arrival, bool first_class, bool cabin, char coach, int seat)
		{
			if (!Records.TryGetValue(train.TrainID, out List<string> value))
				throw new ArgumentNullException("Train for booking cannot be found.");
			else if (value.Contains(coach + seat.ToString()))
				throw new ArgumentException("Specific seating already taken.");

			Booking b;
			b = new Booking(train, name, departure, arrival, first_class, cabin, coach, seat);
			Records[train.TrainID].Add(coach + seat.ToString());
			return b;
		}
	}
}
