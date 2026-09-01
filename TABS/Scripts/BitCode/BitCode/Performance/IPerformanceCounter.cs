namespace BitCode.Performance
{
	public interface IPerformanceCounter
	{
		int Count { get; }

		void Tick();
	}
	public interface IPerformanceCounter<out T, out TAverage> : IPerformanceCounter
	{
		T Current { get; }

		T Max { get; }

		T Min { get; }

		TAverage Average { get; }
	}
}
