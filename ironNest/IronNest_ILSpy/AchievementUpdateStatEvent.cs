public class AchievementUpdateStatEvent
{
	private UserStat _003CUserStat_003Ek__BackingField;

	private int _003CProgressDifference_003Ek__BackingField;

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

	public int ProgressDifference
	{
		get
		{
			return _003CProgressDifference_003Ek__BackingField;
		}
		set
		{
			_003CProgressDifference_003Ek__BackingField = value;
		}
	}

	public AchievementUpdateStatEvent(UserStat userStat, int progressDifference)
	{
		_003CUserStat_003Ek__BackingField = userStat;
		_003CProgressDifference_003Ek__BackingField = progressDifference;
	}
}
