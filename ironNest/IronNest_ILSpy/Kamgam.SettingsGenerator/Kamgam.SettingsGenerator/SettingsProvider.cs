using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator;

public class SettingsProvider : ScriptableObject, ISerializationCallbackReceiver
{
	public enum UnappliedOnCloseBehaviour
	{
		Ignore,
		Revert,
		Apply,
		TriggerCheckForUnappliedInScene
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CscheduleAutoSaveAsync_003Ed__58 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public SettingsProvider _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0068: Expected O, but got I4
			//IL_0071: Expected O, but got I4
			//IL_01dd: Invalid comparison between F4 and O
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_0031: Expected O, but got I4
			//IL_0180: Expected I4, but got I8
			//IL_018b: Expected O, but got Ref
			//IL_011b: Expected O, but got I4
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected I4, but got Unknown
			//IL_0142: Expected O, but got Ref
			SettingsProvider settingsProvider = _003C_003E4__this;
			float num;
			object obj;
			TaskAwaiter awaiter = default(TaskAwaiter);
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				num = 1000f;
				obj = 0;
				awaiter = _003C_003Eu__1;
				goto IL_00ed;
			}
			float autoSaveTime = settingsProvider._autoSaveTime;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			num = 1000f;
			obj = 0;
			object obj2 = 0;
			goto IL_01c6;
			IL_01c6:
			float num2 = autoSaveTime - realtimeSinceStartup;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				float num3 = num2 * num;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				object obj3 = default(object);
				int millisecondsDelay = obj3 + 50;
				Task task = Task.Delay(millisecondsDelay);
				TaskAwaiter awaiter2 = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj4 = default(object);
				if (obj4 != null)
				{
					goto IL_00ed;
				}
				_003C_003E1__state = 0;
				_003C_003Eu__1 = _003C_003Eu__1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
			}
			settingsProvider.Save();
			settingsProvider._autoSaveTime = -1f;
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder2)->SetResult();
			return;
			IL_00ed:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			autoSaveTime = settingsProvider._autoSaveTime;
			realtimeSinceStartup = Time.realtimeSinceStartup;
			obj2 = 0;
			goto IL_01c6;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_000b: Expected O, but got Ref
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public static SettingsProvider LastUsedSettingsProvider;

	protected string playerPrefsKey;

	public SettingsSaverBase SettingsSaver;

	public Settings SettingsAsset;

	[NonSerialized]
	protected bool _initialLoadDone;

	protected Settings _settings;

	public bool DisableAutoInitialization;

	public bool DeleteSavedSettingsAtStart;

	public bool RemoveUnknownSettingsAfterLoad;

	public UnityEvent PreInitializationEvents;

	public bool AutoSave = true;

	public float AutoSaveWaitTimeInSec = 1f;

	public bool AutoSaveOnClose = true;

	public bool AutoSaveOnQuit;

	public UnappliedOnCloseBehaviour UnappliedBehaviourOnClose;

	public UnityEvent<List<ISetting>> OnUnappliedOnClose;

	public bool ApplyOnSceneLoad = true;

	public float ApplyOnSceneLoadDelay;

	public bool ApplyOnSceneLoadInLateUpdate;

	public List<string> ApplyOnSceneLoadIds;

	public InputActionAsset InputActionAsset;

	public bool IncludeUIBindingsInAutoCreation;

	public bool DontApplyBindingOverridesToAllInstances;

	protected int initializedVersion;

	private bool _hasBeenInitialisedInEditor;

	[NonSerialized]
	private double _awakeTime;

	[NonSerialized]
	protected float _autoSaveTime;

	private static List<ISetting> s_tmpListOfUnappliedSettings;

	public bool InitialLoadDone => _initialLoadDone;

	public Settings Settings
	{
		get
		{
			//IL_00e1: Expected I, but got O
			//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b1: Expected O, but got Unknown
			//IL_0385: Expected I, but got O
			//IL_0110: Expected I, but got O
			//IL_0158: Expected I, but got O
			//IL_013a: Expected I, but got O
			//IL_02a0: Expected I, but got O
			//IL_03c3: Expected O, but got I
			LastUsedSettingsProvider = this;
			bool flag = _settings == null;
			if (!flag && _initialLoadDone != flag)
			{
				goto IL_033b;
			}
			if (DeleteSavedSettingsAtStart)
			{
				if (!(_settings != null))
				{
					if ((object)SettingsSaver != null)
					{
						SettingsSaver.Delete(playerPrefsKey);
						goto IL_015d;
					}
				}
				else
				{
					Settings settings = _settings;
					bool flag2 = (object)_settings == null;
					nint num = (nint)playerPrefsKey;
					if (!flag2)
					{
						if (Settings.CustomDeleteMethod == null)
						{
							bool flag3 = (object)SettingsSaver == null;
							num = (nint)playerPrefsKey;
							if (!flag3)
							{
								SettingsSaver.Delete(playerPrefsKey);
								num = (nint)playerPrefsKey;
								goto IL_015d;
							}
						}
						else
						{
							Settings.CustomStorageMethod customDeleteMethod = Settings.CustomDeleteMethod;
							bool flag4 = Settings.CustomDeleteMethod == null;
							num = (nint)playerPrefsKey;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v485.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
								num = (nint)playerPrefsKey;
								goto IL_015d;
							}
						}
					}
				}
				goto IL_0342;
			}
			goto IL_015d;
			IL_0342:
			throw new NullReferenceException();
			IL_033b:
			return _settings;
			IL_015d:
			if (_settings == null)
			{
				Settings settings2 = ((!(SettingsAsset != null)) ? ScriptableObject.CreateInstance<Settings>() : UnityEngine.Object.Instantiate(SettingsAsset));
				_settings = settings2;
			}
			QualityPresets.AddCurrentLevel();
			if ((object)_settings != null)
			{
				SettingsProvider provider = default(SettingsProvider);
				_settings.Load(playerPrefsKey, SettingsSaver, RemoveUnknownSettingsAfterLoad, provider);
				Settings settings = _settings;
				Action<ISetting> b = onSettingChanged;
				if ((object)_settings != null)
				{
					Delegate obj = settings.OnSettingChanged;
					object obj2 = _settings + 24;
					Settings result = default(Settings);
					bool flag7;
					Delegate obj5 = default(Delegate);
					do
					{
						Delegate obj3 = Delegate.Combine(obj, b);
						bool flag5 = (object)obj3 == null;
						Delegate obj4 = obj3;
						if (!flag5)
						{
							((SettingsProvider)(object)obj3).onSettingChanged((ISetting)typeof(Action<ISetting>));
							bool flag6 = (object)obj4 == null;
							nint num = (nint)typeof(Action<ISetting>);
							settings = (Settings)(object)obj3;
							if (flag6)
							{
								((SettingsProvider)(object)settings).onSettingChanged((ISetting)num);
								return result;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
						flag7 = (object)obj5 != obj;
						obj = obj5;
					}
					while (flag7);
					_initialLoadDone = true;
					Func<bool> value = onApplicationQuit;
					Application.wantsToQuit -= value;
					Func<bool> value2 = onApplicationQuit;
					Application.wantsToQuit += value2;
					goto IL_033b;
				}
			}
			goto IL_0342;
		}
	}

	public Settings GetOrCreateRuntimeSettingsAsset()
	{
		if (_settings == null)
		{
			Settings settings = ((!(SettingsAsset != null)) ? ScriptableObject.CreateInstance<Settings>() : UnityEngine.Object.Instantiate(SettingsAsset));
			_settings = settings;
		}
		return _settings;
	}

	private bool onApplicationQuit()
	{
		if (AutoSaveOnQuit)
		{
			Save();
		}
		return true;
	}

	public Settings GetSettingsAssetOrRuntimeCopy()
	{
		return Settings;
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
	}

	public bool HasSettings()
	{
		return _settings != null;
	}

	private string getDefaultStorageKey()
	{
		string productName = Application.productName;
		string text = Regex.Replace(productName, "[^-a-zA-Z0-9_]", "");
		return "Settings." + text;
	}

	public void OnEnable()
	{
		if (string.IsNullOrEmpty(playerPrefsKey))
		{
			string productName = Application.productName;
			string text = Regex.Replace(productName, "[^-a-zA-Z0-9_]", "");
			string text2 = "Settings." + text;
			playerPrefsKey = text2;
		}
		if (SettingsSaver == null)
		{
			SettingsSaverPlayerPrefs settingsSaver = ScriptableObject.CreateInstance<SettingsSaverPlayerPrefs>();
			SettingsSaver = settingsSaver;
		}
	}

	public void Reset()
	{
		Settings settings = Settings;
		if (settings != null)
		{
			Settings settings2 = Settings;
			settings2.Reset();
		}
	}

	public void ResetControls()
	{
		Settings settings = Settings;
		settings.ResetControls();
	}

	public void ResetWrongControls()
	{
		Settings settings = Settings;
		if (settings != null)
		{
			Settings settings2 = Settings;
			settings2.ResetWrongControls();
		}
	}

	public void Reset(string[] ids)
	{
		Settings settings = Settings;
		settings.Reset(ids);
	}

	public void ResetGroups(string[] groups)
	{
		Settings settings = Settings;
		settings.ResetGroups(groups);
	}

	public void ResetGroup(string group)
	{
		Settings settings = Settings;
		settings.ResetGroups(new string[1] { group });
	}

	public void ResetWithoutGroup()
	{
		Settings settings = Settings;
		settings.ResetWithoutGroups();
	}

	public void ResetToUnappliedValues()
	{
		Settings settings = Settings;
		settings.ResetToUnappliedValues(propagateChange: true);
	}

	public void ResetToUnappliedValues(bool propagateChange)
	{
		Settings settings = Settings;
		settings.ResetToUnappliedValues(propagateChange);
	}

	public void Apply()
	{
		Settings?.Apply();
	}

	public void Apply(bool changedOnly)
	{
		Settings?.Apply(changedOnly);
	}

	public void Apply(bool changedOnly, bool triggerChangeEvents)
	{
		Settings?.Apply(changedOnly, triggerChangeEvents);
	}

	public void Load()
	{
		//IL_0175: Expected O, but got I4
		//IL_017e: Expected O, but got I4
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		if (_settings != null)
		{
			Settings settings = Settings;
			List<ISetting> settingsOrderedByConnectionOrderASC = settings.getSettingsOrderedByConnectionOrderASC((IEnumerable<ISetting>)settings._settingsCache);
			object obj = 0;
			object obj2 = 0;
			ISetting setting = default(ISetting);
			object obj3 = default(object);
			while ((nint)obj2 < settingsOrderedByConnectionOrderASC._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (setting.IsActive)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj3 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
					}
				}
				obj++;
				obj2 = obj;
			}
			Settings settings2 = Settings;
			SettingsGeneratorSettings orCreate = SettingsGeneratorSettings.GetOrCreate();
			SettingsProvider provider = orCreate.Provider;
			SettingsProvider provider2 = default(SettingsProvider);
			settings2.Load(playerPrefsKey, SettingsSaver, removeUnknownSettingsAfterLoad: false, provider2);
		}
		else
		{
			Settings settings3 = Settings;
			settings3.RefreshRegisteredResolvers();
		}
	}

	public void ResetToLastSave()
	{
		Settings settings = Settings;
		SettingsGeneratorSettings orCreate = SettingsGeneratorSettings.GetOrCreate();
		SettingsProvider provider = orCreate.Provider;
		SettingsProvider provider2 = default(SettingsProvider);
		settings.Load(playerPrefsKey, SettingsSaver, removeUnknownSettingsAfterLoad: false, provider2);
	}

	public void Save()
	{
		Settings settings = Settings;
		if ((object)settings != null)
		{
			if (Settings.CustomSaveMethod == null)
			{
				SettingsSaver.Save(playerPrefsKey, settings);
				return;
			}
			Settings.CustomStorageMethod customSaveMethod = Settings.CustomSaveMethod;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v175.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public void Delete()
	{
		//IL_003a: Expected I, but got O
		//IL_0054: Expected O, but got I
		//IL_0064: Expected O, but got I
		//IL_009e: Expected I, but got O
		//IL_00ae: Expected O, but got I
		//IL_00be: Expected O, but got I
		while (true)
		{
			if (!(_settings != null))
			{
				SettingsSaverBase settingsSaver = SettingsSaver;
				nint num = (nint)settingsSaver;
				string text = playerPrefsKey;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+198]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+1A0]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v87 @ rax_v16 (should have been resolved before IL gen)");
			}
			Settings settings = _settings;
			string text2 = playerPrefsKey;
			SettingsSaverBase settingsSaver2 = SettingsSaver;
			if (Settings.CustomDeleteMethod == null)
			{
				nint num2 = (nint)settingsSaver2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r8_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+198]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r8_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+1A0]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v161 @ rax_v13 (should have been resolved before IL gen)");
			}
			Settings.CustomStorageMethod customDeleteMethod = Settings.CustomDeleteMethod;
			IntPtr invoke_impl = ((Delegate)customDeleteMethod).invoke_impl;
			IntPtr method = ((Delegate)customDeleteMethod).method;
			IntPtr method_code = ((Delegate)customDeleteMethod).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v162 @ rax_v11 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	protected void onSettingChanged(ISetting setting)
	{
		//IL_002a: Invalid comparison between I4 and F4
		if (AutoSave)
		{
			if (!(0f > _autoSaveTime))
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				float autoSaveTime = realtimeSinceStartup + AutoSaveWaitTimeInSec;
				_autoSaveTime = autoSaveTime;
			}
			else
			{
				float realtimeSinceStartup2 = Time.realtimeSinceStartup;
				float autoSaveTime2 = realtimeSinceStartup2 + AutoSaveWaitTimeInSec;
				_autoSaveTime = autoSaveTime2;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
				AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
				_003CscheduleAutoSaveAsync_003Ed__58 stateMachine = default(_003CscheduleAutoSaveAsync_003Ed__58);
				asyncVoidMethodBuilder2.Start(ref stateMachine);
			}
		}
	}

	public void ScheduleAutoSave(float autoSaveWaitTimeInSec)
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (!(0f > _autoSaveTime))
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float autoSaveTime = realtimeSinceStartup + autoSaveWaitTimeInSec;
			_autoSaveTime = autoSaveTime;
		}
		else
		{
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
			float autoSaveTime2 = realtimeSinceStartup2 + autoSaveWaitTimeInSec;
			_autoSaveTime = autoSaveTime2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
			_003CscheduleAutoSaveAsync_003Ed__58 stateMachine = default(_003CscheduleAutoSaveAsync_003Ed__58);
			asyncVoidMethodBuilder2.Start(ref stateMachine);
		}
	}

	protected void scheduleAutoSaveAsync()
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CscheduleAutoSaveAsync_003Ed__58 stateMachine = default(_003CscheduleAutoSaveAsync_003Ed__58);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public void OnAllResolversDeactivated(bool isQuitting)
	{
		//IL_0176: Expected O, but got I
		Settings settings = Settings;
		bool flag = settings == null;
		if (flag)
		{
			return;
		}
		if (AutoSaveOnClose != flag && !isQuitting)
		{
			Save();
		}
		if (UnappliedBehaviourOnClose != UnappliedOnCloseBehaviour.Apply)
		{
			if (UnappliedBehaviourOnClose != UnappliedOnCloseBehaviour.Revert)
			{
				if (UnappliedBehaviourOnClose == UnappliedOnCloseBehaviour.TriggerCheckForUnappliedInScene)
				{
					SettingsCheckForUnapplied.TriggerCheck();
				}
			}
			else
			{
				Settings settings2 = Settings;
				settings2.ResetToUnappliedValues(propagateChange: true);
			}
		}
		else
		{
			Settings settings3 = Settings;
			settings3.Apply();
		}
		if (OnUnappliedOnClose != null)
		{
			Settings settings4 = Settings;
			List<ISetting> unappliedSettings = settings4.GetUnappliedSettings(s_tmpListOfUnappliedSettings);
			OnUnappliedOnClose.Invoke(s_tmpListOfUnappliedSettings);
			((UnityEvent<List<ISetting>>)(object)s_tmpListOfUnappliedSettings).Invoke((List<ISetting>)0);
		}
	}

	public SettingsProvider()
	{
		List<string> applyOnSceneLoadIds = new List<string>();
		ApplyOnSceneLoadIds = applyOnSceneLoadIds;
		_autoSaveTime = -1f;
		base._002Ector();
	}

	static SettingsProvider()
	{
		List<ISetting> list = new List<ISetting>();
		s_tmpListOfUnappliedSettings = list;
	}
}
