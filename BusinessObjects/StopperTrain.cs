using System;
using System.Collections.Generic;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan
	 * Class: StopperTrain
	 * Description: This class is a child of Train and implents IIntermediate, used to define the attributes of an Stopper train
	 * and adds intermediate functionality.
	 * Last modified: 23/11/2018
	 */
	[Serializable]
	public class StopperTrain : Train, IIntermediate
	{
		private List<string> intermediates;

		public StopperTrain() { }

		//StopperTrain Constructor: Uses parent constructor and sets its type, sleeper capabilities and intermediate stations
		public StopperTrain(string id, string departure, string destination, bool first_class, TimeSpan departure_time, DateTime departure_day, List<string> intermediates)
			: base(id, departure, destination, first_class, departure_time, departure_day)
		{
			type = "Stopping";
			sleeper_berth = false;
			Intermediates = intermediates;
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
