using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator;

public abstract class SettingResolverForVisualElement : SettingResolver, ISettingResolver
{
	private sealed class _003CRefreshDelayedAsync_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SettingResolverForVisualElement _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRefreshDelayedAsync_003Ed__13(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0031: Expected I4, but got I8
			//IL_007a: Expected I4, but got I8
			//IL_00b7: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				_003C_003E4__this.Refresh();
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public static string SettingsClassNamePrefix = "set_";

	public static string SettingsClassNameSeparator = "__";

	public string BindingClass;

	protected UIDocument _document;

	protected VisualElement _visualElement;

	public UIDocument Document
	{
		get
		{
			if (_document == null)
			{
				Transform transform = base.transform;
				if ((object)transform == null)
				{
					return (UIDocument)(object)new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UIDocument document = default(UIDocument);
				_document = document;
			}
			return _document;
		}
	}

	public VisualElement VisualElement
	{
		get
		{
			//IL_0125: Expected I, but got O
			if (_visualElement == null && !string.IsNullOrEmpty(BindingClass))
			{
				UIDocument document = Document;
				if (document != null)
				{
					UIDocument document2 = Document;
					if ((object)document2 == null)
					{
						goto IL_0163;
					}
					VisualElement rootVisualElement = document2.rootVisualElement;
					VisualElement visualElement = UQueryExtensions.Q(rootVisualElement, null, BindingClass);
					_visualElement = visualElement;
					if (_visualElement == null)
					{
						string message = "No element with binding class '" + BindingClass + "' found.";
						Logger.LogWarning(message);
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingResolverForVisualElement>)+250]");
						EventCallback<DetachFromPanelEvent> callback = new EventCallback<DetachFromPanelEvent>(this, (IntPtr)0);
						nint num = (nint)this;
						if (_visualElement == null)
						{
							goto IL_0163;
						}
						_visualElement.RegisterCallback(callback);
					}
				}
			}
			return _visualElement;
			IL_0163:
			return (VisualElement)(object)new NullReferenceException();
		}
		set
		{
			_visualElement = value;
			if (value == null)
			{
				BindingClass = (string)(object)value;
			}
		}
	}

	public static bool HasSettingClass(VisualElement element)
	{
		string settingClassName = GetSettingClassName(element);
		bool flag = settingClassName == null;
		return !flag;
	}

	public unsafe static string GetSettingClassName(VisualElement element)
	{
		//IL_003e: Expected O, but got Ref
		VisualElement visualElement = default(VisualElement);
		if (visualElement != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2F890");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj3 = default(object);
				object obj2 = (object)(&obj3);
				object obj4 = default(object);
				string text = default(string);
				while (true)
				{
					if (obj3 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj4 != null)
						{
							bool flag = obj3 == null;
							VisualElement visualElement2 = null;
							if (!flag)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								visualElement2 = null;
								if (text == null)
								{
									break;
								}
								if (text.StartsWith(SettingsClassNamePrefix))
								{
									if (obj2 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
									}
									return text;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						return null;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	protected virtual void detachFromPanel(DetachFromPanelEvent evt)
	{
		resetUIElements();
		if (this != null && base.isActiveAndEnabled)
		{
			IEnumerator routine = RefreshDelayedAsync();
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	protected virtual IEnumerator RefreshDelayedAsync()
	{
		_003CRefreshDelayedAsync_003Ed__13 obj = new _003CRefreshDelayedAsync_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public unsafe void BindTo(VisualElement element)
	{
		//IL_0026: Expected O, but got Ref
		_document = null;
		_visualElement = null;
		if (element != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2F890");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			object obj = (object)(&obj2);
			string text = null;
			object obj3 = default(object);
			string text2 = default(string);
			while (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj3 != null)
				{
					bool flag = obj2 == null;
					text = null;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						text = null;
						if (text2 != null)
						{
							if (!text2.StartsWith(SettingsClassNamePrefix))
							{
								continue;
							}
							string[] array = Regex.Split(text2, SettingsClassNameSeparator);
							if (array.Length != 0)
							{
								string settingsClassNamePrefix = SettingsClassNamePrefix;
								if (SettingsClassNamePrefix == null)
								{
									throw new NullReferenceException();
								}
								if (array[0] == null)
								{
									throw new NullReferenceException();
								}
								string iD = array[0].Substring(settingsClassNamePrefix._stringLength);
								ID = iD;
							}
							BindingClass = text2;
							if (obj != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							}
							return;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
		}
		BindingClass = null;
	}

	public void Unbind()
	{
		resetUIElements();
		BindingClass = null;
	}

	public override void OnDisable()
	{
		resetUIElements();
		StopAllCoroutines();
		base.OnDisable();
	}

	protected virtual void resetUIElements()
	{
		//IL_0020: Expected I, but got O
		_document = null;
		if (_visualElement != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingResolverForVisualElement>)+250]");
			EventCallback<DetachFromPanelEvent> callback = new EventCallback<DetachFromPanelEvent>(this, (IntPtr)0);
			nint num = (nint)this;
			_visualElement.UnregisterCallback(callback);
		}
		_visualElement = null;
	}

	public override void OnDestroy()
	{
		resetUIElements();
		BindingClass = null;
		StopAllCoroutines();
		base.OnDestroy();
	}
}
