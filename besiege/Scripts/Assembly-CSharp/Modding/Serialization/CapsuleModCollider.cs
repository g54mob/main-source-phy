using System;
using System.ComponentModel;
using System.Xml.Serialization;
using InternalModding.Blocks;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public class CapsuleModCollider : ModCollider
	{
		[Serializable]
		public class CapsuleWrapper : Element
		{
			[XmlAttribute("radius")]
			public float Radius;

			[XmlAttribute("height")]
			public float Height;

			[XmlAttribute("direction")]
			public Direction Direction;

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
				if (Height < 0f)
				{
					return InvalidData(elemName, "Height may not be negative!");
				}
				return true;
			}
		}

		[XmlElement]
		[RequireToValidate]
		public CapsuleWrapper Capsule;

		[DefaultValue(null)]
		[XmlElement]
		public Vector3 Rotation { get; set; }

		[XmlIgnore]
		public Direction Dir
		{
			get
			{
				return Capsule.Direction;
			}
		}

		[XmlIgnore]
		public float Radius
		{
			get
			{
				return Capsule.Radius;
			}
		}

		[XmlIgnore]
		public float Height
		{
			get
			{
				return Capsule.Height;
			}
		}

		public CapsuleModCollider()
		{
			base.Position = Vector3.zero;
			Rotation = Vector3.zero;
		}

		public override Collider CreateCollider(Transform parent)
		{
			GameObject gameObject = new GameObject("Capsule Collider");
			CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = Vector3.zero;
			capsuleCollider.radius = Radius;
			capsuleCollider.height = Height;
			capsuleCollider.direction = (int)Dir;
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = base.Position;
			gameObject.transform.localRotation = Quaternion.Euler(Rotation);
			if (base.LayerSpecified)
			{
				gameObject.layer = base.Layer;
			}
			capsuleCollider.isTrigger = base.Trigger;
			return capsuleCollider;
		}

		public override Transform CreateVisual(Transform overallParent)
		{
			Transform transform = new GameObject("Capsule Collider").transform;
			transform.parent = overallParent;
			transform.localPosition = base.Position;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			Transform transform2 = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Modding/HalfGridSphere")).transform;
			Transform transform3 = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Modding/HalfGridSphere")).transform;
			transform2.gameObject.name = "Top";
			transform3.gameObject.name = "Bottom";
			transform2.parent = transform;
			transform3.parent = transform;
			float num = Height - 2f * Radius;
			transform2.localPosition = UnityEngine.Vector3.forward * num / 2f;
			transform2.localRotation = Quaternion.Euler(90f, 0f, 0f);
			transform2.localScale = UnityEngine.Vector3.one * Radius * 2f;
			transform3.localPosition = -UnityEngine.Vector3.forward * num / 2f;
			transform3.localRotation = Quaternion.Euler(270f, 0f, 0f);
			transform3.localScale = UnityEngine.Vector3.one * Radius * 2f;
			Material sharedMaterial = ((!base.Trigger) ? SingleInstanceFindOnly<BlockLoader>.Instance.ColliderVisualMaterial : SingleInstanceFindOnly<BlockLoader>.Instance.TriggerVisualMaterial);
			transform2.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
			transform3.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
			if (Height > 2f * Radius)
			{
				Transform transform4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform;
				UnityEngine.Object.Destroy(transform4.GetComponent<Collider>());
				transform4.parent = transform;
				transform4.localPosition = Vector3.zero;
				transform4.localRotation = Quaternion.Euler(90f, 0f, 0f);
				transform4.localScale = new Vector3(Radius * 2f, num / 2f, Radius * 2f);
				transform4.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
				Transform transform5 = GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform;
				UnityEngine.Object.Destroy(transform5.GetComponent<Collider>());
				transform5.parent = transform;
				transform5.localPosition = Vector3.zero;
				transform5.localRotation = Quaternion.Euler(0f, -90f, -90f);
				transform5.localScale = new Vector3(Radius * 2f, num / 2f, Radius * 2f);
				transform5.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
			}
			transform.gameObject.SetLayerRecursively(25);
			transform.localRotation = Quaternion.Euler(Rotation);
			if (Dir == Direction.X)
			{
				transform.localRotation *= Quaternion.Euler(0f, 90f, 0f);
			}
			else if (Dir == Direction.Y)
			{
				transform.localRotation *= Quaternion.Euler(90f, 0f, 0f);
			}
			return transform;
		}
	}
}
