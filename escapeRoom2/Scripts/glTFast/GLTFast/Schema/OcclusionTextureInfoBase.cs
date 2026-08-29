using System;
using Unity.Mathematics;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class OcclusionTextureInfoBase<TExtensions> : OcclusionTextureInfoBase where TExtensions : TextureInfoExtensions, new()
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
	public abstract class OcclusionTextureInfoBase : TextureInfoBase
	{
		public float strength = 1f;

		internal override void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeTextureInfo(writer);
			if (math.abs(strength - 1f) > 0.001f)
			{
				writer.AddProperty("strength", strength);
			}
			writer.Close();
		}
	}
}
