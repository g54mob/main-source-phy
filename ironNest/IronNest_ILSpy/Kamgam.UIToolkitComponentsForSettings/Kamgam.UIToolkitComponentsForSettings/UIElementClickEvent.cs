using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Kamgam.UIToolkitComponentsForSettings;

public class UIElementClickEvent : MonoBehaviour
{
	public UIElementType Type;

	public string BindingClass;

	public string BindingName;

	public bool MultipleResults;

	public UnityEvent<ClickEvent> OnClick;

	protected UIDocument _document;

	[NonSerialized]
	public List<VisualElement> Elements;

	public Predicate<VisualElement> BindingPredicate;

	public UIDocument Document
	{
		get
		{
			if (_document == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UIDocument document = default(UIDocument);
				_document = document;
			}
			return _document;
		}
	}

	public virtual void OnEnable()
	{
		//IL_000b: Expected I, but got O
		//IL_001b: Expected O, but got I
		//IL_002b: Expected O, but got I
		RefreshElements();
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1A8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1B0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v7 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public void RefreshElements()
	{
		UIDocument document = Document;
		if (!(document != null))
		{
			return;
		}
		UIDocument document2 = Document;
		VisualElement rootVisualElement = document2.rootVisualElement;
		if (rootVisualElement == null)
		{
			return;
		}
		Elements.Clear();
		Predicate<VisualElement> predicate = default(Predicate<VisualElement>);
		if (!MultipleResults)
		{
			UIDocument document3 = Document;
			bool flag = string.IsNullOrEmpty(BindingName);
			string text = null;
			if (!flag)
			{
				text = BindingName;
			}
			bool flag2 = string.IsNullOrEmpty(BindingClass);
			string className = null;
			if (!flag2)
			{
				className = BindingClass;
			}
			Type type = UIElementTypes.GetType(Type);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
			object obj = default(object);
			if (obj != null)
			{
				VisualElement visualElement = UIElementTypes.QueryType(document3, type, text, className, predicate);
				if (visualElement != null)
				{
					Elements.Add(visualElement);
				}
			}
		}
		else
		{
			UIDocument document4 = Document;
			bool flag3 = string.IsNullOrEmpty(BindingName);
			string text2 = null;
			if (!flag3)
			{
				text2 = BindingName;
			}
			bool flag4 = string.IsNullOrEmpty(BindingClass);
			string className2 = null;
			if (!flag4)
			{
				className2 = BindingClass;
			}
			Type type2 = UIElementTypes.GetType(Type);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
			object obj2 = default(object);
			if (obj2 != null)
			{
				Predicate<VisualElement> predicate2 = default(Predicate<VisualElement>);
				List<VisualElement> list = UIElementTypes.QueryTypes(document4, type2, text2, className2, (List<VisualElement>)(object)predicate, predicate2);
			}
		}
	}

	public virtual void OnDisable()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public virtual void OnDestroy()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public virtual void RegisterEvents()
	{
		//IL_007c: Expected I, but got O
		List<VisualElement> elements = Elements;
		if (elements._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<VisualElement>.Enumerator enumerator = default(List<VisualElement>.Enumerator);
		CallbackEventHandler callbackEventHandler = default(CallbackEventHandler);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (OnClick != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r8_v6 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1D0]");
					EventCallback<ClickEvent> callback = new EventCallback<ClickEvent>(this, (IntPtr)0);
					nint num = (nint)this;
					if (callbackEventHandler == null)
					{
						break;
					}
					callbackEventHandler.RegisterCallback(callback);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public virtual void UnregisterEvents()
	{
		//IL_007c: Expected I, but got O
		List<VisualElement> elements = Elements;
		if (elements._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<VisualElement>.Enumerator enumerator = default(List<VisualElement>.Enumerator);
		CallbackEventHandler callbackEventHandler = default(CallbackEventHandler);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (OnClick != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r8_v6 (Il2CppClass<Kamgam.UIToolkitComponentsForSettings.UIElementClickEvent>)+1D0]");
					EventCallback<ClickEvent> callback = new EventCallback<ClickEvent>(this, (IntPtr)0);
					nint num = (nint)this;
					if (callbackEventHandler == null)
					{
						break;
					}
					callbackEventHandler.UnregisterCallback(callback);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	protected virtual void onClick(ClickEvent evt)
	{
		if (OnClick != null)
		{
			OnClick.Invoke(evt);
		}
	}

	public UIElementClickEvent()
	{
		List<VisualElement> elements = new List<VisualElement>();
		Elements = elements;
		base._002Ector();
	}
}
