using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class TextureBase<TExtensions> : TextureBase where TExtensions : TextureExtensions
	{
		public TExtensions extensions;

		public override TextureExtensions Extensions => extensions;

		internal override void UnsetExtensions()
		{
			extensions = null;
		}
	}
	[Serializable]
	public abstract class TextureBase : NamedObject
	{
		public int sampler = -1;

		public int source = -1;

		public abstract TextureExtensions Extensions { get; }

		public bool IsKtx => Extensions?.KHR_texture_basisu != null;

		public int GetImageIndex()
		{
			if (Extensions != null && Extensions.KHR_texture_basisu != null && Extensions.KHR_texture_basisu.source >= 0)
			{
				return Extensions.KHR_texture_basisu.source;
			}
			return source;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (source >= 0)
			{
				writer.AddProperty("source", source);
			}
			if (sampler >= 0)
			{
				writer.AddProperty("sampler", sampler);
			}
			if (Extensions != null)
			{
				writer.AddProperty("extensions");
				Extensions.GltfSerialize(writer);
			}
			writer.Close();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal abstract void UnsetExtensions();

		internal void JsonUtilityCleanup()
		{
			TextureExtensions extensions = Extensions;
			if (extensions != null && (extensions.KHR_texture_basisu?.source ?? (-1)) < 0)
			{
				extensions.KHR_texture_basisu = null;
				UnsetExtensions();
			}
		}
	}
}
