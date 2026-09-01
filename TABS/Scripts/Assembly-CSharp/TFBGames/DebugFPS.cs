using System.Text;
using BitCode.Performance;
using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class DebugFPS : MonoBehaviour
	{
		private enum Verbosity
		{
			None = 0,
			Low = 1,
			Medium = 2,
			High = 3,
			VeryHigh = 4
		}

		public float UpdateInterval = 1f;

		public TextMeshProUGUI DataTextComponent;

		private float nextUpdateTime;

		private IPerformanceCounter<double, double> frameTime;

		private IPerformanceCounter<double, double> cpuTime;

		private IPerformanceCounter<double, double> gpuTime;

		private IPerformanceCounter<long, double> allocatedMemory;

		private IPerformanceCounter<long, double> gcMemory;

		private DynamicResolutionManager dynamicResolutionManager;

		private PerformanceDetector performanceDetector;

		private StringBuilder stringBuilder;

		private MainCam mainCam;

		[SerializeField]
		private Verbosity verbosity = Verbosity.Low;
	}
}
