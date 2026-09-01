public class BlocksPerformanceCounter : PerformanceCounter
{
	public override void Update()
	{
		UpdateValue(StatMaster.BlockCount);
	}
}
