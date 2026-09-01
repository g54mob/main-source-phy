using System;
using System.Xml.Serialization;
using InternalModding.Blocks;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public class SphereModCollider : ModCollider
	{
		[XmlElement]
		public float Radius { get; set; }

		public SphereModCollider()
		{
			base.Position = Vector3.zero;
		}

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			if (Radius < 0f)
			{
				return InvalidData(elemName, "Radius may not be negative!");
			}
			return true;
		}

		public override Collider CreateCollider(Transform parent)
		{
			GameObject gameObject = new GameObject("Sphere Collider");
			SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
			sphereCollider.center = Vector3.zero;
			sphereCollider.radius = Radius;
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = base.Position;
			if (base.LayerSpecified)
			{
				gameObject.layer = base.Layer;
			}
			sphereCollider.isTrigger = base.Trigger;
			return sphereCollider;
		}

		public override Transform CreateVisual(Transform parent)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Modding/GridSphere"));
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = base.Position;
			gameObject.transform.localScale = UnityEngine.Vector3.one * Radius * 2f;
			gameObject.name = "Sphere Collider";
			gameObject.layer = 25;
			if (base.Trigger)
			{
				gameObject.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.TriggerVisualMaterial;
			}
			else
			{
				gameObject.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.ColliderVisualMaterial;
			}
			return gameObject.transform;
		}
	}
}
