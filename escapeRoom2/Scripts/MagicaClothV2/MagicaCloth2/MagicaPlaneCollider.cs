using UnityEngine;

namespace MagicaCloth2
{
	[AddComponentMenu("MagicaCloth2/MagicaPlaneCollider")]
	[HelpURL("https://magicasoft.jp/en/mc2_planecollidercomponent/")]
	public class MagicaPlaneCollider : ColliderComponent
	{
		public override ColliderManager.ColliderType GetColliderType()
		{
			return ColliderManager.ColliderType.Plane;
		}

		public override void DataValidate()
		{
		}
	}
}
