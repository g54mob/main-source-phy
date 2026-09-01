using System;

namespace BitCode.AssetManagement
{
	public interface IResourceLoader : IContentLoader
	{
		bool LoadAllItems(string resourceLocation, bool async, Action<string> onLoadStart = null, Action<string, object[]> onLoadComplete = null, bool addToFront = false);

		bool LoadScene(string sceneName, bool async, Action<string> onLoadStart = null, Action<string, object> onLoadComplete = null, bool addToFront = true);
	}
}
