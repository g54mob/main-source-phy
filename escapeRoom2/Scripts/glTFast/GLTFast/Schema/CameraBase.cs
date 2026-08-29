using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class CameraBase<TOrthographic, TPerspective> : CameraBase where TOrthographic : CameraOrthographic where TPerspective : CameraPerspective
	{
		public TOrthographic orthographic;

		public TPerspective perspective;

		public override CameraOrthographic Orthographic => orthographic;

		public override CameraPerspective Perspective => perspective;
	}
	[Serializable]
	public abstract class CameraBase : NamedObject
	{
		public enum Type
		{
			Orthographic = 0,
			Perspective = 1
		}

		[Obsolete("Use GetCameraType and SetCameraType for access.")]
		public string type;

		private Type? m_TypeEnum;

		public abstract CameraOrthographic Orthographic { get; }

		public abstract CameraPerspective Perspective { get; }

		public Type GetCameraType()
		{
			if (m_TypeEnum.HasValue)
			{
				return m_TypeEnum.Value;
			}
			if (Enum.TryParse<Type>(type, ignoreCase: true, out var result))
			{
				m_TypeEnum = result;
				type = null;
				return m_TypeEnum.Value;
			}
			if (Orthographic != null)
			{
				m_TypeEnum = Type.Orthographic;
			}
			if (Perspective != null)
			{
				m_TypeEnum = Type.Perspective;
			}
			return m_TypeEnum ?? Type.Perspective;
		}

		public void SetCameraType(Type cameraType)
		{
			type = null;
			m_TypeEnum = cameraType;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			writer.AddProperty("type", m_TypeEnum.ToString().ToLowerInvariant());
			if (Perspective != null)
			{
				writer.AddProperty("perspective");
				Perspective.GltfSerialize(writer);
			}
			if (Orthographic != null)
			{
				writer.AddProperty("orthographic");
				Orthographic.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
