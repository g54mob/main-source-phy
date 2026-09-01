using System.Collections.Generic;
using System.Linq;

public abstract class PerformanceCounter
{
	private List<long> valueHistory = new List<long>();

	private long currentValue;

	private long averageValue;

	private long lowestValue = long.MaxValue;

	private long highestValue = long.MinValue;

	public long Highest
	{
		get
		{
			return highestValue;
		}
	}

	public long Lowest
	{
		get
		{
			return lowestValue;
		}
	}

	public long Average
	{
		get
		{
			return averageValue;
		}
	}

	public long Value
	{
		get
		{
			return currentValue;
		}
	}

	protected void UpdateValue(long value)
	{
		if (value != 0L && value != long.MaxValue && value != long.MinValue)
		{
			if (value < lowestValue)
			{
				lowestValue = value;
			}
			currentValue = value;
			if (value > highestValue)
			{
				highestValue = value;
			}
			CalculateAverage();
		}
	}

	private void CalculateAverage()
	{
		valueHistory.Add(currentValue);
		averageValue = (long)valueHistory.Select((long item) => item).Average();
	}

	public abstract void Update();

	public void Clear()
	{
		lowestValue = long.MaxValue;
		highestValue = long.MinValue;
		valueHistory = new List<long>();
		currentValue = 0L;
	}
}
