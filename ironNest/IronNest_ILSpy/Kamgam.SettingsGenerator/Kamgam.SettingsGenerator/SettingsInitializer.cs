using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingsInitializer : MonoBehaviour
{
	private sealed class _003ConInstanceReloaded_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SettingsInitializer _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003ConInstanceReloaded_003Ed__15(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_004a: Expected I4, but got I8
			//IL_00b7: Expected I4, but got O
			SettingsInitializer settingsInitializer = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = _waitForEndOfFrame;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null || (object)settingsInitializer.Provider == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				settingsInitializer.Provider.Apply(changedOnly: false);
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

	private static SettingsInitializer _instance;

	public SettingsProvider Provider;

	public bool DoNotDestroy = true;

	public bool ApplyOnReload;

	public UnityEvent PreInitializationEvents;

	private static WaitForEndOfFrame _waitForEndOfFrame;

	public static SettingsInitializer Instance => _instance;

	public static bool Exists => _instance != null;

	public static Settings Settings
	{
		get
		{
			SettingsInitializer instance = _instance;
			if ((object)_instance != null && (object)instance.Provider != null)
			{
				return instance.Provider.Settings;
			}
			return null;
		}
	}

	public static bool HasSettings()
	{
		//IL_00b5: Expected I4, but got O
		if (_instance != null)
		{
			SettingsInitializer instance = _instance;
			if ((object)_instance != null)
			{
				if (!(instance.Provider != null))
				{
					goto IL_00a1;
				}
				SettingsInitializer instance2 = _instance;
				if ((object)_instance != null && (object)instance2.Provider != null)
				{
					return instance2.Provider.HasSettings();
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_00a1;
		IL_00a1:
		return false;
	}

	public void Awake()
	{
		if (DoNotDestroy)
		{
			if (_instance != null)
			{
				GameObject obj = base.gameObject;
				UnityEngine.Object.Destroy(obj);
				if (ApplyOnReload)
				{
					_003ConInstanceReloaded_003Ed__15 obj2 = new _003ConInstanceReloaded_003Ed__15(0);
					obj2._003C_003E1__state = 0;
					obj2._003C_003E4__this = this;
					Coroutine coroutine = _instance.StartCoroutine(obj2);
				}
				return;
			}
			GameObject target = base.gameObject;
			UnityEngine.Object.DontDestroyOnLoad(target);
		}
		_instance = this;
		if (PreInitializationEvents != null)
		{
			PreInitializationEvents.Invoke();
		}
	}

	public void Start()
	{
		if (!(Provider == null))
		{
			Settings settings = Provider.Settings;
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002570");
		Debug.LogError("You have not set the Provider on you SettingsInitializer. Please set a provider!", this);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Missing Provider on Settings Initializer.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	private IEnumerator onInstanceReloaded()
	{
		_003ConInstanceReloaded_003Ed__15 obj = new _003ConInstanceReloaded_003Ed__15(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	static SettingsInitializer()
	{
		WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		_waitForEndOfFrame = waitForEndOfFrame;
	}
}
