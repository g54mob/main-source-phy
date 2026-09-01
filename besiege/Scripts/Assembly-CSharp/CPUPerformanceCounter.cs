public class CPUPerformanceCounter : PerformanceCounter
{
	public override void Update()
	{
		float cPULoad = SingleInstance<PerformanceAnalyser>.Instance.CPULoad;
		UpdateValue((long)cPULoad);
	}
}
