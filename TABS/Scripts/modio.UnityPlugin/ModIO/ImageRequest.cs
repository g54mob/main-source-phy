using System;
using UnityEngine;

namespace ModIO
{
	public class ImageRequest
	{
		public Texture2D imageTexture;

		public string filePath;

		public bool isDone;

		public WebRequestError error;

		public event Action<ImageRequest> succeeded;

		public event Action<ImageRequest> failed;

		internal void NotifySucceeded()
		{
			if (this.succeeded != null)
			{
				this.succeeded(this);
			}
		}

		internal void NotifyFailed()
		{
			if (this.failed != null)
			{
				this.failed(this);
			}
		}
	}
}
