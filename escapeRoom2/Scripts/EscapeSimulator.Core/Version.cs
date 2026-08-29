using UnityEngine;

public static class Version
{
	private static AssetBundleVersions _instance;

	public static string fullGameVersion => baseGameVersion + (Is.Demo ? "d" : (Is.Release ? "r" : "n"));

	public static string baseGameVersion => "v" + Application.version;

	private static AssetBundleVersions assetBundleVersions
	{
		get
		{
			if (_instance == null)
			{
				_instance = AssetBundleLoader.getAsset<AssetBundleVersions>(AssetBundleType.Misc, "Assets/_Misc/AssetBundleVersions.asset");
			}
			return _instance;
		}
	}

	public static bool isBackwardsCompatibleWithVersion(string version)
	{
		string text = version;
		string text2 = text.Substring(1, text.Length - 1);
		text = "v5749";
		string text3 = text.Substring(1, text.Length - 1);
		text = baseGameVersion;
		string text4 = text.Substring(1, text.Length - 1);
		if (!int.TryParse(text2, out var result))
		{
			Debug.Log("error parsing " + text2);
			return false;
		}
		if (!int.TryParse(text3, out var result2))
		{
			Debug.Log("error parsing " + text3);
			return false;
		}
		if (!int.TryParse(text4, out var result3))
		{
			Debug.Log("error parsing " + text4);
			return false;
		}
		if (result >= result2)
		{
			return result <= result3;
		}
		return false;
	}

	public static int getAssetBundleVersion(string name)
	{
		AssetBundleVersions.Entry entry = assetBundleVersions.versions.Find((AssetBundleVersions.Entry roomVersion) => roomVersion.name.ToLower() == name.ToLower());
		if (entry == null)
		{
			if (Is.Forge)
			{
				Debug.LogError("Cannot find version for " + name);
			}
			return 0;
		}
		return entry.version;
	}

	public static int increaseAssetBundleVersion(string name)
	{
		return 0;
	}

	public static void increaseAllAssetBundleVersions()
	{
	}
}
