using System;
using UnityEngine;

namespace BitCode.AssetManagement
{
	public interface IAssetBundleManager
	{
		AssetBundle this[string bundleName] { get; }

		int Count { get; }

		event Action<string, AssetBundle> Loaded;

		event Action<string, bool> Unloaded;

		void LoadBundle(string bundleName, bool async = true, Action<string, AssetBundle> onLoadComplete = null);

		void UnloadBundle(string bundleName, bool unloadAssets = true);

		void UnloadAllBundles();

		bool IsLoaded(string bundleName);
	}
}
