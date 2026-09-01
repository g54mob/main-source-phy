using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine.UIElements;

namespace Kamgam.UIToolkitComponentsForSettings;

public static class UIElementTypes
{
	public static VisualElement QueryType(UIDocument document, UIElementType type, string name = null, string className = null, Predicate<VisualElement> predicate = null)
	{
		Type type2 = GetType(type);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
		object obj = default(object);
		if (obj == null)
		{
			return null;
		}
		Predicate<VisualElement> predicate2 = default(Predicate<VisualElement>);
		return QueryType(document, type2, name, className, predicate2);
	}

	public unsafe static VisualElement QueryType(UIDocument document, Type type, string name = null, string className = null, Predicate<VisualElement> predicate = null)
	{
		//IL_00cb: Expected O, but got Ref
		UQueryState<VisualElement>.Enumerator enumerator = default(UQueryState<VisualElement>.Enumerator);
		VisualElement visualElement;
		if (document != null)
		{
			if ((object)document == null)
			{
				return (VisualElement)(object)new NullReferenceException();
			}
			VisualElement rootVisualElement = document.rootVisualElement;
			if (rootVisualElement != null)
			{
				VisualElement rootVisualElement2 = document.rootVisualElement;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18076FAC0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093E1B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093FA60");
				object obj = default(object);
				object obj2 = default(object);
				object obj3 = default(object);
				while (enumerator.MoveNext())
				{
					visualElement = enumerator.Current;
					bool flag = obj == null;
					UQueryState<VisualElement>.Enumerator enumerator2 = (UQueryState<VisualElement>.Enumerator)(&enumerator);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v370 @ stack_28+18] (should have been resolved before IL gen)");
						if (obj2 == null)
						{
							continue;
						}
					}
					if (visualElement != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABB0");
						if (obj3 == null)
						{
							continue;
						}
						goto IL_0153;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
			}
		}
		visualElement = null;
		goto IL_01c8;
		IL_01c8:
		return visualElement;
		IL_0153:
		enumerator.Dispose();
		goto IL_01c8;
	}

	public static List<VisualElement> QueryTypes(UIDocument document, UIElementType type, string name = null, string className = null, List<VisualElement> list = null, Predicate<VisualElement> predicate = null)
	{
		Type type2 = GetType(type);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
		object obj = default(object);
		List<VisualElement> result = default(List<VisualElement>);
		if (obj == null)
		{
			return result;
		}
		List<VisualElement> list2 = default(List<VisualElement>);
		Predicate<VisualElement> predicate2 = default(Predicate<VisualElement>);
		return QueryTypes(document, type2, name, className, list2, predicate2);
	}

	public unsafe static List<VisualElement> QueryTypes(UIDocument document, Type type, string name = null, string className = null, List<VisualElement> list = null, Predicate<VisualElement> predicate = null)
	{
		//IL_0061: Expected O, but got I
		//IL_01bb: Expected O, but got Ref
		//IL_01d9: Expected O, but got I
		//IL_0202: Expected O, but got I
		List<VisualElement> list2 = default(List<VisualElement>);
		bool flag = list2 != null;
		List<VisualElement> list3 = list2;
		if (!flag)
		{
			List<VisualElement> list4 = new List<VisualElement>();
			bool flag2 = list4 == null;
			list3 = list4;
			if (flag2)
			{
				goto IL_02b3;
			}
		}
		int version = list3._version + 1;
		list3._version = version;
		((List<VisualElement>)0)._002Ector();
		List<VisualElement> list5 = default(List<VisualElement>);
		if (list5 == null)
		{
			list3._size = 0;
		}
		else
		{
			list3._size = 0;
			if (list3._size > 0)
			{
				Array.Clear(list3._items, 0, list3._size);
			}
		}
		if (document != null)
		{
			if ((object)document == null)
			{
				goto IL_02b3;
			}
			VisualElement rootVisualElement = document.rootVisualElement;
			if (rootVisualElement != null)
			{
				VisualElement rootVisualElement2 = document.rootVisualElement;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18076FAC0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093E1B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093FA60");
				UQueryState<VisualElement>.Enumerator enumerator = default(UQueryState<VisualElement>.Enumerator);
				object obj = default(object);
				string text2 = default(string);
				object obj2 = default(object);
				List<VisualElement> list7 = default(List<VisualElement>);
				while (enumerator.MoveNext())
				{
					VisualElement current = enumerator.Current;
					bool flag3 = obj == null;
					string text = text2;
					List<VisualElement> list6 = (List<VisualElement>)(&enumerator);
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v509 @ stack_30+18]");
						text = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v509 @ stack_30+18] (should have been resolved before IL gen)");
						bool flag4 = obj2 == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v509 @ stack_30+18]");
						text2 = (string)0;
						if (flag4)
						{
							continue;
						}
					}
					if (current != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						bool flag5 = list7.Equals(type);
						bool flag6 = !flag5;
						text2 = text;
						if (!flag6)
						{
							if (list3 == null)
							{
								throw new NullReferenceException();
							}
							list3.Add(current);
							text2 = text;
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
			}
		}
		return list3;
		IL_02b3:
		throw new NullReferenceException();
	}

	public static Type GetType(UIElementType type)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 52 Invalid \"Jump target not found in method: 0x180A77572\"");
		Type result = default(Type);
		return result;
	}
}
