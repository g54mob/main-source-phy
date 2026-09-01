using Poly.Base;
using Poly.Collide;
using Poly.Collide.Unity;
using Poly.Extension;
using UnityEngine;

namespace Poly.Physics.Gameplay
{
	public class TriggerComponent : MonoBehaviour
	{
		internal Trigger trigger;

		private void OnEnable()
		{
			trigger = ConstructTrigger();
			Singleton<TriggerManager, int>.instance.triggers.Add(trigger);
		}

		private void OnDisable()
		{
			Singleton<TriggerManager, int>.instance.triggers.Remove(trigger);
			trigger.Destroy();
			trigger = null;
		}

		private void FixedUpdate()
		{
			if (base.transform.hasChanged)
			{
				base.transform.hasChanged = false;
				trigger.t2 = base.transform;
			}
		}

		private Trigger ConstructTrigger()
		{
			trigger = new Trigger();
			trigger.t2 = base.transform;
			PolygonCollider[] componentsInChildren = GetComponentsInChildren<PolygonCollider>();
			foreach (PolygonCollider polygonCollider in componentsInChildren)
			{
				PolygonShape[] array = polygonCollider.CreateConvexPolygons(in trigger.t2);
				foreach (PolygonShape item in array)
				{
					trigger.shapes.Add(item);
				}
				if (polygonCollider.gameObject != base.gameObject)
				{
					Object.Destroy(polygonCollider.gameObject);
					continue;
				}
				base.transform.DestroyAllChildren();
				Object.Destroy(polygonCollider);
			}
			return trigger;
		}
	}
}
