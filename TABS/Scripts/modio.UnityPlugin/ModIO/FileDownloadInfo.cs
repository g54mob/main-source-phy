using System;
using UnityEngine.Networking;

namespace ModIO
{
	[Serializable]
	public class FileDownloadInfo
	{
		public UnityWebRequest request;

		public WebRequestError error;

		public string target;

		public long fileSize;

		public bool isDone;

		public bool wasAborted;

		public long bytesPerSecond;
	}
}
