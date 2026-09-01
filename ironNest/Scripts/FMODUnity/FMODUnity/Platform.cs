using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;

namespace FMODUnity
{
	public abstract class Platform : ScriptableObject
	{
		public class Property<T>
		{
			public T Value;

			public bool HasValue;
		}

		[Serializable]
		public class PropertyBool : Property<TriStateBool>
		{
		}

		[Serializable]
		public class PropertyScreenPosition : Property<ScreenPosition>
		{
		}

		[Serializable]
		public class PropertyInt : Property<int>
		{
		}

		[Serializable]
		public class PropertySpeakerMode : Property<SPEAKERMODE>
		{
		}

		[Serializable]
		public class PropertyString : Property<string>
		{
		}

		[Serializable]
		public class PropertyStringList : Property<List<string>>
		{
		}

		[Serializable]
		public class PropertyCallbackHandler : Property<PlatformCallbackHandler>
		{
		}

		internal interface PropertyOverrideControl
		{
			bool HasValue(Platform platform);

			void Clear(Platform platform);
		}

		internal struct PropertyAccessor<T> : PropertyOverrideControl
		{
			private readonly Func<PropertyStorage, Property<T>> Getter;

			private readonly T DefaultValue;

			public PropertyAccessor(Func<PropertyStorage, Property<T>> getter, T defaultValue)
			{
				Getter = null;
				DefaultValue = default(T);
			}

			public bool HasValue(Platform platform)
			{
				return false;
			}

			public T Get(Platform platform)
			{
				return default(T);
			}

			public void Set(Platform platform, T value)
			{
			}

			public void Clear(Platform platform)
			{
			}
		}

		[Serializable]
		public class PropertyStorage
		{
			public PropertyBool LiveUpdate;

			public PropertyInt LiveUpdatePort;

			public PropertyBool Overlay;

			public PropertyScreenPosition OverlayPosition;

			public PropertyInt OverlayFontSize;

			public PropertyBool Logging;

			public PropertyInt SampleRate;

			public PropertyString BuildDirectory;

			public PropertySpeakerMode SpeakerMode;

			public PropertyInt VirtualChannelCount;

			public PropertyInt RealChannelCount;

			public PropertyInt DSPBufferLength;

			public PropertyInt DSPBufferCount;

			public PropertyStringList Plugins;

			public PropertyStringList StaticPlugins;

			public PropertyCallbackHandler CallbackHandler;
		}

		internal static class PropertyAccessors
		{
			public static readonly PropertyAccessor<TriStateBool> LiveUpdate;

			public static readonly PropertyAccessor<int> LiveUpdatePort;

			public static readonly PropertyAccessor<TriStateBool> Overlay;

			public static readonly PropertyAccessor<ScreenPosition> OverlayPosition;

			public static readonly PropertyAccessor<int> OverlayFontSize;

			public static readonly PropertyAccessor<TriStateBool> Logging;

			public static readonly PropertyAccessor<int> SampleRate;

			public static readonly PropertyAccessor<string> BuildDirectory;

			public static readonly PropertyAccessor<SPEAKERMODE> SpeakerMode;

			public static readonly PropertyAccessor<int> VirtualChannelCount;

			public static readonly PropertyAccessor<int> RealChannelCount;

			public static readonly PropertyAccessor<int> DSPBufferLength;

			public static readonly PropertyAccessor<int> DSPBufferCount;

			public static readonly PropertyAccessor<List<string>> Plugins;

			public static readonly PropertyAccessor<List<string>> StaticPlugins;

			public static readonly PropertyAccessor<PlatformCallbackHandler> CallbackHandler;
		}

		[Serializable]
		public class PropertyThreadAffinityList : Property<List<ThreadAffinityGroup>>
		{
		}

		[Serializable]
		internal class PropertyCodecChannels : Property<List<CodecChannelCount>>
		{
		}

		internal const float DefaultPriority = 0f;

		internal const string RegisterStaticPluginsClassName = "StaticPluginManager";

		internal const string RegisterStaticPluginsFunctionName = "Register";

		[SerializeField]
		private string identifier;

		[SerializeField]
		private string parentIdentifier;

		[SerializeField]
		private bool active;

		[SerializeField]
		protected PropertyStorage Properties;

		[SerializeField]
		[FormerlySerializedAs("outputType")]
		internal string OutputTypeName;

		private static List<ThreadAffinityGroup> StaticThreadAffinities;

		[SerializeField]
		private PropertyThreadAffinityList threadAffinities;

		[NonSerialized]
		public Platform Parent;

		private static List<CodecChannelCount> staticCodecChannels;

		[SerializeField]
		private PropertyCodecChannels codecChannels;

		internal string Identifier
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal abstract string DisplayName { get; }

		internal virtual float Priority => 0f;

		internal virtual bool MatchesCurrentEnvironment => false;

		internal virtual bool IsIntrinsic => false;

		internal string ParentIdentifier
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal bool IsLiveUpdateEnabled => false;

		internal bool IsOverlayEnabled => false;

		internal bool Active => false;

		internal bool HasAnyOverriddenProperties => false;

		public TriStateBool LiveUpdate => default(TriStateBool);

		public int LiveUpdatePort => 0;

		public TriStateBool Overlay => default(TriStateBool);

		public ScreenPosition OverlayRect => default(ScreenPosition);

		public int OverlayFontSize => 0;

		public TriStateBool Logging => default(TriStateBool);

		public int SampleRate => 0;

		public string BuildDirectory => null;

		public SPEAKERMODE SpeakerMode => default(SPEAKERMODE);

		public int VirtualChannelCount => 0;

		public int RealChannelCount => 0;

		public int DSPBufferLength => 0;

		public int DSPBufferCount => 0;

		public List<string> Plugins => null;

		public List<string> StaticPlugins => null;

		public PlatformCallbackHandler CallbackHandler => null;

		internal virtual List<ThreadAffinityGroup> DefaultThreadAffinities => null;

		public IEnumerable<ThreadAffinityGroup> ThreadAffinities => null;

		internal PropertyThreadAffinityList ThreadAffinitiesProperty => null;

		internal virtual List<CodecChannelCount> DefaultCodecChannels => null;

		internal List<CodecChannelCount> CodecChannels => null;

		internal PropertyCodecChannels CodecChannelsProperty => null;

		internal abstract void DeclareRuntimePlatforms(Settings settings);

		internal virtual void PreSystemCreate(Action<RESULT, string> reportResult)
		{
		}

		internal virtual void PreInitialize(FMOD.Studio.System studioSystem)
		{
		}

		internal virtual string GetBankFolder()
		{
			return null;
		}

		protected virtual string GetPluginBasePath()
		{
			return null;
		}

		internal virtual string GetPluginPath(string pluginName)
		{
			return null;
		}

		internal virtual void LoadPlugins(FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}

		internal virtual void LoadDynamicPlugins(FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}

		internal virtual void LoadStaticPlugins(FMOD.System coreSystem, Action<RESULT, string> reportResult)
		{
		}

		internal void AffirmProperties()
		{
		}

		internal void ClearProperties()
		{
		}

		internal virtual void InitializeProperties()
		{
		}

		internal virtual void EnsurePropertiesAreValid()
		{
		}

		public void SetOverlayFontSize(int size)
		{
		}

		internal bool InheritsFrom(Platform platform)
		{
			return false;
		}

		internal OUTPUTTYPE GetOutputType()
		{
			return default(OUTPUTTYPE);
		}
	}
}
