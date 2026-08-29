using System;
using Unity.Mathematics;

namespace GLTFast.Schema
{
	[Serializable]
	public class MaterialIor
	{
		public const float defaultIndexOfRefraction = 1.5f;

		public float ior = 1.5f;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (math.abs(ior - 1.5f) > 0.001f)
			{
				writer.AddProperty("ior", ior);
			}
			writer.Close();
		}
	}
}
