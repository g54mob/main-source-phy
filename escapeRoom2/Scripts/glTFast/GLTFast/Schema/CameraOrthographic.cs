using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class CameraOrthographic
	{
		public float xmag;

		public float ymag;

		public float zfar;

		public float znear;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("xmag", xmag);
			writer.AddProperty("ymag", ymag);
			writer.AddProperty("zfar", zfar);
			writer.AddProperty("znear", znear);
			writer.Close();
		}
	}
}
