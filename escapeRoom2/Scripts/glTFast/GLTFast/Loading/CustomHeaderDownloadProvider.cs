using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace GLTFast.Loading
{
	public class CustomHeaderDownloadProvider : IDownloadProvider
	{
		private HttpHeader[] m_Headers;

		public CustomHeaderDownloadProvider(HttpHeader[] headers)
		{
			m_Headers = headers;
		}

		public async Task<IDownload> Request(Uri url)
		{
			CustomHeaderDownload req = new CustomHeaderDownload(url, RegisterHttpHeaders);
			await req.WaitAsync();
			return req;
		}

		public async Task<ITextureDownload> RequestTexture(Uri url, bool nonReadable)
		{
			CustomHeaderTextureDownload req = new CustomHeaderTextureDownload(url, nonReadable, RegisterHttpHeaders);
			await req.WaitAsync();
			return req;
		}

		private void RegisterHttpHeaders(UnityWebRequest request)
		{
			if (m_Headers != null)
			{
				HttpHeader[] headers = m_Headers;
				for (int i = 0; i < headers.Length; i++)
				{
					HttpHeader httpHeader = headers[i];
					request.SetRequestHeader(httpHeader.Key, httpHeader.Value);
				}
			}
		}
	}
}
