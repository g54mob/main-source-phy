using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MorphTarget
	{
		public int POSITION = -1;

		public int NORMAL = -1;

		public int TANGENT = -1;

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			if (POSITION >= 0)
			{
				writer.AddProperty("POSITION", POSITION);
			}
			if (NORMAL >= 0)
			{
				writer.AddProperty("NORMAL", NORMAL);
			}
			if (TANGENT >= 0)
			{
				writer.AddProperty("TANGENT", TANGENT);
			}
		}
	}
}
