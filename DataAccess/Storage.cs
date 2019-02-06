using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DataAccess
{
	/*
	 * Author: Raish Allan
	 * Class: Storage
	 * Description: Allows for binary serilization and deserilization on an object
	 * Last Modified: 27/11/2018
	 */
	public static class Storage
	{
		//LoadData(): Deserializes a file into an object
		public static Object LoadData(FileStream read_file)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			Object obj = formatter.Deserialize(read_file);
			read_file.Close();
			return obj;
		}

		//SaveData(): Serializes an object into a file
		public static void SaveData(Object obj, FileStream write_file)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(write_file, obj);
			write_file.Close();
		}
	}
}
