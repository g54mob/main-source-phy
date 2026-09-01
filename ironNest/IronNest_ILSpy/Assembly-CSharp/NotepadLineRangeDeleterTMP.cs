using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NotepadLineRangeDeleterTMP : MonoBehaviour
{
	private DynamicCursorManager cursorManager;

	private bool autoFindCursorManagerByTag;

	private string cursorManagerTag;

	private Interactable expectedInteractable;

	private InputActionReference secondaryClickAction;

	private NotepadSection targetSection;

	private TMP_Text sourceTMP;

	private Camera hitTestCamera;

	private bool highlightHoveredLine;

	private int dragLineThreshold;

	private Transform highlightPrefab;

	private Material highlightMaterial;

	private Color highlightColor;

	private Vector2 localPadding;

	private float normalOffset;

	private bool parentHighlightsToTMP;

	private bool debugLogs;

	private int _003CHoveredLineIndex_003Ek__BackingField;

	private bool _003CIsDraggingSelection_003Ek__BackingField;

	private int _003CSelectedLineMin_003Ek__BackingField;

	private int _003CSelectedLineMax_003Ek__BackingField;

	private TMP_TextInfo _ti;

	private bool _isActiveHoverTarget;

	private bool _pressActive;

	private int _pressStartLine;

	private int _dragAnchorLine;

	private readonly List<Transform> _highlights;

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

	public int LineCount
	{
		get
		{
			//IL_0063: Expected I4, but got O
			if ((object)sourceTMP != null)
			{
				TMP_TextInfo textInfo = sourceTMP.textInfo;
				if (textInfo != null)
				{
					return textInfo.lineCount;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
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
		if (targetSection == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			NotepadSection notepadSection = default(NotepadSection);
			bool flag2 = (object)notepadSection != null;
			NotepadSection notepadSection2 = notepadSection;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				NotepadSection notepadSection3 = default(NotepadSection);
				notepadSection2 = notepadSection3;
			}
			targetSection = notepadSection2;
		}
		if (hitTestCamera == null)
		{
			Camera main = Camera.main;
			hitTestCamera = main;
		}
		bool flag3 = TryResolveCursorManager("Awake");
		if (sourceTMP != null)
		{
			sourceTMP.ForceMeshUpdate();
			TMP_TextInfo textInfo = sourceTMP.textInfo;
			_ti = textInfo;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 292 Invalid \"Jump target not found in method: 0x180446700\"");
		throw new NullReferenceException();
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
		SubscribeCursorHoverGate();
		SubscribeSecondaryClickAction();
	}

	private void OnDisable()
	{
		//IL_0086: Expected I4, but got I8
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded -= value;
		if (cursorManager != null)
		{
			Action<Interactable> value2 = HandleCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value2;
		}
		UnsubscribeSecondaryClickAction();
		ClearSelectionAndHighlights();
		_isActiveHoverTarget = false;
		_003CHoveredLineIndex_003Ek__BackingField = -1;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (cursorManager == null)
		{
			Scene scene2 = default(Scene);
			string text = scene2.name;
			string context = "sceneLoaded:" + text;
			bool flag = TryResolveCursorManager(context);
			SubscribeCursorHoverGate();
		}
	}

	private void Update()
	{
		//IL_0092: Expected I4, but got I8
		if (!EnsureReady())
		{
			return;
		}
		if (!_isActiveHoverTarget)
		{
			bool flag = cursorManager.IsCurrentDeviceGamepad();
			if (!flag)
			{
				if (_pressActive == flag)
				{
					_003CHoveredLineIndex_003Ek__BackingField = -1;
					goto IL_0126;
				}
				return;
			}
		}
		UpdateHoveredLineFromCursorManager();
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
				goto IL_0126;
			}
			return;
		}
		ExpandSelectionToHovered();
		return;
		IL_0126:
		ClearSelectionAndHighlights();
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

	private bool EnsureReady()
	{
		//IL_0182: Expected I4, but got O
		if (sourceTMP != null && expectedInteractable != null && targetSection != null)
		{
			NotepadSection notepadSection = targetSection;
			if ((object)targetSection == null)
			{
				goto IL_0174;
			}
			if (notepadSection.targetText != null)
			{
				if (cursorManager == null)
				{
					bool flag = TryResolveCursorManager("Update");
					SubscribeCursorHoverGate();
				}
				NotepadSection notepadSection2 = targetSection;
				if ((object)targetSection == null)
				{
					goto IL_0174;
				}
				if (notepadSection2.targetText == sourceTMP)
				{
					return cursorManager != null;
				}
			}
		}
		return false;
		IL_0174:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void SubscribeCursorHoverGate()
	{
		//IL_0192: Expected I4, but got I8
		if (!(cursorManager != null))
		{
			return;
		}
		Action<Interactable> value = HandleCursorTargetChanged;
		cursorManager.OnCursorTargetChanged -= value;
		Action<Interactable> value2 = HandleCursorTargetChanged;
		cursorManager.OnCursorTargetChanged += value2;
		DynamicCursorManager dynamicCursorManager = cursorManager;
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

	private void UnsubscribeCursorHoverGate()
	{
		if (cursorManager != null)
		{
			Action<Interactable> value = HandleCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value;
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

	private void SubscribeSecondaryClickAction()
	{
		if (secondaryClickAction != null)
		{
			InputAction action = secondaryClickAction.action;
			if (action != null)
			{
				InputAction action2 = secondaryClickAction.action;
				action2.Enable();
				InputAction action3 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value = HandleSecondaryActionStarted;
				action3.started -= value;
				InputAction action4 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value2 = HandleSecondaryActionPerformed;
				action4.performed -= value2;
				InputAction action5 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value3 = HandleSecondaryActionCanceled;
				action5.canceled -= value3;
				InputAction action6 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value4 = HandleSecondaryActionStarted;
				action6.started += value4;
				InputAction action7 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value5 = HandleSecondaryActionPerformed;
				action7.performed += value5;
				InputAction action8 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value6 = HandleSecondaryActionCanceled;
				action8.canceled += value6;
				return;
			}
		}
		if (debugLogs)
		{
			string text = base.name;
			string message = text + ": Secondary Click Action is not assigned; deleter will not respond to secondary clicks.";
			Debug.LogWarning(message, this);
		}
	}

	private void UnsubscribeSecondaryClickAction()
	{
		if (secondaryClickAction != null)
		{
			InputAction action = secondaryClickAction.action;
			if (action != null)
			{
				InputAction action2 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value = HandleSecondaryActionStarted;
				action2.started -= value;
				InputAction action3 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value2 = HandleSecondaryActionPerformed;
				action3.performed -= value2;
				InputAction action4 = secondaryClickAction.action;
				Action<InputAction.CallbackContext> value3 = HandleSecondaryActionCanceled;
				action4.canceled -= value3;
			}
		}
	}

	private void HandleSecondaryActionStarted(InputAction.CallbackContext ctx)
	{
		StartSecondaryPressIfPossible();
	}

	private void HandleSecondaryActionPerformed(InputAction.CallbackContext ctx)
	{
		StartSecondaryPressIfPossible();
	}

	private void HandleSecondaryActionCanceled(InputAction.CallbackContext ctx)
	{
		EndSecondaryPressAndDelete();
	}

	private void StartSecondaryPressIfPossible()
	{
		if (!EnsureReady() || (!_isActiveHoverTarget && !cursorManager.IsCurrentDeviceGamepad()) || _pressActive)
		{
			return;
		}
		UpdateHoveredLineFromCursorManager();
		if (_003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			_dragAnchorLine = _003CHoveredLineIndex_003Ek__BackingField;
			_pressStartLine = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_pressActive = true;
			_003CIsDraggingSelection_003Ek__BackingField = true;
			UpdateHighlightsForSelectionRange();
			if (debugLogs)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"{arg}: Secondary press started on layout line {arg2}";
				Debug.Log(message, this);
			}
		}
	}

	private void EndSecondaryPressAndDelete()
	{
		//IL_0659: Expected I, but got O
		//IL_0145: Expected O, but got I4
		//IL_0149: Unsupported input type for neg.
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_0246: Expected O, but got I4
		//IL_0261: Expected I, but got O
		//IL_0271: Expected O, but got I
		//IL_0312: Expected I, but got O
		//IL_0322: Expected O, but got I
		//IL_0397: Expected I, but got O
		//IL_03a7: Expected O, but got I
		//IL_041c: Expected I, but got O
		//IL_042c: Expected O, but got I
		//IL_04a1: Expected I, but got O
		//IL_04b1: Expected O, but got I
		if (!_pressActive)
		{
			return;
		}
		_pressActive = false;
		if (EnsureReady())
		{
			UpdateHoveredLineFromCursorManager();
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
			int minLayoutLineIndex = _003CSelectedLineMin_003Ek__BackingField;
			int maxLayoutLineIndex = _003CSelectedLineMax_003Ek__BackingField;
			nint num3 = (nint)typeof(Math);
			object obj = _003CSelectedLineMax_003Ek__BackingField - _003CSelectedLineMin_003Ek__BackingField;
			object obj2 = 0 - obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ rcx_v7 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 < (nint)0)
			{
				obj2 = obj;
			}
			bool flag = dragLineThreshold < 0;
			int num4 = 0;
			if (!flag)
			{
				num4 = dragLineThreshold;
			}
			if ((nint)obj2 < num4)
			{
				int num5 = _003CHoveredLineIndex_003Ek__BackingField >> 63;
				int num6 = num5 & 0x1C;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rax_v65 (System.Int32)+98+v554 @ rcx_v12 (NotepadLineRangeDeleterTMP)]");
				minLayoutLineIndex = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rax_v65 (System.Int32)+98+v554 @ rcx_v12 (NotepadLineRangeDeleterTMP)]");
				maxLayoutLineIndex = 0;
			}
			bool flag2 = targetSection.RemoveLayoutLineRange(minLayoutLineIndex, maxLayoutLineIndex, sourceTMP);
			if (debugLogs)
			{
				object[] array = new object[5];
				string text = base.name;
				bool flag3 = text == null;
				object obj3 = 0;
				string text2 = (string)(object)this;
				if (!flag3)
				{
					nint num7 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v566 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
					obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj4 = default(object);
					bool flag4 = obj4 == null;
					text2 = text;
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text3 = default(string);
						throw text3;
					}
				}
				if (array.Length <= 0)
				{
					throw new IndexOutOfRangeException();
				}
				array[0] = text;
				bool flag5 = (nint)obj2 >= num4;
				string text4 = "range";
				if (!flag5)
				{
					text4 = "single";
				}
				if (text4 != null)
				{
					nint num8 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v684 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
					obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj5 = default(object);
					bool flag6 = obj5 == null;
					text2 = text4;
					if (flag6)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj6 = default(object);
						throw obj6;
					}
				}
				array[1] = text4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj7 = default(object);
				if (obj7 != null)
				{
					nint num9 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v828 @ rdx_v44 (Il2CppClass<System.Object[]>)+40]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj9 = default(object);
					bool flag7 = obj9 == null;
					object obj10 = obj7;
					if (flag7)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj11 = default(object);
						throw obj11;
					}
				}
				array[2] = obj7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj12 = default(object);
				if (obj12 != null)
				{
					nint num10 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v891 @ rdx_v42 (Il2CppClass<System.Object[]>)+40]");
					object obj13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj14 = default(object);
					bool flag8 = obj14 == null;
					object obj15 = obj12;
					if (flag8)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj16 = default(object);
						throw obj16;
					}
				}
				array[3] = obj12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj17 = default(object);
				if (obj17 != null)
				{
					nint num11 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v936 @ rdx_v40 (Il2CppClass<System.Object[]>)+40]");
					object obj18 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj19 = default(object);
					bool flag9 = obj19 == null;
					object obj20 = obj17;
					if (flag9)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj21 = default(object);
						throw obj21;
					}
				}
				array[4] = obj17;
				string message = string.Format("{0}: Deleted {1} layout lines [{2}..{3}], changed={4}.", array);
				Debug.Log(message, this);
			}
			_003CIsDraggingSelection_003Ek__BackingField = false;
			_pressStartLine = -1;
			UpdateHoveredLineFromCursorManager();
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
		}
		else
		{
			_003CIsDraggingSelection_003Ek__BackingField = false;
			_pressStartLine = -1;
		}
	}

	private void ResetDragStateAndHighlights()
	{
		_003CIsDraggingSelection_003Ek__BackingField = false;
		_pressStartLine = -1;
	}

	private unsafe void UpdateHoveredLineFromCursorManager()
	{
		//IL_0072: Expected I4, but got I8
		//IL_0237: Expected O, but got Ref
		//IL_01ef: Expected O, but got I
		//IL_05d3: Expected I, but got O
		//IL_0317: Expected F8, but got I4
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Expected O, but got Unknown
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Expected O, but got Unknown
		//IL_035e: Expected F4, but got I4
		//IL_0367: Expected F8, but got I4
		//IL_0370: Expected F4, but got I4
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Expected O, but got Unknown
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Expected O, but got Unknown
		//IL_07cc: Invalid comparison between F4 and O
		//IL_03ba: Expected O, but got I4
		//IL_03e4: Invalid comparison between F8 and I4
		//IL_03f3: Invalid comparison between F8 and I4
		//IL_041c: Expected O, but got I4
		//IL_0446: Expected O, but got Ref
		//IL_046c: Expected O, but got I4
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_04cf: Invalid comparison between F4 and O
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Expected O, but got Unknown
		//IL_0507: Invalid comparison between O and F4
		//IL_053a: Invalid comparison between O and F4
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Expected O, but got Unknown
		if (!(cursorManager != null) || !(sourceTMP != null) || cursorManager.IsCurrentDeviceGamepad())
		{
			return;
		}
		_003CHoveredLineIndex_003Ek__BackingField = -1;
		if (hitTestCamera == null)
		{
			Camera main = Camera.main;
			hitTestCamera = main;
		}
		if (!(hitTestCamera != null))
		{
			return;
		}
		sourceTMP.ForceMeshUpdate();
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rdi_v10 (UnityEngine.Object)+A8]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rdi_v10 (UnityEngine.Object)+90]");
				if ((UnityEngine.Object)0 != null)
				{
					goto IL_0228;
				}
			}
		}
		int width = Screen.width;
		int height = Screen.height;
		goto IL_0228;
		IL_0228:
		object obj2 = default(object);
		Ray ray = hitTestCamera.ScreenPointToRay((Vector3)(&obj2));
		Transform transform = sourceTMP.transform;
		Vector3 forward = transform.forward;
		Vector3 position = transform.position;
		nint num = (nint)typeof(Math);
		object obj4 = default(object);
		object obj3 = obj4 * obj4;
		float num2 = forward.x * forward.x;
		float num3 = forward.z * forward.z;
		float num4 = (float)obj3 + num2;
		float num5 = num4 + num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v959 @ rcx_v28 (Il2CppClass<System.Math>)+E4]");
		double num6;
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm3\"");
			num6 = 0.0;
		}
		else
		{
			num6 = Math.Sqrt(num5);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
		float num7;
		double num8;
		float num9;
		if (!(num6 > 9.999999747378752E-06))
		{
			num7 = 0f;
			num8 = 0.0;
			num9 = 0f;
		}
		else
		{
			num9 = forward.x / (float)num6;
			num8 = (double)obj4 / num6;
			num7 = forward.z / (float)num6;
		}
		object obj5 = default(object);
		double num10 = num8 * (double)obj5;
		double num11 = num8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ rax_v27 (UnityEngine.Ray)+10]");
		double num12 = num11 * 0.0;
		object obj6 = default(object);
		float num13 = num9 * (float)obj6;
		double num14 = num8 * (double)obj6;
		double num15 = num12 + (double)num13;
		float num16 = num7 * (float)obj6;
		object obj7 = default(object);
		float num17 = num7 * (float)obj7;
		double num18 = num15 + (double)num17;
		float num19 = num9 * position.x;
		float num20 = num9 * (float)ray.m_Origin;
		double num21 = num10 + (double)num19;
		double num22 = num14 + (double)num20;
		float num23 = num7 * position.z;
		double num24 = num22 + (double)num16;
		double num25 = num21 + (double)num23;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj8 = num24 ^ 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj9 = num25 ^ 0;
		object obj10 = obj8 - obj9;
		double num26 = 0.0 - num18;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj11 = num18 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj12 = num26 & 0;
		if ((nint)obj11 <= 0)
		{
			obj11 = 0;
		}
		float num27 = (float)obj11 * 1E-06f;
		float num28 = Mathf.Epsilon * 8f;
		if (!(num27 > num28))
		{
			num27 = num28;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num27) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12))
		{
			return;
		}
		double num29 = (double)obj10 / num18;
		bool flag = num29 < 0.0;
		bool flag2 = num29 == 0.0;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		object obj13 = flag4 & flag3;
		if (obj13 == null)
		{
			return;
		}
		float num30 = default(float);
		Vector3 vector = transform.InverseTransformPoint((Vector3)(&num30));
		TMP_TextInfo ti2 = _ti;
		TMP_TextInfo ti3 = _ti;
		object obj14 = 0;
		float x = vector.x;
		int num31 = 0;
		int num32 = 0;
		int num33 = 0;
		while (num32 < ti2.lineCount)
		{
			TMP_LineInfo[] lineInfo = ti3.lineInfo;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v40 (TMPro.TMP_LineInfo[])+70+v498 @ rcx_v33 (System.Int32)]");
			object obj15 = 0 - localPadding;
			float x2 = vector.x;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)x2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v40 (TMPro.TMP_LineInfo[])+78+v498 @ rcx_v33 (System.Int32)]");
				obj14 = 0 + localPadding;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)vector.x))
				{
					float num34 = (float)obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadLineRangeDeleterTMP)+8C]");
					x = num34 - 0f;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)x))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadLineRangeDeleterTMP)+8C]");
						object obj16 = obj6 + 0;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj16) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
						{
							_003CHoveredLineIndex_003Ek__BackingField = num31;
							break;
						}
					}
				}
			}
			num31++;
			num33 += 96;
			num32 = num31;
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

	private void UpdateHighlightsForHoverOnly()
	{
		if (highlightHoveredLine && _003CHoveredLineIndex_003Ek__BackingField >= 0)
		{
			_003CSelectedLineMin_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			_003CSelectedLineMax_003Ek__BackingField = _003CHoveredLineIndex_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 28 Invalid \"Jump target not found in method: 0x180448BD0\"");
		}
		ClearSelectionAndHighlights();
	}

	private unsafe void UpdateHighlightsForSelectionRange()
	{
		//IL_04d7: Expected O, but got I4
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Expected O, but got Unknown
		//IL_02ed: Expected O, but got I4
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		//IL_03d4: Expected O, but got I4
		//IL_0437: Expected O, but got Ref
		//IL_052d: Expected O, but got Ref
		//IL_053b: Expected O, but got Ref
		if (sourceTMP != null)
		{
			sourceTMP.ForceMeshUpdate();
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
					object obj8 = default(object);
					Quaternion quaternion = default(Quaternion);
					object obj9 = default(object);
					object obj10 = default(object);
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
						if ((nint)obj2 <= 0)
						{
							return;
						}
						int num4 = num;
						int num5 = 0;
						do
						{
							object obj6 = num4 - num;
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
							if (_ti != null)
							{
								TMP_TextInfo ti2 = _ti;
								if (ti2.lineCount > 0)
								{
									object obj7 = num + num5;
									if ((nint)obj7 >= 0)
									{
										TMP_TextInfo ti3 = _ti;
										if (num4 < ti3.lineCount)
										{
											((Transform)obj5).localPosition = (Vector3)(&obj8);
											((Transform)obj5).localRotation = (Quaternion)(&quaternion);
											((Transform)obj5).localScale = (Vector3)(&obj9);
											obj8 = obj10;
										}
									}
								}
							}
							ApplyHighlightTint((Transform)obj5);
							num4++;
							num5++;
						}
						while (num5 < (nint)obj2);
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
		//IL_0411: Expected O, but got Ref
		//IL_0423: Expected O, but got Ref
		Transform transform2;
		if (highlightPrefab == null)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			if ((object)gameObject != null)
			{
				gameObject.name = "LineHighlight (AutoQuad)";
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
							goto IL_03b3;
						}
						material2 = material;
					}
					else
					{
						if ((object)obj == null)
						{
							goto IL_03b3;
						}
						material2 = highlightMaterial;
					}
					((Renderer)obj).SetMaterial(material2);
				}
				Transform transform = gameObject.transform;
				transform2 = transform;
				goto IL_0238;
			}
		}
		else
		{
			Transform transform3 = UnityEngine.Object.Instantiate(highlightPrefab);
			if ((object)highlightPrefab != null)
			{
				string text = highlightPrefab.name;
				string text2 = text + " (LineHighlight)";
				if ((object)transform3 != null)
				{
					transform3.name = text2;
					transform2 = transform3;
					goto IL_0238;
				}
			}
		}
		goto IL_03b3;
		IL_03b3:
		return (Transform)(object)new NullReferenceException();
		IL_0238:
		Transform parent;
		if (parentHighlightsToTMP && sourceTMP != null)
		{
			if ((object)sourceTMP == null)
			{
				goto IL_03b3;
			}
			parent = sourceTMP.transform;
		}
		else
		{
			parent = base.transform;
		}
		if ((object)transform2 != null)
		{
			transform2.SetParent(parent, worldPositionStays: false);
			Quaternion quaternion = default(Quaternion);
			transform2.localRotation = (Quaternion)(&quaternion);
			transform2.localScale = (Vector3)(&quaternion);
			if (transform2 != null)
			{
				GameObject gameObject2 = transform2.gameObject;
				if ((object)gameObject2 == null)
				{
					goto IL_03b3;
				}
				if (gameObject2.activeSelf)
				{
					GameObject gameObject3 = transform2.gameObject;
					if ((object)gameObject3 == null)
					{
						goto IL_03b3;
					}
					gameObject3.SetActive(value: false);
				}
			}
			ApplyHighlightTint(transform2);
			return transform2;
		}
		goto IL_03b3;
	}

	private unsafe void UpdateHighlightForLine(Transform h, int lineIndex)
	{
		//IL_009c: Expected O, but got Ref
		//IL_00b0: Expected O, but got Ref
		//IL_00bd: Expected O, but got Ref
		if (_ti != null)
		{
			TMP_TextInfo ti = _ti;
			if (ti.lineCount > 0 && lineIndex >= 0 && lineIndex < ti.lineCount)
			{
				Quaternion quaternion = default(Quaternion);
				h.localPosition = (Vector3)(&quaternion);
				h.localRotation = (Quaternion)(&quaternion);
				h.localScale = (Vector3)(&quaternion);
			}
		}
	}

	private unsafe void ApplyHighlightTint(Transform h)
	{
		//IL_0101: Expected O, but got Ref
		//IL_00e9: Expected O, but got Ref
		if (!(h != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!(obj != null))
		{
			return;
		}
		Material sharedMaterial = ((Renderer)obj).GetSharedMaterial();
		if (!(sharedMaterial != null))
		{
			return;
		}
		Material sharedMaterial2 = ((Renderer)obj).GetSharedMaterial();
		object obj2 = default(object);
		if (!sharedMaterial2.HasProperty("_BaseColor"))
		{
			if (sharedMaterial2.HasProperty("_Color"))
			{
				sharedMaterial2.SetColor("_Color", (Color)(&obj2));
			}
		}
		else
		{
			sharedMaterial2.SetColor("_BaseColor", (Color)(&obj2));
		}
	}

	public void OverrideHighlightedLine(int lineIndex)
	{
		_003CHoveredLineIndex_003Ek__BackingField = lineIndex;
		if (lineIndex >= 0 && highlightHoveredLine)
		{
			_003CSelectedLineMin_003Ek__BackingField = lineIndex;
			_003CSelectedLineMax_003Ek__BackingField = lineIndex;
			UpdateHighlightsForSelectionRange();
		}
		else
		{
			ClearSelectionAndHighlights();
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

	public NotepadLineRangeDeleterTMP()
	{
		//IL_0043: Expected O, but got I
		//IL_0064: Expected O, but got I4
		//IL_008f: Expected I4, but got I8
		autoFindCursorManagerByTag = true;
		cursorManagerTag = "CursorManager";
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F90]");
		highlightColor = (Color)0;
		highlightHoveredLine = true;
		dragLineThreshold = 1;
		localPadding = (Vector2)1022739087;
		_ = 1022739087;
		normalOffset = 0.0025f;
		parentHighlightsToTMP = true;
		_003CHoveredLineIndex_003Ek__BackingField = -1;
		_003CSelectedLineMin_003Ek__BackingField = -1;
		_pressStartLine = -1;
		List<Transform> highlights = new List<Transform>(16);
		_highlights = highlights;
		base._002Ector();
	}
}
