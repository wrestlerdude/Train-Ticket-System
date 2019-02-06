using System;
using System.Collections.Generic;
using DataAccess;
using System.IO;

namespace BusinessObjects
{
	/*
	 * Author: Raish Allan
	 * Class: Combined
	 * Description: Allows for streamlined and easier serialization of a list of trains and bookings
	 * Last Modified: 30/11/2018
	 */
	[Serializable]
	public class Combined
	{
		//Trains: Contains the list of trains to save/load
		public List<Train> Trains { get; set; }
		//Bookings: Contains the list of bookings to save/load
		public List<Booking> Bookings { get; set; }

		public Combined() { }
		//Combined constructor: Sets references to the lists
		public Combined(List<Train> trains, List<Booking> bookings)
		{
			Trains = trains;
			Bookings = bookings;
		}
	}
	/*
	 * Author: Raish Allan
	 * Class: TrainFacade
	 * Description: Facade Design pattern that links the layers together, allows the presentation layer to use the data layer
	 * Last Modified: 30/11/2018
	 */
	public static class TrainFacade
	{
		//SerializePair(): Passes references to SaveData()
		public static void SerializePair(Combined trains_bookings, FileStream fs) => Storage.SaveData(trains_bookings, fs);
		//DeserializePair(): Passes reference to LoadData(), casts the Object type to Combined and returns it.
		public static Combined DeserializePair(FileStream fs) => (Combined)Storage.LoadData(fs);
	}
}
