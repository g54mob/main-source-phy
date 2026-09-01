using System;
using System.Xml.Serialization;
using InternalModding.Blocks;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public class BoxModCollider : ModCollider
	{
		[XmlElement]
		public Vector3 Rotation { get; internal set; }

		[XmlElement]
		public Vector3 Scale { get; internal set; }

		public BoxModCollider()
		{
			base.Position = Vector3.zero;
			Rotation = Vector3.zero;
			Scale = Vector3.one;
		}

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			if (GameObjectHelper.IsVectorNegative(Scale))
			{
				return InvalidData(elemName, "Scale may not be negative!");
			}
			return true;
		}

		public override Collider CreateCollider(Transform parent)
		{
			GameObject gameObject = new GameObject("Box Collider");
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.center = Vector3.zero;
			boxCollider.size = Vector3.one;
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = base.Position;
			gameObject.transform.localEulerAngles = Rotation;
			gameObject.transform.localScale = Scale;
			if (base.LayerSpecified)
			{
				gameObject.layer = base.Layer;
			}
			boxCollider.isTrigger = base.Trigger;
			return boxCollider;
		}

		public override Transform CreateVisual(Transform parent)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = base.Position;
			gameObject.transform.localEulerAngles = Rotation;
			gameObject.transform.localScale = Scale;
			gameObject.name = "Box Collider";
			gameObject.layer = 25;
			if (base.Trigger)
			{
				gameObject.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.TriggerVisualMaterial;
			}
			else
			{
				gameObject.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.ColliderVisualMaterial;
			}
			UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<Collider>());
			return gameObject.transform;
		}
	}
}
