using UnityEngine;

namespace Koenigz.PerfectCulling
{
	[RequireComponent(typeof(Renderer))]
	public class PerfectCullingRendererTag : MonoBehaviour
	{
		[SerializeField]
		private bool excludeRendererFromBake;

		[SerializeField]
		private bool renderDoubleSided;

		[SerializeField]
		private EBakeRenderMode forcedBakeRenderMode;

		public bool ExcludeRendererFromBake
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool RenderDoubleSided
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public EBakeRenderMode ForcedBakeRenderMode
		{
			get
			{
				return default(EBakeRenderMode);
			}
			set
			{
			}
		}
	}
}
