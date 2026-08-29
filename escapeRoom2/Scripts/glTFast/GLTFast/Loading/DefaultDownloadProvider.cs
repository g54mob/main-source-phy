using System;
using System.Threading.Tasks;

namespace GLTFast.Loading
{
	public class DefaultDownloadProvider : IDownloadProvider
	{
		public async Task<IDownload> Request(Uri url)
		{
			AwaitableDownload req = new AwaitableDownload(url);
			await req.WaitAsync();
			return req;
		}

		public async Task<ITextureDownload> RequestTexture(Uri url, bool nonReadable)
		{
			AwaitableTextureDownload req = new AwaitableTextureDownload(url, nonReadable);
			await req.WaitAsync();
			return req;
		}
	}
}
