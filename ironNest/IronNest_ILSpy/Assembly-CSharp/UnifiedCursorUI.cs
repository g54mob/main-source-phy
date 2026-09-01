using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

public class UnifiedCursorUI : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass34_0
	{
		public Texture2D modeDefault;

		public Texture2D otherModeDefault;

		public UnifiedCursorUI _003C_003E4__this;
	}

	private DynamicCursorManager cursorManager;

	private VirtualCursor virtualCursor;

	private bool showInFPSLockedMode;

	private bool showInFreeMouseMode;

	private Texture2D fpsDefaultTexture;

	private Texture2D freeMouseDefaultTexture;

	private Texture2D sharedHoverTexture;

	private Texture2D sharedGrabTexture;

	private bool usePerObjectTextureOverrides;

	private bool useRuntimeCursorOverrides;

	private Vector2 fpsCenterOffset;

	private Vector2 freeMouseOffset;

	private bool clampFreeMouse;

	private float freeMouseEdgePadding;

	private bool enforceCenteredPivot;

	private bool logStateChanges;

	private bool disableIfInvalidSetup;

	private RectTransform _rect;

	private Canvas _canvas;

	private RawImage _raw;

	private bool _subscribed;

	private bool _valid;

	private DynamicCursorManager.CursorVisualState _currentState;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RectTransform rect = default(RectTransform);
		_rect = rect;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RawImage raw = default(RawImage);
		_raw = raw;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
		Canvas canvas = default(Canvas);
		_canvas = canvas;
		ValidateSetup();
		if (_valid)
		{
			if (enforceCenteredPivot)
			{
				Vector2 pivot = default(Vector2);
				_rect.pivot = pivot;
			}
			ApplyVisualForState(_currentState);
			RepositionInstant();
		}
	}

	private void OnEnable()
	{
		if (_valid)
		{
			Subscribe();
			UpdateVisibility();
			if (_valid)
			{
				ApplyVisualForState(_currentState);
				UpdateVisibility();
				RepositionInstant();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 46 Invalid \"Jump target not found in method: 0x180527AC0\"");
		}
	}

	private void OnDisable()
	{
		Unsubscribe();
	}

	private void LateUpdate()
	{
		if (_valid)
		{
			RepositionInstant();
		}
	}

	private void ValidateSetup()
	{
		if (_rect != null && _raw != null)
		{
			object message;
			if (virtualCursor != null)
			{
				if (!(fpsDefaultTexture == null) || !(freeMouseDefaultTexture == null))
				{
					_valid = true;
					return;
				}
				message = "[UnifiedCursorUI] Need at least one default texture (FPS or FreeMouse).";
			}
			else
			{
				message = "[UnifiedCursorUI] VirtualCursor is required. Assign it in the inspector.";
			}
			Debug.LogError(message, this);
			_valid = false;
			if (!disableIfInvalidSetup)
			{
				return;
			}
		}
		else
		{
			Debug.LogError("[UnifiedCursorUI] Missing RawImage/RectTransform.", this);
			bool flag = !disableIfInvalidSetup;
			_valid = false;
			if (flag)
			{
				return;
			}
		}
		base.enabled = false;
	}

	private void Subscribe()
	{
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Expected O, but got Unknown
		//IL_0090: Expected I, but got O
		//IL_0099: Expected O, but got I4
		//IL_00aa: Expected O, but got I4
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_01e8: Expected I, but got O
		//IL_01f1: Expected O, but got I4
		//IL_0202: Expected O, but got I4
		bool flag = cursorManager == null;
		if (!flag)
		{
			if (_subscribed != flag)
			{
				return;
			}
			DynamicCursorManager dynamicCursorManager = cursorManager;
			Action<DynamicCursorManager.CursorVisualState> b = HandleVisualStateChanged;
			Delegate obj = dynamicCursorManager.OnCursorVisualStateChanged;
			object obj2 = dynamicCursorManager + 32;
			Delegate obj8 = default(Delegate);
			Delegate obj13 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, b);
				bool flag2 = (object)obj3 == null;
				Delegate obj4 = obj3;
				nint num;
				object obj5;
				Delegate obj6;
				object obj7;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag3 = (object)obj4 == null;
					num = (nint)typeof(Action<DynamicCursorManager.CursorVisualState>);
					obj5 = 0;
					obj6 = obj3;
					obj7 = 0;
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj8 != obj;
				obj = obj8;
				if (flag4)
				{
					continue;
				}
				Action<Interactable> value = HandleHoverTargetChanged;
				cursorManager.OnCursorTargetChanged += value;
				DynamicCursorManager dynamicCursorManager2 = cursorManager;
				Action<bool> b2 = HandleSuppressedChanged;
				Delegate obj9 = dynamicCursorManager2.OnSuppressedByLockBrokerChanged;
				object obj10 = dynamicCursorManager2 + 72;
				Delegate obj11;
				while (true)
				{
					obj11 = Delegate.Combine(obj9, b2);
					bool flag5 = (object)obj11 == null;
					Delegate obj12 = obj11;
					if (!flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						if ((object)obj12 == null)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag6 = (object)obj13 != obj9;
					obj9 = obj13;
					if (!flag6)
					{
						DynamicCursorManager dynamicCursorManager3 = cursorManager;
						_subscribed = true;
						_currentState = dynamicCursorManager3._currentVisualState;
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				num = (nint)typeof(Action<bool>);
				obj5 = 0;
				obj6 = obj11;
				obj7 = 0;
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			throw new NullReferenceException();
		}
		Debug.LogWarning("[UnifiedCursorUI] No DynamicCursorManager assigned.", this);
	}

	private void Unsubscribe()
	{
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Expected O, but got Unknown
		//IL_0080: Expected I4, but got O
		//IL_009d: Expected I, but got O
		//IL_011a: Expected I, but got O
		//IL_017f: Expected I, but got O
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_01a6: Expected I4, but got O
		//IL_01c3: Expected I, but got O
		if (!_subscribed || !(cursorManager != null))
		{
			return;
		}
		DynamicCursorManager dynamicCursorManager = cursorManager;
		Action<DynamicCursorManager.CursorVisualState> value = HandleVisualStateChanged;
		if ((object)cursorManager != null)
		{
			Delegate obj = dynamicCursorManager.OnCursorVisualStateChanged;
			object obj2 = cursorManager + 32;
			Delegate obj6 = default(Delegate);
			Delegate obj11 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				Delegate obj5;
				nint num;
				if (!flag)
				{
					((UnifiedCursorUI)(object)obj3).HandleVisualStateChanged((DynamicCursorManager.CursorVisualState)typeof(Action<DynamicCursorManager.CursorVisualState>));
					bool flag2 = (object)obj4 == null;
					num = (nint)typeof(Action<DynamicCursorManager.CursorVisualState>);
					obj5 = obj3;
					if (flag2)
					{
						goto IL_0270;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj6 != obj;
				obj = obj6;
				if (flag3)
				{
					continue;
				}
				Action<Interactable> action = HandleHoverTargetChanged;
				bool flag4 = (object)cursorManager == null;
				num = (nint)typeof(Action<DynamicCursorManager.CursorVisualState>);
				dynamicCursorManager = (DynamicCursorManager)(object)action;
				if (flag4)
				{
					break;
				}
				cursorManager.OnCursorTargetChanged -= action;
				dynamicCursorManager = cursorManager;
				Action<bool> value2 = HandleSuppressedChanged;
				bool flag5 = (object)cursorManager == null;
				num = (nint)typeof(Action<DynamicCursorManager.CursorVisualState>);
				if (flag5)
				{
					break;
				}
				Delegate obj7 = dynamicCursorManager.OnSuppressedByLockBrokerChanged;
				object obj8 = cursorManager + 72;
				while (true)
				{
					Delegate obj9 = Delegate.Remove(obj7, value2);
					bool flag6 = (object)obj9 == null;
					Delegate obj10 = obj9;
					if (!flag6)
					{
						((UnifiedCursorUI)(object)obj9).HandleVisualStateChanged((DynamicCursorManager.CursorVisualState)typeof(Action<bool>));
						bool flag7 = (object)obj10 == null;
						num = (nint)typeof(Action<bool>);
						obj5 = obj9;
						if (flag7)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag8 = (object)obj11 != obj7;
					obj7 = obj11;
					if (!flag8)
					{
						_subscribed = false;
						return;
					}
				}
				((UnifiedCursorUI)(object)obj5).HandleVisualStateChanged((DynamicCursorManager.CursorVisualState)num);
				goto IL_0270;
				IL_0270:
				((UnifiedCursorUI)(object)obj5).HandleVisualStateChanged((DynamicCursorManager.CursorVisualState)num);
				return;
			}
		}
		throw new NullReferenceException();
	}

	private void HandleVisualStateChanged(DynamicCursorManager.CursorVisualState newState)
	{
		//IL_000e: Expected I4, but got O
		_currentState = newState;
		ApplyVisualForState(newState);
		UpdateVisibility();
		if (logStateChanges)
		{
			object obj = default(object);
			object arg = (DynamicCursorManager.CursorVisualState)obj;
			string message = $"[UnifiedCursorUI] State -> {arg}";
			Debug.Log(message, this);
		}
	}

	private void HandleSuppressedChanged(bool _)
	{
		UpdateVisibility();
	}

	private void HandleHoverTargetChanged(Interactable _)
	{
		ApplyVisualForState(_currentState);
	}

	private void ApplyVisualForState(DynamicCursorManager.CursorVisualState state)
	{
		if (!_valid)
		{
			return;
		}
		Texture2D texture2D = ResolveTextureForState(state);
		if (texture2D != null)
		{
			RawImage raw = _raw;
			if (raw.m_Texture != texture2D)
			{
				_raw.texture = texture2D;
			}
		}
	}

	private unsafe Texture2D ResolveTextureForState(DynamicCursorManager.CursorVisualState state)
	{
		//IL_0008: Expected O, but got Ref
		//IL_07e6: Expected O, but got I4
		//IL_020e: Expected O, but got I4
		//IL_0106: Expected I, but got O
		//IL_010e: Expected I, but got O
		//IL_011e: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_025d: Expected O, but got I4
		//IL_0195: Expected O, but got Ref
		//IL_01d3: Expected O, but got Ref
		//IL_0759: Expected O, but got I
		//IL_05f4: Expected O, but got I
		//IL_064e: Expected O, but got I
		//IL_0682: Expected O, but got I
		//IL_03ba: Expected O, but got I
		//IL_0303: Expected O, but got I
		//IL_06af: Expected O, but got I
		//IL_04e7: Expected O, but got I
		//IL_045b: Expected O, but got I
		//IL_03e9: Expected O, but got I
		//IL_0338: Expected O, but got I
		//IL_0488: Expected O, but got I
		//IL_036c: Expected O, but got I
		//IL_0399: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		bool flag;
		if (!(cursorManager != null))
		{
			flag = false;
		}
		else
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			bool flag2 = dynamicCursorManager._currentMode == DynamicCursorManager.PresentationMode.FPSLocked;
			flag = flag2;
		}
		object obj3 = (flag ? 1 : 0) ^ 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (UnifiedCursorUI)+38+v113 @ rax_v12*8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (UnifiedCursorUI)+38+v87 @ rax_v10 (System.Boolean)*8]");
		_ = 0;
		UnityEngine.Object obj4;
		if ((bool)cursorManager)
		{
			DynamicCursorManager dynamicCursorManager2 = cursorManager;
			obj4 = dynamicCursorManager2._currentHover;
		}
		else
		{
			obj4 = null;
		}
		bool flag3;
		if ((bool)cursorManager)
		{
			DynamicCursorManager dynamicCursorManager3 = cursorManager;
			ICursorDraggable activeDrag = dynamicCursorManager3._activeDrag;
			if (dynamicCursorManager3._activeDrag != null)
			{
				nint num = (nint)typeof(Component);
				nint num2 = (nint)activeDrag;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ rdx_v46 (Il2CppClass<UnityEngine.Component>)+130]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v417 @ r8_v37 (Il2CppClass<ICursorDraggable>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ rdx_v46 (Il2CppClass<UnityEngine.Component>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v417 @ r8_v37 (Il2CppClass<ICursorDraggable>)+C8]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v420 @ rax_v103+FFFFFFF8+v419 @ rax_v102*8]");
					if (0 == (nint)typeof(Component))
					{
						object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 31));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1F]");
						flag3 = false;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1F]");
						if ((nint)0 == 0)
						{
							object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+27]");
							flag3 = false;
						}
						goto IL_0839;
					}
				}
			}
		}
		flag3 = false;
		goto IL_0839;
		IL_08d5:
		return (Texture2D)(object)new NullReferenceException();
		IL_078c:
		return sharedHoverTexture;
		IL_0839:
		bool flag4 = state == DynamicCursorManager.CursorVisualState.Default;
		if (!flag4)
		{
			object obj9 = state - 1;
			if (!flag4)
			{
				if ((nint)obj9 == 1)
				{
					if (flag3)
					{
						obj4 = (UnityEngine.Object)flag3;
					}
					if (usePerObjectTextureOverrides && obj4 != null)
					{
						_ = 0;
						_ = 0;
						if (useRuntimeCursorOverrides && obj4 != null && ((Component)obj4).TryGetComponent(out System.Runtime.CompilerServices.Unsafe.As<object, InteractableRuntimeCursorOverride>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41))))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-29]");
							if ((UnityEngine.Object)0 != null)
							{
								ref Texture2D texture = ref System.Runtime.CompilerServices.Unsafe.As<object, Texture2D>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-29]");
								if (((InteractableRuntimeCursorOverride)0).TryGetGrabOverride(out texture))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+67]");
									if ((UnityEngine.Object)0 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+67]");
										return (Texture2D)0;
									}
								}
							}
						}
						_ = Vector2.zeroVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v12 (UnityEngine.Object)+40]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v12 (UnityEngine.Object)+38]");
							if (!((UnityEngine.Object)0 == null))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v12 (UnityEngine.Object)+38]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+7F]");
								return (Texture2D)0;
							}
						}
						_ = 0;
						if (_003CResolveTextureForState_003Eg__TryGetRuntimeHoverTex_007C34_1((Interactable)obj4, out System.Runtime.CompilerServices.Unsafe.As<object, Texture2D>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1)), ref System.Runtime.CompilerServices.Unsafe.As<object, _003C_003Ec__DisplayClass34_0>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25))))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-1]");
							if ((UnityEngine.Object)0 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-1]");
								return (Texture2D)0;
							}
						}
						if (((Interactable)obj4).TryGetCursor(out System.Runtime.CompilerServices.Unsafe.As<object, Texture2D>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 15)), out System.Runtime.CompilerServices.Unsafe.As<object, Vector2>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7))))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+F]");
							return (Texture2D)0;
						}
					}
					if (!(sharedGrabTexture == null))
					{
						return sharedGrabTexture;
					}
					if (!(sharedHoverTexture == null))
					{
						goto IL_078c;
					}
				}
			}
			else
			{
				_ = 0;
				_ = 0;
				if (useRuntimeCursorOverrides && obj4 != null)
				{
					if ((object)obj4 == null)
					{
						goto IL_08d5;
					}
					if (((Component)obj4).TryGetComponent(out System.Runtime.CompilerServices.Unsafe.As<object, InteractableRuntimeCursorOverride>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41))))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-29]");
						if ((UnityEngine.Object)0 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-29]");
							if ((nint)0 == 0)
							{
								goto IL_08d5;
							}
							ref Texture2D texture2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Texture2D>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 33));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-29]");
							if (((InteractableRuntimeCursorOverride)0).TryGetHoverOverride(out texture2))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-21]");
								if ((UnityEngine.Object)0 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-21]");
									return (Texture2D)0;
								}
							}
						}
					}
				}
				if (usePerObjectTextureOverrides && obj4 != null && ((Interactable)obj4).TryGetCursor(out System.Runtime.CompilerServices.Unsafe.As<object, Texture2D>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23)), out System.Runtime.CompilerServices.Unsafe.As<object, Vector2>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7))))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+17]");
					return (Texture2D)0;
				}
				if (sharedHoverTexture != null)
				{
					goto IL_078c;
				}
			}
		}
		return _003CResolveTextureForState_003Eg__DefaultChain_007C34_0(ref System.Runtime.CompilerServices.Unsafe.As<object, _003C_003Ec__DisplayClass34_0>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25)));
	}

	private void RepositionInstant()
	{
		//IL_00c0: Expected O, but got I
		//IL_00f5: Invalid comparison between F4 and O
		//IL_01b0: Invalid comparison between F4 and O
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		if (!_valid)
		{
			return;
		}
		if (cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if (dynamicCursorManager._currentMode == DynamicCursorManager.PresentationMode.FPSLocked)
			{
				int width = Screen.width;
				int height = Screen.height;
				goto IL_018e;
			}
		}
		VirtualCursor virtualCursor = this.virtualCursor;
		bool flag = !clampFreeMouse;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (UnifiedCursorUI)+68]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v10 (VirtualCursor)+70]");
		object obj = num + 0;
		object obj2 = virtualCursor._position + freeMouseOffset;
		if (!flag)
		{
			int width2 = Screen.width;
			float num2 = freeMouseEdgePadding;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				object obj3 = width2 - freeMouseEdgePadding;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
				{
				}
			}
			int height2 = Screen.height;
			float num3 = freeMouseEdgePadding;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				object obj4 = height2 - freeMouseEdgePadding;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
				{
				}
			}
		}
		goto IL_018e;
		IL_018e:
		Vector2 screenPosition = default(Vector2);
		SetScreenPosition(screenPosition);
	}

	private unsafe void SetScreenPosition(Vector2 screenPos)
	{
		//IL_010e: Expected O, but got Ref
		//IL_00e4: Expected O, but got Ref
		Transform rect2;
		object obj = default(object);
		if (_canvas != null && _canvas.renderMode != RenderMode.ScreenSpaceOverlay)
		{
			Transform transform = _canvas.transform;
			Camera worldCamera = _canvas.worldCamera;
			bool flag = (object)transform == null;
			RectTransform rect = null;
			if (!flag)
			{
				bool flag2 = (object)transform.GetType() != typeof(RectTransform);
				rect = null;
				if (!flag2)
				{
					rect = (RectTransform)transform;
				}
			}
			bool flag3 = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, worldCamera, out var _);
			rect2 = _rect;
			if (flag3)
			{
				_rect.localPosition = (Vector3)(&obj);
				return;
			}
		}
		else
		{
			rect2 = _rect;
		}
		Vector3 position = rect2.position;
		rect2.position = (Vector3)(&obj);
	}

	private void UpdateVisibility()
	{
		if (!_valid)
		{
			goto IL_0133;
		}
		if (cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if (dynamicCursorManager._suppressedByLockBroker)
			{
				goto IL_0133;
			}
		}
		bool enable;
		bool flag5;
		if (cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager2 = cursorManager;
			if (dynamicCursorManager2._currentMode == DynamicCursorManager.PresentationMode.FPSLocked)
			{
				bool flag = !showInFPSLockedMode;
				bool flag2 = !flag;
				bool flag3 = !flag2;
				enable = !flag3;
				goto IL_0160;
			}
		}
		else
		{
			bool flag4 = showInFPSLockedMode;
			flag5 = true;
			if (flag4)
			{
				goto IL_016b;
			}
		}
		flag5 = showInFreeMouseMode;
		goto IL_016b;
		IL_016b:
		bool flag6 = !flag5;
		enable = !flag6;
		goto IL_0160;
		IL_0160:
		EnableRenderer(enable);
		return;
		IL_0133:
		enable = false;
		goto IL_0160;
	}

	private void EnableRenderer(bool enable)
	{
		if (_raw != null)
		{
			_raw.enabled = enable;
		}
	}

	public void ToggleVisibilityWhenUsingGamepad(bool cursorEnabled)
	{
		DynamicCursorManager dynamicCursorManager = cursorManager;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A951]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = dynamicCursorManager._playerInput.currentControlScheme;
		GameObject gameObject = ((currentControlScheme == "Gamepad") ? base.gameObject : base.gameObject);
		gameObject.SetActive(value: true);
	}

	public void ForceRefreshVisual()
	{
		if (_valid)
		{
			ApplyVisualForState(_currentState);
			UpdateVisibility();
			RepositionInstant();
		}
	}

	public void SetManager(DynamicCursorManager manager)
	{
		Unsubscribe();
		cursorManager = manager;
		Subscribe();
		if (_valid)
		{
			ApplyVisualForState(_currentState);
			UpdateVisibility();
			RepositionInstant();
		}
	}

	public UnifiedCursorUI()
	{
		//IL_0029: Expected I, but got O
		//IL_0064: Expected I, but got O
		showInFPSLockedMode = true;
		usePerObjectTextureOverrides = true;
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		fpsCenterOffset = Vector2.zeroVector;
		nint num3 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v6 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		freeMouseOffset = Vector2.zeroVector;
		clampFreeMouse = true;
		freeMouseEdgePadding = 2f;
		enforceCenteredPivot = true;
		disableIfInvalidSetup = true;
		base._002Ector();
	}

	private Texture2D _003CResolveTextureForState_003Eg__DefaultChain_007C34_0(ref _003C_003Ec__DisplayClass34_0 P_0)
	{
		//IL_003d: Expected O, but got I
		//IL_005e: Expected O, but got I
		if ((UnityEngine.Object)P_0 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass34_0&)+8]");
			if (!((UnityEngine.Object)0 != null))
			{
				return null;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass34_0&)+8]");
			return (Texture2D)0;
		}
		return (Texture2D)P_0;
	}

	private unsafe bool _003CResolveTextureForState_003Eg__TryGetRuntimeHoverTex_007C34_1(Interactable src, out Texture2D tex, ref _003C_003Ec__DisplayClass34_0 P_2)
	{
		//IL_00d5: Expected I4, but got O
		ref Texture2D reference = ref *(Texture2D*)null;
		if (!useRuntimeCursorOverrides || !(src != null))
		{
			goto IL_00c1;
		}
		if ((object)src != null)
		{
			if (!src.TryGetComponent<InteractableRuntimeCursorOverride>(out var component) || !(component != null))
			{
				goto IL_00c1;
			}
			if ((object)component != null)
			{
				return component.TryGetHoverOverride(out tex);
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00c1:
		return false;
	}

	private unsafe bool _003CResolveTextureForState_003Eg__TryGetRuntimeGrabTex_007C34_2(Interactable src, out Texture2D tex, ref _003C_003Ec__DisplayClass34_0 P_2)
	{
		//IL_00d5: Expected I4, but got O
		ref Texture2D reference = ref *(Texture2D*)null;
		if (!useRuntimeCursorOverrides || !(src != null))
		{
			goto IL_00c1;
		}
		if ((object)src != null)
		{
			if (!src.TryGetComponent<InteractableRuntimeCursorOverride>(out var component) || !(component != null))
			{
				goto IL_00c1;
			}
			if ((object)component != null)
			{
				return component.TryGetGrabOverride(out tex);
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00c1:
		return false;
	}
}
