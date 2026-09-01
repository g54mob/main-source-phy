using System;
using Cpp2ILInjected;
using UnityEngine;

public class AnimatorBoolTogglerRelay : MonoBehaviour
{
	public enum DiscoveryMode
	{
		ByTag,
		DirectReference
	}

	private DiscoveryMode discoveryMode;

	private string targetTag;

	private AnimatorBoolToggler directTarget;

	private bool autoRefreshOnEnable;

	private bool tryRefreshIfMissingOnCall;

	private bool logWarnings;

	private AnimatorBoolToggler _cachedTarget;

	private void Awake()
	{
		if (autoRefreshOnEnable)
		{
			RefreshTarget();
		}
	}

	private void OnEnable()
	{
		if (autoRefreshOnEnable)
		{
			RefreshTarget();
		}
	}

	public void RefreshTarget()
	{
		//IL_00ac: Expected I4, but got O
		_cachedTarget = null;
		if (discoveryMode == DiscoveryMode.ByTag)
		{
			AnimatorBoolToggler cachedTarget = FindFirstTogglerByTag(targetTag);
			_cachedTarget = cachedTarget;
		}
		else if (discoveryMode == DiscoveryMode.DirectReference)
		{
			_cachedTarget = directTarget;
		}
		if (logWarnings && _cachedTarget == null)
		{
			object obj = default(object);
			object arg = (DiscoveryMode)obj;
			string text = string.Format(arg2: (!(directTarget != null)) ? "null" : directTarget.name, format: "Mode={0}, Tag='{1}', DirectTarget={2}.", arg0: arg, arg1: targetTag);
			string message = "[AnimatorBoolTogglerRelay] No AnimatorBoolToggler found. " + text;
			Debug.LogWarning(message, this);
		}
	}

	public void SetEnabled()
	{
		AnimatorBoolToggler targetOrTryRefresh = GetTargetOrTryRefresh();
		if (targetOrTryRefresh != null)
		{
			targetOrTryRefresh.SetBool(value: true);
		}
	}

	public void SetDisabled()
	{
		AnimatorBoolToggler targetOrTryRefresh = GetTargetOrTryRefresh();
		if (targetOrTryRefresh != null)
		{
			targetOrTryRefresh.SetBool(value: false);
		}
	}

	public void Toggle()
	{
		AnimatorBoolToggler targetOrTryRefresh = GetTargetOrTryRefresh();
		if (targetOrTryRefresh != null)
		{
			targetOrTryRefresh.ToggleBool();
		}
	}

	private AnimatorBoolToggler GetTargetOrTryRefresh()
	{
		bool flag = _cachedTarget != null;
		if (!flag && tryRefreshIfMissingOnCall != flag)
		{
			RefreshTarget();
		}
		return _cachedTarget;
	}

	private static AnimatorBoolToggler FindFirstTogglerByTag(string tag)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0046: Expected O, but got I4
		//IL_004f: Expected O, but got I4
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		if (!string.IsNullOrEmpty(tag))
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
			if (array == null)
			{
				goto IL_0118;
			}
			object obj = array + 32;
			object obj2 = 0;
			UnityEngine.Object obj4 = default(UnityEngine.Object);
			for (object obj3 = 0; (nint)obj2 < array.Length; obj3++, obj += 8, obj2 = obj3)
			{
				if (!((UnityEngine.Object)obj != null))
				{
					continue;
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					if (!(obj4 == null))
					{
						return (AnimatorBoolToggler)obj4;
					}
					continue;
				}
				goto IL_0118;
			}
		}
		return null;
		IL_0118:
		return (AnimatorBoolToggler)(object)new NullReferenceException();
	}

	public AnimatorBoolTogglerRelay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F87]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		targetTag = "Untagged";
		autoRefreshOnEnable = true;
		base._002Ector();
	}
}
