using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class AccessorSparseBase<TIndices, TValues> : AccessorSparseBase where TIndices : AccessorSparseIndices where TValues : AccessorSparseValues
	{
		public TIndices indices;

		public TValues values;

		public override AccessorSparseIndices Indices => indices;

		public override AccessorSparseValues Values => values;
	}
	[Serializable]
	public abstract class AccessorSparseBase
	{
		public int count;

		public abstract AccessorSparseIndices Indices { get; }

		public abstract AccessorSparseValues Values { get; }

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("count", count);
			if (Indices != null)
			{
				writer.AddProperty("indices");
				Indices.GltfSerialize(writer);
			}
			if (Values != null)
			{
				writer.AddProperty("values");
				Values.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
