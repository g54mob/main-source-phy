using System;
using Cpp2ILInjected;
using UnityEngine;

public class SteamLeaderboardScoreRelay : MonoBehaviour
{
	private SteamLeaderboardScoreController target;

	private bool autoFindByTag;

	private string targetTag;

	private int fixedAmount;

	private void OnEnable()
	{
		bool flag = ResolveTargetIfNeeded();
	}

	private bool ResolveTargetIfNeeded()
	{
		//IL_015f: Expected I4, but got O
		bool flag = target != null;
		if (!flag)
		{
			if (autoFindByTag != flag && !string.IsNullOrEmpty(targetTag) && targetTag != "Untagged")
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag(targetTag);
				if (gameObject != null)
				{
					if ((object)gameObject == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					SteamLeaderboardScoreController steamLeaderboardScoreController = default(SteamLeaderboardScoreController);
					target = steamLeaderboardScoreController;
					if (!(target == null))
					{
						goto IL_014b;
					}
				}
			}
			bool flag2 = SteamLeaderboardScoreController._003CInstance_003Ek__BackingField != null;
			if (!flag2)
			{
				return flag2;
			}
			target = SteamLeaderboardScoreController._003CInstance_003Ek__BackingField;
		}
		goto IL_014b;
		IL_014b:
		return true;
	}

	public void RelayAdd()
	{
		if (ResolveTargetIfNeeded())
		{
			target.AddToScore(fixedAmount);
			bool includeImage = default(bool);
			LeaderboardManager.Instance.RecordAction("RelayAdd", "", fixedAmount, includeImage);
		}
		else
		{
			Debug.LogWarning("SteamLeaderboardScoreRelay: No target controller found.");
		}
	}

	public void RelayAddValue(int amount)
	{
		if (ResolveTargetIfNeeded())
		{
			target.AddToScore(amount);
			bool includeImage = default(bool);
			LeaderboardManager.Instance.RecordAction("RelayAddValue", "", amount, includeImage);
		}
		else
		{
			Debug.LogWarning("SteamLeaderboardScoreRelay: No target controller found.");
		}
	}

	public void RelaySubmit()
	{
		if (ResolveTargetIfNeeded())
		{
			SteamLeaderboardScoreController steamLeaderboardScoreController = target;
			target.SubmitScoreInternal(steamLeaderboardScoreController.useForceUpdate);
		}
		else
		{
			Debug.LogWarning("SteamLeaderboardScoreRelay: No target controller found.");
		}
	}

	public void RelaySetScore(int value)
	{
		if (ResolveTargetIfNeeded())
		{
			SteamLeaderboardScoreController steamLeaderboardScoreController = target;
			bool flag = value < 0;
			int pendingScore = 0;
			if (!flag)
			{
				pendingScore = value;
			}
			bool flag2 = !steamLeaderboardScoreController.verboseLogging;
			steamLeaderboardScoreController.pendingScore = pendingScore;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[SteamLeaderboardScoreController] SetScore: pendingScore={arg}";
				Debug.Log(message);
			}
		}
		else
		{
			Debug.LogWarning("SteamLeaderboardScoreRelay: No target controller found.");
		}
	}

	public void RelayResetScore()
	{
		if (ResolveTargetIfNeeded())
		{
			SteamLeaderboardScoreController steamLeaderboardScoreController = target;
			bool flag = !steamLeaderboardScoreController.verboseLogging;
			steamLeaderboardScoreController.pendingScore = 0;
			if (!flag)
			{
				Debug.Log("[SteamLeaderboardScoreController] ResetScore: pendingScore=0");
			}
		}
		else
		{
			Debug.LogWarning("SteamLeaderboardScoreRelay: No target controller found.");
		}
	}

	public void SetTarget(SteamLeaderboardScoreController newTarget)
	{
		target = newTarget;
	}

	public SteamLeaderboardScoreRelay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A917]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		targetTag = "Untagged";
		fixedAmount = 1;
		base._002Ector();
	}
}
