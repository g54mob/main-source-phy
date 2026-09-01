using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class InputBindingUGUI : MonoBehaviour
{
	public delegate void OnChangedDelegate(string bindingPath);

	public Func<string, string> PathToDisplayNameFunc;

	public InputBindingForInputSystem InputBinding;

	public UnityEvent<string> OnChangedEvent;

	public OnChangedDelegate OnChanged;

	public Button Button;

	public GameObject Normal;

	public GameObject Active;

	public TextMeshProUGUI TextTf;

	public TextMeshProUGUI DisplayNameTf;

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
			TextMeshProUGUI textTf = TextTf;
			if ((object)TextTf != null)
			{
				nint num = (nint)textTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = TextTf.text;
			if (value != text)
			{
				TextTf.text = value;
			}
		}
	}

	public virtual string DisplayName
	{
		get
		{
			//IL_0054: Expected I, but got O
			if (DisplayNameTf != null)
			{
				TextMeshProUGUI displayNameTf = DisplayNameTf;
				if ((object)DisplayNameTf == null)
				{
					return (string)(object)new NullReferenceException();
				}
				nint num = (nint)displayNameTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v73 @ rdx_v2 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return null;
		}
		set
		{
			string displayName = DisplayName;
			if (value != displayName)
			{
				DisplayNameTf.text = value;
			}
		}
	}

	public string ActiveText
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI activeTextTf = ActiveTextTf;
			if ((object)ActiveTextTf != null)
			{
				nint num = (nint)activeTextTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = ActiveTextTf.text;
			if (value != text)
			{
				ActiveTextTf.text = value;
			}
		}
	}

	public void CopyFrom(InputBindingUGUI other)
	{
		PathToDisplayNameFunc = other.PathToDisplayNameFunc;
		InputBindingForInputSystem inputBindingForInputSystem = new InputBindingForInputSystem();
		inputBindingForInputSystem.LocalConfigBehaviour = InputBindingForInputSystem.LocalConfigBehaviours.AppendLocalToGlobal;
		string[] ignoreControlPaths = new string[0];
		inputBindingForInputSystem.IgnoreControlPaths = ignoreControlPaths;
		string[] abortControlPaths = new string[0];
		inputBindingForInputSystem.AbortControlPaths = abortControlPaths;
		string[] matchControlPaths = new string[0];
		inputBindingForInputSystem.MatchControlPaths = matchControlPaths;
		inputBindingForInputSystem._bindingPath = "<Keyboard>/space";
		InputBinding = inputBindingForInputSystem;
		InputBindingForInputSystem inputBinding = InputBinding;
		InputBindingForInputSystem inputBinding2 = other.InputBinding;
		inputBinding.LocalConfigBehaviour = inputBinding2.LocalConfigBehaviour;
		string[] ignoreControlPaths2 = inputBinding2.IgnoreControlPaths;
		Array.Copy(inputBinding2.IgnoreControlPaths, inputBinding.IgnoreControlPaths, ignoreControlPaths2.Length);
		string[] abortControlPaths2 = inputBinding2.AbortControlPaths;
		Array.Copy(inputBinding2.AbortControlPaths, inputBinding.AbortControlPaths, abortControlPaths2.Length);
		string[] abortControlPaths3 = inputBinding2.AbortControlPaths;
		Array.Copy(inputBinding2.AbortControlPaths, inputBinding.AbortControlPaths, abortControlPaths3.Length);
		string[] matchControlPaths2 = inputBinding2.MatchControlPaths;
		Array.Copy(inputBinding2.MatchControlPaths, inputBinding.MatchControlPaths, matchControlPaths2.Length);
		inputBinding.ControlsHavingToMatchPath = inputBinding2.ControlsHavingToMatchPath;
		inputBinding._bindingPath = inputBinding2._bindingPath;
		inputBinding.AllowComposite = inputBinding2.AllowComposite;
		inputBinding.CheckBindingPathFunc = inputBinding2.CheckBindingPathFunc;
		inputBinding.OnBeforeRebindStart = inputBinding2.OnBeforeRebindStart;
		inputBinding.OnComplete = inputBinding2.OnComplete;
		inputBinding.OnCanceled = inputBinding2.OnCanceled;
	}

	public virtual void SetActive(bool active)
	{
		//IL_0087: Expected O, but got I4
		//IL_043a: Expected O, but got I4
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Expected O, but got Unknown
		//IL_0074: Expected O, but got I4
		//IL_038c: Expected I, but got O
		//IL_02c3: Expected I, but got O
		//IL_03bf: Expected I, but got O
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Expected O, but got Unknown
		//IL_02e7: Expected I, but got O
		//IL_0321: Expected I, but got O
		//IL_0162: Expected I, but got O
		//IL_049c: Expected O, but got I4
		//IL_04a1: Expected I, but got O
		//IL_0182: Expected O, but got I4
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_0234: Expected O, but got I4
		//IL_023c: Expected I, but got O
		//IL_052a: Expected O, but got I4
		//IL_052f: Expected I, but got O
		//IL_0268: Expected O, but got I4
		//IL_0270: Expected I, but got O
		GameObject active2 = Active;
		object obj;
		object obj8;
		NullReferenceException typeFromHandle;
		nint num;
		if ((object)Active != null)
		{
			bool activeSelf = Active.activeSelf;
			active2 = Active;
			if ((object)Active != null)
			{
				bool activeSelf2 = Active.activeSelf;
				obj = ((activeSelf2 != active) ? ((object)((active ? 1 : 0) ^ 1)) : ((object)0));
				object obj2 = activeSelf ^ active;
				object obj3 = active & obj2;
				if (obj3 == null || InputBinding == null)
				{
					goto IL_0275;
				}
				InputBindingForInputSystem inputBinding = InputBinding;
				Action b = onBindingComplete;
				Delegate obj4 = inputBinding.OnComplete;
				object obj5 = inputBinding + 88;
				Delegate obj9 = default(Delegate);
				while (true)
				{
					Delegate obj6 = Delegate.Combine(obj4, b);
					bool flag = (object)obj6 == null;
					Delegate obj7 = null;
					if (!flag)
					{
						bool flag2 = (object)obj6.GetType() != typeof(Action);
						obj7 = null;
						if (!flag2)
						{
							obj7 = obj6;
						}
						bool flag3 = (object)obj7 == null;
						obj8 = 0;
						num = unchecked((nint)null);
						typeFromHandle = (NullReferenceException)(object)typeof(Action);
						if (flag3)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag4 = (object)obj9 != obj4;
					obj4 = obj9;
					if (flag4)
					{
						continue;
					}
					goto IL_013d;
				}
				goto IL_0586;
			}
		}
		goto IL_03f5;
		IL_013d:
		InputBindingForInputSystem inputBinding2 = InputBinding;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ r8_v15 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1B0]");
		Action action = new Action(this, (IntPtr)0);
		nint num2 = (nint)this;
		bool flag5 = InputBinding == null;
		obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ r8_v15 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1B0]");
		num = 0;
		active2 = (GameObject)(object)action;
		if (flag5)
		{
			goto IL_03f5;
		}
		Delegate obj10 = inputBinding2.OnCanceled;
		object obj11 = InputBinding + 96;
		NullReferenceException ex;
		Delegate obj14 = default(Delegate);
		while (true)
		{
			Delegate obj12 = Delegate.Combine(obj10, action);
			bool flag6 = (object)obj12 == null;
			Delegate obj13 = null;
			if (!flag6)
			{
				bool flag7 = (object)obj12.GetType() != typeof(Action);
				obj13 = null;
				if (!flag7)
				{
					obj13 = obj12;
				}
				bool flag8 = (object)obj13 == null;
				obj8 = 0;
				num = unchecked((nint)null);
				ex = (NullReferenceException)(object)obj12;
				active2 = (GameObject)(object)typeof(Action);
				if (flag8)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag9 = (object)obj14 != obj10;
			obj10 = obj14;
			if (flag9)
			{
				continue;
			}
			goto IL_021a;
		}
		goto IL_0591;
		IL_021a:
		bool flag10 = InputBinding == null;
		obj8 = 0;
		num = (nint)obj14;
		active2 = (GameObject)(object)InputBinding;
		if (!flag10)
		{
			InputBinding.StartListening();
			obj8 = 0;
			num = (nint)obj14;
			goto IL_0275;
		}
		goto IL_03f5;
		IL_0591:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = ex;
		goto IL_0586;
		IL_0275:
		if (obj != null)
		{
			EventSystem current = EventSystem.current;
			bool flag11 = current != null;
			bool flag12 = !flag11;
			num = unchecked((nint)null);
			if (!flag12)
			{
				bool flag13 = (object)Button == null;
				num = unchecked((nint)null);
				active2 = (GameObject)(object)Button;
				if (flag13)
				{
					goto IL_03f5;
				}
				GameObject go = Button.gameObject;
				SelectionUtils.SetSelected(go);
				num = unchecked((nint)null);
			}
		}
		active2 = Normal;
		if ((object)Normal != null)
		{
			bool active3 = (byte)((active ? 1u : 0u) ^ 1u) != 0;
			Normal.SetActive(active3);
			active2 = Active;
			bool flag14 = (object)Active == null;
			num = unchecked((nint)null);
			if (!flag14)
			{
				Active.SetActive(active);
				bool flag15 = (object)Button == null;
				num = unchecked((nint)null);
				active2 = (GameObject)(object)Button;
				if (!flag15)
				{
					bool interactable = (byte)((active ? 1u : 0u) ^ 1u) != 0;
					Button.interactable = interactable;
					return;
				}
			}
		}
		goto IL_03f5;
		IL_03f5:
		ex = new NullReferenceException();
		goto IL_0591;
		IL_0586:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	protected void onBindingComplete()
	{
		//IL_0034: Expected I, but got O
		Action value = onBindingComplete;
		InputBinding.OnComplete -= value;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ r8_v4 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1B0]");
		Action value2 = new Action(this, (IntPtr)0);
		nint num = (nint)this;
		InputBinding.OnCanceled -= value2;
		UpdateDisplayName();
		SetActive(active: false);
		OnChangedDelegate onChanged = OnChanged;
		if (OnChanged != null)
		{
			InputBindingForInputSystem inputBinding = InputBinding;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v77.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (OnChangedEvent != null)
		{
			InputBindingForInputSystem inputBinding2 = InputBinding;
			OnChangedEvent.Invoke(inputBinding2._bindingPath);
		}
	}

	protected virtual void onBindingCanceled()
	{
		//IL_0034: Expected I, but got O
		//IL_004e: Expected I, but got O
		//IL_005e: Expected O, but got I
		//IL_006e: Expected O, but got I
		while (true)
		{
			Action value = onBindingComplete;
			InputBinding.OnComplete -= value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v92 @ r8_v4 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1B0]");
			Action value2 = new Action(this, (IntPtr)0);
			nint num = (nint)this;
			InputBinding.OnCanceled -= value2;
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ r8_v7 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+198]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ r8_v7 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1A0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v103 @ rax_v10 (should have been resolved before IL gen)");
		}
	}

	public virtual void UpdateDisplayName()
	{
		if (InputBinding != null)
		{
			bool flag = PathToDisplayNameFunc == null;
			InputBindingForInputSystem inputBinding = InputBinding;
			string text = "CompositeControlSeparator";
			if (!flag)
			{
				Func<string, string> pathToDisplayNameFunc = PathToDisplayNameFunc;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v110 @ rcx_v13 (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
				string text2 = default(string);
				text = text2;
			}
			if (string.IsNullOrEmpty(text) || text == "CompositeControlSeparator")
			{
				text = " + ";
			}
			Func<string, string> localizeFunc = localizeString;
			string displayName = InputUtils.BindingPathToDisplayName(inputBinding._bindingPath, localizeFunc, text);
			DisplayName = displayName;
		}
	}

	private string localizeString(string path)
	{
		if (PathToDisplayNameFunc == null)
		{
			return path;
		}
		Func<string, string> pathToDisplayNameFunc = PathToDisplayNameFunc;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v14 @ rcx_v1 (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
		string result = default(string);
		return result;
	}

	public virtual bool IsCancelKeyPressed()
	{
		return InputUtils.CancelDown();
	}

	public void OnEnable()
	{
		Refresh();
	}

	public void OnDisable()
	{
		InputBindingForInputSystem inputBinding = InputBinding;
		if (inputBinding._rebindingOperation != null)
		{
			inputBinding._rebindingOperation.Cancel();
			if (inputBinding._rebindingOperation != null)
			{
				inputBinding._rebindingOperation.Dispose();
				inputBinding._rebindingOperation = null;
			}
		}
		if (Active.activeSelf)
		{
			SetActive(active: false);
		}
	}

	public virtual void Refresh()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputBindingUGUI>)+1C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
