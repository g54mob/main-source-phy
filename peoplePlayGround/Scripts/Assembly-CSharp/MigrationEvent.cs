using System;

[Serializable]
public class MigrationEvent
{
	public enum Condition
	{
		OlderThan = 0,
		OlderThanOrEqualTo = 1,
		EqualTo = 2,
		NewerThan = 3,
		NewerThanOrEqualTo = 4
	}

	public string Version = "1.27.11";

	public Condition VersionCondition;

	public SpawnableAsset ToSpawnInstead;

	public bool IsApplicable(string originVersion)
	{
		if (ToSpawnInstead == null)
		{
			return false;
		}
		int difference = GameVersion.GetDifference(originVersion, Version);
		switch (VersionCondition)
		{
		case Condition.OlderThan:
			if (difference >= 0)
			{
				return false;
			}
			break;
		case Condition.OlderThanOrEqualTo:
			if (difference > 0)
			{
				return false;
			}
			break;
		case Condition.EqualTo:
			if (difference != 0)
			{
				return false;
			}
			break;
		case Condition.NewerThan:
			if (difference <= 0)
			{
				return false;
			}
			break;
		case Condition.NewerThanOrEqualTo:
			if (difference < 0)
			{
				return false;
			}
			break;
		}
		return true;
	}
}
