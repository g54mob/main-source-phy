using System;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public class LightPunctual : NamedObject
	{
		public enum Type
		{
			Unknown = 0,
			Spot = 1,
			Directional = 2,
			Point = 3
		}

		[Obsolete("Use LightColor for access.")]
		public float[] color = new float[3] { 1f, 1f, 1f };

		public float intensity = 1f;

		public float range = -1f;

		public SpotLight spot;

		[Obsolete("Use GetLightType and SetLightType for access.")]
		public string type;

		[NonSerialized]
		private Type m_TypeEnum;

		public Color LightColor
		{
			get
			{
				return new Color(color[0], color[1], color[2]);
			}
			set
			{
				color = new float[3] { value.r, value.g, value.b };
			}
		}

		public Type GetLightType()
		{
			if (m_TypeEnum != Type.Unknown)
			{
				return m_TypeEnum;
			}
			Enum.TryParse<Type>(type, ignoreCase: true, out m_TypeEnum);
			type = null;
			return m_TypeEnum;
		}

		public void SetLightType(Type lightType)
		{
			m_TypeEnum = lightType;
			type = null;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("type", m_TypeEnum.ToString().ToLowerInvariant());
			GltfSerializeName(writer);
			if (LightColor != Color.white)
			{
				writer.AddArrayProperty("color", color);
			}
			if (Math.Abs((double)intensity - 1.0) > 0.0010000000474974513)
			{
				writer.AddProperty("intensity", intensity);
			}
			if (range > 0f && GetLightType() != Type.Directional)
			{
				writer.AddProperty("range", range);
			}
			if (spot != null)
			{
				writer.AddProperty("spot");
				spot.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
