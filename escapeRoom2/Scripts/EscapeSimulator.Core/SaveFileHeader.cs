using System;

[Serializable]
public class SaveFileHeader
{
	public string gameVersion;

	public string levelVersion;

	public string levelIdentifier;

	public float playTimeInSeconds;

	public long lastSaveTimeInTicks;

	public DateTime lastSaveTimeUtc => new DateTime(lastSaveTimeInTicks);

	public DateTime lastSaveTimeLocal => TimeZoneInfo.ConvertTimeFromUtc(lastSaveTimeUtc, TimeZoneInfo.Local);

	public bool isCustomLevel
	{
		get
		{
			ulong result;
			return ulong.TryParse(levelIdentifier, out result);
		}
	}
}
