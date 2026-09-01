using System;
using Cpp2ILInjected;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

public class DlcCheck : MonoBehaviour
{
	private uint dlcAppId;

	private UnityEvent dlcNotInstalledEvent;

	private UnityEvent dlcInstalledEvent;

	private void Start()
	{
		//IL_0056: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18060C6D0");
		object obj = default(object);
		if (obj == null)
		{
			Debug.LogError("Steamworks is not initialized.");
			return;
		}
		GameObject gameObject = base.gameObject;
		((!gameObject.activeSelf || SteamApps.BIsDlcInstalled((AppId_t)dlcAppId)) ? dlcInstalledEvent : dlcNotInstalledEvent)?.Invoke();
	}

	public bool CheckDlcStatus()
	{
		//IL_00e2: Expected I4, but got O
		//IL_0058: Expected O, but got I4
		GameObject gameObject = base.gameObject;
		if ((object)gameObject != null)
		{
			if (gameObject.activeSelf && !SteamApps.BIsDlcInstalled((AppId_t)dlcAppId))
			{
				if (dlcNotInstalledEvent != null)
				{
					dlcNotInstalledEvent.Invoke();
				}
				return false;
			}
			if (dlcInstalledEvent != null)
			{
				dlcInstalledEvent.Invoke();
			}
			return true;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
