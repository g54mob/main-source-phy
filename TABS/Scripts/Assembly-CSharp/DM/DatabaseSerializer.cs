using System.Text;
using UnityEngine;

namespace DM
{
	public class DatabaseSerializer
	{
		public static byte[] Serialize<T>(T databaseFile)
		{
			return Encoding.UTF8.GetBytes(JsonUtility.ToJson(databaseFile, prettyPrint: true));
		}

		public static T Deserialize<T>(byte[] databaseBytes)
		{
			return JsonUtility.FromJson<T>(Encoding.UTF8.GetString(databaseBytes));
		}
	}
}
