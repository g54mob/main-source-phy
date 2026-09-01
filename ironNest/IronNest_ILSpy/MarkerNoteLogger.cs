using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MarkerNoteLogger : MonoBehaviour
{
	public NotepadSection targetSection;

	private bool autoFindSection;

	private string sectionTag;

	public string logEntryFormat;

	private NotepadSection.WriteMode writeMode;

	private NotepadSection.AddPosition addPosition;

	private static readonly HashSet<string> s_WarnedMissingTags;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A242]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = TryResolveSection("Awake");
	}

	private void OnEnable()
	{
		Action<MapMarkerLineUI> value = LogMarkerData;
		MapMarkerPlacer.OnMarkerFinalized += value;
		UnityAction<Scene, LoadSceneMode> value2 = OnSceneLoaded;
		SceneManager.sceneLoaded += value2;
		bool flag = TryResolveSection("OnEnable");
	}

	private void OnDisable()
	{
		Action<MapMarkerLineUI> value = LogMarkerData;
		MapMarkerPlacer.OnMarkerFinalized -= value;
		UnityAction<Scene, LoadSceneMode> value2 = OnSceneLoaded;
		SceneManager.sceneLoaded -= value2;
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

	private void LogMarkerData(MapMarkerLineUI marker)
	{
		bool flag = marker == null;
		if (!flag && marker.allowNoteLogging != flag && marker._003CHasReachedMinimumDragDistance_003Ek__BackingField != flag)
		{
			bool flag2 = TryResolveSection("LogMarkerData");
			if (targetSection != null)
			{
				string angleLabelText = marker.AngleLabelText;
				string text = logEntryFormat.Replace("{angle}", angleLabelText);
				string distanceLabelText = marker.DistanceLabelText;
				string content = text.Replace("{distance}", distanceLabelText);
				float delaySeconds = default(float);
				NotepadSection.TextRevealMode revealMode = default(NotepadSection.TextRevealMode);
				float typewriterSecondsPerCharacter = default(float);
				targetSection.Write(content, writeMode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
			}
			else
			{
				string text2 = base.name;
				string message = text2 + ": No NotepadSection resolved for MarkerNoteLogger.";
				Debug.LogWarning(message);
			}
		}
	}

	public void LogCustomNote(string note)
	{
		if (!string.IsNullOrEmpty(note))
		{
			bool flag = TryResolveSection("LogCustomNote");
			if (targetSection != null)
			{
				float delaySeconds = default(float);
				NotepadSection.TextRevealMode revealMode = default(NotepadSection.TextRevealMode);
				float typewriterSecondsPerCharacter = default(float);
				targetSection.Write(note, writeMode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
				string text = base.name;
				string message = text + ": Logged custom note: " + note;
				Debug.Log(message);
			}
			else
			{
				string text2 = base.name;
				string message2 = text2 + ": No NotepadSection resolved for LogCustomNote.";
				Debug.LogWarning(message2);
			}
		}
	}

	private void ContextTryResolve()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A249]");
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

	public MarkerNoteLogger()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A24B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoFindSection = true;
		sectionTag = "MainData";
		logEntryFormat = "Angle: {angle} | Distance: {distance}";
		base._002Ector();
	}

	static MarkerNoteLogger()
	{
		HashSet<string> hashSet = new HashSet<string>();
		s_WarnedMissingTags = hashSet;
	}
}
