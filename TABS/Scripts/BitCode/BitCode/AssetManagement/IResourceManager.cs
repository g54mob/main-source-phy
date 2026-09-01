using System;

namespace BitCode.AssetManagement
{
	public interface IResourceManager
	{
		object this[string resourceName] { get; }

		int Count { get; }

		void Unload(string resourceName, bool suppressUnloadUnused = false);

		void Unload(object asset, bool suppressUnloadUnused = false);

		void UnloadAllAssets();

		void Load(string resourceName, bool async = true, Action<string, object> onLoadComplete = null);

		void LoadAll(string resourceLocation, bool async = true, Action<string, object[]> onLoadComplete = null);

		void LoadScene(string sceneName, bool async = true, Action<string, object> onLoadComplete = null);

		void CancelLoad(string resourceName);

		bool IsLoaded(string resourceName);
	}
}
