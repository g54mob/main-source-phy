using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace InternalModding.Common
{
	public class Ghost : Element
	{
		[XmlArray("Colliders")]
		[CanBeEmpty]
		[RequireToValidate]
		[XmlArrayItem("BoxCollider", typeof(BoxModCollider))]
		[XmlArrayItem("CapsuleCollider", typeof(CapsuleModCollider))]
		[XmlArrayItem("SphereCollider", typeof(SphereModCollider))]
		public List<ModCollider> GhostColliders;

		[DefaultValue(null)]
		[XmlElement]
		public TransformValues Hammer;

		protected override bool Validate(string elementName)
		{
			if (Hammer != null)
			{
				Hammer.SetPositionDefault(Vector3.zero).SetRotationDefault(Vector3.zero).HasNoScale();
				if (!Hammer.Check("Hammer"))
				{
					return false;
				}
			}
			return base.Validate(elementName);
		}
	}
}
