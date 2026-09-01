using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public abstract class DownloadDisplayComponent : MonoBehaviour
	{
		public abstract DownloadDisplayData data { get; set; }

		public abstract event Action<DownloadDisplayComponent> onClick;

		public abstract void Initialize();

		public abstract void DisplayDownload(FileDownloadInfo downloadInfo);
	}
}
