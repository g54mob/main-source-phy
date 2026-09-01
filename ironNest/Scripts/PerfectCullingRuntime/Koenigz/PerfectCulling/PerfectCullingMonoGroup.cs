using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public abstract class PerfectCullingMonoGroup : MonoBehaviour
	{
		[Header("Allows to exclude this from other PerfectCullingBakingBehaviours")]
		[SerializeField]
		public PerfectCullingBakingBehaviour[] restrictToBehaviours;

		public virtual List<Renderer> Renderers => null;

		public virtual List<Behaviour> UnityBehaviours => null;

		public abstract void PreSceneSave(PerfectCullingBakingBehaviour bakingBehaviour);

		public abstract void PreBake(PerfectCullingBakingBehaviour bakingBehaviour);

		public abstract void PostBake(PerfectCullingBakingBehaviour bakingBehaviour);
	}
}
