public class ClickPowerControl : ClickBehaviour
{
	public WirePower wirePower;

	public bool pressed;

	protected void Start()
	{
		pressed = false;
	}

	public override void OnClicked()
	{
		if (StatMaster.levelSimulating)
		{
			pressed = !pressed;
			wirePower.powerOn = pressed;
		}
	}
}
