public class AchievementUnlockEvent
{
	private AchievementType _003CAchievementType_003Ek__BackingField;

	public AchievementType AchievementType
	{
		get
		{
			return _003CAchievementType_003Ek__BackingField;
		}
		set
		{
			_003CAchievementType_003Ek__BackingField = value;
		}
	}

	public AchievementUnlockEvent(AchievementType achievementType)
	{
		_003CAchievementType_003Ek__BackingField = achievementType;
	}
}
