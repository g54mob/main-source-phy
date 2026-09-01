using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UniversalTMPTextLogger : MonoBehaviour
{
	public enum LineSelectionMode
	{
		All,
		FirstN,
		LastN
	}

	public TMP_Text sourceTMP;

	public NotepadSection targetSection;

	private bool autoFindSection;

	private string sectionTag;

	public string noteFormat;

	private LineSelectionMode lineSelection;

	private int lineCount;

	private NotepadSection.WriteMode writeMode;

	private NotepadSection.AddPosition addPosition;

	private static readonly HashSet<string> s_WarnedMissingTags;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A282]");
		if ((nint)0 == 0)
		{
			_ = 1;
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
		if (lineCount < 0)
		{
			lineCount = 0;
		}
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

	public void LogTMPTextToNotepad()
	{
		//IL_0129: Expected I4, but got O
		bool flag = TryResolveSection("LogTMPTextToNotepad");
		if (sourceTMP != null && targetSection != null)
		{
			string text = sourceTMP.text;
			if (!string.IsNullOrEmpty(text))
			{
				bool flag2 = lineCount < 0;
				int count = 0;
				if (!flag2)
				{
					count = lineCount;
				}
				string newValue = SelectLines(text, lineSelection, count);
				string content = noteFormat.Replace("{text}", newValue);
				NotepadSection notepadSection = targetSection;
				float delaySeconds = default(float);
				NotepadSection.TextRevealMode revealMode = default(NotepadSection.TextRevealMode);
				float typewriterSecondsPerCharacter = default(float);
				targetSection.Write(content, writeMode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
				string arg = base.name;
				object obj = default(object);
				object arg2 = (LineSelectionMode)obj;
				string arg3;
				if (lineSelection == LineSelectionMode.All)
				{
					arg3 = "";
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg4 = default(object);
					arg3 = $" {arg4}";
				}
				string message = $"{arg}: Logged TMP note ({arg2}{arg3}).";
				Debug.Log(message);
			}
			else
			{
				string text2 = base.name;
				string message2 = text2 + ": Source TMP text is empty, nothing to log.";
				Debug.LogWarning(message2);
			}
		}
		else
		{
			string text3 = base.name;
			string message3 = text3 + ": Missing reference for sourceTMP or targetSection!";
			Debug.LogWarning(message3);
		}
	}

	private void ContextTryResolve()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A288]");
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

	private static string SelectLines(string text, LineSelectionMode mode, int count)
	{
		//IL_03c1: Expected O, but got I
		//IL_03d1: Expected O, but got I
		//IL_0394: Expected O, but got I
		//IL_03a4: Expected O, but got I
		//IL_018a: Expected O, but got I4
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Expected O, but got Unknown
		//IL_02b0: Expected O, but got I4
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		string text3;
		StringBuilder stringBuilder;
		if (!string.IsNullOrEmpty(text))
		{
			if (text != null)
			{
				string text2 = text.Replace("\r\n", "\n");
				if (text2 != null)
				{
					text3 = text2.Replace('\r', '\n');
					if (mode == LineSelectionMode.All)
					{
						goto IL_03ff;
					}
					if (text3 != null)
					{
						string[] array = text3.Split('\n');
						if (array != null)
						{
							if (array.Length != 0 && count >= 0)
							{
								bool flag = count <= array.Length;
								int num = count;
								if (!flag)
								{
									num = array.Length;
								}
								if (num != 0)
								{
									stringBuilder = new StringBuilder();
									if (mode != LineSelectionMode.FirstN)
									{
										object obj = array.Length - num;
										object obj2 = obj + 4;
										object obj3 = obj2 * 8;
										object obj4 = (object)array + obj3;
										object obj5 = obj;
										object obj6 = obj;
										while ((nint)obj6 < array.Length)
										{
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
											{
												if (stringBuilder == null)
												{
													goto IL_03f1;
												}
												StringBuilder stringBuilder2 = stringBuilder.Append('\n');
											}
											if (stringBuilder != null)
											{
												StringBuilder stringBuilder3 = stringBuilder.Append((string)obj4);
												obj6++;
												obj5++;
												obj4 += 8;
												continue;
											}
											goto IL_03f1;
										}
									}
									else if (num > 0)
									{
										object obj7 = array + 32;
										object obj8 = 0;
										while (true)
										{
											if ((nint)obj8 > 0)
											{
												if (stringBuilder == null)
												{
													break;
												}
												StringBuilder stringBuilder4 = stringBuilder.Append('\n');
											}
											if (stringBuilder == null)
											{
												break;
											}
											StringBuilder stringBuilder5 = stringBuilder.Append((string)obj7);
											obj8++;
											obj7 += 8;
											if ((nint)obj8 < num)
											{
												continue;
											}
											goto IL_0372;
										}
										goto IL_03f1;
									}
									if (stringBuilder != null)
									{
										goto IL_0372;
									}
									goto IL_03f1;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							object obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v12+B8]");
							object obj10 = 0;
							text3 = (string)obj10;
							goto IL_03ff;
						}
					}
				}
			}
			goto IL_03f1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v3+B8]");
		return (string)0;
		IL_0372:
		text3 = stringBuilder.ToString();
		goto IL_03ff;
		IL_03f1:
		return (string)(object)new NullReferenceException();
		IL_03ff:
		return text3;
	}

	public UniversalTMPTextLogger()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A28B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoFindSection = true;
		sectionTag = "MainData";
		noteFormat = "Note: {text}";
		lineCount = 5;
		writeMode = NotepadSection.WriteMode.Replace;
		base._002Ector();
	}

	static UniversalTMPTextLogger()
	{
		HashSet<string> hashSet = new HashSet<string>();
		s_WarnedMissingTags = hashSet;
	}
}
