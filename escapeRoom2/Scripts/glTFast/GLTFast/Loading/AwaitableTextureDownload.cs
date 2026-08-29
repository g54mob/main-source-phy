using System;
using UnityEngine;
using UnityEngine.Networking;

namespace GLTFast.Loading
{
	public class AwaitableTextureDownload : AwaitableDownload, ITextureDownload, IDownload, IDisposable
	{
		public Texture2D Texture => (m_Request?.downloadHandler as DownloadHandlerTexture)?.texture;

		protected AwaitableTextureDownload()
		{
		}

		public AwaitableTextureDownload(Uri url, bool nonReadable)
		{
			Init(url, nonReadable);
		}

		protected static UnityWebRequest CreateRequest(Uri url, bool nonReadable)
		{
			return UnityWebRequestTexture.GetTexture(url, nonReadable);
		}

		private void Init(Uri url, bool nonReadable)
		{
			m_Request = CreateRequest(url, nonReadable);
			m_AsyncOperation = m_Request.SendWebRequest();
		}
	}
}
