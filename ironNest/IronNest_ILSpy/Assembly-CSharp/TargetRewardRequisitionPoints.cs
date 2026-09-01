using System;
using Cpp2ILInjected;
using UnityEngine;

public class TargetRewardRequisitionPoints : MonoBehaviour
{
	private int points;

	private bool debugLog;

	private bool autoFindTracker;

	private MissionStatsTracker explicitTracker;

	private string defaultSourceLabel;

	private MissionStatsTracker cachedTracker;

	private MissionStatsTracker Tracker
	{
		get
		{
			if (!explicitTracker)
			{
				bool flag = cachedTracker;
				if (!flag && autoFindTracker != flag)
				{
					MissionStatsTracker missionStatsTracker = UnityEngine.Object.FindFirstObjectByType<MissionStatsTracker>();
					cachedTracker = missionStatsTracker;
				}
				return cachedTracker;
			}
			return explicitTracker;
		}
	}

	public void Grant()
	{
		if (string.IsNullOrWhiteSpace(defaultSourceLabel))
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 44 Invalid \"Jump target not found in method: 0x18047BAB0\"");
		throw new NullReferenceException();
	}

	public void GrantWithSource(string sourceLabel)
	{
		bool flag = string.IsNullOrWhiteSpace(sourceLabel);
		bool flag2 = !flag;
		string sourceLabel2 = sourceLabel;
		if (!flag2)
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			sourceLabel2 = text;
		}
		InternalGrant(points, sourceLabel2);
	}

	public void GrantAmount(int amount)
	{
		string sourceLabel;
		if (string.IsNullOrWhiteSpace(defaultSourceLabel))
		{
			GameObject gameObject = base.gameObject;
			sourceLabel = gameObject.name;
		}
		else
		{
			sourceLabel = defaultSourceLabel;
		}
		InternalGrant(amount, sourceLabel);
	}

	private void InternalGrant(int amount, string sourceLabel)
	{
		string format;
		if (amount > 0)
		{
			UnityEngine.Object obj;
			if (!explicitTracker)
			{
				bool flag = cachedTracker;
				if (!flag && autoFindTracker != flag)
				{
					MissionStatsTracker missionStatsTracker = UnityEngine.Object.FindFirstObjectByType<MissionStatsTracker>();
					cachedTracker = missionStatsTracker;
				}
				obj = cachedTracker;
			}
			else
			{
				obj = explicitTracker;
			}
			if (!obj)
			{
				if (debugLog)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string text = $"[TargetRewardRequisitionPoints] No MissionStatsTracker available. Could not grant {arg} points (Source='{sourceLabel}'). ";
					string message = text + "Assign an explicit tracker or enable Auto Find Tracker and ensure a MissionStatsTracker exists in the scene.";
					Debug.LogWarning(message, this);
				}
				return;
			}
			((MissionStatsTracker)obj).AddRequisitionPoints(amount, sourceLabel);
			if (!debugLog)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			format = "[TargetRewardRequisitionPoints] Granted {0} requisition points (Source='{1}').";
		}
		else
		{
			if (!debugLog)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			format = "[TargetRewardRequisitionPoints] Grant ignored because amount was {0} (Source='{1}').";
		}
		object arg2 = default(object);
		string message2 = string.Format(format, arg2, sourceLabel);
		Debug.Log(message2, this);
	}

	public TargetRewardRequisitionPoints()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A40D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		points = 10;
		debugLog = true;
		defaultSourceLabel = "Reward";
		base._002Ector();
	}
}
