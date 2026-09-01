using UnityEngine;

namespace Landfall
{
	public class ShadowCaster : MonoBehaviour
	{
		[Tooltip("Leave null to use default blob shadow settings")]
		[SerializeField]
		private BlobShadowService.BlobShadowSettings overrideSettings;

		private BlobShadowService blobShadowService;

		public void Start()
		{
			blobShadowService = ServiceLocator.GetService<BlobShadowService>();
			if (blobShadowService != null)
			{
				blobShadowService.AddShadowCaster(base.transform, overrideSettings);
			}
		}

		public void OnDestroy()
		{
			if (blobShadowService != null)
			{
				blobShadowService.RemoveShadowCaster(base.transform);
			}
		}
	}
}
