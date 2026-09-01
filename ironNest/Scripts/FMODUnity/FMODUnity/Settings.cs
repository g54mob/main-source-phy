using System;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.Serialization;

namespace FMODUnity
{
	public class Settings : ScriptableObject
	{
		internal enum SharedLibraryUpdateStages
		{
			Start = 0,
			DisableExistingLibraries = 1,
			RestartUnity = 2,
			CopyNewLibraries = 3
		}

		internal struct PlatformTemplate
		{
			public string Identifier;

			public Func<Platform> CreateInstance;
		}

		internal const string SettingsAssetName = "FMODStudioSettings";

		private static Settings instance;

		private static IEditorSettings editorSettings;

		private static bool isInitializing;

		[SerializeField]
		public bool HasSourceProject;

		[SerializeField]
		public bool HasPlatforms;

		[SerializeField]
		private string sourceProjectPath;

		[SerializeField]
		private string sourceBankPath;

		[FormerlySerializedAs("SourceBankPathUnformatted")]
		[SerializeField]
		private string sourceBankPathUnformatted;

		[SerializeField]
		public int BankRefreshCooldown;

		[SerializeField]
		public bool ShowBankRefreshWindow;

		internal const int BankRefreshPrompt = -1;

		internal const int BankRefreshManual = -2;

		[SerializeField]
		public bool AutomaticEventLoading;

		[SerializeField]
		public BankLoadType BankLoadType;

		[SerializeField]
		public bool AutomaticSampleLoading;

		[SerializeField]
		public string EncryptionKey;

		[SerializeField]
		public ImportType ImportType;

		[SerializeField]
		public string TargetAssetPath;

		[SerializeField]
		public string TargetBankFolder;

		[SerializeField]
		public EventLinkage EventLinkage;

		[SerializeField]
		public bool SerializeGUIDsOnly;

		[SerializeField]
		public DEBUG_FLAGS LoggingLevel;

		[SerializeField]
		internal List<Legacy.PlatformIntSetting> SpeakerModeSettings;

		[SerializeField]
		internal List<Legacy.PlatformIntSetting> SampleRateSettings;

		[SerializeField]
		internal List<Legacy.PlatformBoolSetting> LiveUpdateSettings;

		[SerializeField]
		internal List<Legacy.PlatformBoolSetting> OverlaySettings;

		[SerializeField]
		internal List<Legacy.PlatformStringSetting> BankDirectorySettings;

		[SerializeField]
		internal List<Legacy.PlatformIntSetting> VirtualChannelSettings;

		[SerializeField]
		internal List<Legacy.PlatformIntSetting> RealChannelSettings;

		[SerializeField]
		internal List<string> Plugins;

		[SerializeField]
		public List<string> MasterBanks;

		[SerializeField]
		public List<string> Banks;

		[SerializeField]
		public List<string> BanksToLoad;

		[SerializeField]
		public ushort LiveUpdatePort;

		[SerializeField]
		public bool EnableMemoryTracking;

		[SerializeField]
		public bool AndroidUseOBB;

		[SerializeField]
		public bool AndroidPatchBuild;

		[SerializeField]
		public MeterChannelOrderingType MeterChannelOrdering;

		[SerializeField]
		public bool StopEventsOutsideMaxDistance;

		[SerializeField]
		internal bool BoltUnitOptionsBuildPending;

		[SerializeField]
		public bool EnableErrorCallback;

		[SerializeField]
		internal SharedLibraryUpdateStages SharedLibraryUpdateStage;

		[SerializeField]
		internal double SharedLibraryTimeSinceStart;

		[SerializeField]
		internal int CurrentVersion;

		[SerializeField]
		public bool HideSetupWizard;

		[SerializeField]
		internal int LastEventReferenceScanVersion;

		[SerializeField]
		public List<Platform> Platforms;

		internal Dictionary<RuntimePlatform, List<Platform>> PlatformForRuntimePlatform;

		[NonSerialized]
		public Platform DefaultPlatform;

		[NonSerialized]
		public Platform PlayInEditorPlatform;

		internal static List<PlatformTemplate> PlatformTemplates;

		[NonSerialized]
		private bool hasLoaded;

		public static Settings Instance => null;

		internal static IEditorSettings EditorSettings
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string SourceProjectPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string SourceBankPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal string TargetPath => null;

		public string TargetSubFolder
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal static void Initialize()
		{
		}

		internal static bool IsInitialized()
		{
			return false;
		}

		internal Platform FindPlatform(string identifier)
		{
			return null;
		}

		internal bool PlatformExists(string identifier)
		{
			return false;
		}

		internal void AddPlatform(Platform platform)
		{
		}

		internal void RemovePlatform(string identifier)
		{
		}

		internal void LinkPlatform(Platform platform)
		{
		}

		internal void DeclareRuntimePlatform(RuntimePlatform runtimePlatform, Platform platform)
		{
		}

		private void LinkPlatformToParent(Platform platform)
		{
		}

		internal Platform FindCurrentPlatform()
		{
			return null;
		}

		private Settings()
		{
		}

		internal void AddPlatformProperties(Platform platform)
		{
		}

		public void SetPlatformParent(Platform platform, Platform newParent)
		{
		}

		internal static void AddPlatformTemplate<T>(string identifier)
		{
		}

		private static Platform CreatePlatformInstance<T>(string identifier)
		{
			return null;
		}

		internal void OnEnable()
		{
		}

		private void PopulatePlatformsFromAsset()
		{
		}
	}
}
