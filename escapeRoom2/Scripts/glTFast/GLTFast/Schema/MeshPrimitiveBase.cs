using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MeshPrimitiveBase<TExtensions> : MeshPrimitiveBase where TExtensions : MeshPrimitiveExtensions
	{
		public TExtensions extensions;

		public override MeshPrimitiveExtensions Extensions => extensions;

		internal override void UnsetExtensions()
		{
			extensions = null;
		}
	}
	[Serializable]
	public abstract class MeshPrimitiveBase : ICloneable, IMaterialsVariantsSlot
	{
		public Attributes attributes;

		public int indices = -1;

		public int material = -1;

		public DrawMode mode = DrawMode.Triangles;

		public MorphTarget[] targets;

		public abstract MeshPrimitiveExtensions Extensions { get; }

		public int GetMaterialIndex(int variantIndex)
		{
			MaterialsVariantsMeshPrimitiveExtension materialsVariantsMeshPrimitiveExtension = Extensions?.KHR_materials_variants;
			if (materialsVariantsMeshPrimitiveExtension != null && materialsVariantsMeshPrimitiveExtension.TryGetMaterialIndex(variantIndex, out var materialIndex))
			{
				return materialIndex;
			}
			return material;
		}

		internal abstract void UnsetExtensions();

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (attributes != null)
			{
				writer.AddProperty("attributes");
				attributes.GltfSerialize(writer);
			}
			if (indices >= 0)
			{
				writer.AddProperty("indices", indices);
			}
			if (material >= 0)
			{
				writer.AddProperty("material", material);
			}
			if (mode != DrawMode.Triangles)
			{
				writer.AddProperty("mode", (int)mode);
			}
			if (targets != null)
			{
				writer.AddArray("targets");
				MorphTarget[] array = targets;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].GltfSerialize(writer);
				}
				writer.CloseArray();
			}
			if (Extensions != null)
			{
				writer.AddProperty("extensions");
				Extensions.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
