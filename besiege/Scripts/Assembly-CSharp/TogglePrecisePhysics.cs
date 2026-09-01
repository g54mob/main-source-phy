public class TogglePrecisePhysics : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.MorePrecisePhysics;
		}
		set
		{
			OptionsMaster.BesiegeConfig.MorePrecisePhysics = value;
		}
	}
}
