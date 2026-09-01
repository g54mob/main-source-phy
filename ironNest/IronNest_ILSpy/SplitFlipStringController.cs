using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public sealed class SplitFlipStringController : MonoBehaviour
{
	public enum TextAlignment
	{
		Left,
		Center,
		Right
	}

	private List<MonoBehaviour> displays;

	private bool autoCollectFromChildrenOnAwake;

	private TextAlignment alignment;

	private char blankCharacter;

	private bool alwaysReapplyEvenIfUnchanged;

	private bool treatNullAsEmpty;

	private readonly List<ISplitFlipDisplay> _displays;

	private string _lastAppliedText;

	public int Capacity
	{
		get
		{
			if (_displays != null)
			{
				List<ISplitFlipDisplay> list = _displays;
				return list._size;
			}
			return 0;
		}
	}

	public string LastAppliedText => _lastAppliedText;

	private unsafe void Awake()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0030: Expected O, but got I4
		//IL_0039: Expected O, but got I4
		//IL_0160: Expected O, but got Ref
		//IL_007d: Expected I, but got O
		//IL_008b: Expected I, but got O
		//IL_009b: Expected O, but got I
		//IL_011b: Expected O, but got I4
		//IL_00d7: Expected O, but got I
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_010d: Expected O, but got I4
		if (autoCollectFromChildrenOnAwake)
		{
			ISplitFlipDisplay[] componentsInChildren = GetComponentsInChildren<ISplitFlipDisplay>(includeInactive: true);
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			MonoBehaviour item;
			for (object obj3 = 0; (nint)obj3 < componentsInChildren.Length; displays.Add(item), obj2++, obj += 8, obj3 = obj2)
			{
				MonoBehaviour monoBehaviour = (MonoBehaviour)obj;
				if (obj == null)
				{
					item = null;
					continue;
				}
				nint num = (nint)monoBehaviour;
				nint num2 = (nint)typeof(MonoBehaviour);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rdx_v14 (Il2CppClass<UnityEngine.MonoBehaviour>)+130]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r9_v5 (Il2CppClass<UnityEngine.MonoBehaviour>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rdx_v14 (Il2CppClass<UnityEngine.MonoBehaviour>)+130]");
				object obj6;
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r9_v5 (Il2CppClass<UnityEngine.MonoBehaviour>)+C8]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v407 @ rax_v25+FFFFFFF8+v372 @ rax_v21*8]");
					if (0 == (nint)typeof(MonoBehaviour))
					{
						obj6 = 1;
						goto IL_0200;
					}
				}
				obj6 = 0;
				goto IL_0200;
				IL_0200:
				bool flag = obj6 == null;
				item = null;
				if (!flag)
				{
					item = (MonoBehaviour)obj;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MonoBehaviour>.Enumerator enumerator = default(List<MonoBehaviour>.Enumerator);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag2 = _displays == null;
				object obj7 = (object)(&enumerator);
				if (flag2)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void SetTextAndApply(string text)
	{
		//IL_0111: Expected O, but got I4
		//IL_02b9: Expected O, but got I4
		//IL_02bd: Unsupported input type for neg.
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_017b: Expected O, but got I4
		//IL_01a4: Expected I4, but got O
		//IL_02dd: Expected O, but got I4
		//IL_0232: Expected O, but got I4
		//IL_047d: Expected O, but got I4
		//IL_0291: Expected O, but got I4
		//IL_029f: Expected I4, but got O
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected I4, but got Unknown
		bool flag = text != null;
		string text2 = text;
		if (!flag)
		{
			if ((treatNullAsEmpty ? 1 : 0) == (nint)text)
			{
				return;
			}
			text2 = "";
		}
		if (!alwaysReapplyEvenIfUnchanged && !(text2 != _lastAppliedText))
		{
			return;
		}
		_lastAppliedText = text2;
		if (_displays == null)
		{
			return;
		}
		List<ISplitFlipDisplay> list = _displays;
		if (list._size == 0)
		{
			return;
		}
		bool flag2 = text2._stringLength <= list._size;
		int num = 0;
		if (!flag2)
		{
			bool flag3 = alignment == TextAlignment.Left;
			num = 0;
			if (!flag3)
			{
				object obj = alignment - 1;
				if (!flag3)
				{
					bool flag4 = (nint)obj != 1;
					num = 0;
					if (!flag4)
					{
						num = text2._stringLength - list._size;
					}
				}
				else
				{
					object obj2 = text2._stringLength - list._size;
					object obj3 = obj2 >> 31;
					object obj4 = obj2 - obj3;
					int num2 = obj4 >> 1;
					num = num2;
				}
			}
		}
		string lastAppliedText = _lastAppliedText;
		bool flag5 = lastAppliedText._stringLength >= list._size;
		int num3 = 0;
		if (!flag5)
		{
			int num4 = list._size - lastAppliedText._stringLength;
			bool flag6 = alignment == TextAlignment.Left;
			num3 = 0;
			if (!flag6)
			{
				object obj5 = alignment - 1;
				if (!flag6)
				{
					bool flag7 = (nint)obj5 != 1;
					num3 = 0;
					if (!flag7)
					{
						num3 = num4;
					}
				}
				else
				{
					int num5 = num4 >> 31;
					object obj6 = num4 - num5;
					int num6 = obj6 >> 1;
					num3 = num6;
				}
			}
		}
		if (list._size <= 0)
		{
			return;
		}
		object obj7 = num3 + num;
		object obj8 = 0 - obj7;
		int num7 = -num;
		object obj10 = default(object);
		object obj13;
		do
		{
			object obj9 = num7 + num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj10 == null)
			{
				goto IL_0462;
			}
			object obj11 = obj8 + num;
			if ((nint)obj11 >= 0)
			{
				string lastAppliedText2 = _lastAppliedText;
				object obj12 = obj8 + num;
				if ((nint)obj12 < lastAppliedText2._stringLength && lastAppliedText2._stringLength <= list._size)
				{
					int index = obj8 + num;
					char c = lastAppliedText2.get_Chars(index);
					char c2 = c;
					goto IL_04fb;
				}
			}
			string lastAppliedText3 = _lastAppliedText;
			if (lastAppliedText3._stringLength >= list._size && num >= 0 && num < lastAppliedText3._stringLength)
			{
				char c3 = lastAppliedText3.get_Chars(num);
				char c2 = c3;
			}
			else
			{
				char c2 = blankCharacter;
			}
			goto IL_04fb;
			IL_0462:
			num++;
			obj13 = num7 + num;
			continue;
			IL_04fb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180006850");
			goto IL_0462;
		}
		while ((nint)obj13 < list._size);
	}

	public void ClearAndApply()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0BC]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 25 Invalid \"Jump target not found in method: 0x18041A090\"");
	}

	public SplitFlipStringController()
	{
		List<MonoBehaviour> list = new List<MonoBehaviour>();
		displays = list;
		blankCharacter = ' ';
		treatNullAsEmpty = true;
		_displays = new List<ISplitFlipDisplay>();
		base._002Ector();
	}
}
