using System;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public class TransformValues : Element
	{
		[XmlElement("Position")]
		[DefaultValue(null)]
		public Vector3 Position;

		[XmlIgnore]
		public bool PositionSpecified;

		private bool hasPositionDefault;

		[XmlElement("Rotation")]
		[DefaultValue(null)]
		public Vector3 Rotation;

		[XmlIgnore]
		public bool RotationSpecified;

		private bool hasRotationDefault;

		[DefaultValue(null)]
		[XmlElement("Scale")]
		public Vector3 Scale;

		[XmlIgnore]
		public bool ScaleSpecified;

		private bool hasScaleDefault;

		[XmlIgnore]
		private bool hasScale = true;

		public TransformValues SetPositionDefault(Vector3 position)
		{
			hasPositionDefault = true;
			if (!PositionSpecified)
			{
				Position = position;
			}
			return this;
		}

		public TransformValues SetRotationDefault(Vector3 rotation)
		{
			hasRotationDefault = true;
			if (!RotationSpecified)
			{
				Rotation = rotation;
			}
			return this;
		}

		public TransformValues SetScaleDefault(Vector3 scale)
		{
			hasScaleDefault = true;
			if (!ScaleSpecified)
			{
				Scale = scale;
			}
			return this;
		}

		public TransformValues HasNoScale()
		{
			hasScale = false;
			return this;
		}

		public void SetOnTransform(Transform t)
		{
			t.localPosition = Position;
			t.localRotation = Quaternion.Euler(Rotation);
			if (hasScale)
			{
				t.localScale = Scale;
			}
		}

		public FauxTransform ToFauxTransform()
		{
			return new FauxTransform(Position, Quaternion.Euler(Rotation), Scale);
		}

		protected override bool Validate(string elemName)
		{
			if (!PositionSpecified)
			{
				return MissingElement(elemName, "Position");
			}
			if (!RotationSpecified)
			{
				return MissingElement(elemName, "Rotation");
			}
			if (!ScaleSpecified && hasScale)
			{
				return MissingElement(elemName, "Scale");
			}
			if (ScaleSpecified && !hasScale)
			{
				Warn(elemName, "Does not support Scale element");
			}
			return true;
		}

		public bool Check(string elemName)
		{
			if (!PositionSpecified && !hasPositionDefault)
			{
				return MissingElement(elemName, "Position");
			}
			if (!RotationSpecified && !hasRotationDefault)
			{
				return MissingElement(elemName, "Rotation");
			}
			if (!ScaleSpecified && !hasScaleDefault && hasScale)
			{
				return MissingElement(elemName, "Scale");
			}
			if (ScaleSpecified && !hasScale)
			{
				Warn(elemName, "Does not support Scale element");
			}
			return true;
		}
	}
}
