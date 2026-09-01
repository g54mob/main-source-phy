using System;
using BitCode.Performance;
using JetBrains.Annotations;

namespace TFBGames
{
	public class PerformanceCounterService : IService
	{
		public enum FrameRateTarget
		{
			Sixty = 0,
			Thirty = 1
		}

		public const float SixtyFrameTarget = 16.2f;

		public const float SixtyGpuTarget = 15.8f;

		public const float ThirtyFrameTarget = 32.8f;

		public const float ThirtyComponentTarget = 32.5f;

		public readonly PerformanceCounters Counters;

		[CanBeNull]
		public readonly PerformanceDetector PerformanceDetector;

		[CanBeNull]
		public readonly DynamicResolutionManager DynamicResolutionManager;

		public PerformanceCounterService(SettingsProfileManager settingsManager, PerformanceCounters counters, [CanBeNull] PerformanceDetector performanceDetector, [CanBeNull] DynamicResolutionManager dynamicResolutionManager)
		{
			settingsManager.SettingsProfileChanged += OnSettingsProfileChanged;
			Counters = counters;
			PerformanceDetector = performanceDetector;
			DynamicResolutionManager = dynamicResolutionManager;
		}

		private void OnSettingsProfileChanged(SettingsProfile obj)
		{
			if (obj.FrameRateTarget.HasValue)
			{
				SetFrameRateTarget(obj.FrameRateTarget.Value);
			}
		}

		public void SetFrameRateTarget(FrameRateTarget target)
		{
			if (PerformanceDetector != null)
			{
				switch (target)
				{
				case FrameRateTarget.Sixty:
					PerformanceDetector.FrameTimeTarget = 16.2f;
					PerformanceDetector.GpuTimeTarget = 15.8f;
					PerformanceDetector.CpuTimeTarget = 15.8f;
					break;
				case FrameRateTarget.Thirty:
					PerformanceDetector.FrameTimeTarget = 32.8f;
					PerformanceDetector.GpuTimeTarget = 32.5f;
					PerformanceDetector.CpuTimeTarget = 32.5f;
					break;
				default:
					throw new ArgumentOutOfRangeException("target", target, null);
				}
			}
		}

		void IService.OnRegister()
		{
		}

		void IService.OnAwake()
		{
		}

		void IService.OnStart()
		{
		}

		void IService.OnUpdate()
		{
		}

		void IService.OnFixedUpdate()
		{
		}

		void IService.OnLateUpdate()
		{
		}

		void IService.UnRegister()
		{
		}
	}
}
