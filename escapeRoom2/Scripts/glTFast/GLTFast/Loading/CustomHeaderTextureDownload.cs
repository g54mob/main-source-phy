using System;
using UnityEngine.Networking;

namespace GLTFast.Loading
{
	public class CustomHeaderTextureDownload : AwaitableTextureDownload
	{
		public CustomHeaderTextureDownload(Uri url, bool nonReadable, Action<UnityWebRequest> editor)
		{
			m_Request = AwaitableTextureDownload.CreateRequest(url, nonReadable);
			editor(m_Request);
			m_AsyncOperation = m_Request.SendWebRequest();
		}
	}
}
