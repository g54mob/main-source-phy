using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public static class QualitySettingUtils
{
	private static bool? _areQualitiesOrderedLowToHigh;

	public unsafe static bool AreQualitiesOrderedLowToHigh()
	{
		//IL_0236: Expected I, but got O
		//IL_0219: Expected I, but got O
		//IL_0205: Expected O, but got I4
		//IL_0249: Expected I4, but got O
		//IL_00ac: Expected O, but got I4
		//IL_01e8: Expected O, but got I4
		nint num = (nint)typeof(QualitySettingUtils);
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj = default(object);
		bool result = default(bool);
		bool? flag;
		if (obj == null)
		{
			string[] names = QualitySettings.names;
			if (names != null && names.Length != 0)
			{
				if (names.Length > 0)
				{
					object obj2 = names.Length - 1;
					if ((nint)obj2 < names.Length)
					{
						if (names[0].Contains("High") || names[0].Contains("Best") || names[0].Contains("Ultra") || names[obj2].Contains("Low") || names[obj2].Contains("Bad") || names[obj2].Contains("Worst"))
						{
							flag = (byte)(&result) != 0;
							_areQualitiesOrderedLowToHigh = (bool?)(object)0;
							return false;
						}
						goto IL_01ee;
					}
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
			}
			goto IL_01ee;
		}
		nint num3 = (nint)typeof(QualitySettingUtils);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
		return result;
		IL_01ee:
		flag = (byte)(&result) != 0;
		_areQualitiesOrderedLowToHigh = (bool?)(object)0;
		return true;
	}

	public static int MapToQualityLevel(int value, int min, int max)
	{
		//IL_009b: Expected I4, but got O
		//IL_0036: Expected O, but got I4
		//IL_0051: Expected O, but got I4
		//IL_005e: Expected O, but got I4
		string[] names = QualitySettings.names;
		bool flag = names == null;
		if (!flag)
		{
			object obj = names.Length - 1;
			if (!flag)
			{
				object obj2 = value - min;
				object obj3 = max - min;
				object obj4 = obj2 / obj3;
				object obj5 = obj4 * obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				int result = default(int);
				return result;
			}
			return 0;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public static int InvertQualityLevel(int qualityLevel)
	{
		//IL_0056: Expected I4, but got O
		//IL_0035: Expected O, but got I4
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected I4, but got Unknown
		string[] names = QualitySettings.names;
		if (names != null)
		{
			object obj = names.Length - qualityLevel;
			return obj - 1;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public static int MapQualityLevelToRange(int qualityLevel, int min, int max)
	{
		//IL_009a: Expected I4, but got O
		//IL_0036: Expected O, but got I4
		//IL_0051: Expected O, but got I4
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected I4, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		string[] names = QualitySettings.names;
		bool flag = names == null;
		if (!flag)
		{
			object obj = names.Length - 1;
			if (!flag)
			{
				object obj2 = max - min;
				int num = qualityLevel / obj;
				object obj3 = num * obj2;
				object obj4 = obj3 + min;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				int result = default(int);
				return result;
			}
			return min;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}
}
