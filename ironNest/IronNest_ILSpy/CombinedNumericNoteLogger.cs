using System;
using System.Collections.Generic;
using System.Globalization;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CombinedNumericNoteLogger : MonoBehaviour
{
	public OdometerDisplay odometerDisplay;

	public DialInteractable dialInteractable;

	public NotepadSection targetSection;

	private bool autoFindSection;

	private string sectionTag;

	public string noteFormat;

	private int odoDecimalPlaces;

	private int dialDecimalPlaces;

	public string contextText;

	private NotepadSection.WriteMode writeMode;

	private NotepadSection.AddPosition addPosition;

	private static readonly HashSet<string> s_WarnedMissingTags;

	private void Awake()
	{
		if (this.odometerDisplay == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			OdometerDisplay odometerDisplay = default(OdometerDisplay);
			this.odometerDisplay = odometerDisplay;
		}
		if (this.dialInteractable == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DialInteractable dialInteractable = default(DialInteractable);
			this.dialInteractable = dialInteractable;
		}
		bool flag = TryResolveSection("Awake");
	}

	private void OnEnable()
	{
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded += value;
		bool flag = TryResolveSection("OnEnable");
	}

	private void OnDisable()
	{
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded -= value;
	}

	private void OnValidate()
	{
		bool flag = odoDecimalPlaces < 0;
		int num = 0;
		if (!flag)
		{
			num = odoDecimalPlaces;
		}
		odoDecimalPlaces = num;
		bool flag2 = dialDecimalPlaces < 0;
		int num2 = 0;
		if (!flag2)
		{
			num2 = dialDecimalPlaces;
		}
		dialDecimalPlaces = num2;
		if (autoFindSection && !string.IsNullOrWhiteSpace(sectionTag) && !s_WarnedMissingTags.Contains(sectionTag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(sectionTag);
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (targetSection == null)
		{
			Scene scene2 = default(Scene);
			string text = scene2.name;
			string context = "sceneLoaded:" + text;
			bool flag = TryResolveSection(context);
		}
	}

	public void LogCombinedNote()
	{
		bool flag = TryResolveSection("LogCombinedNote");
		if (odometerDisplay != null && this.dialInteractable != null && targetSection != null)
		{
			float displayedNumber = odometerDisplay.DisplayedNumber;
			DialInteractable dialInteractable = this.dialInteractable;
			string newValue = FormatWithDecimalPlaces(displayedNumber, odoDecimalPlaces);
			string newValue2 = FormatWithDecimalPlaces(dialInteractable.accumulatedValue, dialDecimalPlaces);
			string text = noteFormat.Replace("{odo}", newValue);
			string text2 = text.Replace("{dial}", newValue2);
			string newValue3 = contextText;
			if (contextText == null)
			{
				newValue3 = "";
			}
			string text3 = text2.Replace("{text}", newValue3);
			float delaySeconds = default(float);
			NotepadSection.TextRevealMode revealMode = default(NotepadSection.TextRevealMode);
			float typewriterSecondsPerCharacter = default(float);
			targetSection.Write(text3, writeMode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
			string text4 = base.name;
			string message = text4 + ": Logged combined note: " + text3;
			Debug.Log(message);
		}
		else
		{
			string text5 = base.name;
			string message2 = text5 + ": Missing reference for odometerDisplay, dialInteractable, or targetSection!";
			Debug.LogWarning(message2);
		}
	}

	private void ContextTryResolve()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A20C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = TryResolveSection("ContextMenu");
	}

	private bool TryResolveSection(string context)
	{
		//IL_01a6: Expected I4, but got O
		bool flag = targetSection != null;
		if (!flag)
		{
			if (autoFindSection != flag && !string.IsNullOrWhiteSpace(sectionTag))
			{
				NotepadSection notepadSection = NotepadSection.ResolveByTag(sectionTag);
				if (notepadSection != null)
				{
					targetSection = notepadSection;
					string[] array = new string[6];
					string text = base.name;
					if (array != null)
					{
						array[0] = text;
						array[1] = ": Auto-assigned NotepadSection via Tag='";
						array[2] = sectionTag;
						array[3] = "' (";
						array[4] = context;
						array[5] = ").";
						string message = string.Concat(array);
						Debug.Log(message, this);
						goto IL_0187;
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
			}
			return false;
		}
		goto IL_0187;
		IL_0187:
		return true;
	}

	private static string FormatWithDecimalPlaces(float value, int decimals)
	{
		switch (decimals)
		{
		default:
		{
			int num = default(int);
			string text = num.ToString();
			string text2 = "F" + text;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			float num2 = default(float);
			return num2.ToString(text2, invariantCulture);
		}
		}
	}

	public CombinedNumericNoteLogger()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A20F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoFindSection = true;
		sectionTag = "MainData";
		noteFormat = "ODO={odo} | Dial={dial} | Note: {text}";
		odoDecimalPlaces = 2;
		dialDecimalPlaces = 2;
		contextText = "";
		base._002Ector();
	}

	static CombinedNumericNoteLogger()
	{
		HashSet<string> hashSet = new HashSet<string>();
		s_WarnedMissingTags = hashSet;
	}
}
