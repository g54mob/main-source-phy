using System;
using UnityEngine;

public class JsonHelper
{
	[Serializable]
	private class Wrapper<T>
	{
		public T[] array;
	}

	public static T[] getJsonArray<T>(string json)
	{
		return JsonUtility.FromJson<Wrapper<T>>("{ \"array\": " + json + "}").array;
	}
}
