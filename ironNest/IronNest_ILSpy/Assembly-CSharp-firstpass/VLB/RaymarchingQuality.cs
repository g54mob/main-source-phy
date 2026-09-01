using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

[Serializable]
public class RaymarchingQuality
{
	public string name;

	public int stepCount;

	private int _UniqueID;

	private static RaymarchingQuality ms_DefaultInstance;

	private const int kRandomUniqueIdMinRange = 4;

	public int uniqueID => _UniqueID;

	public bool hasValidUniqueID
	{
		get
		{
			int num = _UniqueID >> 31;
			return (byte)(num ^ 1) != 0;
		}
	}

	public static RaymarchingQuality defaultInstance => ms_DefaultInstance;

	private RaymarchingQuality(int uniqueID)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CA2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		_UniqueID = uniqueID;
		name = "New quality";
		stepCount = 10;
	}

	public static RaymarchingQuality New()
	{
		int num = UnityEngine.Random.Range(4, 2147483647);
		RaymarchingQuality raymarchingQuality = new RaymarchingQuality(0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CA2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		raymarchingQuality._UniqueID = num;
		raymarchingQuality.name = "New quality";
		raymarchingQuality.stepCount = 10;
		return raymarchingQuality;
	}

	public static RaymarchingQuality New(string name, int forcedUniqueID, int stepCount)
	{
		RaymarchingQuality raymarchingQuality = new RaymarchingQuality(0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CA2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		raymarchingQuality._UniqueID = forcedUniqueID;
		raymarchingQuality.name = "New quality";
		raymarchingQuality.stepCount = 10;
		raymarchingQuality.name = name;
		raymarchingQuality.stepCount = stepCount;
		return raymarchingQuality;
	}

	private static bool HasRaymarchingQualityWithSameUniqueID(RaymarchingQuality[] values, int id)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_00d2: Expected I4, but got O
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		object obj = values + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj2 < values.Length)
			{
				if ((nint)obj3 >= values.Length)
				{
					break;
				}
				object obj4 = obj;
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v23 @ rcx_v3+1C]");
					if ((nint)0 == id)
					{
						return true;
					}
				}
				obj3++;
				obj += 8;
				obj2 = obj3;
				continue;
			}
			return false;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}

	static RaymarchingQuality()
	{
		//IL_005a: Expected I4, but got I8
		RaymarchingQuality raymarchingQuality = new RaymarchingQuality(0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CA2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		raymarchingQuality._UniqueID = -1;
		raymarchingQuality.name = "New quality";
		raymarchingQuality.stepCount = 10;
		ms_DefaultInstance = raymarchingQuality;
	}
}
