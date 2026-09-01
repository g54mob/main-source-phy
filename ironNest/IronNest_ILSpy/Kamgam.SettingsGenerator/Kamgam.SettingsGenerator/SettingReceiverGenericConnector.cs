using System;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class SettingReceiverGenericConnector : MonoBehaviour
{
	public bool ApplyOnStart = true;

	public SettingsProvider SettingsProvider;

	public string SettingId;

	public string Path;

	protected GameObjectInspector _inspector;

	public GameObjectInspector Inspector
	{
		get
		{
			if (_inspector == null)
			{
				GameObject target = base.gameObject;
				GameObjectInspector gameObjectInspector = new GameObjectInspector(null);
				Dictionary<string, object> components = new Dictionary<string, object>();
				gameObjectInspector._components = components;
				Dictionary<string, (object, PropertyInfo)> properties = new Dictionary<string, (object, PropertyInfo)>();
				gameObjectInspector._properties = properties;
				Dictionary<string, (object, FieldInfo)> fields = new Dictionary<string, (object, FieldInfo)>();
				gameObjectInspector._fields = fields;
				Dictionary<string, (object, MethodInfo)> getMethods = new Dictionary<string, (object, MethodInfo)>();
				gameObjectInspector._getMethods = getMethods;
				Dictionary<string, (object, MethodInfo)> setMethods = new Dictionary<string, (object, MethodInfo)>();
				gameObjectInspector._setMethods = setMethods;
				gameObjectInspector.Target = target;
				_inspector = gameObjectInspector;
			}
			return _inspector;
		}
	}

	public ISetting Setting
	{
		get
		{
			if (!(SettingsProvider != null) || string.IsNullOrEmpty(SettingId))
			{
				goto IL_00c4;
			}
			if ((object)SettingsProvider != null)
			{
				Settings settingsAssetOrRuntimeCopy = SettingsProvider.GetSettingsAssetOrRuntimeCopy();
				if (!(settingsAssetOrRuntimeCopy != null))
				{
					goto IL_00c4;
				}
				if ((object)settingsAssetOrRuntimeCopy != null)
				{
					return settingsAssetOrRuntimeCopy.GetSetting(SettingId);
				}
			}
			return (ISetting)new NullReferenceException();
			IL_00c4:
			return null;
		}
	}

	public bool IsSettingCompatibleWithPath()
	{
		//IL_01e5: Expected I4, but got O
		bool result;
		if (SettingsProvider != null)
		{
			GameObjectInspector inspector = Inspector;
			bool flag = CollectionExtensions.IsNullOrEmpty(Path);
			if (inspector != null)
			{
				result = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
				if (!(SettingsProvider != null) || string.IsNullOrEmpty(Path) || string.IsNullOrEmpty(SettingId))
				{
					goto IL_01ef;
				}
				if ((object)SettingsProvider != null)
				{
					Settings settingsAssetOrRuntimeCopy = SettingsProvider.GetSettingsAssetOrRuntimeCopy();
					if (!(settingsAssetOrRuntimeCopy != null))
					{
						goto IL_01ef;
					}
					if ((object)settingsAssetOrRuntimeCopy != null)
					{
						ISetting setting = settingsAssetOrRuntimeCopy.GetSetting(SettingId);
						if (setting == null)
						{
							goto IL_01ef;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (SettingData.CompatibleTypes != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
							Type typeOfPath = inspector.GetTypeOfPath(Path);
							List<Type> list = default(List<Type>);
							if (list != null)
							{
								bool flag2 = list.Contains(typeOfPath);
								result = flag2;
								goto IL_01ef;
							}
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
		IL_01ef:
		return result;
	}

	public void Start()
	{
		if (!(SettingsProvider != null) || !SettingsProvider.HasSettings())
		{
			return;
		}
		Settings settings = SettingsProvider.Settings;
		if (!settings.HasID(SettingId))
		{
			return;
		}
		Settings settings2 = SettingsProvider.Settings;
		ISetting setting = settings2.GetSetting(SettingId);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj != null)
		{
			Action<ISetting> action = OnSettingChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			if (ApplyOnStart)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			string text = default(string);
			string message = "Trying to access inactive setting '" + text + "'.";
			Logger.LogWarning(message);
		}
	}

	public void OnDisable()
	{
		if (SettingsProvider != null && SettingsProvider.HasSettings())
		{
			Settings settings = SettingsProvider.Settings;
			if (settings.HasID(SettingId))
			{
				Settings settings2 = SettingsProvider.Settings;
				ISetting setting = settings2.GetSetting(SettingId);
				Action<ISetting> action = OnSettingChanged;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			}
		}
	}

	private void OnSettingChanged(ISetting setting)
	{
		if (IsSettingCompatibleWithPath())
		{
			GameObjectInspector inspector = Inspector;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object value = default(object);
			inspector.Set(Path, value);
		}
	}
}
