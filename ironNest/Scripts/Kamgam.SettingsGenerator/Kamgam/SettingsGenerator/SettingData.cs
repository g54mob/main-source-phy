using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingData
	{
		public enum DataType
		{
			Unknown = 0,
			Int = 1,
			Float = 2,
			Bool = 3,
			String = 4,
			Color = 5,
			KeyCombination = 6,
			Option = 7,
			ColorOption = 8
		}

		public static Dictionary<DataType, Type> Types;

		public static Dictionary<DataType, List<Type>> CompatibleTypes;

		public string ID;

		public DataType Type;

		[SerializeField]
		public int[] IntValues;

		[SerializeField]
		public float[] FloatValues;

		[SerializeField]
		public string[] StringValues;

		public SettingData(string path, DataType type, int[] intValues, float[] floatValues, string[] stringValues)
		{
		}

		public SettingData(string path, DataType type)
		{
		}
	}
}
