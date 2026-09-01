using System;
using System.Collections.Generic;
using BitCode.Extensions;
using BitCode.Performance;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Debug.UI
{
	public class PerformanceMonitor<TLabel> : MonoBehaviour
	{
		private struct lthOtidQHooYreqLHnUerPNzAGqZ
		{
			public IDisplayableMetric qLbgfPLwtdYLxGDLmUxpqLKCNbdw;

			public TLabel FrnzhsNEcZhUuEgqFKNlkWaXsVzh;

			public Action<TLabel, string, Color> xSsGyVcrNkWAjCZTjdzDYqtvKrWHA;
		}

		[SerializeField]
		[Tooltip("How frequently, in realtime seconds, to update displayed labels. If negative, updating is paused, if zero, updates every frame.")]
		private float uiRefreshInterval = 0.5f;

		private float uiRefreshTimer;

		private readonly List<lthOtidQHooYreqLHnUerPNzAGqZ> trackedMetrics = new List<lthOtidQHooYreqLHnUerPNzAGqZ>();

		protected virtual void Update()
		{
			if (uiRefreshInterval < 0f)
			{
				goto IL_000d;
			}
			goto IL_0049;
			IL_000d:
			int num = 1830355642;
			goto IL_0012;
			IL_0012:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x6E4995AD)) % 5)
			{
			case 0u:
				break;
			case 4u:
				return;
			case 1u:
				goto IL_0049;
			case 2u:
				return;
			default:
			{
				uiRefreshTimer = uiRefreshInterval;
				using (List<lthOtidQHooYreqLHnUerPNzAGqZ>.Enumerator enumerator = trackedMetrics.GetEnumerator())
				{
					while (true)
					{
						int num3;
						int num4;
						if (enumerator.MoveNext())
						{
							num3 = 429144916;
							num4 = num3;
						}
						else
						{
							num3 = 688119806;
							num4 = num3;
						}
						while (true)
						{
							switch ((num2 = (uint)(num3 ^ 0x6E4995AD)) % 4)
							{
							case 0u:
								num3 = 429144916;
								continue;
							default:
								return;
							case 1u:
							{
								lthOtidQHooYreqLHnUerPNzAGqZ current = enumerator.Current;
								IDisplayableMetric qLbgfPLwtdYLxGDLmUxpqLKCNbdw = current.qLbgfPLwtdYLxGDLmUxpqLKCNbdw;
								EventExtensions.SafelyInvoke(arg1: current.FrnzhsNEcZhUuEgqFKNlkWaXsVzh, arg2: qLbgfPLwtdYLxGDLmUxpqLKCNbdw.ToString(), arg3: qLbgfPLwtdYLxGDLmUxpqLKCNbdw.DisplayColor, self: current.xSsGyVcrNkWAjCZTjdzDYqtvKrWHA);
								num3 = 435967263;
								continue;
							}
							case 2u:
								break;
							case 3u:
								return;
							}
							break;
						}
					}
				}
			}
			}
			goto IL_000d;
			IL_0049:
			uiRefreshTimer -= Time.unscaledDeltaTime;
			int num5;
			if (uiRefreshTimer > 0f)
			{
				num = 1867828391;
				num5 = num;
			}
			else
			{
				num = 1086460656;
				num5 = num;
			}
			goto IL_0012;
		}

		protected virtual void OnDestroy()
		{
			using (List<lthOtidQHooYreqLHnUerPNzAGqZ>.Enumerator enumerator = trackedMetrics.GetEnumerator())
			{
				while (true)
				{
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = -1800758185;
						num2 = num;
					}
					else
					{
						num = -1171717811;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -13876826)) % 4)
						{
						case 2u:
							num = -1171717811;
							continue;
						default:
							return;
						case 3u:
							enumerator.Current.qLbgfPLwtdYLxGDLmUxpqLKCNbdw.TryDispose();
							num = -1565194322;
							continue;
						case 0u:
							break;
						case 1u:
							return;
						}
						break;
					}
				}
			}
		}

		public PerformanceMonitor<TLabel> AddMetric([NotNull] TLabel label, [NotNull] IDisplayableMetric metric, [NotNull] Action<TLabel, string, Color> refreshFunction)
		{
			if (label == null)
			{
				goto IL_000b;
			}
			goto IL_00df;
			IL_000b:
			int num = -1094674866;
			goto IL_0010;
			IL_0010:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -2098830174)) % 8)
				{
				case 7u:
					break;
				case 4u:
					throw new ArgumentNullException("label");
				case 0u:
					throw new ArgumentNullException("refreshFunction");
				case 1u:
					goto IL_0078;
				case 6u:
					trackedMetrics.Add(new lthOtidQHooYreqLHnUerPNzAGqZ
					{
						FrnzhsNEcZhUuEgqFKNlkWaXsVzh = label,
						qLbgfPLwtdYLxGDLmUxpqLKCNbdw = metric,
						xSsGyVcrNkWAjCZTjdzDYqtvKrWHA = refreshFunction
					});
					num = -1699594919;
					continue;
				case 2u:
					throw new ArgumentNullException("metric");
				case 5u:
					goto IL_00df;
				default:
					return this;
				}
				break;
				IL_0078:
				int num3;
				if (refreshFunction == null)
				{
					num = -804782966;
					num3 = num;
				}
				else
				{
					num = -2141462812;
					num3 = num;
				}
			}
			goto IL_000b;
			IL_00df:
			int num4;
			if (metric != null)
			{
				num = -2008777941;
				num4 = num;
			}
			else
			{
				num = -1249697280;
				num4 = num;
			}
			goto IL_0010;
		}

		public PerformanceMonitor<TLabel> AddDefaultMetrics([NotNull] PerformanceCounters performanceCounters, [NotNull] Func<TLabel> labelFactory, [NotNull] Action<TLabel, string, Color> refreshFunction, bool sixtyFpsTarget)
		{
			if (performanceCounters == null)
			{
				goto IL_0006;
			}
			goto IL_009d;
			IL_0006:
			int num = 1440993494;
			goto IL_000b;
			IL_000b:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x7FAEBCFD)) % 7)
				{
				case 0u:
					break;
				case 5u:
					goto IL_0038;
				case 1u:
					throw new ArgumentNullException("labelFactory");
				case 3u:
					throw new ArgumentNullException("refreshFunction");
				case 6u:
					throw new ArgumentNullException("performanceCounters");
				case 4u:
					goto IL_009d;
				default:
					return AddFrameTimeCounter(performanceCounters, labelFactory(), refreshFunction, sixtyFpsTarget).AddCpuTimeCounter(performanceCounters, labelFactory(), refreshFunction, sixtyFpsTarget).AddGpuTimeCounter(performanceCounters, labelFactory(), refreshFunction, sixtyFpsTarget).AddHeapMemoryCounter(performanceCounters, labelFactory(), refreshFunction);
				}
				break;
				IL_0038:
				int num3;
				if (refreshFunction == null)
				{
					num = 29292896;
					num3 = num;
				}
				else
				{
					num = 705467428;
					num3 = num;
				}
			}
			goto IL_0006;
			IL_009d:
			int num4;
			if (labelFactory != null)
			{
				num = 1076583736;
				num4 = num;
			}
			else
			{
				num = 65403393;
				num4 = num;
			}
			goto IL_000b;
		}

		public PerformanceMonitor<TLabel> AddFrameTimeCounter(PerformanceCounters performanceCounters, TLabel label, Action<TLabel, string, Color> refreshFunction, bool sixtyFpsTarget)
		{
			AddMetric(label, DisplayablePerformanceCounter.ForMillisecondMetric(performanceCounters.FrameTimeCounter, sixtyFpsTarget ? ThresholdColoriser.SixtyFpsMsColoriser : ThresholdColoriser.ThirtyFpsMsColoriser, "FPS: "), refreshFunction);
			return this;
		}

		public PerformanceMonitor<TLabel> AddCpuTimeCounter(PerformanceCounters performanceCounters, TLabel label, Action<TLabel, string, Color> refreshFunction, bool sixtyFpsTarget)
		{
			AddMetric(label, DisplayablePerformanceCounter.ForMillisecondMetric(performanceCounters.CpuCounter, sixtyFpsTarget ? ThresholdColoriser.SixtyFpsMsColoriser : ThresholdColoriser.ThirtyFpsMsColoriser, "CPU: ", null, includePerSecond: false), refreshFunction);
			return this;
		}

		public PerformanceMonitor<TLabel> AddGpuTimeCounter(PerformanceCounters performanceCounters, TLabel label, Action<TLabel, string, Color> refreshFunction, bool sixtyFpsTarget)
		{
			AddMetric(label, DisplayablePerformanceCounter.ForMillisecondMetric(performanceCounters.GpuCounter, sixtyFpsTarget ? ThresholdColoriser.SixtyFpsMsColoriser : ThresholdColoriser.ThirtyFpsMsColoriser, "GPU: ", null, includePerSecond: false), refreshFunction);
			return this;
		}

		public PerformanceMonitor<TLabel> AddHeapMemoryCounter(PerformanceCounters performanceCounters, TLabel label, Action<TLabel, string, Color> refreshFunction)
		{
			AddMetric(label, DisplayablePerformanceCounter.ForByteMetric(performanceCounters.AllocatedMemoryCounter, null, "MEM: "), refreshFunction);
			return this;
		}
	}
}
