using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator.Examples;

public class SceneLoader : MonoBehaviour
{
	private sealed class _003CLoadDelayed_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SceneLoader _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadDelayed_003Ed__5(int _003C_003E1__state)
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
			//IL_0075: Expected I4, but got I8
			//IL_00b8: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
				_003C_003E2__current = waitForSeconds;
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
				_003C_003E4__this.Load();
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

	public string SceneName;

	public bool LoadAdditively = true;

	public float Delay;

	protected SettingInt audioMusicVolumeSetting;

	private void Start()
	{
		if (!(Delay > 0.001f))
		{
			bool flag = !LoadAdditively;
			bool mode = !flag;
			SceneManager.LoadScene(SceneName, mode ? LoadSceneMode.Additive : LoadSceneMode.Single);
		}
		else
		{
			_003CLoadDelayed_003Ed__5 obj = new _003CLoadDelayed_003Ed__5(0);
			obj._003C_003E4__this = this;
			obj._003C_003E1__state = 0;
			obj.delay = Delay;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	public IEnumerator LoadDelayed(float delay)
	{
		_003CLoadDelayed_003Ed__5 obj = new _003CLoadDelayed_003Ed__5(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.delay = delay;
		return obj;
	}

	public void Load()
	{
		bool flag = !LoadAdditively;
		bool mode = !flag;
		SceneManager.LoadScene(SceneName, mode ? LoadSceneMode.Additive : LoadSceneMode.Single);
	}
}
