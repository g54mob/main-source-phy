namespace BitCode.Performance
{
	public interface IPerformanceDetector
	{
		MeasuredPerformanceState State { get; }
	}
}
