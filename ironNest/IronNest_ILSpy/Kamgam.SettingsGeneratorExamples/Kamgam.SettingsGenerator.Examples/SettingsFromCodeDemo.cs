using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class SettingsFromCodeDemo : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass1_0
	{
		public List<string> difficulties;

		public SettingColorOption opponentColor;

		public SettingColorOption teamColorSetting;

		internal void _003CAwake_003Eb__0(int selectedIndex)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string text = default(string);
			string message = "Selected difficulty is " + text + ".";
			Debug.Log(message);
		}

		internal void _003CAwake_003Eb__1(int selectedColor)
		{
			List<Color> optionLabels = opponentColor.GetOptionLabels();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj = default(object);
			object arg = (Color)obj;
			string message = $"Opponent color is {arg}.";
			Debug.Log(message);
		}

		internal void _003CAwake_003Eb__2(int selectedColor)
		{
			List<Color> optionLabels = teamColorSetting.GetOptionLabels();
			if (optionLabels == null)
			{
				Debug.LogWarning("Team color options are not yet initialized (one of the caveats of defining the colors in the UI is the system has to wait for the UI to load before the options are available).");
				return;
			}
			List<Color> optionLabels2 = teamColorSetting.GetOptionLabels();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj = default(object);
			object arg = (Color)obj;
			string message = $"Selected team color is {arg}.";
			Debug.Log(message);
		}
	}

	public SettingsProvider Provider;

	protected int _healthRegeneration;

	private float _logTimer;

	public unsafe void Awake()
	{
		//IL_0179: Expected O, but got Ref
		//IL_0187: Expected O, but got Ref
		//IL_0199: Expected O, but got Ref
		_003C_003Ec__DisplayClass1_0 CS_0024_003C_003E8__locals8 = new _003C_003Ec__DisplayClass1_0();
		Settings orCreateRuntimeSettingsAsset = Provider.GetOrCreateRuntimeSettingsAsset();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F15E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		IConnection<bool> connection = default(IConnection<bool>);
		SettingsProvider settingsProvider = default(SettingsProvider);
		SettingBool orCreateBool = orCreateRuntimeSettingsAsset.GetOrCreateBool("enableBossFights", defaultValue: true, null, connection, settingsProvider);
		Func<int> getter = getHealthRegeneration;
		Action<int> action = setHealthRegeneration;
		action._002Ector((object)this, (IntPtr)(nint)__ldftn(SettingsFromCodeDemo.setHealthRegeneration));
		GetSetConnection<int> getSetConnection = new GetSetConnection<int>(getter, action);
		SettingInt orCreateInt = orCreateRuntimeSettingsAsset.GetOrCreateInt("healthRegeneration", 0, null, (IConnection<int>)connection, settingsProvider);
		List<string> list = new List<string>();
		list.Add("Easy");
		list.Add("Normal");
		list.Add("Hard");
		CS_0024_003C_003E8__locals8.difficulties = list;
		SettingsProvider provider = default(SettingsProvider);
		SettingOption orCreateOption = orCreateRuntimeSettingsAsset.GetOrCreateOption("difficulty", 1, null, (List<string>)connection, (IConnectionWithOptions<string>)settingsProvider, provider);
		Action<int> onChanged = delegate
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string text = default(string);
			string message = "Selected difficulty is " + text + ".";
			Debug.Log(message);
		};
		orCreateOption.AddChangeListener(onChanged);
		List<Color> list2 = new List<Color>();
		object obj = default(object);
		list2.Add((Color)(&obj));
		list2.Add((Color)(&obj));
		list2.Add((Color)(&obj));
		SettingColorOption orCreateColorOption = orCreateRuntimeSettingsAsset.GetOrCreateColorOption("opponentColor", 0, null, (List<Color>)connection, (IConnectionWithOptions<Color>)settingsProvider, provider);
		CS_0024_003C_003E8__locals8.opponentColor = orCreateColorOption;
		Action<int> onChanged2 = delegate
		{
			List<Color> optionLabels = CS_0024_003C_003E8__locals8.opponentColor.GetOptionLabels();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj2 = default(object);
			object arg = (Color)obj2;
			string message = $"Opponent color is {arg}.";
			Debug.Log(message);
		};
		CS_0024_003C_003E8__locals8.opponentColor.AddChangeListener(onChanged2);
		SettingColorOption orCreateColorOption2 = orCreateRuntimeSettingsAsset.GetOrCreateColorOption("teamColor", 0, null, (List<Color>)connection, (IConnectionWithOptions<Color>)settingsProvider, provider);
		CS_0024_003C_003E8__locals8.teamColorSetting = orCreateColorOption2;
		Action<int> onChanged3 = delegate
		{
			List<Color> optionLabels = CS_0024_003C_003E8__locals8.teamColorSetting.GetOptionLabels();
			if (optionLabels == null)
			{
				Debug.LogWarning("Team color options are not yet initialized (one of the caveats of defining the colors in the UI is the system has to wait for the UI to load before the options are available).");
			}
			else
			{
				List<Color> optionLabels2 = CS_0024_003C_003E8__locals8.teamColorSetting.GetOptionLabels();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj2 = default(object);
				object arg = (Color)obj2;
				string message = $"Selected team color is {arg}.";
				Debug.Log(message);
			}
		};
		CS_0024_003C_003E8__locals8.teamColorSetting.AddChangeListener(onChanged3);
		AudioPausedConnection audioPausedConnection = new AudioPausedConnection();
		SettingBool orCreateBool2 = orCreateRuntimeSettingsAsset.GetOrCreateBool("audioEnabled", defaultValue: false, null, connection, settingsProvider);
	}

	public void Start()
	{
		bool flag = default(bool);
		string text = flag.ToString();
		string message = "Settings loaded: " + text;
		Debug.Log(message);
		Settings settings = Provider.Settings;
		settings.Apply(changedOnly: true, triggerChangeEvents: true);
	}

	private static void addEnableBossFightsBoolean(Settings settings)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F15E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		IConnection<bool> connection = default(IConnection<bool>);
		SettingsProvider provider = default(SettingsProvider);
		SettingBool orCreateBool = settings.GetOrCreateBool("enableBossFights", defaultValue: true, null, connection, provider);
	}

	protected unsafe void addHealthRegenerationPercentage(Settings settings)
	{
		Func<int> getter = getHealthRegeneration;
		Action<int> action = setHealthRegeneration;
		action._002Ector((object)this, (IntPtr)(nint)__ldftn(SettingsFromCodeDemo.setHealthRegeneration));
		GetSetConnection<int> getSetConnection = new GetSetConnection<int>(getter, action);
		IConnection<int> connection = default(IConnection<int>);
		SettingsProvider provider = default(SettingsProvider);
		SettingInt orCreateInt = settings.GetOrCreateInt("healthRegeneration", 0, null, connection, provider);
	}

	protected int getHealthRegeneration()
	{
		return _healthRegeneration;
	}

	protected void setHealthRegeneration(int value)
	{
		int healthRegeneration = default(int);
		_healthRegeneration = healthRegeneration;
		int num = default(int);
		string text = num.ToString();
		string message = "Health regeneration has been set to: " + text;
		Debug.Log(message);
	}

	public void Update()
	{
		float deltaTime = Time.deltaTime;
		if ((_logTimer = deltaTime + _logTimer) > 2f)
		{
			_logTimer = 0f;
			Settings settings = Provider.Settings;
			SettingBool settingBool = settings.GetBool("enableBossFights");
			bool value = settingBool.GetValue();
			bool flag = default(bool);
			string text = flag.ToString();
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = default(float);
			string text2 = num.ToString();
			string message = "Enable Boss Fights is: " + text + " (time: " + text2 + ")";
			Debug.Log(message);
		}
	}
}
