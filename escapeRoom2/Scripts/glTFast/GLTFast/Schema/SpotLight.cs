using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class SpotLight
	{
		public float innerConeAngle;

		public float outerConeAngle = MathF.PI / 4f;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("innerConeAngle", innerConeAngle);
			writer.AddProperty("outerConeAngle", outerConeAngle);
			writer.Close();
		}
	}
}
