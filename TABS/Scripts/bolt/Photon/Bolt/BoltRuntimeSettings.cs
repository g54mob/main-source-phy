using System.Linq;
using Photon.Bolt.Utils;
using UdpKit.Platform.Photon;
using UnityEngine;

namespace Photon.Bolt
{
	[CreateAssetMenu(fileName = "BoltRuntimeSettings", menuName = "Bolt/Create BoltRuntimeSettings")]
	public class BoltRuntimeSettings : ScriptableObject
	{
		private static BoltRuntimeSettings _instance;

		[SerializeField]
		internal BoltConfig _config = new BoltConfig();

		[SerializeField]
		public int debugClientCount = 1;

		[SerializeField]
		public int debugStartPort = 54321;

		[SerializeField]
		public int debugBuildMode = 1;

		[SerializeField]
		public string debugStartMapName;

		[SerializeField]
		public bool debugPlayAsServer;

		[SerializeField]
		public bool showDebugInfo = true;

		[SerializeField]
		public bool overrideTimeScale = true;

		[SerializeField]
		public BoltEditorStartMode debugEditorMode = BoltEditorStartMode.Server;

		[SerializeField]
		public KeyCode consoleToggleKey = KeyCode.Tab;

		[SerializeField]
		public bool consoleVisibleByDefault = true;

		[SerializeField]
		public int compilationWarnLevel = 4;

		[SerializeField]
		public int editorSkin = 4;

		[SerializeField]
		public bool scopeModeHideWarningInGui;

		[SerializeField]
		public bool showBoltEntityHints = true;

		[SerializeField]
		public bool serializeProjectAsText;

		[SerializeField]
		public string photonAppId = "";

		[SerializeField]
		public bool photonUsePunch;

		[SerializeField]
		public int photonCloudRegionIndex;

		[SerializeField]
		public BoltPrefabInstantiateMode instantiateMode;

		[SerializeField]
		public QueryComponentOptionsGlobal globalEntityBehaviourQueryOption = QueryComponentOptionsGlobal.ComponentsInChildren;

		[SerializeField]
		public QueryComponentOptionsGlobal globalEntityPriorityCalculatorQueryOption = QueryComponentOptionsGlobal.ComponentsInChildren;

		[SerializeField]
		public QueryComponentOptionsGlobal globalEntityReplicationFilterQueryOption = QueryComponentOptionsGlobal.ComponentsInChildren;

		[SerializeField]
		public int a2sServerPort = 21777;

		[SerializeField]
		public bool enableA2sServer = true;

		[SerializeField]
		public float RoomCreateTimeout = 10f;

		[SerializeField]
		public float RoomJoinTimeout = 10f;

		[SerializeField]
		public bool enableClientMetrics;

		[SerializeField]
		public bool enableSourceProvider;

		public static BoltRuntimeSettings instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (BoltRuntimeSettings)Resources.Load(typeof(BoltRuntimeSettings).Name, typeof(BoltRuntimeSettings));
					_ = _instance == null;
				}
				return _instance;
			}
		}

		public static string[] photonCloudRegions => PhotonRegion.regions.Values.Select((PhotonRegion region) => region.ToString()).ToArray();

		public static string[] photonCloudRegionsId => PhotonRegion.regions.Values.Select((PhotonRegion region) => region.Code).ToArray();

		public BoltConfig GetConfigCopy()
		{
			return _config.Clone();
		}

		public void UpdateBestRegion(PhotonRegion newRegion)
		{
			PhotonRegion[] array = PhotonRegion.regions.Values.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				PhotonRegion photonRegion = array[i];
				if (newRegion.Code.Equals(photonRegion.Code))
				{
					photonCloudRegionIndex = i;
					break;
				}
			}
		}
	}
}
