using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class AssetBundleLoader
{
	public static bool useAssetBundles = true;

	private static List<string> loadedSceneBundles = new List<string>();

	private static Dictionary<string, AssetBundleCreateRequest> currentLoadingAssetBundles = new Dictionary<string, AssetBundleCreateRequest>();

	private static Dictionary<string, object> currentLoadingOrLoadedAssets = new Dictionary<string, object>();

	private static Dictionary<AssetBundleType, AssetBundle> loadedAssetBundles = new Dictionary<AssetBundleType, AssetBundle>();

	public static string getAssetBundleNameForScene(string sceneName)
	{
		if (sceneName == "SecretLevel1")
		{
			return "_internal";
		}
		return sceneName;
	}

	public static AssetBundleCreateRequest loadAssetBundleAsync(AssetBundleType bundleType)
	{
		string text = Path.Combine(Application.streamingAssetsPath, "AssetBundles", bundleType.ToString().ToLower());
		if (currentLoadingAssetBundles.TryGetValue(text, out var value))
		{
			return value;
		}
		Debug.Log("Loading async new asset bundle from path: " + text);
		AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(text);
		currentLoadingAssetBundles.Add(text, assetBundleCreateRequest);
		return assetBundleCreateRequest;
	}

	public static AssetBundle loadAssetBundle(AssetBundleType bundleType)
	{
		if (!useAssetBundles)
		{
			return null;
		}
		if (loadedAssetBundles.TryGetValue(bundleType, out var value))
		{
			return value;
		}
		string text = Path.Combine(Application.streamingAssetsPath, "AssetBundles", bundleType.ToString().ToLower());
		Debug.Log("Loading new asset bundle from path: " + text);
		value = AssetBundle.LoadFromFile(text);
		loadedAssetBundles[bundleType] = value;
		return value;
	}

	public static void startLoadScene(string sceneName)
	{
		Debug.Log("startLoadScene: " + sceneName);
		if (useAssetBundles)
		{
			DLC dLC = Menu.findDlcFromName(sceneName);
			if (!loadedSceneBundles.Contains(sceneName))
			{
				string sceneAssetBundleBasePath = getSceneAssetBundleBasePath(dLC);
				Debug.Log($"[DLC LOADING BUG]: startLoadScene({sceneName}, {dLC}) asset bundle path = {sceneAssetBundleBasePath}");
				string assetBundleNameForScene = getAssetBundleNameForScene(sceneName);
				AssetBundleCreateRequest value = AssetBundle.LoadFromFileAsync(Path.Combine(sceneAssetBundleBasePath, assetBundleNameForScene.ToLower()));
				currentLoadingAssetBundles.Add(sceneName, value);
			}
			else
			{
				currentLoadingAssetBundles.Add(sceneName, null);
			}
		}
		else
		{
			if (!loadedSceneBundles.Contains(sceneName))
			{
				loadedSceneBundles.Add(sceneName);
			}
			currentLoadingAssetBundles.Add(sceneName, null);
		}
	}

	public static string getSceneAssetBundleBasePath(DLC dlc)
	{
		if (Is.Android)
		{
			return "/sdcard/Android/obb/com.PineStudio.EscapeSimulator";
		}
		if (dlc != DLC.None)
		{
			return Menu.dlcBasePath();
		}
		return Path.Combine(Application.streamingAssetsPath, "AssetBundles", "Levels");
	}

	public static bool processLoadingScene(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			return false;
		}
		bool result = false;
		if (loadedSceneBundles.Contains(sceneName))
		{
			if (currentLoadingAssetBundles.ContainsKey(sceneName))
			{
				currentLoadingAssetBundles.Remove(sceneName);
				result = true;
			}
		}
		else
		{
			AssetBundleCreateRequest assetBundleCreateRequest = currentLoadingAssetBundles[sceneName];
			Debug.Log(assetBundleCreateRequest.isDone + " " + assetBundleCreateRequest.progress);
			if (assetBundleCreateRequest.isDone)
			{
				loadedSceneBundles.Add(sceneName);
				currentLoadingAssetBundles.Remove(sceneName);
				result = true;
			}
		}
		return result;
	}

	public static T getAsset<T>(AssetBundleType type, string path) where T : Object
	{
		T result = null;
		if (currentLoadingOrLoadedAssets.TryGetValue(path, out var value))
		{
			if (value == null)
			{
				return null;
			}
			if (value is AssetBundleAsyncRequest<T> assetBundleAsyncRequest)
			{
				return assetBundleAsyncRequest.asset;
			}
			return value as T;
		}
		if (useAssetBundles)
		{
			return loadAssetBundle(type).LoadAsset<T>(path);
		}
		return result;
	}

	public static AssetBundleAsyncRequest<T> getAssetAsync<T>(AssetBundleType type, string path) where T : Object
	{
		AssetBundleAsyncRequest<T> output = new AssetBundleAsyncRequest<T>();
		if (currentLoadingOrLoadedAssets.TryGetValue(path, out var value))
		{
			output = (AssetBundleAsyncRequest<T>)value;
			return output;
		}
		if (useAssetBundles)
		{
			AssetBundleCreateRequest bundleRequest = loadAssetBundleAsync(type);
			Debug.Log($"START LOADING ASSET BUNDLE {type}");
			bundleRequest.completed += delegate
			{
				Debug.Log($"END LOADING ASSET BUNDLE {type}");
				if (!loadedAssetBundles.ContainsKey(type))
				{
					loadedAssetBundles[type] = bundleRequest.assetBundle;
				}
				output.request = bundleRequest.assetBundle.LoadAssetAsync<T>(path);
			};
		}
		else
		{
			output = new AssetBundleAsyncRequest<T>
			{
				asset = getAsset<T>(type, path)
			};
		}
		output.frameCountWhenCreated = Time.frameCount;
		currentLoadingOrLoadedAssets.Add(path, output);
		return output;
	}
}
