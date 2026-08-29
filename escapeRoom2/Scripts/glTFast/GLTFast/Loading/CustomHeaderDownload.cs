using System;
using UnityEngine.Networking;

namespace GLTFast.Loading
{
	public class CustomHeaderDownload : AwaitableDownload
	{
		public CustomHeaderDownload(Uri url, Action<UnityWebRequest> editor)
		{
			m_Request = UnityWebRequest.Get(url);
			editor(m_Request);
			m_AsyncOperation = m_Request.SendWebRequest();
		}
	}
}
