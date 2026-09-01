using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Kamgam.UIToolkitComponentsForSettings;

public class UIElementEvents : UIElementClickEvent
{
	public UnityEvent<PointerDownEvent> OnPointerDown;

	public UnityEvent<PointerUpEvent> OnPointerUp;

	public UnityEvent<PointerEnterEvent> OnPointerEnter;

	public UnityEvent<PointerLeaveEvent> OnPointerLeave;

	public UnityEvent<FocusEvent> OnFocus;

	public UnityEvent<BlurEvent> OnBlur;

	public UnityEvent<KeyDownEvent> OnKeyDown;

	public UnityEvent<KeyUpEvent> OnKeyUp;

	public UnityEvent<ChangeEvent<bool>> OnChangeBool;

	public UnityEvent<ChangeEvent<int>> OnChangeInt;

	public UnityEvent<ChangeEvent<float>> OnChangeFloat;

	public UnityEvent<ChangeEvent<string>> OnChangeString;

	public override void RegisterEvents()
	{
		//IL_007b: Expected I, but got O
		//IL_00f5: Expected I, but got O
		//IL_0188: Expected I, but got O
		//IL_021b: Expected I, but got O
		//IL_02ae: Expected I, but got O
		//IL_0341: Expected I, but got O
		//IL_03d4: Expected I, but got O
		//IL_0467: Expected I, but got O
		//IL_04fa: Expected I, but got O
		//IL_058d: Expected I, but got O
		//IL_0621: Expected I, but got O
		//IL_06ac: Expected I, but got O
		List<VisualElement> elements = Elements;
		if (elements._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<VisualElement>.Enumerator enumerator = default(List<VisualElement>.Enumerator);
		CallbackEventHandler callbackEventHandler = default(CallbackEventHandler);
		nint num2 = default(nint);
		EventCallback<PointerDownEvent> eventCallback2 = default(EventCallback<PointerDownEvent>);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				CallbackEventHandler callbackEventHandler2;
				if (OnPointerDown != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ r8_v61 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+1E0]");
					EventCallback<PointerDownEvent> eventCallback = new EventCallback<PointerDownEvent>(this, (IntPtr)0);
					nint num = (nint)this;
					if (callbackEventHandler == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler.RegisterCallback(eventCallback);
					callbackEventHandler2 = callbackEventHandler;
					num2 = 0;
					eventCallback2 = eventCallback;
				}
				else
				{
					callbackEventHandler2 = callbackEventHandler;
				}
				bool flag = OnPointerUp == null;
				nint num3 = num2;
				EventCallback<PointerUpEvent> eventCallback3 = (EventCallback<PointerUpEvent>)eventCallback2;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v720 @ r8_v58 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+1F0]");
					EventCallback<PointerUpEvent> eventCallback4 = new EventCallback<PointerUpEvent>(this, (IntPtr)0);
					nint num4 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						break;
					}
					callbackEventHandler2.RegisterCallback(eventCallback4);
					num3 = 0;
					eventCallback3 = eventCallback4;
				}
				bool flag2 = OnPointerEnter == null;
				nint num5 = num3;
				EventCallback<PointerEnterEvent> eventCallback5 = (EventCallback<PointerEnterEvent>)eventCallback3;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v798 @ r8_v55 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+200]");
					EventCallback<PointerEnterEvent> eventCallback6 = new EventCallback<PointerEnterEvent>(this, (IntPtr)0);
					nint num6 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback6);
					num5 = 0;
					eventCallback5 = eventCallback6;
				}
				bool flag3 = OnPointerLeave == null;
				nint num7 = num5;
				EventCallback<PointerLeaveEvent> eventCallback7 = (EventCallback<PointerLeaveEvent>)eventCallback5;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v876 @ r8_v52 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+210]");
					EventCallback<PointerLeaveEvent> eventCallback8 = new EventCallback<PointerLeaveEvent>(this, (IntPtr)0);
					nint num8 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback8);
					num7 = 0;
					eventCallback7 = eventCallback8;
				}
				bool flag4 = OnFocus == null;
				nint num9 = num7;
				EventCallback<FocusEvent> eventCallback9 = (EventCallback<FocusEvent>)eventCallback7;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ r8_v49 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+220]");
					EventCallback<FocusEvent> eventCallback10 = new EventCallback<FocusEvent>(this, (IntPtr)0);
					nint num10 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback10);
					num9 = 0;
					eventCallback9 = eventCallback10;
				}
				bool flag5 = OnBlur == null;
				nint num11 = num9;
				EventCallback<BlurEvent> eventCallback11 = (EventCallback<BlurEvent>)eventCallback9;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v955 @ r8_v46 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+230]");
					EventCallback<BlurEvent> eventCallback12 = new EventCallback<BlurEvent>(this, (IntPtr)0);
					nint num12 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback12);
					num11 = 0;
					eventCallback11 = eventCallback12;
				}
				bool flag6 = OnKeyDown == null;
				nint num13 = num11;
				EventCallback<KeyDownEvent> eventCallback13 = (EventCallback<KeyDownEvent>)eventCallback11;
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v991 @ r8_v43 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+240]");
					EventCallback<KeyDownEvent> eventCallback14 = new EventCallback<KeyDownEvent>(this, (IntPtr)0);
					nint num14 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback14);
					num13 = 0;
					eventCallback13 = eventCallback14;
				}
				bool flag7 = OnKeyUp == null;
				nint num15 = num13;
				EventCallback<KeyUpEvent> eventCallback15 = (EventCallback<KeyUpEvent>)eventCallback13;
				if (!flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1027 @ r8_v40 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+250]");
					EventCallback<KeyUpEvent> eventCallback16 = new EventCallback<KeyUpEvent>(this, (IntPtr)0);
					nint num16 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback16);
					num15 = 0;
					eventCallback15 = eventCallback16;
				}
				bool flag8 = OnChangeBool == null;
				nint num17 = num15;
				EventCallback<ChangeEvent<bool>> eventCallback17 = (EventCallback<ChangeEvent<bool>>)eventCallback15;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1063 @ r8_v37 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+260]");
					EventCallback<ChangeEvent<bool>> eventCallback18 = new EventCallback<ChangeEvent<bool>>(this, (IntPtr)0);
					nint num18 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback18);
					num17 = 0;
					eventCallback17 = eventCallback18;
				}
				bool flag9 = OnChangeInt == null;
				nint num19 = num17;
				EventCallback<ChangeEvent<int>> eventCallback19 = (EventCallback<ChangeEvent<int>>)eventCallback17;
				if (!flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1099 @ r8_v34 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+270]");
					EventCallback<ChangeEvent<int>> eventCallback20 = new EventCallback<ChangeEvent<int>>(this, (IntPtr)0);
					nint num20 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback20);
					num19 = 0;
					eventCallback19 = eventCallback20;
				}
				bool flag10 = OnChangeFloat == null;
				num2 = num19;
				EventCallback<ChangeEvent<float>> eventCallback21 = (EventCallback<ChangeEvent<float>>)eventCallback19;
				if (!flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1119 @ r8_v31 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+280]");
					EventCallback<ChangeEvent<float>> eventCallback22 = new EventCallback<ChangeEvent<float>>(this, (IntPtr)0);
					nint num21 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback22);
					num2 = 0;
					eventCallback21 = eventCallback22;
				}
				bool flag11 = OnChangeString == null;
				eventCallback2 = (EventCallback<PointerDownEvent>)eventCallback21;
				if (!flag11)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1124 @ r8_v28 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+290]");
					EventCallback<ChangeEvent<string>> eventCallback23 = new EventCallback<ChangeEvent<string>>(this, (IntPtr)0);
					nint num22 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.RegisterCallback(eventCallback23);
					num2 = 0;
					eventCallback2 = (EventCallback<PointerDownEvent>)eventCallback23;
				}
				continue;
			}
			enumerator.Dispose();
			base.RegisterEvents();
			return;
		}
		throw new NullReferenceException();
	}

	public override void UnregisterEvents()
	{
		//IL_007b: Expected I, but got O
		//IL_00f5: Expected I, but got O
		//IL_0188: Expected I, but got O
		//IL_021b: Expected I, but got O
		//IL_02ae: Expected I, but got O
		//IL_0341: Expected I, but got O
		//IL_03d4: Expected I, but got O
		//IL_0467: Expected I, but got O
		//IL_04fa: Expected I, but got O
		//IL_058d: Expected I, but got O
		//IL_0621: Expected I, but got O
		//IL_06ac: Expected I, but got O
		List<VisualElement> elements = Elements;
		if (elements._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<VisualElement>.Enumerator enumerator = default(List<VisualElement>.Enumerator);
		CallbackEventHandler callbackEventHandler = default(CallbackEventHandler);
		nint num2 = default(nint);
		EventCallback<PointerDownEvent> eventCallback2 = default(EventCallback<PointerDownEvent>);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				CallbackEventHandler callbackEventHandler2;
				if (OnPointerDown != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ r8_v61 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+1E0]");
					EventCallback<PointerDownEvent> eventCallback = new EventCallback<PointerDownEvent>(this, (IntPtr)0);
					nint num = (nint)this;
					if (callbackEventHandler == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler.UnregisterCallback(eventCallback);
					callbackEventHandler2 = callbackEventHandler;
					num2 = 0;
					eventCallback2 = eventCallback;
				}
				else
				{
					callbackEventHandler2 = callbackEventHandler;
				}
				bool flag = OnPointerUp == null;
				nint num3 = num2;
				EventCallback<PointerUpEvent> eventCallback3 = (EventCallback<PointerUpEvent>)eventCallback2;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v720 @ r8_v58 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+1F0]");
					EventCallback<PointerUpEvent> eventCallback4 = new EventCallback<PointerUpEvent>(this, (IntPtr)0);
					nint num4 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						break;
					}
					callbackEventHandler2.UnregisterCallback(eventCallback4);
					num3 = 0;
					eventCallback3 = eventCallback4;
				}
				bool flag2 = OnPointerEnter == null;
				nint num5 = num3;
				EventCallback<PointerEnterEvent> eventCallback5 = (EventCallback<PointerEnterEvent>)eventCallback3;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v798 @ r8_v55 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+200]");
					EventCallback<PointerEnterEvent> eventCallback6 = new EventCallback<PointerEnterEvent>(this, (IntPtr)0);
					nint num6 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback6);
					num5 = 0;
					eventCallback5 = eventCallback6;
				}
				bool flag3 = OnPointerLeave == null;
				nint num7 = num5;
				EventCallback<PointerLeaveEvent> eventCallback7 = (EventCallback<PointerLeaveEvent>)eventCallback5;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v876 @ r8_v52 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+210]");
					EventCallback<PointerLeaveEvent> eventCallback8 = new EventCallback<PointerLeaveEvent>(this, (IntPtr)0);
					nint num8 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback8);
					num7 = 0;
					eventCallback7 = eventCallback8;
				}
				bool flag4 = OnFocus == null;
				nint num9 = num7;
				EventCallback<FocusEvent> eventCallback9 = (EventCallback<FocusEvent>)eventCallback7;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ r8_v49 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+220]");
					EventCallback<FocusEvent> eventCallback10 = new EventCallback<FocusEvent>(this, (IntPtr)0);
					nint num10 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback10);
					num9 = 0;
					eventCallback9 = eventCallback10;
				}
				bool flag5 = OnBlur == null;
				nint num11 = num9;
				EventCallback<BlurEvent> eventCallback11 = (EventCallback<BlurEvent>)eventCallback9;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v955 @ r8_v46 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+230]");
					EventCallback<BlurEvent> eventCallback12 = new EventCallback<BlurEvent>(this, (IntPtr)0);
					nint num12 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback12);
					num11 = 0;
					eventCallback11 = eventCallback12;
				}
				bool flag6 = OnKeyDown == null;
				nint num13 = num11;
				EventCallback<KeyDownEvent> eventCallback13 = (EventCallback<KeyDownEvent>)eventCallback11;
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v991 @ r8_v43 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+240]");
					EventCallback<KeyDownEvent> eventCallback14 = new EventCallback<KeyDownEvent>(this, (IntPtr)0);
					nint num14 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback14);
					num13 = 0;
					eventCallback13 = eventCallback14;
				}
				bool flag7 = OnKeyUp == null;
				nint num15 = num13;
				EventCallback<KeyUpEvent> eventCallback15 = (EventCallback<KeyUpEvent>)eventCallback13;
				if (!flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1027 @ r8_v40 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+250]");
					EventCallback<KeyUpEvent> eventCallback16 = new EventCallback<KeyUpEvent>(this, (IntPtr)0);
					nint num16 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback16);
					num15 = 0;
					eventCallback15 = eventCallback16;
				}
				bool flag8 = OnChangeBool == null;
				nint num17 = num15;
				EventCallback<ChangeEvent<bool>> eventCallback17 = (EventCallback<ChangeEvent<bool>>)eventCallback15;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1063 @ r8_v37 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+260]");
					EventCallback<ChangeEvent<bool>> eventCallback18 = new EventCallback<ChangeEvent<bool>>(this, (IntPtr)0);
					nint num18 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback18);
					num17 = 0;
					eventCallback17 = eventCallback18;
				}
				bool flag9 = OnChangeInt == null;
				nint num19 = num17;
				EventCallback<ChangeEvent<int>> eventCallback19 = (EventCallback<ChangeEvent<int>>)eventCallback17;
				if (!flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1099 @ r8_v34 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+270]");
					EventCallback<ChangeEvent<int>> eventCallback20 = new EventCallback<ChangeEvent<int>>(this, (IntPtr)0);
					nint num20 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback20);
					num19 = 0;
					eventCallback19 = eventCallback20;
				}
				bool flag10 = OnChangeFloat == null;
				num2 = num19;
				EventCallback<ChangeEvent<float>> eventCallback21 = (EventCallback<ChangeEvent<float>>)eventCallback19;
				if (!flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1119 @ r8_v31 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+280]");
					EventCallback<ChangeEvent<float>> eventCallback22 = new EventCallback<ChangeEvent<float>>(this, (IntPtr)0);
					nint num21 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback22);
					num2 = 0;
					eventCallback21 = eventCallback22;
				}
				bool flag11 = OnChangeString == null;
				eventCallback2 = (EventCallback<PointerDownEvent>)eventCallback21;
				if (!flag11)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1124 @ r8_v28 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementEvents>)+290]");
					EventCallback<ChangeEvent<string>> eventCallback23 = new EventCallback<ChangeEvent<string>>(this, (IntPtr)0);
					nint num22 = (nint)this;
					if (callbackEventHandler2 == null)
					{
						throw new NullReferenceException();
					}
					callbackEventHandler2.UnregisterCallback(eventCallback23);
					num2 = 0;
					eventCallback2 = (EventCallback<PointerDownEvent>)eventCallback23;
				}
				continue;
			}
			enumerator.Dispose();
			base.UnregisterEvents();
			return;
		}
		throw new NullReferenceException();
	}

	protected virtual void onPointerDown(PointerDownEvent evt)
	{
		if (OnPointerDown != null)
		{
			OnPointerDown.Invoke(evt);
		}
	}

	protected virtual void onPointerUp(PointerUpEvent evt)
	{
		if (OnPointerUp != null)
		{
			OnPointerUp.Invoke(evt);
		}
	}

	protected virtual void onPointerEnter(PointerEnterEvent evt)
	{
		if (OnPointerEnter != null)
		{
			OnPointerEnter.Invoke(evt);
		}
	}

	protected virtual void onPointerLeave(PointerLeaveEvent evt)
	{
		if (OnPointerLeave != null)
		{
			OnPointerLeave.Invoke(evt);
		}
	}

	protected virtual void onFocus(FocusEvent evt)
	{
		if (OnFocus != null)
		{
			OnFocus.Invoke(evt);
		}
	}

	protected virtual void onBlur(BlurEvent evt)
	{
		if (OnBlur != null)
		{
			OnBlur.Invoke(evt);
		}
	}

	protected virtual void onKeyDown(KeyDownEvent evt)
	{
		if (OnKeyDown != null)
		{
			OnKeyDown.Invoke(evt);
		}
	}

	protected virtual void onKeyUp(KeyUpEvent evt)
	{
		if (OnKeyUp != null)
		{
			OnKeyUp.Invoke(evt);
		}
	}

	protected virtual void onChangeBool(ChangeEvent<bool> evt)
	{
		if (OnChangeBool != null)
		{
			OnChangeBool.Invoke(evt);
		}
	}

	protected virtual void onChangeInt(ChangeEvent<int> evt)
	{
		if (OnChangeInt != null)
		{
			OnChangeInt.Invoke(evt);
		}
	}

	protected virtual void onChangeFloat(ChangeEvent<float> evt)
	{
		if (OnChangeFloat != null)
		{
			OnChangeFloat.Invoke(evt);
		}
	}

	protected virtual void onChangeString(ChangeEvent<string> evt)
	{
		if (OnChangeString != null)
		{
			OnChangeString.Invoke(evt);
		}
	}

	public UIElementEvents()
	{
		List<VisualElement> elements = new List<VisualElement>();
		Elements = elements;
		((MonoBehaviour)this)._002Ector();
	}
}
