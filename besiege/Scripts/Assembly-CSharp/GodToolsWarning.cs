public class GodToolsWarning : WarningPopupBase
{
	public static GodToolsWarning current;

	protected override void Awake()
	{
		base.Awake();
		current = this;
	}

	public void CheatsEnabled()
	{
		if (!LevelAttributes.instance.sandBoxLevel)
		{
			ShowWarning();
		}
	}
}
