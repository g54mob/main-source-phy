using System;
using Cpp2ILInjected;
using UnityEngine;

public sealed class EscapeMenuOpenBlocker : MonoBehaviour
{
	private string blockerLabel;

	private string escapeMenuTag;

	private EscapeMenuToggleUnityEvent cachedEscapeMenu;

	public string BlockerLabel => blockerLabel;

	private void OnEnable()
	{
		EscapeMenuToggleUnityEvent escapeMenu = GetEscapeMenu();
		if (escapeMenu != null)
		{
			cachedEscapeMenu = escapeMenu;
			EscapeMenuToggleUnityEvent escapeMenuToggleUnityEvent = cachedEscapeMenu;
			if (this != null)
			{
				escapeMenuToggleUnityEvent.activeBlockers.Add(this);
			}
		}
	}

	private void OnDisable()
	{
		Unregister();
	}

	private void OnDestroy()
	{
		Unregister();
	}

	private void Register()
	{
		EscapeMenuToggleUnityEvent escapeMenu = GetEscapeMenu();
		if (escapeMenu != null)
		{
			cachedEscapeMenu = escapeMenu;
			EscapeMenuToggleUnityEvent escapeMenuToggleUnityEvent = cachedEscapeMenu;
			if (this != null)
			{
				escapeMenuToggleUnityEvent.activeBlockers.Add(this);
			}
		}
	}

	private void Unregister()
	{
		if (cachedEscapeMenu != null)
		{
			EscapeMenuToggleUnityEvent escapeMenuToggleUnityEvent = cachedEscapeMenu;
			if (this != null)
			{
				bool flag = escapeMenuToggleUnityEvent.activeBlockers.Remove(this);
			}
			cachedEscapeMenu = null;
		}
	}

	private EscapeMenuToggleUnityEvent GetEscapeMenu()
	{
		string[] array2;
		object obj2;
		if (cachedEscapeMenu == null)
		{
			GameObject gameObject = GameObject.FindWithTag(escapeMenuTag);
			if (gameObject != null)
			{
				if ((object)gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (!(obj == null))
					{
						return (EscapeMenuToggleUnityEvent)obj;
					}
					string[] array = new string[5];
					if (array != null)
					{
						array[0] = "[EscapeMenuOpenBlocker] \"";
						array[1] = blockerLabel;
						array[2] = "\" found tag \"";
						array[3] = escapeMenuTag;
						array2 = array;
						obj2 = "\" but no EscapeMenuToggleUnityEvent component on it. Blocker will not be registered.";
						goto IL_01dc;
					}
				}
			}
			else
			{
				string[] array3 = new string[5];
				if (array3 != null)
				{
					array3[0] = "[EscapeMenuOpenBlocker] \"";
					array3[1] = blockerLabel;
					array3[2] = "\" could not find a GameObject with tag \"";
					array3[3] = escapeMenuTag;
					array2 = array3;
					obj2 = "\". Blocker will not be registered.";
					goto IL_01dc;
				}
			}
			return (EscapeMenuToggleUnityEvent)(object)new NullReferenceException();
		}
		return cachedEscapeMenu;
		IL_01dc:
		array2[4] = (string)obj2;
		string message = string.Concat(array2);
		Debug.LogWarning(message, this);
		return null;
	}

	public EscapeMenuOpenBlocker()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39FAF]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		blockerLabel = "Unnamed Blocker";
		escapeMenuTag = "EscapeMenu";
		base._002Ector();
	}
}
