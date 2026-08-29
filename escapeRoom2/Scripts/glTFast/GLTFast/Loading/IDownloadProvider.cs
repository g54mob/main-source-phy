using System;
using System.Threading.Tasks;

namespace GLTFast.Loading
{
	public interface IDownloadProvider
	{
		Task<IDownload> Request(Uri url);

		Task<ITextureDownload> RequestTexture(Uri url, bool nonReadable);
	}
}
