using System;
using System.Collections.Generic;

[Serializable]
public class PerformanceCounterSamples
{
	public List<PerformanceCounterSample> IdleSamples = new List<PerformanceCounterSample>();

	public List<PerformanceCounterSample> PostLoadSamples = new List<PerformanceCounterSample>();

	public List<PerformanceCounterSample> SimulationSamples = new List<PerformanceCounterSample>();

	public float TestDuration;
}
