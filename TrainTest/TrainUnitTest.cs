using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessObjects;
using System.Collections.Generic;

namespace TrainTest
{
	/*
	 * Author: Raish Allan
	 * Class: TrainUnitTest
	 * Description: Unit test for BusinessObjects
	 * Last Modified: 01/12/2018
	 */
	[TestClass]
	public class TrainUnitTest
	{
		private static Train train = new ExpressTrain();
		private static Booking booking = new Booking();
		private static TrainFactory factory = TrainFactory.Instance();
		private static SleeperTrain example = new SleeperTrain("SL3P", "Edinburgh (Waverley)", "London (Kings Cross)", false,
																new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"),
																new List<string> { "Peterborough", "Darlington" });

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestTrainID() => train.TrainID = "AAAAA";

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestTrainID2() => train.TrainID = "";

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestTrainID3() => train.TrainID = "11";

		[TestMethod]
		public void TestTrainID4()
		{
			train.TrainID = "BE74";
			StringAssert.Contains(train.TrainID, "BE74");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestTrainDeptDest() => train.Departure = "abcdefgh";

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestTrainDeptDest2() => train.Destination = "abcdefgh";

		[TestMethod]
		public void TestTrainDeptDest3()
		{
			train.Departure = "Edinburgh (Waverley)";
			StringAssert.Contains(train.Departure, "Edinburgh (Waverley)");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestTrainDeptDest4()
		{
			train.Departure = "Edinburgh (Waverley)";
			train.Destination = "Edinburgh (Waverly)";
		}

		[TestMethod]
		public void TestTrainDeptDest5()
		{
			train.Departure = "Edinburgh (Waverley)";
			train.Destination = "London (Kings Cross)";
			StringAssert.Contains(train.Destination, "London (Kings Cross)");
		}

		[TestMethod]
		public void TestExpressTrain()
		{
			ExpressTrain express = new ExpressTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"));
			Assert.AreEqual(false, express.SleeperBerth);
		}

		[TestMethod]
		public void TestSleeperTrain()
		{
			try
			{
				SleeperTrain sleeper = new SleeperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
														new TimeSpan(8, 0, 0), DateTime.Parse("2018-01-01"), new List<string> { });
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Sleeper trains must depart after 9pm.");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		public void TestSleeperTrain2()
		{
			SleeperTrain sleeper = new SleeperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"), new List<string> { });
			Assert.AreEqual(true, sleeper.SleeperBerth);
		}

		[TestMethod]
		public void TestStopperTrain()
		{
			StopperTrain stopper = new StopperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"), new List<string> { });
			Assert.AreEqual(false, stopper.SleeperBerth);
		}

		[TestMethod]
		public void TestStopperTrain2()
		{
			List<string> intermediates = new List<string> { "Fake", "Fake 2", "Fake 3" };
			try
			{
				StopperTrain stopper = new StopperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"), intermediates);
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Intermediates are not valid.");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		public void TestStopperTrain3()
		{
			List<string> intermediates = new List<string> { "Peterborough", "Newcastle" };
			StopperTrain stopper = new StopperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"), intermediates);
		}

		[TestMethod]
		public void TestStopperTrain4()
		{
			List<string> intermediates = new List<string> { "Peterborough", "Newcastle" };
			StopperTrain stopper = new StopperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"), intermediates);
			Assert.AreEqual(false, stopper.AddIntermediate("Fake"));
		}

		[TestMethod]
		public void TestStopperTrain5()
		{
			List<string> intermediates = new List<string> { "Peterborough", "Newcastle" };
			StopperTrain stopper = new StopperTrain("A134", "Edinburgh (Waverley)", "London (Kings Cross)", false,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"), intermediates);
			Assert.AreEqual(true, stopper.AddIntermediate("Darlington"));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestBookingName1() => booking.Name = "";

		[TestMethod]
		public void TestBookingName2() => booking.Name = "Raish";

		[TestMethod]
		public void TestBookingDept1()
		{
			booking.TrainObj = example;
			try
			{
				booking.DepartureStation = "Fake";
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Not a valid departure station.");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		public void TestBookingDept2()
		{
			booking.TrainObj = example;
			booking.DepartureStation = "Edinburgh (Waverley)";
		}

		[TestMethod]
		public void TestBookingArrival()
		{
			booking.TrainObj = example;
			booking.DepartureStation = "Edinburgh (Waverley)";
			try
			{
				booking.ArrivalStation = "Edinburgh (Waverley)";
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Arrival cannot be the same as departure!");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		public void TestBookingArrival2()
		{
			booking.TrainObj = example;
			booking.DepartureStation = "Edinburgh (Waverley)";
			booking.ArrivalStation = "Darlington";
		}

		[TestMethod]
		public void TestBookingFirstClass()
		{
			booking.TrainObj = example;
			try
			{
				booking.FirstClass = true;
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Booking cannot be first class because train doesn't offer it.");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		public void TestBookingFirstClass2()
		{
			SleeperTrain example2 = new SleeperTrain("SL3P", "Edinburgh (Waverley)", "London (Kings Cross)", true,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"),
													new List<string> { "Peterborough", "Darlington" });
			booking.TrainObj = example2;
			booking.FirstClass = true;
		}

		[TestMethod]
		public void TestBookingCabin()
		{
			SleeperTrain example2 = new SleeperTrain("SL3P", "Edinburgh (Waverley)", "London (Kings Cross)", true,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"),
													new List<string> { "Peterborough", "Darlington" });
			booking.TrainObj = example2;
			booking.Cabin = true;
		}

		[TestMethod]
		public void TestBookingCabin2()
		{
			StopperTrain example2 = new StopperTrain("SL3P", "Edinburgh (Waverley)", "London (Kings Cross)", true,
													new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"),
													new List<string> { "Peterborough", "Darlington" });
			booking.TrainObj = example2;
			try
			{
				booking.Cabin = true;
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Booking cannot have cabin because train doesn't offer it.");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestBookingCoach() => booking.Coach = 'Z';


		[TestMethod]
		public void TestBookingCoach2() => booking.Coach = 'B';

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestBookingSeat() => booking.Seat = 500;

		[TestMethod]
		public void TestBookingSeat2() => booking.Seat = 50;

		[TestMethod]
		public void TestFactoryTrain()
		{
			TrainFactory.Records = new Dictionary<string, List<string>>();
			factory.CreateTrain("SL3P", "Sleeper", "Edinburgh (Waverley)", "London (Kings Cross)", true,
								new List<string> { "Peterborough", "Darlington" },
								new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestFactoryTrain2()
		{
			TrainFactory.Records = new Dictionary<string, List<string>>();
			factory.CreateTrain("SL3P", "Sleeper", "Edinburgh (Waverley)", "London (Kings Cross)", true,
								new List<string> { "Peterborough", "Darlington" },
								new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"));
			factory.CreateTrain("SL3P", "Sleeper", "Edinburgh (Waverley)", "London (Kings Cross)", true,
								new List<string> { "Peterborough", "Darlington" },
								new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"));
		}

		[TestMethod]
		public void TestFactoryTrain3()
		{
			TrainFactory.Records = new Dictionary<string, List<string>>();
			try
			{
				factory.CreateTrain("SL3P", "Fake", "Edinburgh (Waverley)", "London (Kings Cross)", true,
									new List<string> { "Peterborough", "Darlington" },
									new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"));
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Not a valid train type!");
				return;
			}
			throw new Exception();
		}

		public Train InitFactoryBooking()
		{
			TrainFactory.Records = new Dictionary<string, List<string>>();
			Train t = factory.CreateTrain("11AA", "Sleeper", "Edinburgh (Waverley)", "London (Kings Cross)", true,
								new List<string> { "Peterborough", "Darlington" },
								new TimeSpan(22, 0, 0), DateTime.Parse("2018-01-01"));
			return t;
		}

		[TestMethod]
		public void TestFactoryBooking()
		{
			Train t = InitFactoryBooking();
			factory.CreateBooking(t, "Test", "Peterborough", "London (Kings Cross)", true, true, 'A', 8);
		}

		[TestMethod]
		public void TestFactoryBooking2()
		{
			InitFactoryBooking();
			try
			{
				factory.CreateBooking(example, "Test", "Peterborough", "London (Kings Cross)", false, false, 'A', 8);
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Train for booking cannot be found.");
				return;
			}
			throw new Exception();
		}

		[TestMethod]
		public void TestFactoryBooking3()
		{
			Train t = InitFactoryBooking();
			factory.CreateBooking(t, "Test", "Peterborough", "London (Kings Cross)", false, false, 'A', 8);
			try
			{
				factory.CreateBooking(t, "Test2", "Darlington", "Edinburgh (Waverley)", true, true, 'A', 8);
			}
			catch (Exception e)
			{
				StringAssert.Contains(e.Message, "Specific seating already taken.");
				return;
			}
			throw new Exception();
		}

	}
}
