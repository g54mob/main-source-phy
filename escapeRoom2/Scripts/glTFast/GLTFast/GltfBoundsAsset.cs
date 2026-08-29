using System.Threading.Tasks;
using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;
using UnityEngine;

namespace GLTFast
{
	public class GltfBoundsAsset : GltfAsset
	{
		[SerializeField]
		[Tooltip("If true, a box collider encapsulating the glTF asset is created")]
		private bool createBoxCollider = true;

		public bool CreateBoxCollider
		{
			get
			{
				return createBoxCollider;
			}
			set
			{
				createBoxCollider = value;
			}
		}

		public Bounds Bounds { get; private set; }

		public override async Task<bool> Load(string gltfUrl, IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
		{
			base.Importer = new GltfImport(downloadProvider, deferAgent, materialGenerator, logger);
			bool flag = await base.Importer.Load(gltfUrl);
			if (flag)
			{
				GameObjectBoundsInstantiator instantiator = (GameObjectBoundsInstantiator)GetDefaultInstantiator(logger);
				if (base.SceneId >= 0)
				{
					flag = await base.Importer.InstantiateSceneAsync(instantiator, base.SceneId);
					base.CurrentSceneId = (flag ? new int?(base.SceneId) : ((int?)null));
				}
				else
				{
					flag = await base.Importer.InstantiateMainSceneAsync(instantiator);
					base.CurrentSceneId = base.Importer.DefaultSceneIndex;
				}
				base.SceneInstance = instantiator.SceneInstance;
				if (flag)
				{
					SetBounds(instantiator);
				}
			}
			return flag;
		}

		public override async Task<bool> InstantiateScene(int sceneIndex, ICodeLogger logger = null)
		{
			GameObjectBoundsInstantiator instantiator = (GameObjectBoundsInstantiator)GetDefaultInstantiator(logger);
			bool flag = await InstantiateScene(sceneIndex, instantiator);
			base.CurrentSceneId = (flag ? new int?(sceneIndex) : ((int?)null));
			base.SceneInstance = instantiator.SceneInstance;
			if (flag)
			{
				SetBounds(instantiator);
			}
			return flag;
		}

		protected override IInstantiator GetDefaultInstantiator(ICodeLogger logger)
		{
			return new GameObjectBoundsInstantiator(base.Importer, base.transform, logger, base.InstantiationSettings);
		}

		private void SetBounds(GameObjectBoundsInstantiator instantiator)
		{
			Bounds? bounds = ((instantiator.SceneInstance != null) ? instantiator.CalculateBounds() : ((Bounds?)null));
			if (bounds.HasValue)
			{
				Bounds = bounds.Value;
				if (createBoxCollider)
				{
					BoxCollider boxCollider = base.gameObject.AddComponent<BoxCollider>();
					boxCollider.center = Bounds.center;
					boxCollider.size = Bounds.size;
				}
			}
		}
	}
}
