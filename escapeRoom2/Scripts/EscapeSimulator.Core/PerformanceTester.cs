using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class PerformanceTester
{
	[Serializable]
	private class RoomResult
	{
		public string roomName;

		public List<SpotResult> spotResults = new List<SpotResult>();

		private SpotResult totalResult;
	}

	[Serializable]
	private class SpotResult
	{
		public float angles;

		public float minFPS;

		public float maxFPS;

		public float avgFPS;
	}

	public static int numOfSpots;

	public static bool isActive;

	private static List<RoomDatabase.Room> roomsToTest;

	private static List<RoomResult> results;

	private static int currentRoom;

	public static int currentSpot;

	static PerformanceTester()
	{
		numOfSpots = 8;
		isActive = Is.perfTest();
		roomsToTest = new List<RoomDatabase.Room>();
		results = new List<RoomResult>();
		currentRoom = -1;
		currentSpot = -1;
		foreach (RoomDatabase.Room allRoom in RoomDatabase.getAllRooms())
		{
			if (allRoom != null && allRoom.shouldBeBuilt)
			{
				string name = allRoom.name;
				if (!(name == "Lobby1") && !(name == "Test1") && !allRoom.name.Contains("Darkest") && !allRoom.name.Contains("Zombie"))
				{
					roomsToTest.Add(allRoom);
				}
			}
		}
	}

	public static void startTestingIfNeeded()
	{
		currentRoom = -1;
		loadNextRoomIfNeeded();
	}

	private static string floatToString(float value)
	{
		return value.ToString("0.00", CultureInfo.InvariantCulture);
	}

	public static void loadNextRoomIfNeeded()
	{
		if (!isActive)
		{
			return;
		}
		currentSpot = 0;
		currentRoom++;
		if (currentRoom >= roomsToTest.Count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RoomResult result in results)
			{
				stringBuilder.AppendLine(result.roomName + ",Spot Angles,Min FPS,Max FPS,Avg FPS");
				foreach (SpotResult spotResult in result.spotResults)
				{
					stringBuilder.AppendLine("," + floatToString(spotResult.angles) + "," + floatToString(spotResult.minFPS) + "," + floatToString(spotResult.maxFPS) + "," + floatToString(spotResult.avgFPS));
				}
				float value = result.spotResults.Min((SpotResult x) => x.minFPS);
				float value2 = result.spotResults.Max((SpotResult x) => x.maxFPS);
				float value3 = result.spotResults.Sum((SpotResult x) => x.avgFPS) / (float)result.spotResults.Count;
				stringBuilder.AppendLine(",Total," + floatToString(value) + "," + floatToString(value2) + "," + floatToString(value3));
				stringBuilder.AppendLine("");
			}
			Debug.Log("PerformanceTester: Testing complete. Results:\n" + stringBuilder.ToString());
			Debug.Log("PerformanceTester: Quitting application.");
			File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"ES2_PerformanceTester_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt"), stringBuilder.ToString());
			if (!Is.Editor)
			{
				Application.Quit();
			}
		}
		else
		{
			RoomResult roomResult = new RoomResult
			{
				roomName = roomsToTest[currentRoom].name
			};
			results.Add(roomResult);
			PineSceneManager.loadScene(roomResult.roomName, "", "", null);
		}
	}

	public static void reportSpot(List<float> fps)
	{
		if (currentSpot < numOfSpots)
		{
			Debug.Log("PerformanceTester: Room " + currentRoom + "/" + roomsToTest.Count + " Spot " + currentSpot + "/" + numOfSpots);
			SpotResult spotResult = new SpotResult();
			spotResult.angles = (float)currentSpot * (360f / (float)numOfSpots);
			spotResult.minFPS = Mathf.Min(fps.ToArray());
			spotResult.maxFPS = Mathf.Max(fps.ToArray());
			spotResult.avgFPS = fps.Sum() / (float)fps.Count;
			results[currentRoom].spotResults.Add(spotResult);
			currentSpot++;
			if (currentSpot >= numOfSpots)
			{
				loadNextRoomIfNeeded();
			}
		}
	}
}
