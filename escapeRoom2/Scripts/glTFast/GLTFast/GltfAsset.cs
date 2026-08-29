using System.IO;
using System.Threading.Tasks;
using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;
using UnityEngine;
using UnityEngine.Playables;

namespace GLTFast
{
	public class GltfAsset : GltfAssetBase
	{
		[SerializeField]
		[Tooltip("URL to load the glTF from. Loading local file paths works by prefixing them with \"file://\"")]
		private string url;

		[SerializeField]
		[Tooltip("Automatically load at start.")]
		private bool loadOnStartup = true;

		[SerializeField]
		[Tooltip("Override scene to load (-1 loads glTFs default scene)")]
		private int sceneId = -1;

		[SerializeField]
		[Tooltip("If true, the first animation clip starts playing right after instantiation")]
		private bool playAutomatically = true;

		[SerializeField]
		[Tooltip("If checked, url is treated as relative StreamingAssets path.")]
		private bool streamingAsset;

		[SerializeField]
		private InstantiationSettings instantiationSettings;

		public string Url
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}

		public bool LoadOnStartup
		{
			get
			{
				return loadOnStartup;
			}
			set
			{
				loadOnStartup = value;
			}
		}

		protected int SceneId => sceneId;

		public bool PlayAutomatically => playAutomatically;

		public bool StreamingAsset
		{
			get
			{
				return streamingAsset;
			}
			set
			{
				streamingAsset = value;
			}
		}

		public InstantiationSettings InstantiationSettings
		{
			get
			{
				return instantiationSettings;
			}
			set
			{
				instantiationSettings = value;
			}
		}

		public GameObjectSceneInstance SceneInstance { get; protected set; }

		public string FullUrl
		{
			get
			{
				if (!streamingAsset)
				{
					return url;
				}
				return Path.Combine(Application.streamingAssetsPath, url);
			}
		}

		protected virtual async void Start()
		{
			if (loadOnStartup && !string.IsNullOrEmpty(url))
			{
				await Load(FullUrl);
			}
		}

		public override async Task<bool> Load(string gltfUrl, IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
		{
			logger = logger ?? new ConsoleLogger();
			bool flag = await base.Load(gltfUrl, downloadProvider, deferAgent, materialGenerator, logger);
			if (flag)
			{
				if (deferAgent != null)
				{
					await deferAgent.BreakPoint();
				}
				flag = ((sceneId < 0) ? (await Instantiate(logger)) : (await InstantiateScene(sceneId, logger)));
			}
			return flag;
		}

		protected override IInstantiator GetDefaultInstantiator(ICodeLogger logger)
		{
			return new GameObjectInstantiator(base.Importer, base.transform, logger, instantiationSettings);
		}

		protected override void PostInstantiation(IInstantiator instantiator, bool success)
		{
			SceneInstance = (instantiator as GameObjectInstantiator)?.SceneInstance;
			if (SceneInstance != null && playAutomatically)
			{
				SceneInstance.LegacyAnimation?.Play();
				SceneInstance.Playable?.GetGraph().Play();
			}
			base.PostInstantiation(instantiator, success);
		}

		public override void ClearScenes()
		{
			foreach (Transform item in base.transform)
			{
				Object.Destroy(item.gameObject);
			}
			if (SceneInstance?.LegacyAnimation != null)
			{
				Object.Destroy(SceneInstance.LegacyAnimation);
			}
			SceneInstance = null;
		}
	}
}
