using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuStateRelay : MonoBehaviour
{
	private enum MainMenuTestState
	{
		None,
		Loading,
		Loaded,
		Unloading,
		Unloaded
	}

	private MissionManager missionManager;

	private bool unsubscribeAfterFirstForward;

	private UnityEvent onMainMenuLoading;

	private UnityEvent onMainMenuLoaded;

	private UnityEvent onMainMenuUnloading;

	private UnityEvent onMainMenuUnloaded;

	private bool verbose;

	private bool enableTestMode;

	private MainMenuTestState testState;

	private bool _subscribed;

	private bool _hasForwardedAny;

	private MainMenuTestState _lastTestState;

	private void OnEnable()
	{
		bool flag = missionManager != null;
		MainMenuStateRelay mainMenuStateRelay = this;
		if (flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 65 Invalid \"Jump target not found in method: 0x180507550\"");
			MainMenuStateRelay mainMenuStateRelay2 = default(MainMenuStateRelay);
			mainMenuStateRelay = mainMenuStateRelay2;
		}
		if (!mainMenuStateRelay.enableTestMode)
		{
			string text = mainMenuStateRelay.name;
			string message = "[MainMenuStateRelay] '" + text + "' has no MissionManager reference assigned.";
			Debug.LogError(message, mainMenuStateRelay);
		}
		else if (mainMenuStateRelay.verbose)
		{
			string text2 = mainMenuStateRelay.name;
			string message2 = "[MainMenuStateRelay] '" + text2 + "': MissionManager missing, but Test Mode is enabled (OK).";
			Debug.Log(message2, mainMenuStateRelay);
		}
	}

	private void OnDisable()
	{
		Unsubscribe();
	}

	private void Update()
	{
		//IL_0179: Expected O, but got I4
		//IL_0082: Expected I4, but got O
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		if (!enableTestMode || testState == _lastTestState)
		{
			return;
		}
		_lastTestState = testState;
		if (testState == MainMenuTestState.None)
		{
			return;
		}
		if (verbose)
		{
			string arg = base.name;
			object obj = default(object);
			object arg2 = (MainMenuTestState)obj;
			string message = $"[MainMenuStateRelay] '{arg}': TestMode firing state '{arg2}'.";
			Debug.Log(message, this);
		}
		object obj2 = testState - 1;
		bool flag = testState == MainMenuTestState.Loading;
		UnityEvent evt;
		if (!flag)
		{
			object obj3 = obj2 - 1;
			if (!flag)
			{
				object obj4 = obj3 - 1;
				if (!flag)
				{
					if ((nint)obj4 != 1)
					{
						return;
					}
					evt = onMainMenuUnloaded;
				}
				else
				{
					evt = onMainMenuUnloading;
				}
			}
			else
			{
				evt = onMainMenuLoaded;
			}
		}
		else
		{
			evt = onMainMenuLoading;
		}
		SafeInvoke(evt);
		MarkForwardedAndMaybeUnsubscribe();
	}

	private void FireTestState(MainMenuTestState state)
	{
		//IL_012a: Expected O, but got I4
		//IL_0037: Expected I4, but got O
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		if (state == MainMenuTestState.None)
		{
			return;
		}
		if (verbose)
		{
			string arg = base.name;
			object obj = default(object);
			object arg2 = (MainMenuTestState)obj;
			string message = $"[MainMenuStateRelay] '{arg}': TestMode firing state '{arg2}'.";
			Debug.Log(message, this);
		}
		object obj2 = state - 1;
		bool flag = state == MainMenuTestState.Loading;
		UnityEvent evt;
		if (!flag)
		{
			object obj3 = obj2 - 1;
			if (!flag)
			{
				object obj4 = obj3 - 1;
				if (!flag)
				{
					if ((nint)obj4 != 1)
					{
						return;
					}
					evt = onMainMenuUnloaded;
				}
				else
				{
					evt = onMainMenuUnloading;
				}
			}
			else
			{
				evt = onMainMenuLoaded;
			}
		}
		else
		{
			evt = onMainMenuLoading;
		}
		SafeInvoke(evt);
		MarkForwardedAndMaybeUnsubscribe();
	}

	private void Subscribe()
	{
		if (!_subscribed)
		{
			Action<string> value = HandleMainMenuLoading;
			missionManager.MainMenuLoading += value;
			Action<string> value2 = HandleMainMenuLoaded;
			missionManager.MainMenuLoaded += value2;
			Action<string> value3 = HandleMainMenuUnloading;
			missionManager.MainMenuUnloading += value3;
			Action<string> value4 = HandleMainMenuUnloaded;
			missionManager.MainMenuUnloaded += value4;
			bool flag = !verbose;
			_subscribed = true;
			if (!flag)
			{
				string text = base.name;
				string message = "[MainMenuStateRelay] '" + text + "': Subscribed to MissionManager.";
				Debug.Log(message, this);
			}
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed && missionManager != null)
		{
			Action<string> value = HandleMainMenuLoading;
			missionManager.MainMenuLoading -= value;
			Action<string> value2 = HandleMainMenuLoaded;
			missionManager.MainMenuLoaded -= value2;
			Action<string> value3 = HandleMainMenuUnloading;
			missionManager.MainMenuUnloading -= value3;
			Action<string> value4 = HandleMainMenuUnloaded;
			missionManager.MainMenuUnloaded -= value4;
			bool flag = !verbose;
			_subscribed = false;
			if (!flag)
			{
				string text = base.name;
				string message = "[MainMenuStateRelay] '" + text + "': Unsubscribed.";
				Debug.Log(message, this);
			}
		}
	}

	private void HandleMainMenuLoading(string sceneName)
	{
		if (verbose)
		{
			string message = "[MainMenuStateRelay] MainMenuLoading: " + sceneName;
			Debug.Log(message, this);
		}
		SafeInvoke(onMainMenuLoading);
		MarkForwardedAndMaybeUnsubscribe();
	}

	private void HandleMainMenuLoaded(string sceneName)
	{
		if (verbose)
		{
			string message = "[MainMenuStateRelay] MainMenuLoaded: " + sceneName;
			Debug.Log(message, this);
		}
		SafeInvoke(onMainMenuLoaded);
		MarkForwardedAndMaybeUnsubscribe();
	}

	private void HandleMainMenuUnloading(string sceneName)
	{
		if (verbose)
		{
			string message = "[MainMenuStateRelay] MainMenuUnloading: " + sceneName;
			Debug.Log(message, this);
		}
		SafeInvoke(onMainMenuUnloading);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 66 Invalid \"Jump target not found in method: 0x1805072D0\"");
	}

	private void HandleMainMenuUnloaded(string sceneName)
	{
		if (verbose)
		{
			string message = "[MainMenuStateRelay] MainMenuUnloaded: " + sceneName;
			Debug.Log(message, this);
		}
		SafeInvoke(onMainMenuUnloaded);
		MarkForwardedAndMaybeUnsubscribe();
	}

	private void MarkForwardedAndMaybeUnsubscribe()
	{
		bool flag = !unsubscribeAfterFirstForward;
		_hasForwardedAny = true;
		if (!flag)
		{
			if (verbose)
			{
				string text = base.name;
				string message = "[MainMenuStateRelay] '" + text + "': Unsubscribing after first forward.";
				Debug.Log(message, this);
			}
			Unsubscribe();
		}
	}

	private static void SafeInvoke(UnityEvent evt)
	{
		evt?.Invoke();
	}
}
