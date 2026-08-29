using System.Threading.Tasks;

namespace GLTFast.Loading
{
	internal abstract class TextureDownloadBase
	{
		public IDownload Download { get; protected set; }

		public abstract Task Load();
	}
}
