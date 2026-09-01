using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public class PerfectCullingSceneGroup : PerfectCullingMonoGroup
	{
		[SerializeField]
		private Renderer[] renderers;

		[SerializeField]
		private Behaviour[] behaviours;

		public override List<Renderer> Renderers => null;

		public override List<Behaviour> UnityBehaviours => null;

		public void SetRenderers(Renderer[] newRenderers)
		{
		}

		public override void PreSceneSave(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		public override void PreBake(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		public override void PostBake(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}
	}
}
