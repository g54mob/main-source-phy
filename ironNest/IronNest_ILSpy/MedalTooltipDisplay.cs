using System;
using Cpp2ILInjected;
using Localisation;
using TMPro;
using UnityEngine;

public class MedalTooltipDisplay : MonoBehaviour
{
	private TMP_Text displayNameText;

	private TMP_Text hintText;

	private string cursorManagerTag;

	private DynamicCursorManager _cursorManager;

	private void OnEnable()
	{
		TrySubscribe();
	}

	private void OnDisable()
	{
		if (_cursorManager != null)
		{
			Action<Interactable> value = OnPassiveTargetChanged;
			_cursorManager.OnPassiveTargetChanged -= value;
			_cursorManager = null;
		}
		ClearTexts();
	}

	private void TrySubscribe()
	{
		if (!(_cursorManager == null))
		{
			return;
		}
		if (!string.IsNullOrEmpty(cursorManagerTag))
		{
			GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
			string[] array;
			if (gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				DynamicCursorManager cursorManager = default(DynamicCursorManager);
				_cursorManager = cursorManager;
				if (!(_cursorManager == null))
				{
					Action<Interactable> value = OnPassiveTargetChanged;
					_cursorManager.OnPassiveTargetChanged += value;
					return;
				}
				string text = base.name;
				string text2 = gameObject.name;
				array = new string[7] { "[MedalTooltipDisplay] '", text, "': GameObject '", text2, "' (tag '", cursorManagerTag, "') does not have a DynamicCursorManager component." };
			}
			else
			{
				string text3 = base.name;
				array = new string[5] { "[MedalTooltipDisplay] '", text3, "': No GameObject found with tag '", cursorManagerTag, "'. Ensure the DynamicCursorManager is tagged correctly and loaded before this card." };
			}
			string message = string.Concat(array);
			Debug.LogWarning(message, this);
		}
		else
		{
			string text4 = base.name;
			string message2 = "[MedalTooltipDisplay] '" + text4 + "': CursorManagerTag is empty. Assign a tag in the Inspector.";
			Debug.LogWarning(message2, this);
		}
	}

	private void Unsubscribe()
	{
		if (_cursorManager != null)
		{
			Action<Interactable> value = OnPassiveTargetChanged;
			_cursorManager.OnPassiveTargetChanged -= value;
			_cursorManager = null;
		}
	}

	private void OnPassiveTargetChanged(Interactable target)
	{
		//IL_00a9: Expected O, but got I
		//IL_00f9: Expected O, but got I
		//IL_010f: Expected O, but got I
		//IL_0165: Expected O, but got I
		//IL_0185: Expected O, but got I
		//IL_0196: Expected I, but got O
		//IL_01a6: Expected O, but got I
		//IL_01b6: Expected O, but got I
		if (target != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				Transform transform = ((Component)obj).transform;
				Transform parent = base.transform;
				if (transform.IsChildOf(parent))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v4 (UnityEngine.Object)+20]");
					if ((UnityEngine.Object)0 != null)
					{
						if (displayNameText != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v4 (UnityEngine.Object)+20]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ rax_v25+20]");
							string text = ((TextIdentifier)0).Get();
							displayNameText.text = text;
						}
						if (!(hintText != null))
						{
							return;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v4 (UnityEngine.Object)+20]");
						object obj3 = 0;
						TMP_Text tMP_Text = hintText;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ rax_v22+28]");
						string text2 = ((TextIdentifier)0).Get();
						nint num = (nint)tMP_Text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ r8_v12 (Il2CppClass<TMPro.TMP_Text>)+558]");
						object obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ r8_v12 (Il2CppClass<TMPro.TMP_Text>)+560]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v317 @ r9_v4 (should have been resolved before IL gen)");
					}
				}
			}
		}
		ClearTexts();
	}

	private void ClearTexts()
	{
		//IL_003e: Expected O, but got I
		//IL_004e: Expected O, but got I
		//IL_009b: Expected O, but got I
		//IL_00ab: Expected O, but got I
		if (displayNameText != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v14+B8]");
			object text = 0;
			displayNameText.text = (string)text;
		}
		if (hintText != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rax_v8+B8]");
			object text2 = 0;
			hintText.text = (string)text2;
		}
	}

	public MedalTooltipDisplay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A131]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		cursorManagerTag = "CursorManager";
		base._002Ector();
	}
}
