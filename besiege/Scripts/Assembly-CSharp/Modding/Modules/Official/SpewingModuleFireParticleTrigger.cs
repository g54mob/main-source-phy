using UnityEngine;

namespace Modding.Modules.Official
{
	public class SpewingModuleFireParticleTrigger : MonoBehaviour
	{
		public FireController Controller;

		public void OnParticleCollision(GameObject other)
		{
			FireTag componentInChildren = other.GetComponentInChildren<FireTag>();
			if (componentInChildren == null || !componentInChildren.fireControllerCode.HasBasicInfo)
			{
				return;
			}
			BasicInfo basicInfo = componentInChildren.fireControllerCode.basicInfo;
			Collider[] componentsInChildren = basicInfo.gameObject.GetComponentsInChildren<Collider>();
			Collider[] array = componentsInChildren;
			foreach (Collider collider in array)
			{
				if (!collider.isTrigger)
				{
					Controller.OnTriggerEnter(collider);
					break;
				}
			}
		}
	}
}
