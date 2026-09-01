public class ShowGuideBook : ClickBehaviour
{
	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClickReleased()
	{
		Open();
	}

	private void Open()
	{
		OverviewBlockMapper.Close();
		GuideBook.Open();
	}
}
