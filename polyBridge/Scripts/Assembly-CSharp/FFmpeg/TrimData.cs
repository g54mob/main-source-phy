using System;

namespace FFmpeg
{
	[Serializable]
	public class TrimData : BaseData
	{
		public string fromTime;

		public int durationSec;
	}
}
