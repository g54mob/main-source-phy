internal static class OVRTelemetryConstants
{
	public static class OVRManager
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int Init = 163069401;

			public const int Consent = 163056770;
		}

		public static class AnnotationTypes
		{
			public const string ProjectName = "ProjectName";

			public const string ProjectGuid = "ProjectGuid";

			public const string Origin = "Origin";

			public const string BatchMode = "BatchMode";

			public const string ProcessorType = "ProcessorType";
		}

		public enum ConsentOrigins
		{
			Popup = 0,
			Settings = 1,
			Legacy = 2
		}

		public static readonly OVRTelemetry.MarkerPoint InitializeInsightPassthrough = new OVRTelemetry.MarkerPoint("InitializeInsightPassthrough");

		public static readonly OVRTelemetry.MarkerPoint InitPermissionRequest = new OVRTelemetry.MarkerPoint("InitPermissionRequest");
	}

	public static class Editor
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int Start = 163067235;

			public const int ComponentAdd = 163060094;

			public const int FeaturesInScene = 163069415;
		}

		public static class AnnotationType
		{
			public const string ComponentName = "ComponentName";

			public const string AssemblyName = "AssemblyName";

			public const string UsesProSkin = "UsesProSkin";

			public const string Origin = "Origin";
		}

		public enum AnnotationVariant
		{
			Required = 0,
			Optional = 1
		}
	}

	public static class BB
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int OpenWindow = 163062905;

			public const int AddBlock = 163060420;

			public const int UpdateBlock = 163064521;

			public const int RunBlock = 163063912;

			public const int InstallSDK = 163067801;

			public const int RemoveSDK = 163067560;

			public const int InstallBlockData = 163065449;

			public const int OpenSceneWithBlock = 163063649;

			public const int VariantsWindowFlow = 163065580;

			public const int VariantsWindowOpen = 163068476;
		}

		public static class AnnotationType
		{
			public const string BlockId = "BlockId";

			public const string BlockName = "BlockName";

			public const string InstanceId = "InstanceId";

			public const string Version = "Version";

			public const string ActionTrigger = "action_trigger";

			public const string Error = "error";

			public const string SceneSizeInB = "SceneSizeInB";

			public const string InstallationRoutineId = "InstallationRoutineId";

			public const string InstallationRoutineData = "InstallationRoutineData";

			public const string BlocksCount = "BlocksCount";
		}

		public static class Origins
		{
			public const string BlockGrid = "BlockGrid";

			public const string BlockDetails = "BlockDetails";

			public const string BlockInspector = "BlockInspector";
		}
	}

	public static class GuidedSetup
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int OpenSSAWindow = 163069502;

			public const int CloseSSAWindow = 163064312;

			public const int SetAppIdFromGuidedSetup = 163061548;

			public const int URLOpen = 163066819;
		}

		public static class AnnotationType
		{
			public const string ActionTrigger = "action_trigger";

			public const string HasAppId = "app_id_exist";

			public const string GSTSource = "gst_source";

			public const string URL = "url";
		}
	}

	public static class XRSim
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int SESInteraction = 163056472;

			public const int ToggleState = 163059165;

			public const int EditorRun = 163063015;
		}

		public static class AnnotationType
		{
			public const string IsActive = "active";

			public const string Action = "action";

			public const string XRSimEnabled = "xrsimenabled";
		}
	}

	public static class Scene
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int UseOVRSceneManager = 163061745;

			public const int UseDefaultSceneModelLoader = 163059869;

			public const int SceneOpen = 163063049;

			public const int SceneClose = 163068562;
		}

		public static class AnnotationType
		{
			public const string UsingBasicPrefabs = "basic_prefabs";

			public const string UsingPrefabOverrides = "prefab_overrides";

			public const string ActiveRoomsOnly = "active_rooms_only";

			public const string Guid = "Guid";

			public const string BuildTarget = "BuildTarget";

			public const string RuntimePlatform = "RuntimePlatform";

			public const string Features = "Features";

			public const string EnabledSettings = "FeaturesSupportInSettings";
		}
	}

	public static class Utils
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int DownloadContent = 163067281;
		}

		public static class AnnotationType
		{
			public const string ErrorMessage = "error_message";

			public const string StatusCode = "status_code";

			public const string ContentType = "content_type";
		}
	}

	public static class ProjectSettings
	{
		[OVRTelemetry.Markers]
		public static class MarkerId
		{
			public const int RenderThreadingMode = 163060994;

			public const int RenderingPath = 163068301;

			public const int XrPluginType = 163069107;
		}

		public static class AnnotationType
		{
			public const string RenderThreadingMode = "render_threading_mode";

			public const string RenderingPath = "rendering_path";

			public const string XrPluginType = "xr_plugin_type";
		}

		public enum RenderThreadingMode
		{
			Unknown = 0,
			Multithreaded = 1,
			LegacyGraphicsJobs = 2,
			NativeGraphicsJobs = 3
		}

		public enum RenderingPath
		{
			Unknown = 0,
			Forward = 1,
			ForwardPlus = 2,
			Deferred = 3
		}

		public enum XrPlugin
		{
			Unknown = 0,
			Oculus = 1,
			OpenXR = 2
		}
	}
}
