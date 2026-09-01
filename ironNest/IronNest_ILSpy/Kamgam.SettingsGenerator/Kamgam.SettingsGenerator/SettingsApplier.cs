using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator;

public class SettingsApplier : MonoBehaviour
{
	private sealed class _003CStart_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SettingsApplier _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStart_003Ed__9(int _003C_003E1__state)
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
			//IL_007d: Expected I4, but got I8
			SettingsApplier settingsApplier = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(settingsApplier.ApplyOnStartDelay);
				_003C_003E2__current = waitForSecondsRealtime;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				bool flag = settingsApplier.Provider == null;
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002570");
					Debug.LogError("You have not set the Provider on you SettingsApplier. Please set a provider!", settingsApplier);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Exception ex = new Exception("Missing Provider on Settings Initializer.");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				if (settingsApplier.ApplyOnStart != flag)
				{
					settingsApplier.Apply();
				}
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

	public static List<SettingsApplier> Appliers;

	public SettingsProvider Provider;

	public bool ApplyOnStart = true;

	public float ApplyOnStartDelay;

	public bool ApplyOnLateUpdate;

	public List<string> SettingIds;

	public void OnEnable()
	{
		if (!Appliers.Contains(this))
		{
			Appliers.Add(this);
		}
	}

	public static SettingsApplier GetApplier(Scene? scene = null)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingsApplier>.Enumerator enumerator = default(List<SettingsApplier>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		object obj2 = default(object);
		Scene scene3 = default(Scene);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj != null)
				{
					GameObject gameObject = ((Component)obj).gameObject;
					if ((object)gameObject != null)
					{
						if (!gameObject.activeInHierarchy || !((Behaviour)obj).isActiveAndEnabled)
						{
							continue;
						}
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
						if (obj2 != null)
						{
							GameObject gameObject2 = ((Component)obj).gameObject;
							if ((object)gameObject2 == null)
							{
								break;
							}
							Scene scene2 = gameObject2.scene;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
							if (!(scene2 == scene3))
							{
								continue;
							}
						}
						enumerator.Dispose();
						return (SettingsApplier)obj;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	public static SettingsApplier CreateApplier(SettingsProvider provider, Scene? scene = null)
	{
		Scene activeScene = SceneManager.GetActiveScene();
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj = default(object);
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			Scene activeScene2 = default(Scene);
			bool flag = SceneManager.SetActiveScene(activeScene2);
		}
		GameObject gameObject = new GameObject();
		if ((object)gameObject != null)
		{
			gameObject.name = "Kamgam.SettingsGenerator.SettingsApplier";
			gameObject.hideFlags = (HideFlags)12;
			gameObject.SetActive(value: false);
			SettingsApplier settingsApplier = gameObject.AddComponent<SettingsApplier>();
			if ((object)settingsApplier != null)
			{
				settingsApplier.Provider = provider;
				if ((object)provider != null)
				{
					settingsApplier.SettingIds = provider.ApplyOnSceneLoadIds;
					settingsApplier.ApplyOnStartDelay = provider.ApplyOnSceneLoadDelay;
					settingsApplier.ApplyOnLateUpdate = provider.ApplyOnSceneLoadInLateUpdate;
					gameObject.SetActive(value: true);
					bool flag2 = SceneManager.SetActiveScene(activeScene);
					return settingsApplier;
				}
			}
		}
		return (SettingsApplier)(object)new NullReferenceException();
	}

	public IEnumerator Start()
	{
		_003CStart_003Ed__9 obj = new _003CStart_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void LateUpdate()
	{
		if (ApplyOnLateUpdate)
		{
			Apply();
		}
	}

	public void Apply()
	{
		if (SettingIds != null)
		{
			List<string> settingIds = SettingIds;
			if (settingIds._size != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				string id = default(string);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if ((object)Provider != null)
						{
							Settings settings = Provider.Settings;
							if ((object)settings == null)
							{
								break;
							}
							ISetting setting = settings.GetSetting(id);
							if (setting != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							}
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					return;
				}
				throw new NullReferenceException();
			}
		}
		Settings settings2 = Provider.Settings;
		settings2.Apply(changedOnly: false);
	}

	public void OnDisable()
	{
		bool flag = Appliers.Remove(this);
	}

	public SettingsApplier()
	{
		List<string> settingIds = new List<string>();
		SettingIds = settingIds;
		base._002Ector();
	}

	static SettingsApplier()
	{
		List<SettingsApplier> appliers = new List<SettingsApplier>();
		Appliers = appliers;
	}
}
