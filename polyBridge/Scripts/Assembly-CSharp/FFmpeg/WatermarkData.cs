using System;

namespace FFmpeg
{
	[Serializable]
	public class WatermarkData : BaseData
	{
		public string imagePath;

		public float imageScale;

		public float xPosNormal;

		public float yPosNormal;
	}
}
