public class AchievementSetStatEvent
{
	private UserStat _003CUserStat_003Ek__BackingField;

	private int _003CProgress_003Ek__BackingField;

	public UserStat UserStat
	{
		get
		{
			return _003CUserStat_003Ek__BackingField;
		}
		set
		{
			_003CUserStat_003Ek__BackingField = value;
		}
	}

	public int Progress
	{
		get
		{
			return _003CProgress_003Ek__BackingField;
		}
		set
		{
			_003CProgress_003Ek__BackingField = value;
		}
	}

	public AchievementSetStatEvent(UserStat userStat, int progress)
	{
		_003CUserStat_003Ek__BackingField = userStat;
		_003CProgress_003Ek__BackingField = progress;
	}
}
