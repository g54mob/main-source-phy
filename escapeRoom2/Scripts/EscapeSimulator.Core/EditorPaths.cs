using System.Collections.Generic;
using UnityEngine;

public static class EditorPaths
{
	public class PropAssetBundleInfo
	{
		public string propPath;

		public string assetBundleName;

		public PropAssetBundleInfo(string propPath, string assetBundleName)
		{
			this.propPath = propPath;
			this.assetBundleName = assetBundleName;
		}
	}

	public const string ROOT_PATH = "Assets/_RoomEditor/";

	public const string ASSETS_META_PATH = "Assets/_RoomEditor/EditorAssetsMeta.asset";

	public const string PROP_ICONS_PATH = "Assets/_RoomEditor/EditorPropIcons";

	public const string SKYBOXES_PATH = "Assets/_RoomEditor/EditorSkyboxes";

	public const string DEFAULTS_PATH = "Assets/_RoomEditor/EditorDefaults";

	public const string MATERIALS_PATH = "Assets/_RoomEditor/EditorMaterials";

	public const string SCREENSHOT_SCENE_PATH = "Assets/_RoomEditor/RoomEditorPropsScreenshoter.unity";

	public const string SCREENSHOT_SCENE_NAME = "RoomEditorPropsScreenshoter";

	public const string SCREENSHOT_CAMERA_PATH = "Assets/_RoomEditor/RoomEditorScreenshotCamera.prefab";

	public const string SCREENSHOT_CAMERA_NAME = "RoomEditorScreenshotCamera";

	public static readonly List<PropAssetBundleInfo> propInfos = new List<PropAssetBundleInfo>
	{
		new PropAssetBundleInfo("Assets/_RoomEditor/EditorProps", "editorassets"),
		new PropAssetBundleInfo("Assets/_RoomEditor/EditorProps2", "editorassets2")
	};

	private static List<string> cachePropsPathOutput = new List<string>(2);

	public static string PROJECT_PATH => Application.dataPath.Substring(0, Application.dataPath.Length - "/Assets".Length);

	public static string getPropIconPath(string localPath)
	{
		return "Assets/_RoomEditor/EditorPropIcons/" + localPath;
	}

	public static string getSkyboxTexPath(string localPath)
	{
		return "Assets/_RoomEditor/EditorSkyboxes/" + localPath;
	}

	public static string getDefaultsTexPath(string localPath)
	{
		return "Assets/_RoomEditor/EditorDefaults/" + localPath;
	}

	public static List<string> getPropPaths(string localPath)
	{
		cachePropsPathOutput.Clear();
		if (!localPath.EndsWith(".prefab"))
		{
			localPath += ".prefab";
		}
		foreach (PropAssetBundleInfo propInfo in propInfos)
		{
			string item = propInfo.propPath + "/" + localPath;
			cachePropsPathOutput.Add(item);
		}
		return cachePropsPathOutput;
	}
}
