using GameGrind;

public class OpenAchievementsList : ClickBehaviour
{
	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClickReleased()
	{
		ToggleAchievementList();
	}

	private void ToggleAchievementList()
	{
		if (SingleInstance<AchievementUIList>.hasInstance())
		{
			SingleInstance<AchievementUIList>.Instance.TogglePanel();
		}
	}
}
