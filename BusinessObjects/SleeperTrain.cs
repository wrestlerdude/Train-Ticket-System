using System;
using System.Collections.Generic;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan
	 * Class: SleeperTrain
	 * Description: This class is a child of Train and implents IIntermediate, used to define the attributes of a Sleeper train
	 * and adds intermediate functionality.
	 * Last modified: 23/11/2018
	 */
	[Serializable]
	public class SleeperTrain : Train, IIntermediate
	{
		private List<string> intermediates;

		public SleeperTrain() { }

		//SleeperTrain Constructor: Uses parent constructor and sets its type, sleeper capabilities and intermediate stations
		public SleeperTrain(string id, string departure, string destination, bool first_class, TimeSpan departure_time, DateTime departure_day, List<string> intermediates)
			: base(id, departure, destination, first_class, departure_time, departure_day)
		{
			type = "Sleeper";
			sleeper_berth = true;
			Intermediates = intermediates;
		}

		//DepartureTime: Getter and setter that overrides parent property to validate if sleeper train is departing after 9pm.
		public override TimeSpan DepartureTime
		{
			get => departure_time;
			set
			{
				TimeSpan start = new TimeSpan(21, 0, 0);
				TimeSpan end = new TimeSpan(23, 59, 59);
				if (value > start && value <= end)
					departure_time = value;
				else
					throw new ArgumentOutOfRangeException("Sleeper trains must depart after 9pm.");
			}
		}

		//Intermediates: Getter and setter for intermediate stations, value is checked if it contains valid stations
		public List<string> Intermediates
		{
			get => intermediates;
			set
			{
				this.IntermediateValidation(value);
				intermediates = value;
			}
		}
	}
}
