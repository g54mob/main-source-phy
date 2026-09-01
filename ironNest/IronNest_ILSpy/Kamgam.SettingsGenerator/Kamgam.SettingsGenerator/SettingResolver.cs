using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator;

public abstract class SettingResolver : MonoBehaviour, ISettingResolver
{
	protected SettingsProvider _settingsProvider;

	public LocalizationProvider LocalizationProvider;

	public string ID;

	private static bool _isQuitting;

	public SettingsProvider SettingsProvider
	{
		get
		{
			if (!(_settingsProvider == null))
			{
				return _settingsProvider;
			}
			return SettingsGeneratorSettings.GetProvider();
		}
		set
		{
			_settingsProvider = value;
		}
	}

	public SettingsProvider GetProviderAsset()
	{
		return _settingsProvider;
	}

	public SettingsProvider SetProviderAsset(SettingsProvider provider)
	{
		_settingsProvider = provider;
		return provider;
	}

	public SettingsProvider GetProvider()
	{
		return SettingsProvider;
	}

	public SettingsProvider SetProvider(SettingsProvider provider)
	{
		_settingsProvider = provider;
		return provider;
	}

	public string GetID()
	{
		return ID;
	}

	public abstract SettingData.DataType[] GetSupportedDataTypes();

	public virtual void Start()
	{
		SettingsProvider settingsProvider = SettingsProvider;
		if (settingsProvider != null)
		{
			SettingsProvider settingsProvider2 = SettingsProvider;
			Settings settings = settingsProvider2.Settings;
			if (settings != null)
			{
				SettingsProvider settingsProvider3 = SettingsProvider;
				Settings settings2 = settingsProvider3.Settings;
				settings2.RegisterResolver(this);
			}
		}
		_isQuitting = false;
	}

	public virtual void OnEnable()
	{
		Refresh();
		SettingsProvider settingsProvider = SettingsProvider;
		if (settingsProvider != null)
		{
			SettingsProvider settingsProvider2 = SettingsProvider;
			if (settingsProvider2.HasSettings())
			{
				SettingsProvider settingsProvider3 = SettingsProvider;
				Settings settings = settingsProvider3.Settings;
				int activeResolverCount = settings.ActiveResolverCount + 1;
				settings.ActiveResolverCount = activeResolverCount;
			}
		}
	}

	private void OnApplicationQuit()
	{
		_isQuitting = true;
	}

	public virtual void OnDisable()
	{
		SettingsProvider settingsProvider = SettingsProvider;
		if (!(settingsProvider != null))
		{
			return;
		}
		SettingsProvider settingsProvider2 = SettingsProvider;
		if (settingsProvider2.HasSettings())
		{
			SettingsProvider settingsProvider3 = SettingsProvider;
			Settings settings = settingsProvider3.Settings;
			int activeResolverCount = settings.ActiveResolverCount - 1;
			settings.ActiveResolverCount = activeResolverCount;
			SettingsProvider settingsProvider4 = SettingsProvider;
			Settings settings2 = settingsProvider4.Settings;
			if (settings2.ActiveResolverCount <= 0)
			{
				SettingsProvider settingsProvider5 = SettingsProvider;
				settingsProvider5.OnAllResolversDeactivated(_isQuitting);
			}
		}
	}

