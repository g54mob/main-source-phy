public class FPSPerformanceCounter : PerformanceCounter
{
	public override void Update()
	{
		float fPS = SingleInstance<PerformanceAnalyser>.Instance.FPS;
		UpdateValue((long)fPS);
	}
}
