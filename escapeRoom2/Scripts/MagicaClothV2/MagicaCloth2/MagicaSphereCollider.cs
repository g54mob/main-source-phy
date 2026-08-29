using UnityEngine;

namespace MagicaCloth2
{
	[AddComponentMenu("MagicaCloth2/MagicaSphereCollider")]
	[HelpURL("https://magicasoft.jp/en/mc2_spherecollidercomponent/")]
	public class MagicaSphereCollider : ColliderComponent
	{
		public override ColliderManager.ColliderType GetColliderType()
		{
			return ColliderManager.ColliderType.Sphere;
		}

		public override void DataValidate()
		{
			size.x = Mathf.Max(size.x, 0.001f);
		}

		public void SetSize(float radius)
		{
			SetSize(new Vector3(radius, 0f, 0f));
		}
	}
}
