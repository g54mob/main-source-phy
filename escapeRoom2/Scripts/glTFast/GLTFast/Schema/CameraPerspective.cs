using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class CameraPerspective
	{
		public float aspectRatio = -1f;

		public float yfov;

		public float zfar = -1f;

		public float znear;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (aspectRatio > 0f)
			{
				writer.AddProperty("aspectRatio", aspectRatio);
			}
			writer.AddProperty("yfov", yfov);
			if (zfar < float.MaxValue)
			{
				writer.AddProperty("zfar", zfar);
			}
			writer.AddProperty("znear", znear);
			writer.Close();
		}
	}
}
