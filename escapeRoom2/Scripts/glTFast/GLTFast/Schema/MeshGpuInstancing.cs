using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class MeshGpuInstancing
	{
		[Serializable]
		public class Attributes
		{
			public int TRANSLATION = -1;

			public int ROTATION = -1;

			public int SCALE = -1;
		}

		public Attributes attributes;

		internal void GltfSerialize(JsonWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
