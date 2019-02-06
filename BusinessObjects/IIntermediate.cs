using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessObjects
{
	/* Author: Raish Allan
	 * Description: Adding intermediate functionality to derived train classes
	 * Last Modified: 30/11/2018
	 */
	//IIntermediate: Interface for Trains that can have intermediate stops between its departure and destination
	public interface IIntermediate
	{
		//Intermediates: List of intermediate stations offered
		List<string> Intermediates { get; set; }
	}
	
	/*IntermediateExtension: Interface Extension design pattern that has Multiple-Inheritance-like capabilities.
	  Gives all classes which inherits IIntermediate the implementation of these methods, so it can use them as if they were its own method.*/
	public static class IntermediateExtension
	{
		private static string[] stations = { "Peterborough", "Darlington", "York", "Newcastle" };
		//IntermediateValidation(): Exception thrown if a list of string contains invalid station values
		public static void IntermediateValidation(this IIntermediate train, List<string> value)
		{
			foreach (var station in value)
			{
				if (!stations.Contains(station))
					throw new ArgumentOutOfRangeException("Intermediates are not valid.");
			}
		}
		//AddIntermediate(): Allows another station to be added to Intermediates
		public static bool AddIntermediate(this IIntermediate train, string intermediate)
		{
			if (!stations.Contains(intermediate) || train.Intermediates.Contains(intermediate))
				return false;
			train.Intermediates.Add(intermediate);
			return true;
		}
		//IString(): Returns the list of intermediates as a single string joined by a hyphen
		public static string IString(this IIntermediate train) => string.Join("-", train.Intermediates.ToArray());
	}
}