	public virtual void OnDestroy()
	{
		//IL_00f8: Expected I, but got O
		SettingsProvider settingsProvider = SettingsProvider;
		if (!(settingsProvider != null))
		{
			return;
		}
		SettingsProvider settingsProvider2 = SettingsProvider;
		if (settingsProvider2.HasSettings())
		{
			SettingsProvider settingsProvider3 = SettingsProvider;
			Settings settings = settingsProvider3.Settings;
			settings.UnregisterResolver(this);
			SettingsProvider settingsProvider4 = SettingsProvider;
			Settings settings2 = settingsProvider4.Settings;
			ISetting setting = settings2.GetSetting(ID);
			if (setting != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r8_v7 (Il2CppClass<Kamgam.SettingsGenerator.SettingResolver>)+240]");
				Action action = new Action(this, (IntPtr)0);
				nint num = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			}
		}
	}

	public bool HasValidSettingForID(string id, SettingData.DataType[] allowedTypes)
	{
		//IL_044b: Expected I4, but got O
		SettingsProvider settingsProvider = SettingsProvider;
		bool flag;
		if (settingsProvider != null)
		{
			SettingsProvider settingsProvider2 = SettingsProvider;
			Settings settings = settingsProvider2.Settings;
			if (settings != null)
			{
				if (string.IsNullOrEmpty(id))
				{
					goto IL_0420;
				}
				SettingsProvider settingsProvider3 = SettingsProvider;
				Settings settings2 = settingsProvider3.Settings;
				flag = settings2.HasID(id);
				if (flag)
				{
					goto IL_020a;
				}
				string[] array = new string[5];
				if (array.Length > 0)
				{
					array[0] = "SGSettingResolver: No setting with ID '";
					if (array.Length > 1)
					{
						array[1] = id;
						if (array.Length > 2)
						{
							array[2] = "' found in '";
							string text = base.name;
							if (array.Length > 3)
							{
								array[3] = text;
								if (array.Length > 4)
								{
									array[4] = "'. This setting will NOT be saved!";
									string message = string.Concat(array);
									Logger.LogWarning(message, this);
									goto IL_020a;
								}
							}
						}
					}
				}
				goto IL_043d;
			}
		}
		string[] array2 = new string[5];
		if (array2.Length > 0)
		{
			array2[0] = "SGSettingResolver: Settings or SettingsProvider is NULL (on Object: '";
			GameObject gameObject = base.gameObject;
			string text2 = gameObject.name;
			if (array2.Length > 1)
			{
				array2[1] = text2;
				if (array2.Length > 2)
				{
					array2[2] = "', ID: '";
					if (array2.Length > 3)
					{
						array2[3] = id;
						if (array2.Length > 4)
						{
							array2[4] = "').";
							string message2 = string.Concat(array2);
							Logger.LogError(message2, this);
							goto IL_0420;
						}
					}
				}
			}
		}
		goto IL_043d;
		IL_0420:
		flag = false;
		goto IL_044b;
		IL_043d:
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
		IL_044b:
		return flag;
		IL_020a:
		if (allowedTypes != null && allowedTypes.Length != 0)
		{
			SettingsProvider settingsProvider4 = SettingsProvider;
			Settings settings3 = settingsProvider4.Settings;
			ISetting setting = settings3.GetSetting(id);
			if (setting != null && !setting.MatchesAnyDataType(allowedTypes))
			{
				goto IL_0420;
			}
		}
		goto IL_044b;
	}

	public bool HasSettingForID(string id)
	{
		//IL_0075: Expected I4, but got O
		SettingsProvider settingsProvider = SettingsProvider;
		if ((object)settingsProvider != null)
		{
			Settings settings = settingsProvider.Settings;
			if ((object)settings != null)
			{
				return settings.HasID(id);
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public bool HasActiveSettingForID(string id)
	{
		//IL_0075: Expected I4, but got O
		SettingsProvider settingsProvider = SettingsProvider;
		if ((object)settingsProvider != null)
		{
			Settings settings = settingsProvider.Settings;
			if ((object)settings != null)
			{
				return settings.HasActiveID(id);
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public SettingData.DataType GetDataType()
	{
		//IL_011a: Expected I4, but got O
		SettingsProvider settingsProvider = SettingsProvider;
		if (!(settingsProvider != null))
		{
			goto IL_0106;
		}
		SettingsProvider settingsProvider2 = SettingsProvider;
		if ((object)settingsProvider2 != null)
		{
			Settings settings = settingsProvider2.Settings;
			if (!(settings != null))
			{
				goto IL_0106;
			}
			SettingsProvider settingsProvider3 = SettingsProvider;
			if ((object)settingsProvider3 != null)
			{
				Settings settings2 = settingsProvider3.Settings;
				if ((object)settings2 != null)
				{
					ISetting setting = settings2.GetSetting(ID);
					if (setting != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						SettingData.DataType result = default(SettingData.DataType);
						return result;
					}
					goto IL_0106;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (SettingData.DataType)ex;
		IL_0106:
		return SettingData.DataType.Unknown;
	}

	public void RegisterAsActivated()
	{
		SettingsProvider settingsProvider = SettingsProvider;
		if (settingsProvider != null)
		{
			SettingsProvider settingsProvider2 = SettingsProvider;
			Settings settings = settingsProvider2.Settings;
			if (settings != null)
			{
				SettingsProvider settingsProvider3 = SettingsProvider;
				Settings settings2 = settingsProvider3.Settings;
				settings2.RegisterResolver(this);
			}
		}
	}

	public void Unregister()
	{
		SettingsProvider settingsProvider = SettingsProvider;
		if (settingsProvider != null)
		{
			SettingsProvider settingsProvider2 = SettingsProvider;
			Settings settings = settingsProvider2.Settings;
			if (settings != null)
			{
				SettingsProvider settingsProvider3 = SettingsProvider;
				Settings settings2 = settingsProvider3.Settings;
				settings2.UnregisterResolver(this);
			}
		}
	}

	public abstract void Refresh();

	public static List<ISettingResolver> FindResolversInLoadedScenes(bool includeInactive = true)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0077: Expected O, but got I4
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00fb: Expected O, but got I4
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		List<ISettingResolver> list = new List<ISettingResolver>();
		int num = 0;
		bool includeInactive2 = includeInactive;
		Scene scene = default(Scene);
		while (true)
		{
			int sceneCount = SceneManager.sceneCount;
			if (num < sceneCount)
			{
				Scene sceneAt = SceneManager.GetSceneAt(num);
				GameObject[] rootGameObjects = scene.GetRootGameObjects();
				object obj = rootGameObjects + 32;
				bool flag = rootGameObjects == null;
				object obj2 = 0;
				if (flag)
				{
					break;
				}
				while ((nint)obj2 < rootGameObjects.Length)
				{
					if (obj == null)
					{
						goto end_IL_0005;
					}
					ISettingResolver[] componentsInChildren = ((GameObject)obj).GetComponentsInChildren<ISettingResolver>(includeInactive2);
					object obj3 = componentsInChildren + 32;
					bool flag2 = componentsInChildren == null;
					object obj4 = 0;
					if (flag2)
					{
						goto end_IL_0005;
					}
					while ((nint)obj4 < componentsInChildren.Length)
					{
						if (obj3 != null)
						{
							if (list == null)
							{
								goto end_IL_0005;
							}
							list.Add((ISettingResolver)obj3);
						}
						obj4++;
						obj3 += 8;
					}
					obj2++;
					obj += 8;
					includeInactive2 = includeInactive;
				}
				num++;
				continue;
			}
			return list;
			continue;
			end_IL_0005:
			break;
		}
		return (List<ISettingResolver>)(object)new NullReferenceException();
	}
}
