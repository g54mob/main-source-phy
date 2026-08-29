using System;
using System.Collections.Generic;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class MeshBase<TExtras, TPrimitive> : MeshBase, ICloneable where TExtras : MeshExtras where TPrimitive : MeshPrimitiveBase
	{
		public TExtras extras;

		public TPrimitive[] primitives;

		public override MeshExtras Extras => extras;

		public override IReadOnlyList<MeshPrimitiveBase> Primitives => primitives;

		public object Clone()
		{
			MeshBase<TExtras, TPrimitive> meshBase = (MeshBase<TExtras, TPrimitive>)MemberwiseClone();
			if (Primitives != null)
			{
				meshBase.primitives = new TPrimitive[primitives.Length];
				for (int i = 0; i < primitives.Length; i++)
				{
					meshBase.primitives[i] = (TPrimitive)primitives[i].Clone();
				}
			}
			return meshBase;
		}
	}
	[Serializable]
	public abstract class MeshBase : NamedObject
	{
		public float[] weights;

		public abstract IReadOnlyList<MeshPrimitiveBase> Primitives { get; }

		public abstract MeshExtras Extras { get; }

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (Primitives != null)
			{
				writer.AddArray("primitives");
				foreach (MeshPrimitiveBase primitive in Primitives)
				{
					primitive.GltfSerialize(writer);
				}
				writer.CloseArray();
			}
			if (weights != null)
			{
				writer.AddArrayProperty("weights", weights);
			}
			if (Extras != null)
			{
				writer.AddProperty("extras");
				Extras.GltfSerialize(writer);
				writer.Close();
			}
			writer.Close();
		}
	}
}
