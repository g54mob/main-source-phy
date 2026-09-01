using System;
using System.Collections.Generic;
using System.Globalization;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OdometerNoteLogger : MonoBehaviour
{
	public OdometerDisplay odometerDisplay;

	public NotepadSection targetSection;

	private bool autoFindSection;

	private string sectionTag;

	public string noteFormat;

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

	public void LogOdometerNote()
	{
		bool flag = TryResolveSection("LogOdometerNote");
		if (odometerDisplay != null && targetSection != null)
		{
			float displayedNumber = odometerDisplay.DisplayedNumber;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			float num = default(float);
			string newValue = num.ToString("F2", invariantCulture);
			string text = noteFormat.Replace("{value}", newValue);
			float delaySeconds = default(float);
			NotepadSection.TextRevealMode revealMode = default(NotepadSection.TextRevealMode);
			float typewriterSecondsPerCharacter = default(float);
			targetSection.Write(text, writeMode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
			string text2 = base.name;
			string message = text2 + ": Logged odometer note: " + text;
			Debug.Log(message);
		}
		else
		{
			string text3 = base.name;
			string message2 = text3 + ": Missing reference for odometerDisplay or targetSection!";
			Debug.LogWarning(message2);
		}
	}

	private void ContextTryResolve()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A27E]");
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

	public OdometerNoteLogger()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A280]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoFindSection = true;
		sectionTag = "Ballistics";
		noteFormat = "Odometer Reading: {value}";
		base._002Ector();
	}

	static OdometerNoteLogger()
	{
		HashSet<string> hashSet = new HashSet<string>();
		s_WarnedMissingTags = hashSet;
	}
}
