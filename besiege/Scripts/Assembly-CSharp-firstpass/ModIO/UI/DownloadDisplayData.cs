using System;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct DownloadDisplayData
	{
		public long bytesReceived;

		public long bytesPerSecond;

		public long bytesTotal;

		public bool isActive;
	}
}
