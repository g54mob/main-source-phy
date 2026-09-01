using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Common
{
	public class FireInteraction : Element
	{
		[XmlAttribute("burnDuration")]
		public float BurnDuration;

		[XmlAttribute("igniteOnStart")]
		[DefaultValue(false)]
		public bool IgniteOnStart;

		[XmlAttribute("disableParticles")]
		[DefaultValue(false)]
		public bool DisableParticles;

		[XmlElement("ParticleTransform")]
		[DefaultValue(null)]
		public TransformValues ParticleTransform;

		[DefaultValue(null)]
		[XmlElement("BoxTrigger")]
		[RequireToValidate]
		public BoxModCollider BoxTrigger;

		[XmlElement("SphereTrigger")]
		[DefaultValue(null)]
		[RequireToValidate]
		public SphereModCollider SphereTrigger;

		[XmlElement("CapsuleTrigger")]
		[DefaultValue(null)]
		[RequireToValidate]
		public CapsuleModCollider CapsuleTrigger;

		[XmlIgnore]
		public ModCollider Trigger { get; private set; }

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			int num = 0;
			if (BoxTrigger != null)
			{
				num++;
			}
			if (SphereTrigger != null)
			{
				num++;
			}
			if (CapsuleTrigger != null)
			{
				num++;
			}
			if (num == 0)
			{
				SphereTrigger = new SphereModCollider
				{
					Position = new Modding.Serialization.Vector3(0f, 0f, 0.91f),
					Radius = 1.52f,
					Layer = LayerMask.NameToLayer("Fire"),
					LayerSpecified = true,
					Trigger = true
				};
			}
			else if (num > 1)
			{
				return InvalidData(elemName, "Can only specify one of BoxTrigger, SphereTrigger, or CapsuleTrigger!");
			}
			if (BoxTrigger != null)
			{
				Trigger = BoxTrigger;
			}
			else if (SphereTrigger != null)
			{
				Trigger = SphereTrigger;
			}
			else if (CapsuleTrigger != null)
			{
				Trigger = CapsuleTrigger;
			}
			Modding.Serialization.Vector3 vector = new Modding.Serialization.Vector3(0f, -0.01681f, 0.88278f);
			Modding.Serialization.Vector3 vector2 = new Modding.Serialization.Vector3(0f, 0f, 0f);
			if (ParticleTransform == null)
			{
				ParticleTransform = new TransformValues();
				ParticleTransform.Position = vector;
				ParticleTransform.Rotation = vector2;
			}
			else if (!ParticleTransform.SetPositionDefault(vector).SetRotationDefault(vector2).HasNoScale()
				.Check("ParticleTransform"))
			{
				return false;
			}
			return true;
		}
	}
}
