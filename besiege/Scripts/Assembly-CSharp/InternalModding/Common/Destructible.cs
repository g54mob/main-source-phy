using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding;
using Modding.Serialization;

namespace InternalModding.Common
{
	[Serializable]
	public class Destructible : Element
	{
		[Serializable]
		public class BreakForceWrapper : Element
		{
			[XmlAttribute("power")]
			public float Power;

			[XmlAttribute("radius")]
			public float Radius;

			public BreakForceWrapper()
			{
			}

			public BreakForceWrapper(float power, float radius)
			{
				Power = power;
				Radius = radius;
			}
		}

		[Serializable]
		public class Particle : Element
		{
			[RequireToValidate]
			[XmlElement("Mesh")]
			public MeshReference MeshReference;

			[XmlArrayItem("CapsuleCollider", typeof(CapsuleModCollider))]
			[XmlArray]
			[XmlArrayItem("SphereCollider", typeof(SphereModCollider))]
			[XmlArrayItem("BoxCollider", typeof(BoxModCollider))]
			[RequireToValidate]
			public List<ModCollider> Colliders;

			[XmlIgnore]
			public ModMesh Mesh { get; set; }

			public Particle()
			{
				Colliders = new List<ModCollider>();
			}
		}

		[XmlAttribute("forceToBreak")]
		public float ForceToBreak;

		[XmlIgnore]
		public bool ForceToBreakSpecified;

		[XmlElement("BreakForce")]
		[DefaultValue(null)]
		public BreakForceWrapper BreakForce;

		[XmlArrayItem("Particle", typeof(Particle))]
		[RequireToValidate]
		[XmlArray("Particles")]
		public List<Particle> Particles;

		[RequireToValidate]
		[XmlElement("Sound")]
		public ResourceReference SoundReference;

		[XmlIgnore]
		public ModAudioClip Sound;

		public Destructible()
		{
			Particles = new List<Particle>();
			BreakForce = new BreakForceWrapper(200f, 6f);
		}
	}
}
