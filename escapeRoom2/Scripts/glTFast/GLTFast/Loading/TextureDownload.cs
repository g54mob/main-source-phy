using System.Threading.Tasks;

namespace GLTFast.Loading
{
	internal class TextureDownload<T> : TextureDownloadBase where T : IDownload
	{
		private Task<T> m_Task;

		public TextureDownload(Task<T> task)
		{
			m_Task = task;
		}

		public override async Task Load()
		{
			base.Download = await m_Task;
		}
	}
}
