using System;
using Unity.Mathematics;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class NormalTextureInfoBase<TExtensions> : NormalTextureInfoBase where TExtensions : TextureInfoExtensions, new()
	{
		public TExtensions extensions;

		public override TextureInfoExtensions Extensions => extensions;

		internal override void SetTextureTransform(TextureTransform textureTransform)
		{
			extensions = extensions ?? new TExtensions();
			extensions.KHR_texture_transform = textureTransform;
		}
	}
	[Serializable]
	public abstract class NormalTextureInfoBase : TextureInfoBase
	{
		public float scale = 1f;

		internal override void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeTextureInfo(writer);
			if (math.abs(scale - 1f) > 0.001f)
			{
				writer.AddProperty("scale", scale);
			}
			writer.Close();
		}
	}
}
