using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class OptionsButtonUGUI : MonoBehaviour
{
	public delegate void OnValueChangedDelegate(int optionIndex);

	public static string UndefinedText = "-";

	public TextMeshProUGUI TextTf;

	public bool Loop = true;

	protected bool _enableButtonControls;

	protected AutoNavigationOverrides _autoNavigationOverrides;

	protected Selectable _selectable;

	public Func<string, string> OptionToTextFunc;

	public UnityEvent<int> OnValueChangedEvent;

	public OnValueChangedDelegate OnValueChanged;

	protected List<string> _options;

	protected List<string> _getOptionsCache;

	protected int _value;

	public bool EnableButtonControls
	{
		get
		{
			return _enableButtonControls;
		}
		set
		{
			AutoNavigationOverrides autoNavigationOverrides = AutoNavigationOverrides;
			if (autoNavigationOverrides != null)
			{
				AutoNavigationOverrides autoNavigationOverrides2 = AutoNavigationOverrides;
				autoNavigationOverrides2.BlockLeft = _enableButtonControls;
				AutoNavigationOverrides autoNavigationOverrides3 = AutoNavigationOverrides;
				autoNavigationOverrides3.BlockRight = _enableButtonControls;
			}
		}
	}

	public AutoNavigationOverrides AutoNavigationOverrides
	{
		get
		{
			if (_autoNavigationOverrides == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				AutoNavigationOverrides autoNavigationOverrides = default(AutoNavigationOverrides);
				_autoNavigationOverrides = autoNavigationOverrides;
			}
			return _autoNavigationOverrides;
		}
	}

	public Selectable Selectable
	{
		get
		{
			if (_selectable == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Selectable selectable = default(Selectable);
				_selectable = selectable;
			}
			return _selectable;
		}
	}

	public unsafe int SelectedIndex
	{
		get
		{
			return _value;
		}
		set
		{
			if (value == _value)
			{
				return;
			}
			if (_options != null)
			{
				List<string> options = _options;
				if (options._size != 0)
				{
					int num = (_value = value % options._size);
					if (num < 0)
					{
						int value2 = options._size + num;
						_value = value2;
					}
					UpdateText();
					if (OnValueChangedEvent != null)
					{
						object obj = default(object);
						OnValueChangedEvent.Invoke((int)(&obj));
					}
					OnValueChangedDelegate onValueChanged = OnValueChanged;
					if (OnValueChanged == null)
					{
						return;
					}
					IntPtr invoke_impl = ((Delegate)onValueChanged).invoke_impl;
					IntPtr method = ((Delegate)onValueChanged).method;
					IntPtr method_code = ((Delegate)onValueChanged).method_code;
					int value3 = _value;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v124 @ rax_v9 (System.IntPtr) (should have been resolved before IL gen)");
				}
			}
			_value = 0;
		}
	}

	public int NumOfOptions
	{
		get
		{
			//IL_001d: Expected I4, but got O
			List<string> options = _options;
			if (_options != null)
			{
				return options._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public void Start()
	{
		AutoNavigationOverrides autoNavigationOverrides = AutoNavigationOverrides;
		if (autoNavigationOverrides != null)
		{
			AutoNavigationOverrides autoNavigationOverrides2 = AutoNavigationOverrides;
			autoNavigationOverrides2.BlockLeft = _enableButtonControls;
			AutoNavigationOverrides autoNavigationOverrides3 = AutoNavigationOverrides;
			autoNavigationOverrides3.BlockRight = _enableButtonControls;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 91 Invalid \"Jump target not found in method: 0x180A6EAB0\"");
		throw new NullReferenceException();
	}

	public virtual void Update()
	{
		//IL_0124: Expected O, but got I4
		if (!_enableButtonControls)
		{
			return;
		}
		EventSystem current = EventSystem.current;
		if (!(current != null))
		{
			return;
		}
		EventSystem current2 = EventSystem.current;
		Selectable selectable = Selectable;
		GameObject gameObject = selectable.gameObject;
		if (!(current2.m_CurrentSelected == gameObject))
		{
			return;
		}
		int selectedIndex;
		if (!InputUtils.LeftPressed())
		{
			if (!InputUtils.RightPressed())
			{
				return;
			}
			List<string> options = _options;
			if (options._size == 0)
			{
				return;
			}
			List<string> options2 = _options;
			object obj = options2._size - 1;
			if (_value == (nint)obj && !Loop)
			{
				return;
			}
			selectedIndex = _value + 1;
		}
		else
		{
			List<string> options3 = _options;
			if (options3._size == 0 || (_value == 0 && !Loop))
			{
				return;
			}
			selectedIndex = _value - 1;
		}
		SelectedIndex = selectedIndex;
	}

	public void SetOptions(IList<string> options)
	{
		List<string> options2 = _options;
		int version = options2._version + 1;
		options2._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			options2._size = 0;
		}
		else
		{
			options2._size = 0;
			if (options2._size > 0)
			{
				Array.Clear(options2._items, 0, options2._size);
			}
		}
		_options.AddRange(options);
		UpdateText();
	}

	public List<string> GetOptions()
	{
		List<string> getOptionsCache = _getOptionsCache;
		if (_getOptionsCache != null)
		{
			int version = getOptionsCache._version + 1;
			getOptionsCache._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				getOptionsCache._size = 0;
			}
			else
			{
				getOptionsCache._size = 0;
				if (getOptionsCache._size > 0)
				{
					Array.Clear(getOptionsCache._items, 0, getOptionsCache._size);
				}
			}
			if (_options != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				string item = default(string);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (_getOptionsCache == null)
						{
							break;
						}
						_getOptionsCache.Add(item);
						continue;
					}
					enumerator.Dispose();
					return _getOptionsCache;
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	public void UpdateText()
	{
		//IL_00e8: Expected I, but got O
		//IL_00f8: Expected O, but got I
		//IL_0108: Expected O, but got I
		//IL_00a4: Expected I, but got O
		//IL_00b4: Expected O, but got I
		//IL_00c4: Expected O, but got I
		List<string> options = _options;
		if (options._size == 0 || options._size >= _value)
		{
			TextTf.text = UndefinedText;
		}
		bool flag = OptionToTextFunc == null;
		TextMeshProUGUI textTf = TextTf;
		if (!flag)
		{
			Func<string, string> optionToTextFunc = OptionToTextFunc;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rsi_v4 (System.Func`2<System.String, System.String>)+28]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v67 @ rsi_v4 (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
			nint num2 = (nint)textTf;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ r8_v9 (Il2CppClass<TMPro.TextMeshProUGUI>)+558]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ r8_v9 (Il2CppClass<TMPro.TextMeshProUGUI>)+560]");
			object obj2 = 0;
			object obj4 = default(object);
			object obj3 = obj4;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			nint num3 = (nint)textTf;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r8_v5 (Il2CppClass<TMPro.TextMeshProUGUI>)+558]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r8_v5 (Il2CppClass<TMPro.TextMeshProUGUI>)+560]");
			object obj2 = 0;
			nint num = 0;
			object obj5 = default(object);
			object obj3 = obj5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v278 @ rax_v6 (should have been resolved before IL gen)");
	}

	public void ClearOptions()
	{
		List<string> options = _options;
		int version = options._version + 1;
		options._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			options._size = 0;
		}
		else
		{
			options._size = 0;
			if (options._size > 0)
			{
				Array.Clear(options._items, 0, options._size);
			}
		}
		UpdateText();
	}

	public void Prev()
	{
		List<string> options = _options;
		if (options._size != 0 && (_value != 0 || Loop))
		{
			int selectedIndex = _value - 1;
			SelectedIndex = selectedIndex;
		}
	}

	public void Next()
	{
		//IL_003f: Expected O, but got I4
		List<string> options = _options;
		if (options._size != 0)
		{
			List<string> options2 = _options;
			object obj = options2._size - 1;
			if (_value != (nint)obj || Loop)
			{
				int selectedIndex = _value + 1;
				SelectedIndex = selectedIndex;
			}
		}
	}

	public void SetSelected()
	{
		Selectable selectable = Selectable;
		if (selectable != null)
		{
			EventSystem current = EventSystem.current;
			if (current != null)
			{
				EventSystem current2 = EventSystem.current;
				Selectable selectable2 = Selectable;
				GameObject selectedGameObject = selectable2.gameObject;
				current2.SetSelectedGameObject(selectedGameObject);
			}
		}
	}

	public OptionsButtonUGUI()
	{
		List<string> options = new List<string>();
		_options = options;
		_getOptionsCache = new List<string>();
		base._002Ector();
	}
}
