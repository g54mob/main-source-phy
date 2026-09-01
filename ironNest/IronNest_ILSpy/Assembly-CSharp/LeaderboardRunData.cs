using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class LeaderboardRunData
{
	[Serializable]
	public class ActionData
	{
		public string ActionName;

		public string Details;

		public int ScoreDelta;

		public DateTime TimestampUTC;

		public string PerformanceStatsJson;
	}

	public Guid? SessionId;

	private int _a;

	private int _b;

	private int _key;

	private int _salt;

	private int _check;

	private bool _t;

	public List<ActionData> Actions;

	public int Score
	{
		get
		{
			//IL_0011: Expected O, but got I4
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected I4, but got Unknown
			object obj = _a - _b;
			int num = obj ^ _key;
			int num2 = num - _salt;
			int num3 = ~num2;
			if (_check != num3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC99]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				_t = true;
				PlayerPrefs.SetInt("1577626b-18aa-47c6-8067-1bf1f5127fa6", 1);
			}
			return num2;
		}
		set
		{
			//IL_0013: Expected I4, but got I8
			//IL_0034: Expected I4, but got I8
			//IL_0054: Expected O, but got I4
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected I4, but got Unknown
			//IL_0076: Expected I4, but got I8
			int key = UnityEngine.Random.Range(-2147483648, 2147483647);
			_key = key;
			object obj = (_salt = UnityEngine.Random.Range(-2147483648, 2147483647)) + value;
			int num = obj ^ _key;
			int num2 = UnityEngine.Random.Range(-2147483648, 2147483647);
			int check = ~value;
			_b = num2;
			int a = num2 + num;
			_check = check;
			_a = a;
		}
	}

	public bool Check
	{
		get
		{
			//IL_0011: Expected O, but got I4
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected I4, but got Unknown
			//IL_002f: Expected O, but got I4
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			object obj = _a - _b;
			int num = obj ^ _key;
			object obj2 = num - _salt;
			object obj3 = ~obj2;
			object obj4 = _check - obj3;
			bool flag = obj4 == null;
			return !flag;
		}
	}

	public bool FinalCheck
	{
		get
		{
			//IL_0036: Expected O, but got I4
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected I4, but got Unknown
			//IL_0054: Expected O, but got I4
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			if (_t)
			{
				return true;
			}
			object obj = _a - _b;
			int num = obj ^ _key;
			object obj2 = num - _salt;
			object obj3 = ~obj2;
			object obj4 = _check - obj3;
			bool flag = obj4 == null;
			return !flag;
		}
	}

	private void FlagTamper()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC99]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_t = true;
		PlayerPrefs.SetInt("1577626b-18aa-47c6-8067-1bf1f5127fa6", 1);
	}

	public LeaderboardRunData()
	{
		List<ActionData> actions = new List<ActionData>();
		Actions = actions;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
