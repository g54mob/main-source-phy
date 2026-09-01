using System;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public abstract class ModCollider : Element
	{
		[DefaultValue(null)]
		[XmlElement]
		public Vector3 Position { get; set; }

		[XmlAttribute("layer")]
		[DefaultValue(-1)]
		public int Layer { get; set; }

		[XmlIgnore]
		public bool LayerSpecified { get; set; }

		[XmlAttribute("trigger")]
		[DefaultValue(false)]
		public bool Trigger { get; set; }

		[XmlAttribute("ignoreForGhost")]
		[DefaultValue(false)]
		public bool IgnoreForGhost { get; set; }

		protected ModCollider()
		{
			Position = Vector3.zero;
			Trigger = false;
			IgnoreForGhost = false;
		}

		public abstract Collider CreateCollider(Transform parent);

		public abstract Transform CreateVisual(Transform parent);
	}
}
