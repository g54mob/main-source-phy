using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.UGUIComponentsForSettings;

public class DropDownUGUI : MonoBehaviour
{
	public delegate void OnSelectionChangedDelegate(int optionIndex);

	public UnityEvent<int> OnSelectionChangedEvent;

	public OnSelectionChangedDelegate OnSelectionChanged;

	public TMP_Dropdown DropDown;

	protected List<string> _getOptionsCache;

	public int SelectedIndex
	{
		get
		{
			//IL_0041: Expected I4, but got O
			TMP_Dropdown dropDown = DropDown;
			if ((object)DropDown != null)
			{
				return dropDown.m_Value;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			TMP_Dropdown dropDown = DropDown;
			if (dropDown.m_Value != value)
			{
				dropDown.value = value;
			}
		}
	}

	public void Start()
	{
		TMP_Dropdown dropDown = DropDown;
		UnityAction<int> call = onValueChanged;
		dropDown.m_OnValueChanged.AddListener(call);
	}

	protected unsafe void onValueChanged(int index)
	{
		if (OnSelectionChangedEvent != null)
		{
			object obj = default(object);
			OnSelectionChangedEvent.Invoke((int)(&obj));
		}
		OnSelectionChangedDelegate onSelectionChanged = OnSelectionChanged;
		if (OnSelectionChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v50.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public unsafe void SetOptions(IList<string> options)
	{
		//IL_0043: Expected I, but got O
		//IL_006d: Expected O, but got Ref
		//IL_0156: Expected O, but got I4
		//IL_00fb: Expected O, but got I
		//IL_0104: Expected O, but got I4
		//IL_0176: Expected I, but got O
		//IL_019e: Expected O, but got I
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		if (options == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj == null)
		{
			return;
		}
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		nint num = (nint)options;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v2 (Il2CppClass<System.Collections.Generic.IList`1<System.String>>)+12E]");
		nint num2 = 0;
		IEnumerator<string> enumerator = options.GetEnumerator();
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj4 = default(object);
		object obj14 = default(object);
		string text = default(string);
		while (true)
		{
			object obj13;
			object obj6;
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					bool flag = obj3 == null;
					IEnumerable<string> enumerable = null;
					if (!flag)
					{
						object obj5 = obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v7+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_013b;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v7+B0]");
						obj6 = 0;
						object obj7 = 0;
						while (true)
						{
							object obj8 = obj7 + obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ r8_v13+v433 @ rcx_v28*8]");
							if (0 == (nint)typeof(IEnumerator<string>))
							{
								break;
							}
							obj7++;
							object obj9 = obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v7+12E]");
							if ((nint)obj9 < 0)
							{
								continue;
							}
							goto IL_013b;
						}
						object obj10 = obj7 + obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ r8_v13+8+v514 @ rcx_v30*8]");
						object obj11 = (nint)0 << 4;
						object obj12 = obj11 + 312;
						obj13 = obj12 + obj5;
						goto IL_02d1;
					}
					throw new NullReferenceException();
				}
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				break;
			}
			throw new NullReferenceException();
			IL_013b:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj13 = obj14;
			obj6 = 0;
			goto IL_02d1;
			IL_02d1:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v519 @ rdx_v17] (should have been resolved before IL gen)");
			TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData(text);
			if (list != null)
			{
				list.Add(item);
				num2 = (nint)typeof(IEnumerator<string>);
				continue;
			}
			throw new NullReferenceException();
		}
		if ((object)DropDown != null)
		{
			DropDown.ClearOptions();
			if ((object)DropDown != null)
			{
				DropDown.AddOptions(list);
				return;
			}
		}
		throw new NullReferenceException();
	}

	public List<string> GetOptions()
	{
		//IL_016e: Expected O, but got I
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
			if ((object)DropDown != null)
			{
				List<TMP_Dropdown.OptionData> options = DropDown.options;
				if (options != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<TMP_Dropdown.OptionData>.Enumerator enumerator = default(List<TMP_Dropdown.OptionData>.Enumerator);
					object obj2 = default(object);
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (obj2 != null)
							{
								if (_getOptionsCache == null)
								{
									break;
								}
								List<string> getOptionsCache2 = _getOptionsCache;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_18_v4+10]");
								getOptionsCache2.Add((string)0);
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						return _getOptionsCache;
					}
					throw new NullReferenceException();
				}
			}
		}
		throw new NullReferenceException();
	}

	public void ClearOptions()
	{
		DropDown.ClearOptions();
	}

	public void AddOptions(List<Sprite> options)
	{
		while (true)
		{
		}
	}

	public void AddOptions(List<string> options)
	{
		while (true)
		{
		}
	}

	public void AddOptions(List<TMP_Dropdown.OptionData> options)
	{
		DropDown.AddOptions(options);
	}

	public DropDownUGUI()
	{
		List<string> getOptionsCache = new List<string>();
		_getOptionsCache = getOptionsCache;
		base._002Ector();
	}
}
