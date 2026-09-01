public class OpenKeyOverview : ClickBehaviour
{
	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if (!(machine == null) && !machine.isSimulating && OverviewBlockMapper.CurrentInstance == null)
		{
			OverviewBlockMapper.Open(machine);
		}
	}
}
