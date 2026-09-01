using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator.Examples;

public class SettingsUIToolkitDemo : MonoBehaviour
{
	private sealed class _003CwaitForUIDocumentToLoad_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private UIDocument _003Cdocument_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CwaitForUIDocumentToLoad_003Ed__3(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0096: Expected I4, but got I8
			//IL_0221: Expected I4, but got O
			//IL_0177: Expected O, but got Ref
			//IL_01bb: Expected O, but got Ref
			//IL_0204: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_020d;
				}
				_003C_003E1__state = -1;
				if (_003Cdocument_003E5__2 != null)
				{
					if ((object)_003Cdocument_003E5__2 != null)
					{
						VisualElement rootVisualElement = _003Cdocument_003E5__2.rootVisualElement;
						if (rootVisualElement == null)
						{
							goto IL_0019;
						}
						if (!(_003Cdocument_003E5__2 != null))
						{
							goto IL_020d;
						}
						if ((object)_003Cdocument_003E5__2 != null)
						{
							string rootVisualElement2 = (string)(object)_003Cdocument_003E5__2.rootVisualElement;
							object obj = default(object);
							UQueryBuilder<VisualElement> uQueryBuilder = UQueryExtensions.Query((VisualElement)(&obj), rootVisualElement2, "SettingsResetButton");
							if ((object)_003Cdocument_003E5__2 != null)
							{
								string rootVisualElement3 = (string)(object)_003Cdocument_003E5__2.rootVisualElement;
								UQueryBuilder<VisualElement> uQueryBuilder2 = UQueryExtensions.Query((VisualElement)(&obj), rootVisualElement3, "SettingsApplyButton");
								if ((object)_003Cdocument_003E5__2 != null)
								{
									string rootVisualElement4 = (string)(object)_003Cdocument_003E5__2.rootVisualElement;
									UQueryBuilder<VisualElement> uQueryBuilder3 = UQueryExtensions.Query((VisualElement)(&obj), rootVisualElement4, "SettingsSaveButton");
									goto IL_020d;
								}
							}
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
			}
			goto IL_0019;
			IL_0019:
			UIDocument uIDocument = UnityEngine.Object.FindFirstObjectByType<UIDocument>(FindObjectsInactive.Include);
			_003Cdocument_003E5__2 = uIDocument;
			WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
			_003C_003E2__current = waitForSeconds;
			_003C_003E1__state = 1;
			return true;
			IL_020d:
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

	public SettingsProvider SettingsProvider;

	public void Awake()
	{
		Settings settings = SettingsProvider.Settings;
	}

	public void Start()
	{
		Settings settings = SettingsProvider.Settings;
		IConnection<string> connection = default(IConnection<string>);
		SettingsProvider provider = default(SettingsProvider);
		SettingString orCreateString = settings.GetOrCreateString("playerName", "", null, connection, provider);
		Action<string> onChanged = onPlayerNameChanged;
		orCreateString.AddChangeListener(onChanged);
		_003CwaitForUIDocumentToLoad_003Ed__3 obj = new _003CwaitForUIDocumentToLoad_003Ed__3(0);
		obj._003C_003E1__state = 0;
		Coroutine coroutine = StartCoroutine(obj);
	}

	public IEnumerator waitForUIDocumentToLoad()
	{
		_003CwaitForUIDocumentToLoad_003Ed__3 obj = new _003CwaitForUIDocumentToLoad_003Ed__3(0);
		obj._003C_003E1__state = 0;
		return obj;
	}

	private void onPlayerNameChanged(string playerName)
	{
		string message = "Player name changed to: " + playerName;
		Debug.Log(message);
	}

	public void Apply()
	{
		SettingsProvider.Apply();
	}

	public void Save()
	{
		SettingsProvider.Save();
	}

	public void Reset()
	{
		SettingsProvider.Reset();
	}
}
