using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration.API;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Kamgam.UGUIComponentsForSettings;

public class TabButtonUGUI : MonoBehaviour
{
	public int GroupID;

	public GameObject Normal;

	public GameObject Active;

	public GameObject Content;

	public GameObject GamepadContent;

	private PlayerInput _playerInput;

	public TextMeshProUGUI NormalTextTf;

	public TextMeshProUGUI ActiveTextTf;

	public bool IsActive
	{
		get
		{
			//IL_0041: Expected I4, but got O
			if ((object)Active != null)
			{
				return Active.activeSelf;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public string Text
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI normalTextTf = NormalTextTf;
			if ((object)NormalTextTf != null)
			{
				nint num = (nint)normalTextTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = NormalTextTf.text;
			if (value != text)
			{
				NormalTextTf.text = value;
				ActiveTextTf.text = value;
			}
		}
	}

	public void SetActive(bool active)
	{
		setActiveInternal(active);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x180A73C40\"");
	}

	public void SetActive(bool active, bool includeInactiveSiblings)
	{
		setActiveInternal(active);
		UpdateSiblings(includeInactiveSiblings);
	}

	protected void setActiveInternal(bool active)
	{
		GameObject gameObject = Normal.gameObject;
		bool active2 = (byte)((active ? 1u : 0u) ^ 1u) != 0;
		gameObject.SetActive(active2);
		GameObject gameObject2 = Active.gameObject;
		gameObject2.SetActive(active);
		bool flag = GamepadContent != null;
		GameObject gameObject3 = Content;
		if (flag)
		{
			GameObject gameObject4 = Content.gameObject;
			gameObject4.SetActive(value: false);
			GameObject gameObject5 = GamepadContent.gameObject;
			gameObject5.SetActive(value: false);
			if (!App._003CInitialised_003Ek__BackingField)
			{
				goto IL_0182;
			}
			if (!SteamUtils.IsSteamRunningOnSteamDeck())
			{
				string currentControlScheme = _playerInput.currentControlScheme;
				if (!(currentControlScheme == "Gamepad"))
				{
					goto IL_0182;
				}
			}
			gameObject3 = GamepadContent;
		}
		goto IL_0191;
		IL_0191:
		GameObject gameObject6 = gameObject3.gameObject;
		gameObject6.SetActive(active);
		return;
		IL_0182:
		gameObject3 = Content;
		goto IL_0191;
	}

	public void UpdateSiblings(bool includeInactive = false)
	{
		List<TabButtonUGUI> list = FindSiblings(includeInactive);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<TabButtonUGUI>.Enumerator enumerator = default(List<TabButtonUGUI>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != this)
				{
					if ((object)obj == null)
					{
						break;
					}
					((TabButtonUGUI)obj).setActiveInternal(false);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public List<TabButtonUGUI> FindSiblings(bool includeInactive = false)
	{
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		List<TabButtonUGUI> list = new List<TabButtonUGUI>();
		Transform transform = base.transform;
		Transform parent = transform.parent;
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!(parent == null))
		{
			int num = 0;
			int num2 = 0;
			while (true)
			{
				int childCount = parent.childCount;
				if (num2 >= childCount)
				{
					break;
				}
				Transform child = parent.GetChild(num);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ stack_-38_v7 (UnityEngine.Object)+20]");
					if ((nint)0 == GroupID)
					{
						GameObject gameObject = ((Component)obj).gameObject;
						bool flag = (object)gameObject == null;
						bool activeSelf = gameObject.activeSelf;
						if (!flag)
						{
							list.Add((TabButtonUGUI)obj);
						}
					}
				}
				num++;
				num2 = num;
			}
		}
		else
		{
			Transform transform2 = base.transform;
			GameObject gameObject2 = transform2.gameObject;
			Scene scene = gameObject2.scene;
			Scene scene2 = default(Scene);
			GameObject[] rootGameObjects = scene2.GetRootGameObjects();
			object obj2 = rootGameObjects + 32;
			int num3 = 0;
			int num4 = 0;
			while (num4 < rootGameObjects.Length)
			{
				if (num3 < rootGameObjects.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ stack_-38_v7 (UnityEngine.Object)+20]");
						if ((nint)0 == GroupID)
						{
							GameObject gameObject3 = ((Component)obj).gameObject;
							bool flag2 = (object)gameObject3 == null;
							bool activeSelf2 = gameObject3.activeSelf;
							if (!flag2)
							{
								list.Add((TabButtonUGUI)obj);
							}
						}
					}
					num3++;
					obj2 += 8;
					num4 = num3;
					continue;
				}
				return (List<TabButtonUGUI>)(object)new IndexOutOfRangeException();
			}
		}
		return list;
	}
}
