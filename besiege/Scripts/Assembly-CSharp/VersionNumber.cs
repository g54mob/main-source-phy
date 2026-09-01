using UnityEngine;

public static class VersionNumber
{
	public const int Version = 177;

	private const string version = "1.77";

	private static string versionString;

	private static string changesetString = string.Empty;

	public static void SetChangeset(string changeset)
	{
		changesetString = changeset;
		FormatVersionString();
	}

	public static string GetVersionString()
	{
		if (string.IsNullOrEmpty(versionString))
		{
			LoadAdditionalVersionInfo();
			FormatVersionString();
		}
		return versionString;
	}

	public static int GetChangeset()
	{
		int result;
		if (!int.TryParse(changesetString, out result))
		{
			Debug.LogWarning("[VersionHandler] Changeset isn't a number: " + changesetString);
			return -1;
		}
		return result;
	}

	private static void FormatVersionString()
	{
		versionString = string.Format(ReferenceMaster.versionFormat, "1.77", changesetString);
	}

	private static void LoadAdditionalVersionInfo()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("version_info".Trim().Normalize());
		if (textAsset != null)
		{
			changesetString = textAsset.text;
		}
	}
}
