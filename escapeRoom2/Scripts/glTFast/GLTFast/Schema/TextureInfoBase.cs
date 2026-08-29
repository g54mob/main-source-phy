using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class TextureInfoBase<TExtensions> : TextureInfoBase where TExtensions : TextureInfoExtensions, new()
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
	public abstract class TextureInfoBase
	{
		public int index = -1;

		public int texCoord;

		public abstract TextureInfoExtensions Extensions { get; }

		internal abstract void SetTextureTransform(TextureTransform textureTransform);

		internal void GltfSerializeTextureInfo(JsonWriter writer)
		{
			if (index >= 0)
			{
				writer.AddProperty("index", index);
			}
			if (texCoord > 0)
			{
				writer.AddProperty("texCoord", texCoord);
			}
			if (Extensions != null)
			{
				writer.AddProperty("extensions");
				Extensions.GltfSerialize(writer);
			}
		}

		internal virtual void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeTextureInfo(writer);
			writer.Close();
		}
	}
}
