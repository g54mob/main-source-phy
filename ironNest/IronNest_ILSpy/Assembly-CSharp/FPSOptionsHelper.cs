using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class FPSOptionsHelper : MonoBehaviour
{
	private sealed class _003CDelayedSet_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FPSOptionsHelper _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedSet_003Ed__4(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_024e: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_00e5: Expected I4, but got I8
			//IL_0286: Expected I4, but got O
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_00d1: Expected I4, but got I8
			//IL_006e: Expected I4, but got I8
			FPSOptionsHelper fPSOptionsHelper = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 == 1)
						{
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this == null || (object)fPSOptionsHelper._resolver == null)
							{
								goto IL_0278;
							}
							fPSOptionsHelper._resolver.Refresh();
						}
						return false;
					}
					_003C_003E1__state = -1;
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this == null)
					{
						goto IL_0278;
					}
					if (!(fPSOptionsHelper._settingsProvider != null))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
						return true;
					}
				}
				if ((object)_003C_003E4__this != null && (object)fPSOptionsHelper._settingsProvider != null)
				{
					Settings settings = fPSOptionsHelper._settingsProvider.Settings;
					if ((object)settings != null)
					{
						SettingOption option = settings.GetOption(fPSOptionsHelper.id);
						int vSyncCount = QualitySettings.vSyncCount;
						if (vSyncCount > 0)
						{
							if (option == null)
							{
								goto IL_0278;
							}
							option.SetValue(0);
						}
						WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
						_003C_003E2__current = waitForEndOfFrame;
						_003C_003E1__state = 3;
						return true;
					}
				}
				goto IL_0278;
			}
			_003C_003E1__state = -1;
			WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame2;
			_003C_003E1__state = 1;
			return true;
			IL_0278:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
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

	private SettingsProvider _settingsProvider;

	private string id;

	private OptionsButtonUGUIResolver _resolver;

	public void UpdateFPS()
	{
		_003CDelayedSet_003Ed__4 obj = new _003CDelayedSet_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator DelayedSet()
	{
		_003CDelayedSet_003Ed__4 obj = new _003CDelayedSet_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
