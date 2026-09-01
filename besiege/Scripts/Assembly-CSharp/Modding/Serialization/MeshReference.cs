using System;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	[Reloadable]
	public class MeshReference : ResourceReference, IReloadable
	{
		[XmlElement]
		[DefaultValue(null)]
		[Reloadable]
		public Vector3 Position = Vector3.zero;

		[DefaultValue(null)]
		[Reloadable]
		[XmlElement]
		public Vector3 Rotation = Vector3.zero;

		[XmlElement]
		[DefaultValue(null)]
		[Reloadable]
		public Vector3 Scale = Vector3.one;

		public void OnReload(IReloadable newObject)
		{
		}

		public void PreprocessForReloading()
		{
		}

		public void SetTransformValues(Transform t)
		{
			t.localPosition = Position;
			t.localRotation = Quaternion.Euler(Rotation);
			t.localScale = Scale;
		}
	}
}
