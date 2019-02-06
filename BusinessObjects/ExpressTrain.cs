using System;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan 
	 * Class: ExpressTrain
	 * Decription: This class is a child of Train, used to define the attributes of an Express train.
	 * Last Modified: 23/11/2018
	 */
	[Serializable]
	public class ExpressTrain : Train
	{
		public ExpressTrain() { }

		//ExpressTrain Constructor: Uses parent constructor and sets its type and sleeper capabilities
		public ExpressTrain(string id, string departure, string destination, bool first_class, TimeSpan departure_time, DateTime departure_day)
			: base(id, departure, destination, first_class, departure_time, departure_day)
		{
			type = "Express";
			sleeper_berth = false;
		}
	}
}
