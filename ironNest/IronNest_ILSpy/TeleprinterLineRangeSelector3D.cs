using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Cpp2ILInjected;
using TMPro;
using TMPSelection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TeleprinterLineRangeSelector3D : MonoBehaviour
{
	public enum CopyExtractionMode
	{
		RichTextRawSlice_PreserveAuthorNewlines,
		VisualLayoutLinesJoined_WithSeparator
	}

	public enum HighlightStyle
	{
		Solid,
		Outline
	}

	private struct OutlineParts
	{
		public bool IsValid;

		public Transform Top;

		public Transform Bottom;

		public Transform Left;

		public Transform Right;
	}

	private DynamicCursorManager cursorManager;

	private bool autoFindCursorManagerByTag;

	private string cursorManagerTag;

	private Interactable expectedInteractable;

	private TMP_Text sourceTMP;

	private Camera hitTestCamera;

	private bool copyToClipboardOnRelease;

	private CopyExtractionMode copyMode;

	private string lineSeparator;

	private bool trimSelectedText;

	private bool writeToNotepadOnRelease;

	private NotepadSection targetSection;

	private bool autoFindNotepadSectionByTag;

	private string sectionTag;

	private string noteFormat;

	private NotepadSection.WriteMode writeMode;

	private NotepadSection.AddPosition addPosition;

	private bool overrideNotepadRevealForTeleprinterWrites;

	private NotepadSection.TextRevealMode notepadRevealModeOverride;

	private float notepadDelayOverrideSeconds;

	private bool highlightHoveredLine;

	private int dragLineThreshold;

	private Transform highlightPrefab;

	private Material highlightMaterial;

	private Color highlightColor;

	private HighlightStyle highlightStyle;

	private float outlineThicknessLocal;

	private Vector2 localPadding;

	private float normalOffset;

	private bool parentHighlightsToTMP;

	private bool useBrokerLockWhileDragging;

	private string lockBrokerTag;

	private string brokerDebugLabel;

	private bool debugLogs;

	private InputActionReference upAction;

	private InputActionReference downAction;

	private int _003CHoveredLineIndex_003Ek__BackingField;

	private bool _003CIsDraggingSelection_003Ek__BackingField;

	private int _003CSelectedLineMin_003Ek__BackingField;

	private int _003CSelectedLineMax_003Ek__BackingField;

	private TMP_TextInfo _ti;

	private bool _subscribed;

	private bool _isActiveHoverTarget;

	private bool _pressActive;

	private int _pressStartLine;

	private int _dragAnchorLine;

	private readonly List<Transform> _highlights;

	private readonly Dictionary<Transform, OutlineParts> _outlinePartsByRoot;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _dragHandle;

	public int HoveredLineIndex
	{
		get
		{
			return _003CHoveredLineIndex_003Ek__BackingField;
		}
		private set
		{
			_003CHoveredLineIndex_003Ek__BackingField = value;
		}
	}

	public bool IsDraggingSelection
	{
		get
		{
			return _003CIsDraggingSelection_003Ek__BackingField;
		}
		private set
		{
			_003CIsDraggingSelection_003Ek__BackingField = value;
		}
	}

	public int SelectedLineMin
	{
		get
		{
			return _003CSelectedLineMin_003Ek__BackingField;
		}
		private set
		{
			_003CSelectedLineMin_003Ek__BackingField = value;
		}
	}

	public int SelectedLineMax
	{
		get
		{
			return _003CSelectedLineMax_003Ek__BackingField;
		}
		private set
		{
			_003CSelectedLineMax_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		if (sourceTMP == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			TMP_Text tMP_Text = default(TMP_Text);
			sourceTMP = tMP_Text;
		}
		if (expectedInteractable == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Interactable interactable = default(Interactable);
			bool flag = (object)interactable != null;
			Interactable interactable2 = interactable;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				Interactable interactable3 = default(Interactable);
				interactable2 = interactable3;
			}
			expectedInteractable = interactable2;
		}
		if (hitTestCamera == null)
		{
			Camera main = Camera.main;
			hitTestCamera = main;
		}
		bool flag2 = TryResolveCursorManager("Awake");
		bool flag3 = TryResolveNotepadSection("Awake");
		if (sourceTMP != null)
		{
			TMP_TextInfo textInfo = sourceTMP.textInfo;
			_ti = textInfo;
		}
		ClearSelectionAndHighlights();
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
	}

	private void OnEnable()
	{
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded += value;
		if (hitTestCamera == null)
		{
			Camera main = Camera.main;
			hitTestCamera = main;
		}
		bool flag = TryResolveCursorManager("OnEnable");
		bool flag2 = TryResolveNotepadSection("OnEnable");
		SubscribeIfPossible();
		if (_broker == null)
		{
			TryFindBroker();
		}
	}

	private void OnDisable()
	{
		//IL_011a: Expected I4, but got I8
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded -= value;
		if (_subscribed && cursorManager != null)
		{
			Action<Interactable> value2 = HandleCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value2;
			Action<Interactable> value3 = HandlePrimaryClickDown;
			cursorManager.OnPrimaryClickDown -= value3;
			Action<Interactable> value4 = HandlePrimaryClickUp;
			cursorManager.OnPrimaryClickUp -= value4;
			_subscribed = false;
		}
		ReleaseBrokerDragLockIfHeld();
		ClearSelectionAndHighlights();
		_isActiveHoverTarget = false;
		_003CHoveredLineIndex_003Ek__BackingField = -1;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Scene scene2 = default(Scene);
		if (cursorManager == null)
		{
			string text = scene2.name;
			string context = "sceneLoaded:" + text;
			bool flag = TryResolveCursorManager(context);
			SubscribeIfPossible();
		}
		if (targetSection == null)
		{
			string text2 = scene2.name;
			string context2 = "sceneLoaded:" + text2;
			bool flag2 = TryResolveNotepadSection(context2);
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
	}

	private void Update()
	{
		//IL_03be: Expected I4, but got I8
		if (!(sourceTMP != null) || !(expectedInteractable != null))
		{
			return;
		}
		if (cursorManager == null)
		{
			bool flag = TryResolveCursorManager("Update");
			SubscribeIfPossible();
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
		if (!(cursorManager != null))
		{
			return;
		}
		if (_isActiveHoverTarget)
		{
			if (cursorManager.IsCurrentDeviceGamepad() && _isActiveHoverTarget)
			{
				InputAction action = upAction.action;
				action.Enable();
				InputAction action2 = downAction.action;
				action2.Enable();
				InputAction action3 = upAction.action;
				if (!action3.WasPressedThisFrame())
				{
					InputAction action4 = downAction.action;
					if (action4.WasPressedThisFrame())
					{
						int num = _003CHoveredLineIndex_003Ek__BackingField + 1;
						_003CHoveredLineIndex_003Ek__BackingField = num;
					}
				}
				else
				{
					int num2 = _003CHoveredLineIndex_003Ek__BackingField - 1;
					_003CHoveredLineIndex_003Ek__BackingField = num2;
				}
				int num3 = _003CHoveredLineIndex_003Ek__BackingField;
				TMP_TextInfo textInfo = sourceTMP.textInfo;
				if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
				{
					if (num3 > textInfo.lineCount)
					{
						_003CHoveredLineIndex_003Ek__BackingField = textInfo.lineCount;
						goto IL_0306;
					}
				}
				else
				{
					num3 = 0;
				}
				_003CHoveredLineIndex_003Ek__BackingField = num3;
			}
			else if (_003CHoveredLineIndex_003Ek__BackingField == -1 || !cursorManager.IsCurrentDeviceGamepad())
			{
				UpdateHoveredLineFromCursorManager();
			}
			goto IL_0306;
		}
		if (!_pressActive)
		{
			_003CHoveredLineIndex_003Ek__BackingField = -1;
			goto IL_03e8;
		}
		return;
		IL_03e8:
		ClearSelectionAndHighlights();
		return;
		IL_0306:
		if (!_003CIsDraggingSelection_003Ek__BackingField)
		{
			if (highlightHoveredLine)
			{
				if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
				{
					_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
					_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
					UpdateHighlightsForSelectionRange();
					return;
				}
				goto IL_03e8;
			}
			return;
		}
		ExpandSelectionToHovered();
	}

	private void CheckForInput()
	{
		InputAction action = upAction.action;
		action.Enable();
		InputAction action2 = downAction.action;
		action2.Enable();
		InputAction action3 = upAction.action;
		if (!action3.WasPressedThisFrame())
		{
			InputAction action4 = downAction.action;
			if (action4.WasPressedThisFrame())
			{
				int num = _003CHoveredLineIndex_003Ek__BackingField + 1;
				_003CHoveredLineIndex_003Ek__BackingField = num;
			}
		}
		else
		{
			int num2 = _003CHoveredLineIndex_003Ek__BackingField - 1;
			_003CHoveredLineIndex_003Ek__BackingField = num2;
		}
		int num3 = _003CHoveredLineIndex_003Ek__BackingField;
		TMP_TextInfo textInfo = sourceTMP.textInfo;
		if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			if (num3 > textInfo.lineCount)
			{
				_003CHoveredLineIndex_003Ek__BackingField = textInfo.lineCount;
				return;
			}
		}
		else
		{
			num3 = 0;
		}
		_003CHoveredLineIndex_003Ek__BackingField = num3;
	}

	private bool TryResolveCursorManager(string context)
	{
		//IL_0673: Expected I4, but got O
		bool flag = cursorManager != null;
		if (flag)
		{
			goto IL_02dd;
		}
		string[] array3;
		if (autoFindCursorManagerByTag != flag && !string.IsNullOrWhiteSpace(cursorManagerTag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(cursorManagerTag);
			if (gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj != null)
				{
					cursorManager = (DynamicCursorManager)obj;
					if (!debugLogs)
					{
						goto IL_02dd;
					}
					string[] array = new string[8];
					string text = base.name;
					if (array.Length > 0)
					{
						array[0] = text;
						if (array.Length > 1)
						{
							array[1] = ": Resolved DynamicCursorManager via tag '";
							if (array.Length > 2)
							{
								array[2] = cursorManagerTag;
								if (array.Length > 3)
								{
									array[3] = "' on '";
									string text2 = gameObject.name;
									if (array.Length > 4)
									{
										array[4] = text2;
										if (array.Length > 5)
										{
											array[5] = "'. (";
											if (array.Length > 6)
											{
												array[6] = context;
												if (array.Length > 7)
												{
													array[7] = ")";
													string message = string.Concat(array);
													Debug.Log(message, this);
													goto IL_02dd;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					string[] array2 = new string[8];
					string text3 = base.name;
					if (array2.Length > 0)
					{
						array2[0] = text3;
						if (array2.Length > 1)
						{
							array2[1] = ": GameObject '";
							string text4 = gameObject.name;
							if (array2.Length > 2)
							{
								array2[2] = text4;
								if (array2.Length > 3)
								{
									array2[3] = "' has tag '";
									if (array2.Length > 4)
									{
										array2[4] = cursorManagerTag;
										if (array2.Length > 5)
										{
											array2[5] = "' but no DynamicCursorManager was found. (";
											if (array2.Length > 6)
											{
												array2[6] = context;
												if (array2.Length > 7)
												{
													array2[7] = ")";
													array3 = array2;
													goto IL_0673;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (!debugLogs)
				{
					goto IL_04d4;
				}
				string[] array4 = new string[6];
				string text5 = base.name;
				if (array4.Length > 0)
				{
					array4[0] = text5;
					if (array4.Length > 1)
					{
						array4[1] = ": No GameObject found with tag '";
						if (array4.Length > 2)
						{
							array4[2] = cursorManagerTag;
							if (array4.Length > 3)
							{
								array4[3] = "' to resolve DynamicCursorManager. (";
								if (array4.Length > 4)
								{
									array4[4] = context;
									if (array4.Length > 5)
									{
										array4[5] = ")";
										array3 = array4;
										goto IL_0673;
									}
								}
							}
						}
					}
				}
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
		}
		goto IL_04d4;
		IL_04d4:
		return false;
		IL_02dd:
		return true;
		IL_0673:
		string message2 = string.Concat(array3);
		Debug.LogWarning(message2, this);
		goto IL_04d4;
	}

	private bool TryResolveNotepadSection(string context)
	{
		//IL_0485: Expected I4, but got O
		bool flag = targetSection != null;
		if (flag)
		{
			goto IL_02b7;
		}
		if (autoFindNotepadSectionByTag != flag && !string.IsNullOrWhiteSpace(sectionTag))
		{
			NotepadSection notepadSection = NotepadSection.ResolveByTag(sectionTag);
			if (notepadSection != null)
			{
				targetSection = notepadSection;
				if (!debugLogs)
				{
					goto IL_02b7;
				}
				string[] array = new string[8];
				string text = base.name;
				if (array.Length > 0)
				{
					array[0] = text;
					if (array.Length > 1)
					{
						array[1] = ": Resolved NotepadSection via tag '";
						if (array.Length > 2)
						{
							array[2] = sectionTag;
							if (array.Length > 3)
							{
								array[3] = "' -> '";
								string text2 = notepadSection.name;
								if (array.Length > 4)
								{
									array[4] = text2;
									if (array.Length > 5)
									{
										array[5] = "'. (";
										if (array.Length > 6)
										{
											array[6] = context;
											if (array.Length > 7)
											{
												array[7] = ")";
												string message = string.Concat(array);
												Debug.Log(message, this);
												goto IL_02b7;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (!debugLogs)
				{
					goto IL_0464;
				}
				string[] array2 = new string[6];
				string text3 = base.name;
				if (array2.Length > 0)
				{
					array2[0] = text3;
					if (array2.Length > 1)
					{
						array2[1] = ": Could not resolve NotepadSection by tag '";
						if (array2.Length > 2)
						{
							array2[2] = sectionTag;
							if (array2.Length > 3)
							{
								array2[3] = "'. (";
								if (array2.Length > 4)
								{
									array2[4] = context;
									if (array2.Length > 5)
									{
										array2[5] = ")";
										string message2 = string.Concat(array2);
										Debug.LogWarning(message2, this);
										goto IL_0464;
									}
								}
							}
						}
					}
				}
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
		}
		goto IL_0464;
		IL_0464:
		return false;
		IL_02b7:
		return true;
	}

	private bool EnsureReady()
	{
		if (sourceTMP != null && expectedInteractable != null)
		{
			if (cursorManager == null)
			{
				bool flag = TryResolveCursorManager("Update");
				SubscribeIfPossible();
			}
			if (_broker == null)
			{
				TryFindBroker();
			}
			return cursorManager != null;
		}
		return false;
	}

	private void SubscribeIfPossible()
	{
		//IL_01cc: Expected I4, but got I8
		if (_subscribed || !(cursorManager != null))
		{
			return;
		}
		Action<Interactable> value = HandleCursorTargetChanged;
		cursorManager.OnCursorTargetChanged += value;
		Action<Interactable> value2 = HandlePrimaryClickDown;
		cursorManager.OnPrimaryClickDown += value2;
		Action<Interactable> value3 = HandlePrimaryClickUp;
		cursorManager.OnPrimaryClickUp += value3;
		DynamicCursorManager dynamicCursorManager = cursorManager;
		_subscribed = true;
		if (_isActiveHoverTarget = dynamicCursorManager._currentHover == expectedInteractable)
		{
			UpdateHoveredLineFromCursorManager();
			if (_003CIsDraggingSelection_003Ek__BackingField || !highlightHoveredLine)
			{
				return;
			}
			if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
			{
				_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
				_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
				UpdateHighlightsForSelectionRange();
				return;
			}
		}
		else
		{
			if (_pressActive)
			{
				return;
			}
			_003CIsDraggingSelection_003Ek__BackingField = false;
			_pressStartLine = -1;
			_003CHoveredLineIndex_003Ek__BackingField = -1;
		}
		ClearSelectionAndHighlights();
	}

	private void Unsubscribe()
	{
		if (_subscribed && cursorManager != null)
		{
			Action<Interactable> value = HandleCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value;
			Action<Interactable> value2 = HandlePrimaryClickDown;
			cursorManager.OnPrimaryClickDown -= value2;
			Action<Interactable> value3 = HandlePrimaryClickUp;
			cursorManager.OnPrimaryClickUp -= value3;
			_subscribed = false;
		}
	}

	private void HandleCursorTargetChanged(Interactable hover)
	{
		//IL_0108: Expected I4, but got I8
		if (_isActiveHoverTarget = hover == expectedInteractable)
		{
			UpdateHoveredLineFromCursorManager();
			if (_003CIsDraggingSelection_003Ek__BackingField || !highlightHoveredLine)
			{
				return;
			}
			if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
			{
				_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
				_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
				UpdateHighlightsForSelectionRange();
				return;
			}
		}
		else
		{
			if (_pressActive)
			{
				return;
			}
			_003CIsDraggingSelection_003Ek__BackingField = false;
			_pressStartLine = -1;
			_003CHoveredLineIndex_003Ek__BackingField = -1;
		}
		ClearSelectionAndHighlights();
	}

	private void HandlePrimaryClickDown(Interactable pressedHover)
	{
		if (!(pressedHover == expectedInteractable))
		{
			return;
		}
		if (!cursorManager.IsCurrentDeviceGamepad() || _003CHoveredLineIndex_003Ek__BackingField == -1)
		{
			UpdateHoveredLineFromCursorManager();
		}
		if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			_pressActive = true;
			TryAcquireBrokerDragLockIfNeeded();
			_dragAnchorLine = _003CHoveredLineIndex_003Ek__BackingField;
			_pressStartLine = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_003CIsDraggingSelection_003Ek__BackingField = true;
			UpdateHighlightsForSelectionRange();
			if (debugLogs)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"{arg}: PrimaryDown started on line {arg2}";
				Debug.Log(message, this);
			}
		}
	}

	private void HandlePrimaryClickUp(Interactable releasedHover)
	{
		//IL_061a: Expected I, but got O
		//IL_0176: Expected O, but got I4
		//IL_017a: Unsupported input type for neg.
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_0470: Invalid comparison between I4 and F4
		//IL_0482: Expected F4, but got I4
		//IL_06d7: Expected O, but got I4
		//IL_0456: Expected O, but got I4
		if (!_pressActive)
		{
			return;
		}
		_pressActive = false;
		if (!cursorManager.IsCurrentDeviceGamepad() || _003CHoveredLineIndex_003Ek__BackingField == -1)
		{
			UpdateHoveredLineFromCursorManager();
		}
		if (_003CIsDraggingSelection_003Ek__BackingField && _dragAnchorLine >= 0 && _003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			int num = _dragAnchorLine;
			if (_dragAnchorLine >= _003CHoveredLineIndex_003Ek__BackingField)
			{
				num = _003CHoveredLineIndex_003Ek__BackingField;
			}
			int num2 = _dragAnchorLine;
			if (_dragAnchorLine <= _003CHoveredLineIndex_003Ek__BackingField)
			{
				num2 = _003CHoveredLineIndex_003Ek__BackingField;
			}
			if (num != _003CSelectedLineMin_003Ek__BackingField || num2 != _003CSelectedLineMax_003Ek__BackingField)
			{
				_003CSelectedLineMin_003Ek__BackingField = num;
				_003CSelectedLineMax_003Ek__BackingField = num2;
				UpdateHighlightsForSelectionRange();
			}
		}
		int minLayoutLine = _003CSelectedLineMin_003Ek__BackingField;
		int num3 = _003CSelectedLineMax_003Ek__BackingField;
		nint num4 = (nint)typeof(Math);
		object obj = _003CSelectedLineMax_003Ek__BackingField - _003CSelectedLineMin_003Ek__BackingField;
		object obj2 = 0 - obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v456 @ rcx_v9 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 < (nint)0)
		{
			obj2 = obj;
		}
		bool flag = dragLineThreshold < 0;
		int num5 = 0;
		if (!flag)
		{
			num5 = dragLineThreshold;
		}
		if ((nint)obj2 < num5)
		{
			int num6 = _003CHoveredLineIndex_003Ek__BackingField >> 63;
			int num7 = num6 & 0x1C;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v547 @ rax_v58 (System.Int32)+108+this @ rcx (TeleprinterLineRangeSelector3D)]");
			minLayoutLine = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v547 @ rax_v58 (System.Int32)+108+this @ rcx (TeleprinterLineRangeSelector3D)]");
			num3 = 0;
		}
		string text = BuildSelectedTextFromLayoutLineRange(minLayoutLine, num3);
		bool flag2 = !trimSelectedText;
		string text2 = text;
		if (!flag2)
		{
			bool flag3 = text == null;
			text2 = text;
			if (!flag3)
			{
				string text3 = text.Trim();
				text2 = text3;
			}
		}
		string message;
		if (string.IsNullOrEmpty(text2))
		{
			if (debugLogs)
			{
				string text4 = base.name;
				message = text4 + ": PrimaryUp produced empty selection text; nothing copied/written.";
				goto IL_028a;
			}
		}
		else
		{
			if (copyToClipboardOnRelease)
			{
				GUIUtility.systemCopyBuffer = text2;
			}
			bool flag4 = !writeToNotepadOnRelease;
			string text5 = null;
			int num8 = num3;
			if (!flag4)
			{
				bool flag5 = TryResolveNotepadSection("PrimaryClickUp");
				if (targetSection != null)
				{
					string text6 = noteFormat;
					if (noteFormat == null)
					{
						text6 = "{text}";
					}
					string content = text6.Replace("{text}", text2);
					if (!overrideNotepadRevealForTeleprinterWrites)
					{
						targetSection.Write(content, writeMode, addPosition);
						text5 = (string)addPosition;
						num8 = (int)writeMode;
					}
					else
					{
						bool flag6 = !(0f < notepadDelayOverrideSeconds);
						float num9 = 0f;
						if (!flag6)
						{
							num9 = notepadDelayOverrideSeconds;
						}
						float delaySeconds = default(float);
						NotepadSection.TextRevealMode revealMode = default(NotepadSection.TextRevealMode);
						float typewriterSecondsPerCharacter = default(float);
						targetSection.Write(content, writeMode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
						text5 = (string)addPosition;
						num8 = (int)writeMode;
					}
				}
				else
				{
					string text7 = base.name;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string text8 = $"AutoFind={arg}, Tag='{sectionTag}'.\n";
					string message2 = text7 + ": WriteToNotepadOnRelease is enabled, but NotepadSection could not be resolved.\n" + text8 + "Ensure the tag exists in Project Settings and the NotepadSection GameObject is tagged correctly.";
					Debug.LogWarning(message2, this);
					bool flag7 = autoFindNotepadSectionByTag;
					text5 = "Ensure the tag exists in Project Settings and the NotepadSection GameObject is tagged correctly.";
					num8 = 0;
				}
			}
			if (debugLogs)
			{
				string arg2 = base.name;
				bool flag8 = (nint)obj2 >= num5;
				object arg3 = "range";
				if (!flag8)
				{
					arg3 = "single";
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg4 = default(object);
				message = $"{arg2}: Copied {arg3} text len={arg4}.";
				goto IL_028a;
			}
		}
		goto IL_0299;
		IL_0299:
		_003CIsDraggingSelection_003Ek__BackingField = false;
		_pressStartLine = -1;
		if (_isActiveHoverTarget && highlightHoveredLine && _003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			UpdateHighlightsForSelectionRange();
		}
		else
		{
			ClearSelectionAndHighlights();
		}
		ReleaseBrokerDragLockIfHeld();
		return;
		IL_028a:
		Debug.Log(message, this);
		goto IL_0299;
	}

	private void TryFindBroker()
	{
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
	}

	private unsafe void TryAcquireBrokerDragLockIfNeeded()
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00ed: Expected O, but got Ref
		if (!useBrokerLockWhileDragging || !(cursorManager != null))
		{
			return;
		}
		DynamicCursorManager dynamicCursorManager = cursorManager;
		if (dynamicCursorManager._currentMode != DynamicCursorManager.PresentationMode.FPSLocked)
		{
			return;
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
		if (_broker != null)
		{
			InteractionLockBroker.LockHandle lockHandle = (InteractionLockBroker.LockHandle)(this + 328);
			if (!((InteractionLockBroker.LockHandle*)lockHandle)->IsValid)
			{
				object obj = default(object);
				InteractionLockBroker.LockHandle dragHandle = _broker.Acquire((InteractionLockBroker.LockRequest)(&obj));
				_dragHandle = dragHandle;
			}
		}
		else
		{
			string message = "[TeleprinterLineRangeSelector3D] InteractionLockBroker not found (tag='" + lockBrokerTag + "'). Drag lock not acquired.";
			Debug.LogWarning(message, this);
		}
	}

	private unsafe void ReleaseBrokerDragLockIfHeld()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_0091: Expected O, but got I4
		InteractionLockBroker.LockHandle lockHandle = (InteractionLockBroker.LockHandle)(this + 328);
		if (((InteractionLockBroker.LockHandle*)lockHandle)->IsValid)
		{
			if (_broker == null)
			{
				TryFindBroker();
			}
			if (_broker != null)
			{
				bool flag = _broker.Release(_dragHandle);
			}
			_dragHandle = (InteractionLockBroker.LockHandle)0;
		}
	}

	private unsafe void UpdateHoveredLineFromCursorManager()
	{
		//IL_0246: Expected I4, but got I8
		//IL_01d4: Expected O, but got Ref
		//IL_0186: Expected O, but got I
		_003CHoveredLineIndex_003Ek__BackingField = -1;
		if (!(cursorManager != null) || !(sourceTMP != null))
		{
			return;
		}
		if (hitTestCamera == null)
		{
			Camera main = Camera.main;
			hitTestCamera = main;
		}
		TMP_TextInfo textInfo = sourceTMP.textInfo;
		_ti = textInfo;
		if (_ti == null)
		{
			return;
		}
		TMP_TextInfo ti = _ti;
		if (ti.characterCount <= 0 || ti.lineCount <= 0)
		{
			return;
		}
		UnityEngine.Object obj = cursorManager;
		if (cursorManager != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rdi_v8 (UnityEngine.Object)+A8]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rdi_v8 (UnityEngine.Object)+90]");
				if ((UnityEngine.Object)0 != null)
				{
					goto IL_01bf;
				}
			}
		}
		int width = Screen.width;
		int height = Screen.height;
		goto IL_01bf;
		IL_01bf:
		object obj2 = default(object);
		int num = TMP_TextUtilities.FindIntersectingLine(sourceTMP, (Vector3)(&obj2), hitTestCamera);
		if (num >= 0)
		{
			TMP_TextInfo ti2 = _ti;
			if (num < ti2.lineCount)
			{
				_003CHoveredLineIndex_003Ek__BackingField = num;
			}
		}
	}

	private static Vector2 GetCursorScreenPositionFromManager(DynamicCursorManager mgr)
	{
		if (!(mgr != null))
		{
			goto IL_00b8;
		}
		Vector2 result = default(Vector2);
		if ((object)mgr != null)
		{
			if (mgr._currentMode == DynamicCursorManager.PresentationMode.FPSLocked || !(mgr.virtualCursor != null))
			{
				goto IL_00b8;
			}
			if ((object)mgr.virtualCursor != null)
			{
				return result;
			}
		}
		return (Vector2)new NullReferenceException();
		IL_00b8:
		int width = Screen.width;
		int height = Screen.height;
		return result;
	}

	private void ExpandSelectionToHovered()
	{
		if (_dragAnchorLine >= 0 && _003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			int num = _dragAnchorLine;
			if (_dragAnchorLine >= _003CHoveredLineIndex_003Ek__BackingField)
			{
				num = _003CHoveredLineIndex_003Ek__BackingField;
			}
			int num2 = _dragAnchorLine;
			if (_dragAnchorLine <= _003CHoveredLineIndex_003Ek__BackingField)
			{
				num2 = _003CHoveredLineIndex_003Ek__BackingField;
			}
			if (num != _003CSelectedLineMin_003Ek__BackingField || num2 != _003CSelectedLineMax_003Ek__BackingField)
			{
				_003CSelectedLineMax_003Ek__BackingField = num2;
				_003CSelectedLineMin_003Ek__BackingField = num;
				UpdateHighlightsForSelectionRange();
			}
		}
	}

	private string BuildSelectedTextFromLayoutLineRange(int minLayoutLine, int maxLayoutLine)
	{
		//IL_05a6: Expected O, but got I
		//IL_05b6: Expected O, but got I
		//IL_0579: Expected O, but got I
		//IL_0589: Expected O, but got I
		//IL_01dd: Expected O, but got I4
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_0216: Expected I4, but got I8
		//IL_054c: Expected O, but got I
		//IL_055c: Expected O, but got I
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Expected O, but got Unknown
		//IL_0357: Expected O, but got I
		//IL_0367: Expected O, but got I
		StringBuilder stringBuilder;
		int num6;
		int num8;
		if (sourceTMP != null)
		{
			if ((object)sourceTMP != null)
			{
				TMP_TextInfo textInfo = sourceTMP.textInfo;
				_ti = textInfo;
				if (_ti != null)
				{
					TMP_TextInfo ti = _ti;
					if (ti.lineCount > 0 && ti.characterCount > 0)
					{
						int num;
						if (minLayoutLine >= 0)
						{
							num = ti.lineCount - 1;
							if (minLayoutLine <= num)
							{
								num = minLayoutLine;
							}
						}
						else
						{
							num = 0;
						}
						int num3;
						if (maxLayoutLine >= 0)
						{
							int num2 = ti.lineCount - 1;
							bool flag = maxLayoutLine <= num2;
							num3 = maxLayoutLine;
							if (!flag)
							{
								num3 = num2;
							}
						}
						else
						{
							num3 = 0;
						}
						bool flag2 = num3 < num;
						int num4 = num;
						if (!flag2)
						{
							num4 = num3;
						}
						if (num3 >= num)
						{
							num3 = num;
						}
						if (copyMode == CopyExtractionMode.VisualLayoutLinesJoined_WithSeparator)
						{
							if (sourceTMP != null)
							{
								if ((object)sourceTMP == null)
								{
									goto IL_05c8;
								}
								TMP_TextInfo textInfo2 = sourceTMP.textInfo;
								_ti = textInfo2;
								if (_ti != null)
								{
									TMP_TextInfo ti2 = _ti;
									if (ti2.lineCount > 0)
									{
										stringBuilder = new StringBuilder(1024);
										bool flag3 = num3 > num4;
										int num5 = num3;
										if (flag3)
										{
											goto IL_050d;
										}
										while (true)
										{
											if (num5 > num3 && lineSeparator != null)
											{
												if (stringBuilder == null)
												{
													break;
												}
												StringBuilder stringBuilder2 = stringBuilder.Append(lineSeparator);
											}
											AppendLineToBuilder(_ti, num5, stringBuilder);
											num5++;
											if (num5 <= num4)
											{
												continue;
											}
											goto IL_050d;
										}
										goto IL_05c8;
									}
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v641 @ rax_v23+B8]");
							return (string)0;
						}
						if (num3 <= num4)
						{
							TMP_TextInfo ti3 = _ti;
							object obj2 = num3 * 2;
							object obj3 = num3 + obj2;
							object obj4 = obj3 << 5;
							num6 = 2147483647;
							int num7 = num3;
							num8 = -2147483648;
							while (true)
							{
								TMP_LineInfo[] lineInfo = ti3.lineInfo;
								if (ti3.lineInfo == null)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r8_v15+38+v118 @ rcx_v29 (TMPro.TMP_LineInfo[])]");
								if ((nint)0 >= (nint)0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r8_v15+40+v118 @ rcx_v29 (TMPro.TMP_LineInfo[])]");
									if ((nint)0 >= (nint)0)
									{
										int num9 = num6;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r8_v15+38+v118 @ rcx_v29 (TMPro.TMP_LineInfo[])]");
										if ((nint)num9 >= (nint)0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r8_v15+38+v118 @ rcx_v29 (TMPro.TMP_LineInfo[])]");
											num6 = 0;
										}
										int num10 = num8;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r8_v15+40+v118 @ rcx_v29 (TMPro.TMP_LineInfo[])]");
										if ((nint)num10 <= (nint)0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r8_v15+40+v118 @ rcx_v29 (TMPro.TMP_LineInfo[])]");
											num8 = 0;
										}
									}
								}
								num3++;
								num7++;
								obj4 += 96;
								if (num7 <= num4)
								{
									continue;
								}
								goto IL_02b3;
							}
							goto IL_05c8;
						}
					}
				}
				goto IL_0569;
			}
			goto IL_05c8;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v5+B8]");
		return (string)0;
		IL_02b3:
		if (num6 == 2147483647 || num8 == 2147483648L)
		{
			goto IL_0569;
		}
		if ((object)sourceTMP != null)
		{
			string text = sourceTMP.text;
			bool flag4 = text != null;
			string raw = text;
			if (!flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v716 @ rax_v50+B8]");
				object obj7 = 0;
				raw = (string)obj7;
			}
			return TMP_RichTextSelectionUtility.ExtractRichSubstringByPlainRange(raw, num6, num8, trimResult: false);
		}
		goto IL_05c8;
		IL_050d:
		if (stringBuilder != null)
		{
			return stringBuilder.ToString();
		}
		goto IL_05c8;
		IL_05c8:
		return (string)(object)new NullReferenceException();
		IL_0569:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rax_v13+B8]");
		return (string)0;
	}

	private string BuildSelectedText_LegacyLayoutLinesJoined(int minLine, int maxLine)
	{
		//IL_0205: Expected O, but got I
		//IL_0215: Expected O, but got I
		//IL_01d8: Expected O, but got I
		//IL_01e8: Expected O, but got I
		StringBuilder stringBuilder;
		if (sourceTMP != null)
		{
			if ((object)sourceTMP != null)
			{
				TMP_TextInfo textInfo = sourceTMP.textInfo;
				_ti = textInfo;
				if (_ti != null)
				{
					TMP_TextInfo ti = _ti;
					if (ti.lineCount > 0)
					{
						stringBuilder = new StringBuilder(1024);
						bool flag = minLine > maxLine;
						int num = minLine;
						if (flag)
						{
							goto IL_0199;
						}
						while (true)
						{
							if (num > minLine && lineSeparator != null)
							{
								if (stringBuilder == null)
								{
									break;
								}
								StringBuilder stringBuilder2 = stringBuilder.Append(lineSeparator);
							}
							AppendLineToBuilder(_ti, num, stringBuilder);
							num++;
							if (num <= maxLine)
							{
								continue;
							}
							goto IL_0199;
						}
						goto IL_0222;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rax_v14+B8]");
				return (string)0;
			}
			goto IL_0222;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v5+B8]");
		return (string)0;
		IL_0222:
		return (string)(object)new NullReferenceException();
		IL_0199:
		if (stringBuilder != null)
		{
			return stringBuilder.ToString();
		}
		goto IL_0222;
	}

	private static void AppendLineToBuilder(TMP_TextInfo ti, int lineIndex, StringBuilder sb)
	{
		//IL_00af: Expected O, but got I4
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_01af: Expected O, but got I
		//IL_01bf: Expected O, but got I
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		if (ti == null || sb == null || lineIndex < 0 || lineIndex >= ti.lineCount)
		{
			return;
		}
		TMP_LineInfo[] lineInfo = ti.lineInfo;
		object obj = lineIndex * 2;
		object obj2 = lineIndex + obj;
		StringBuilder stringBuilder = (StringBuilder)(obj2 << 5);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+38+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		if ((nint)0 < (nint)0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+40+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		if ((nint)0 < (nint)0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+38+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		if ((nint)0 >= (nint)ti.characterCount)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+40+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		if ((nint)0 >= (nint)ti.characterCount)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+38+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+40+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		if (num > 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+38+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		object obj3 = (nint)0 * (nint)376;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+38+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		object obj4 = 0;
		object obj5;
		do
		{
			TMP_CharacterInfo[] characterInfo = ti.characterInfo;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rsi_v8+24+v211 @ rax_v12 (TMPro.TMP_CharacterInfo[])]");
			if ((nint)0 != 10)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rsi_v8+24+v211 @ rax_v12 (TMPro.TMP_CharacterInfo[])]");
				if ((nint)0 != 13)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rsi_v8+24+v211 @ rax_v12 (TMPro.TMP_CharacterInfo[])]");
					StringBuilder stringBuilder2 = sb.Append('\0');
					lineInfo = null;
				}
			}
			obj4++;
			obj3 += 376;
			obj5 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v9 (System.Text.StringBuilder)+40+v284 @ r8_v8 (TMPro.TMP_LineInfo[])]");
		}
		while ((nint)obj5 <= 0);
	}

	private void UpdateHighlightsForHoverOnly()
	{
		if (highlightHoveredLine && _003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 28 Invalid \"Jump target not found in method: 0x180489B20\"");
		}
		ClearSelectionAndHighlights();
	}

	private void UpdateHighlightsForSelectionRange()
	{
		//IL_040d: Expected O, but got I4
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		if (sourceTMP != null)
		{
			TMP_TextInfo textInfo = sourceTMP.textInfo;
			_ti = textInfo;
			if (_ti != null)
			{
				TMP_TextInfo ti = _ti;
				if (ti.lineCount > 0)
				{
					int num;
					if (_003CSelectedLineMin_003Ek__BackingField >= 0)
					{
						num = ti.lineCount - 1;
						if (_003CSelectedLineMin_003Ek__BackingField <= num)
						{
							num = _003CSelectedLineMin_003Ek__BackingField;
						}
					}
					else
					{
						num = 0;
					}
					int num2 = _003CSelectedLineMax_003Ek__BackingField;
					if (_003CSelectedLineMax_003Ek__BackingField >= 0)
					{
						int num3 = ti.lineCount - 1;
						if (num2 > num3)
						{
							num2 = num3;
						}
					}
					else
					{
						num2 = 0;
					}
					object obj = num2 - num;
					object obj2 = obj + 1;
					List<Transform> highlights = _highlights;
					while (highlights._size < (nint)obj2)
					{
						Transform item = CreateHighlightInstance();
						_highlights.Add(item);
						highlights = _highlights;
					}
					List<Transform> highlights2 = _highlights;
					object obj3 = obj2;
					object obj4 = obj2;
					UnityEngine.Object obj5 = default(UnityEngine.Object);
					bool flag;
					do
					{
						if ((nint)obj4 < highlights2._size)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj5 != null)
							{
								GameObject gameObject = ((Component)obj5).gameObject;
								if (gameObject.activeSelf)
								{
									GameObject gameObject2 = ((Component)obj5).gameObject;
									gameObject2.SetActive(value: false);
								}
							}
							highlights2 = _highlights;
							obj3++;
							flag = _highlights != null;
							obj4 = obj3;
							continue;
						}
						bool flag2 = (nint)obj2 <= 0;
						int num4 = 0;
						if (flag2)
						{
							return;
						}
						do
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj5 != null)
							{
								GameObject gameObject3 = ((Component)obj5).gameObject;
								bool activeSelf = gameObject3.activeSelf;
								if (!activeSelf)
								{
									GameObject gameObject4 = ((Component)obj5).gameObject;
									gameObject4.SetActive(value: true);
								}
							}
							int lineIndex = num + num4;
							UpdateHighlightForLine((Transform)obj5, lineIndex);
							ApplyHighlightTint((Transform)obj5);
							num4++;
						}
						while (num4 < (nint)obj2);
						return;
					}
					while (flag);
					throw new NullReferenceException();
				}
			}
		}
		ClearSelectionAndHighlights();
	}

	private void EnsureHighlightsCount(int needed)
	{
		List<Transform> highlights = _highlights;
		while (highlights._size < needed)
		{
			Transform item = CreateHighlightInstance();
			_highlights.Add(item);
			highlights = _highlights;
		}
	}

	private void ClearSelectionAndHighlights()
	{
		//IL_00d6: Expected O, but got I4
		//IL_00df: Expected O, but got I4
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		List<Transform> highlights = _highlights;
		_003CSelectedLineMin_003Ek__BackingField = -1;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < highlights._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				GameObject gameObject = ((Component)obj3).gameObject;
				if (gameObject.activeSelf)
				{
					GameObject gameObject2 = ((Component)obj3).gameObject;
					gameObject2.SetActive(value: false);
				}
			}
			highlights = _highlights;
			obj++;
			obj2 = obj;
		}
	}

	private unsafe Transform CreateHighlightInstance()
	{
		//IL_00ec: Expected O, but got Ref
		//IL_0382: Expected O, but got Ref
		//IL_0394: Expected O, but got Ref
		//IL_03b8: Expected O, but got Ref
		//IL_03ca: Expected O, but got Ref
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_021b: Expected O, but got I4
		//IL_0224: Expected O, but got I4
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		Transform transform6;
		Vector3 oneVector = default(Vector3);
		if (highlightPrefab == null)
		{
			GameObject gameObject = new GameObject("LineHighlight (Auto)");
			Transform transform = gameObject.transform;
			if (highlightStyle != HighlightStyle.Solid)
			{
				Transform transform2 = CreateAutoQuad("Top", transform);
				Transform transform3 = CreateAutoQuad("Bottom", transform);
				Transform transform4 = CreateAutoQuad("Left", transform);
				Transform transform5 = CreateAutoQuad("Right", transform);
				transform6 = transform;
			}
			else
			{
				Transform transform7 = CreateAutoQuad("Fill", transform);
				transform7.localPosition = (Vector3)(&oneVector);
				transform7.localRotation = (Quaternion)(&oneVector);
				Quaternion quaternion = default(Quaternion);
				transform7.localScale = (Vector3)(&quaternion);
				oneVector = Vector3.oneVector;
				transform6 = transform;
			}
		}
		else
		{
			Transform transform8 = UnityEngine.Object.Instantiate(highlightPrefab);
			string text = highlightPrefab.name;
			string text2 = text + " (LineHighlight)";
			transform8.name = text2;
			transform6 = transform8;
		}
		Transform parent = ((!parentHighlightsToTMP || !(sourceTMP != null)) ? base.transform : sourceTMP.transform);
		transform6.SetParent(parent, worldPositionStays: false);
		transform6.localRotation = (Quaternion)(&oneVector);
		Quaternion quaternion2 = default(Quaternion);
		transform6.localScale = (Vector3)(&quaternion2);
		if (transform6 != null)
		{
			Collider[] componentsInChildren = transform6.GetComponentsInChildren<Collider>(includeInactive: true);
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			for (object obj3 = 0; (nint)obj3 < componentsInChildren.Length; obj2++, obj += 8, obj3 = obj2)
			{
				if ((nint)obj2 < componentsInChildren.Length)
				{
					if (!((UnityEngine.Object)obj != null))
					{
						continue;
					}
					if ((nint)obj2 < componentsInChildren.Length)
					{
						UnityEngine.Object.Destroy((UnityEngine.Object)obj);
						continue;
					}
				}
				return (Transform)(object)new IndexOutOfRangeException();
			}
		}
		if (transform6 != null)
		{
			GameObject gameObject2 = transform6.gameObject;
			if (gameObject2.activeSelf)
			{
				GameObject gameObject3 = transform6.gameObject;
				gameObject3.SetActive(value: false);
			}
		}
		CacheOutlinePartsIfNeeded(transform6);
		ApplyHighlightTint(transform6);
		return transform6;
	}

	private Transform CreateAutoQuad(string quadName, Transform parent)
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		if ((object)gameObject != null)
		{
			gameObject.name = quadName;
			Transform transform = gameObject.transform;
			if ((object)transform != null)
			{
				transform.SetParent(parent, worldPositionStays: false);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj != null)
				{
					UnityEngine.Object.Destroy(obj);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				if (obj != null)
				{
					Material material2;
					if (highlightMaterial == null)
					{
						Shader shader = Shader.Find("Unlit/Color");
						bool flag = shader == null;
						Shader shader2 = shader;
						if (flag)
						{
							Shader shader3 = Shader.Find("Sprites/Default");
							shader2 = shader3;
						}
						Material material = new Material(shader2);
						if ((object)obj == null)
						{
							goto IL_0197;
						}
						material2 = material;
					}
					else
					{
						if ((object)obj == null)
						{
							goto IL_0197;
						}
						material2 = highlightMaterial;
					}
					((Renderer)obj).SetMaterial(material2);
				}
				return gameObject.transform;
			}
		}
		goto IL_0197;
		IL_0197:
		return (Transform)(object)new NullReferenceException();
	}

	private static void RemoveAllCollidersRecursive(Transform root)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_004f: Expected O, but got I4
		//IL_0058: Expected O, but got I4
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		if (!(root != null))
		{
			return;
		}
		Collider[] componentsInChildren = root.GetComponentsInChildren<Collider>(includeInactive: true);
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < componentsInChildren.Length)
		{
			if ((UnityEngine.Object)obj != null)
			{
				UnityEngine.Object.Destroy((UnityEngine.Object)obj);
			}
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	private void CacheOutlinePartsIfNeeded(Transform root)
	{
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		_ = 0;
		_ = 0;
		_ = 0;
		if (!(root != null) || _outlinePartsByRoot.ContainsKey(root))
		{
			return;
		}
		object obj = default(object);
		if (highlightStyle == HighlightStyle.Outline)
		{
			Transform transform = root.Find("Top");
			Transform transform2 = root.Find("Bottom");
			Transform transform3 = root.Find("Left");
			Transform transform4 = root.Find("Right");
			if (transform != null && transform2 != null && transform3 != null)
			{
				bool flag = transform4 != null;
			}
			else
			{
				bool flag = false;
			}
			_ = 0;
			_ = 0;
			_ = 0;
			OutlineParts value = (OutlineParts)(obj - 48);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-50]");
			_ = 0;
			_outlinePartsByRoot.set_Item(root, value);
		}
		else
		{
			_ = 0;
			_ = 0;
			_ = 0;
			OutlineParts value2 = (OutlineParts)(obj - 48);
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
			_ = 0;
			_outlinePartsByRoot.set_Item(root, value2);
		}
	}

	private unsafe void UpdateHighlightForLine(Transform h, int lineIndex)
	{
		//IL_00c5: Expected O, but got I4
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0186: Expected O, but got Ref
		//IL_033a: Expected O, but got Ref
		//IL_02ff: Expected O, but got Ref
		//IL_036f: Expected O, but got Ref
		//IL_0386: Invalid comparison between F4 and O
		//IL_03b2: Invalid comparison between F4 and O
		//IL_0236: Expected O, but got Ref
		//IL_0249: Expected O, but got Ref
		//IL_025b: Expected O, but got Ref
		//IL_026a: Expected O, but got Ref
		//IL_027a: Expected O, but got Ref
		//IL_0289: Expected O, but got Ref
		//IL_029b: Expected O, but got Ref
		//IL_02ae: Expected O, but got Ref
		//IL_02c0: Expected O, but got Ref
		//IL_02cf: Expected O, but got Ref
		//IL_02df: Expected O, but got Ref
		//IL_02ec: Expected O, but got Ref
		if (_ti == null)
		{
			return;
		}
		TMP_TextInfo ti = _ti;
		if (ti.lineCount <= 0 || lineIndex < 0 || lineIndex >= ti.lineCount || !(h != null))
		{
			return;
		}
		TMP_TextInfo ti2 = _ti;
		TMP_LineInfo[] lineInfo = ti2.lineInfo;
		object obj = lineIndex * 2;
		object obj2 = lineIndex + obj;
		object obj3 = obj2 << 5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TeleprinterLineRangeSelector3D)+D4]");
		object obj5 = default(object);
		object obj4 = obj5 + 0;
		object obj6 = obj5 + (object)localPadding;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ rcx_v8+70+v328 @ rdx_v6 (TMPro.TMP_LineInfo[])]");
		object obj7 = 0 - localPadding;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TeleprinterLineRangeSelector3D)+D4]");
		object obj8 = obj5 - 0;
		object obj9 = obj6 - obj7;
		object obj10 = obj4 - obj8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj11 = obj9 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj12 = obj10 & 0;
		Quaternion quaternion = default(Quaternion);
		h.localPosition = (Vector3)(&quaternion);
		h.localRotation = (Quaternion)(&quaternion);
		bool flag = highlightStyle == HighlightStyle.Solid;
		quaternion = Quaternion.identityQuaternion;
		Vector3 localScale = default(Vector3);
		Transform transform3 = default(Transform);
		if (!flag)
		{
			h.localScale = (Vector3)(&quaternion);
			CacheOutlinePartsIfNeeded(h);
			bool flag2 = _outlinePartsByRoot.TryGetValue(h, out var value);
			bool flag3 = !flag2;
			quaternion = (Quaternion)Vector3.oneVector;
			if (!flag3)
			{
				bool flag4 = (object)value == null;
				quaternion = (Quaternion)Vector3.oneVector;
				if (!flag4)
				{
					bool flag5 = !(0.0001f < outlineThicknessLocal);
					float num = 0.0001f;
					if (!flag5)
					{
						num = outlineThicknessLocal;
					}
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
					{
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-138), the output could be wrong!");
							/*Error: End of method reached without returning.*/;
						}
						Transform transform = default(Transform);
						transform.localPosition = (Vector3)(&quaternion);
						transform.localRotation = (Quaternion)(&quaternion);
						transform.localScale = (Vector3)(&quaternion);
						((Transform)null).localPosition = (Vector3)(&quaternion);
						((Transform)null).localRotation = (Quaternion)(&quaternion);
						((Transform)null).localScale = (Vector3)(&quaternion);
						Transform transform2 = default(Transform);
						transform2.localPosition = (Vector3)(&quaternion);
						transform2.localRotation = (Quaternion)(&quaternion);
						transform2.localScale = (Vector3)(&quaternion);
						((Transform)null).localPosition = (Vector3)(&quaternion);
						((Transform)null).localRotation = (Quaternion)(&quaternion);
						localScale = (Vector3)(&quaternion);
						transform3 = null;
					}
					goto IL_0398;
				}
			}
		}
		localScale = (Vector3)(&quaternion);
		transform3 = h;
		goto IL_0398;
		IL_0398:
		transform3.localScale = localScale;
	}

	private unsafe void ApplyHighlightTint(Transform h)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_004f: Expected O, but got I4
		//IL_0058: Expected O, but got I4
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0176: Expected O, but got Ref
		if (!(h != null))
		{
			return;
		}
		Renderer[] componentsInChildren = h.GetComponentsInChildren<Renderer>(includeInactive: true);
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		Color color = default(Color);
		for (object obj3 = 0; (nint)obj3 < componentsInChildren.Length; obj2++, obj += 8, obj3 = obj2)
		{
			if (!((UnityEngine.Object)obj != null))
			{
				continue;
			}
			Material sharedMaterial = ((Renderer)obj).GetSharedMaterial();
			if (!(sharedMaterial != null))
			{
				continue;
			}
			Material sharedMaterial2 = ((Renderer)obj).GetSharedMaterial();
			string text;
			Material material;
			if (!sharedMaterial2.HasProperty("_BaseColor"))
			{
				if (!sharedMaterial2.HasProperty("_Color"))
				{
					continue;
				}
				text = "_Color";
				material = sharedMaterial2;
			}
			else
			{
				text = "_BaseColor";
				material = sharedMaterial2;
			}
			material.SetColor(text, (Color)(&color));
		}
	}

	private static void SetHighlightActive(Transform h, bool active)
	{
		if (h != null)
		{
			GameObject gameObject = h.gameObject;
			bool activeSelf = gameObject.activeSelf;
			if (activeSelf != active)
			{
				GameObject gameObject2 = h.gameObject;
				gameObject2.SetActive(active);
			}
		}
	}

	public TeleprinterLineRangeSelector3D()
	{
		//IL_0022: Expected O, but got I
		//IL_0064: Expected O, but got I4
		//IL_00aa: Expected I4, but got I8
		autoFindCursorManagerByTag = true;
		cursorManagerTag = "CursorManager";
		copyToClipboardOnRelease = true;
		lineSeparator = "\n";
		trimSelectedText = true;
		autoFindNotepadSectionByTag = true;
		sectionTag = "MainNotes";
		noteFormat = "{text}";
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182207000]");
		highlightColor = (Color)0;
		overrideNotepadRevealForTeleprinterWrites = true;
		highlightHoveredLine = true;
		dragLineThreshold = 1;
		highlightStyle = HighlightStyle.Outline;
		outlineThicknessLocal = 0.02f;
		localPadding = (Vector2)1022739087;
		_ = 1022739087;
		normalOffset = 0.0025f;
		parentHighlightsToTMP = true;
		lockBrokerTag = "LockBroker";
		brokerDebugLabel = "TeleprinterLineRangeSelector3D:Drag";
		_003CHoveredLineIndex_003Ek__BackingField = -1;
		_003CSelectedLineMin_003Ek__BackingField = -1;
		_pressStartLine = -1;
		_highlights = new List<Transform>(16);
		_outlinePartsByRoot = new Dictionary<Transform, OutlineParts>(16);
		base._002Ector();
	}
}
