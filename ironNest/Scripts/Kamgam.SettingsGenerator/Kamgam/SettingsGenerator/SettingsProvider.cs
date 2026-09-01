using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "SettingsProvider", menuName = "SettingsGenerator/SettingsProvider", order = 1)]
	public class SettingsProvider : ScriptableObject, ISerializationCallbackReceiver
	{
		public enum UnappliedOnCloseBehaviour
		{
			Ignore = 0,
			Revert = 1,
			Apply = 2,
			TriggerCheckForUnappliedInScene = 3
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CscheduleAutoSaveAsync_003Ed__58 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public SettingsProvider _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public static SettingsProvider LastUsedSettingsProvider;

		[Header("Storage")]
		[SerializeField]
		[Tooltip("The player prefs key (or file name) under which your settings will be saved.\n\nIt is still named 'PlayerPrefs..' for backwards compatibility reasons but if you save as json is the filename (within persistent data path) without the extension.")]
		protected string playerPrefsKey;

		[Tooltip("Hee  you can choose how the settings will be saved. Currently Prefabs and JSON is supported but you can add your own method by creating a new ScriptableObject derived from SettingsSaverBase. If null then 'Prefabs' will be used as fallback.")]
		public SettingsSaverBase SettingsSaver;

		[Tooltip("The default settings asset.\nYou can leave this empty if you define all your settings via script.")]
		[FormerlySerializedAs("Default")]
		public Settings SettingsAsset;

		[NonSerialized]
		protected bool _initialLoadDone;

		protected Settings _settings;

		[FormerlySerializedAs("DeleteSavedSettingsAsStart")]
		[Header("Initialization")]
		[Tooltip("If enabled then you will have to use a settings initializer prefab to trigger initialization manually.\n\nNOTICE: The settings will always auto initialize once the settings UI is shown (not matter what you have set here) because the UI requires the settings to work.")]
		public bool DisableAutoInitialization;

		[Tooltip("If enabled then the save settings are always deleted at the start.\n\nThis may seem useless but it is very handy for testing build (simulate first boot from a settings perspective).\n\nJust be sure to disable it again!")]
		public bool DeleteSavedSettingsAtStart;

		[Tooltip("Should settings that are not in the Settings Asset list (but are still in the stored data on disk) be removed after loading?\nNOTICE: If enabled then settings that are added via code need to be added before loading (otherwise those would be removed too).")]
		public bool RemoveUnknownSettingsAfterLoad;

		[Tooltip("Use this to register event methods that should be executed BEFORE the settings are initialized.")]
		public UnityEvent PreInitializationEvents;

		[Header("Auto Save")]
		[Tooltip("If turned on then for each change in a setting a save will be SCHEDULED. If for AutoSaveWaitTimeInSec after the last change no further change happens then it will save.")]
		public bool AutoSave;

		[Tooltip("Only used if AutoSave is turned on. If for AutoSaveWaitTimeInSec after the last change no further change happens then it will save.")]
		public float AutoSaveWaitTimeInSec;

		[Tooltip("Should the settings be saved once the UI is closed (all resolvers are inactive)?\nNOTICE: This will NOT trigger a save on game exit if you have 'AutoSaveOnQuit' disabled.")]
		public bool AutoSaveOnClose;

		[Tooltip("Should the settings be saved once the application is being closed?\n\nNOTICE: It is not recommended to enable this on MOBILE devices since code execution while quitting can be aborted and lead to lost or broken data.")]
		public bool AutoSaveOnQuit;

		[Header("Apply")]
		[Tooltip("Defines what to do with unapplied settings once the UI is closed (all resolvers are inactive)?\nIf set to TriggerCheckForUnappliedInScene then you should have a SettingsCheckForUnapplied component in your scene. It will trigger the check on it.\n\nNOTICE: If you have SettingsCheckForUnapplied components in your scene and selected 'Ignore' here (the default) then those in the scene will still be executed as before (backwards compatibility).")]
		public UnappliedOnCloseBehaviour UnappliedBehaviourOnClose;

		public UnityEvent<List<ISetting>> OnUnappliedOnClose;

		[Header("On Scene Load")]
		[Tooltip("If enabled then you can remove any setting appliers you have because it will automatically create one in each new loaded scene at runtime.\nNOTICE: If you still have SettingsAppliers in your scene then this will do nothing (your existing applier will take precedence). This ensures backwards compatibility.")]
		public bool ApplyOnSceneLoad;

		[Tooltip("On start delay in seconds.")]
		public float ApplyOnSceneLoadDelay;

		[Tooltip("Only use this as a last resort if another system keeps overriding your settings.\nYou really should find out what system that is and route the settings through that instead of using this.")]
		public bool ApplyOnSceneLoadInLateUpdate;

		[Tooltip("Leave empty to apply all settings. If set then only these setting ids will be applied on scene load.")]
		public List<string> ApplyOnSceneLoadIds;

		[Header("Input Binding (New Input System)")]
		[Tooltip("Add your input action asset here and click the button below to generate settings for your input bindings. If you update your bindings then you have to click the button again to also update the settings (connections).\n\nINFO: The setting connections are stored in a folder called 'SettingsInputBindingConnections' right next to your InputAction asset (this is just for info).")]
		public InputActionAsset InputActionAsset;

		[Tooltip("By default Unity adds UI specific bindings for input. Usually these should not be editable by the player and thus can be skipped in auto-generation as they are usually not shown in any settings screen.\nIf you want to generate settings for them after updating the input binding connections then you can enable this here.")]
		public bool IncludeUIBindingsInAutoCreation;

		[Tooltip("If disabled then a search for all instances of the 'Input Action Asset' from above is made and the binding overrides are applied to all of them.This is necessary because Unity makes lots of hidden copies of the asset during runtime.\n\nNOTICE: This is a behaviour change as this is now disabled by default (was enabled in v1.72).\n\nWhen do you need to enable this?\nIf you want to support multiple players with separate bindings for each (local multiplayer). Then you will have to enable this an use Settings.SetInputActionAsset(..) to specify the input action asset that should be bound manually.")]
		public bool DontApplyBindingOverridesToAllInstances;

		[SerializeField]
		[HideInInspector]
		protected int initializedVersion;

		[SerializeField]
		[HideInInspector]
		private bool _hasBeenInitialisedInEditor;

		[NonSerialized]
		private double _awakeTime;

		[NonSerialized]
		protected float _autoSaveTime;

		private static List<ISetting> s_tmpListOfUnappliedSettings;

		public bool InitialLoadDone => false;

		public Settings Settings => null;

		public Settings GetOrCreateRuntimeSettingsAsset()
		{
			return null;
		}

		private bool onApplicationQuit()
		{
			return false;
		}

		public Settings GetSettingsAssetOrRuntimeCopy()
		{
			return null;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}

		public bool HasSettings()
		{
			return false;
		}

		private string getDefaultStorageKey()
		{
			return null;
		}

		public void OnEnable()
		{
		}

		public void Reset()
		{
		}

		public void ResetControls()
		{
		}

		public void ResetWrongControls()
		{
		}

		public void Reset(params string[] ids)
		{
		}

		public void ResetGroups(params string[] groups)
		{
		}

		public void ResetGroup(string group)
		{
		}

		public void ResetWithoutGroup()
		{
		}

		public void ResetToUnappliedValues()
		{
		}

		public void ResetToUnappliedValues(bool propagateChange)
		{
		}

		public void Apply()
		{
		}

		public void Apply(bool changedOnly)
		{
		}

		public void Apply(bool changedOnly, bool triggerChangeEvents)
		{
		}

		public void Load()
		{
		}

		public void ResetToLastSave()
		{
		}

		public void Save()
		{
		}

		public void Delete()
		{
		}

		protected void onSettingChanged(ISetting setting)
		{
		}

		public void ScheduleAutoSave(float autoSaveWaitTimeInSec)
		{
		}

		[AsyncStateMachine(typeof(_003CscheduleAutoSaveAsync_003Ed__58))]
		protected void scheduleAutoSaveAsync()
		{
		}

		public void OnAllResolversDeactivated(bool isQuitting)
		{
		}
	}
}
