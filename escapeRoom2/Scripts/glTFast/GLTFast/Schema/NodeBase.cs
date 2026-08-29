using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class NodeBase<TExtensions> : NodeBase where TExtensions : NodeExtensions
	{
		public TExtensions extensions;

		public override NodeExtensions Extensions => extensions;

		internal override void UnsetExtensions()
		{
			extensions = null;
		}
	}
	[Serializable]
	public abstract class NodeBase : NamedObject
	{
		public uint[] children;

		public int mesh = -1;

		public float[] matrix;

		public float[] rotation;

		public float[] scale;

		public float[] translation;

		public int skin = -1;

		public int camera = -1;

		public abstract NodeExtensions Extensions { get; }

		internal abstract void UnsetExtensions();

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (children != null)
			{
				writer.AddArrayProperty("children", children);
			}
			if (mesh >= 0)
			{
				writer.AddProperty("mesh", mesh);
			}
			if (translation != null)
			{
				writer.AddArrayProperty("translation", translation);
			}
			if (rotation != null)
			{
				writer.AddArrayProperty("rotation", rotation);
			}
			if (scale != null)
			{
				writer.AddArrayProperty("scale", scale);
			}
			if (matrix != null)
			{
				writer.AddArrayProperty("matrix", matrix);
			}
			if (skin >= 0)
			{
				writer.AddProperty("skin", skin);
			}
			if (camera >= 0)
			{
				writer.AddProperty("camera", camera);
			}
			if (Extensions != null)
			{
				writer.AddProperty("extensions");
				Extensions.GltfSerialize(writer);
			}
			writer.Close();
		}

		public virtual void JsonUtilityCleanup()
		{
			NodeExtensions extensions = Extensions;
			if (extensions != null)
			{
				if (extensions.EXT_mesh_gpu_instancing?.attributes == null)
				{
					extensions.EXT_mesh_gpu_instancing = null;
				}
				if ((extensions.KHR_lights_punctual?.light ?? (-1)) < 0)
				{
					extensions.KHR_lights_punctual = null;
				}
				if (extensions.EXT_mesh_gpu_instancing == null && extensions.KHR_lights_punctual == null)
				{
					UnsetExtensions();
				}
			}
		}
	}
}
