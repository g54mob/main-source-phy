using System;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class TextValueLoader : MonoBehaviour
{
	[Serializable]
	public class ValueSource
	{
		public GameObject sourceObject;

		public Component sourceComponent;

		public string propertyName;

		[NonSerialized]
		public PropertyInfo cachedProperty;

		[NonSerialized]
		public Type cachedComponentType;

		[NonSerialized]
		public string cachedPropertyName;
	}

	public TMP_Text targetText;

	public string format = "{0}%";

	public List<ValueSource> values;

	public bool refreshEveryFrame;

	private void OnEnable()
	{
		UpdateUI();
	}

	private void Update()
	{
		if (refreshEveryFrame)
		{
			UpdateUI();
		}
	}

	public void UpdateUI()
	{
		//IL_0047: Expected O, but got I4
		//IL_00a2: Expected O, but got I4
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00d1: Expected O, but got I4
		//IL_0147: Expected O, but got I4
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_0223: Expected O, but got I4
		//IL_017f: Expected I, but got O
		//IL_018f: Expected O, but got I
		//IL_01b7: Expected O, but got I4
		//IL_0250: Expected O, but got I4
		if (!(targetText != null))
		{
			return;
		}
		ValueSource valueSource = (ValueSource)(object)values;
		bool flag = values == null;
		object obj = 0;
		UnityEngine.Object obj2 = targetText;
		if (!flag)
		{
			valueSource = (ValueSource)(object)valueSource.sourceComponent;
			object[] array = new object[(object)valueSource.sourceComponent];
			List<ValueSource> list = values;
			bool flag2 = values == null;
			obj = 0;
			obj2 = null;
			if (!flag2)
			{
				object obj3 = array + 32;
				UnityEngine.Object obj4 = null;
				obj = 0;
				UnityEngine.Object obj5 = null;
				ValueSource valueSource2 = default(ValueSource);
				object obj6 = default(object);
				object obj7 = default(object);
				while (true)
				{
					if ((nint)obj5 < list._size)
					{
						obj2 = (UnityEngine.Object)(object)values;
						if (values == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						object value = GetValue(valueSource2);
						bool flag3 = array == null;
						nint num = 0;
						valueSource = valueSource2;
						obj = 0;
						obj2 = this;
						if (flag3)
						{
							break;
						}
						if (value != null)
						{
							nint num2 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rdx_v17 (Il2CppClass<System.Object[]>)+40]");
							valueSource = (ValueSource)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag4 = obj6 == null;
							num = 0;
							obj = 0;
							obj2 = (UnityEngine.Object)value;
							if (flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj7;
							}
						}
						obj3 = value;
						obj4 = (UnityEngine.Object)(obj4 + 1);
						obj3 += 8;
						list = values;
						bool flag5 = values == null;
						num = 0;
						valueSource = (ValueSource)value;
						obj = 0;
						obj2 = obj4;
						if (flag5)
						{
							break;
						}
						num = 0;
						valueSource = (ValueSource)value;
						obj = 0;
						obj5 = obj4;
						continue;
					}
					string text = string.Format(format, array);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	private object GetValue(ValueSource source)
	{
		//IL_0266: Expected O, but got I
		//IL_0276: Expected O, but got I
		//IL_0239: Expected O, but got I
		//IL_0249: Expected O, but got I
		if (source == null || !(source.sourceComponent != null) || string.IsNullOrEmpty(source.propertyName))
		{
			goto IL_0256;
		}
		if ((object)source.sourceComponent != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
				object obj2 = default(object);
				if (obj2 == null && !(source.cachedPropertyName != source.propertyName))
				{
					goto IL_0177;
				}
			}
			Type type = default(Type);
			source.cachedComponentType = type;
			source.cachedPropertyName = source.propertyName;
			if ((object)type != null)
			{
				PropertyInfo property = type.GetProperty(source.propertyName, (BindingFlags)20);
				source.cachedProperty = property;
				goto IL_0177;
			}
		}
		goto IL_0298;
		IL_0298:
		return new NullReferenceException();
		IL_0177:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
		object obj3 = default(object);
		if (obj3 == null)
		{
			if ((object)source.cachedProperty == null)
			{
				goto IL_0298;
			}
			if (source.cachedProperty.CanRead)
			{
				object obj4 = source.cachedProperty.GetValue(source.sourceComponent);
				if (obj4 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v348 @ rax_v20+B8]");
					object obj6 = 0;
					obj4 = obj6;
				}
				return obj4;
			}
		}
		goto IL_0256;
		IL_0256:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rax_v3+B8]");
		return 0;
	}

	public TextValueLoader()
	{
		List<ValueSource> list = new List<ValueSource>();
		values = list;
		base._002Ector();
	}
}
