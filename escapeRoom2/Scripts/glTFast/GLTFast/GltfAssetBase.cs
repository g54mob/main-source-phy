using System.Threading.Tasks;
using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;
using UnityEngine;

namespace GLTFast
{
	public abstract class GltfAssetBase : MonoBehaviour
	{
		[SerializeField]
		private ImportSettings importSettings;

		public ImportSettings ImportSettings
		{
			get
			{
				return importSettings;
			}
			set
			{
				importSettings = value;
			}
		}

		public GltfImport Importer { get; protected set; }

		public bool IsDone
		{
			get
			{
				if (Importer != null)
				{
					return Importer.LoadingDone;
				}
				return false;
			}
		}

		public int? CurrentSceneId { get; protected set; }

		public int SceneCount => Importer?.SceneCount ?? 0;

		public string[] SceneNames
		{
			get
			{
				if (Importer != null && Importer.SceneCount > 0)
				{
					string[] array = new string[Importer.SceneCount];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Importer.GetSceneName(i);
					}
					return array;
				}
				return null;
			}
		}

		public virtual async Task<bool> Load(string gltfUrl, IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
		{
			Importer = new GltfImport(downloadProvider, deferAgent, materialGenerator, logger);
			return await Importer.Load(gltfUrl, importSettings);
		}

		public async Task<bool> Instantiate(ICodeLogger logger = null)
		{
			if (Importer == null)
			{
				return false;
			}
			IInstantiator instantiator = GetDefaultInstantiator(logger);
			bool flag = await Importer.InstantiateMainSceneAsync(instantiator);
			PostInstantiation(instantiator, flag);
			return flag;
		}

		public virtual async Task<bool> InstantiateScene(int sceneIndex, ICodeLogger logger = null)
		{
			if (Importer == null)
			{
				return false;
			}
			IInstantiator instantiator = GetDefaultInstantiator(logger);
			bool flag = await Importer.InstantiateSceneAsync(instantiator, sceneIndex);
			PostInstantiation(instantiator, flag);
			return flag;
		}

		protected async Task<bool> InstantiateScene(int sceneIndex, GameObjectInstantiator instantiator)
		{
			if (Importer == null)
			{
				return false;
			}
			bool flag = await Importer.InstantiateSceneAsync(instantiator, sceneIndex);
			PostInstantiation(instantiator, flag);
			return flag;
		}

		public abstract void ClearScenes();

		public Material GetMaterial(int index = 0)
		{
			return Importer?.GetMaterial(index);
		}

		protected abstract IInstantiator GetDefaultInstantiator(ICodeLogger logger);

		protected virtual void PostInstantiation(IInstantiator instantiator, bool success)
		{
			CurrentSceneId = (success ? Importer.DefaultSceneIndex : ((int?)null));
		}

		public void Dispose()
		{
			if (Importer != null)
			{
				Importer.Dispose();
				Importer = null;
			}
		}

		protected virtual void OnDestroy()
		{
			Dispose();
		}
	}
}
