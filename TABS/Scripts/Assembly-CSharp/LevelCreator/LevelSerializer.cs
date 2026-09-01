using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LevelCreator
{
	public class LevelSerializer
	{
		private static byte[] magicNumbers = new byte[4] { 103, 140, 84, 76 };

		public static byte[] Serialize(Level level)
		{
			return magicNumbers.Concat(new byte[2] { 0, 4 }).Concat(Encoding.UTF8.GetBytes(JsonUtility.ToJson(Level_Latest.ParseLevel(level), prettyPrint: false))).ToArray();
		}

		public static Level Deserialize(byte[] levelBytes)
		{
			int length = levelBytes.GetLength(0);
			if (length < 6)
			{
				throw new Exception("Bad level file.");
			}
			if (magicNumbers[0] != levelBytes[0] || magicNumbers[1] != levelBytes[1] || magicNumbers[2] != levelBytes[2] || magicNumbers[3] != levelBytes[3])
			{
				return JsonUtility.FromJson<Level_0>(Encoding.UTF8.GetString(levelBytes)).Upgrade().BuildLevel();
			}
			switch ((ushort)(levelBytes[4] * 256 + levelBytes[5]))
			{
			case 1:
				return JsonUtility.FromJson<Level_1>(Encoding.UTF8.GetString(levelBytes, 6, length - 6)).Upgrade().BuildLevel();
			case 2:
				return JsonUtility.FromJson<Level_2>(Encoding.UTF8.GetString(levelBytes, 6, length - 6)).Upgrade().BuildLevel();
			case 3:
				return JsonUtility.FromJson<Level_3>(Encoding.UTF8.GetString(levelBytes, 6, length - 6)).Upgrade().BuildLevel();
			case 4:
				return JsonUtility.FromJson<Level_Latest>(Encoding.UTF8.GetString(levelBytes, 6, length - 6)).BuildLevel();
			default:
				throw new Exception("Unsupported level file version.");
			}
		}
	}
}
