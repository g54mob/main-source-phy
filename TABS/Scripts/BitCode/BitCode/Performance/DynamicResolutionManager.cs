using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Performance
{
	public class DynamicResolutionManager : IDisposable, IUpdateableService
	{
		[CompilerGenerated]
		private MeasuredPerformanceState KLrVAGHEiwFyXlZNHWxzFUMgtJKu;

		[CompilerGenerated]
		private float hDLlHLgyPyBxxRaHXznEBstYBvUeA;

		[CompilerGenerated]
		private float? ewzKIWaDaftTMGtAgwnrYDsjPuUB;

		private readonly IPerformanceDetector FAyHPlBtQBCaxgvxCIJOlKLJQbOA;

		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		private readonly float iZeIVBautqgEhaqsWnHHQeVEQYSS;

		private readonly float oYzzyKPFXDfzfqlJjDooSiBdOPyd;

		private readonly float rvlKNIhjoMhysuEQQExwsaBdJBrU;

		private readonly float QVpoWpfjGMBVgHZGsNjwjYBANikK;

		private readonly ulong VRVESvJQqaziaZakxRrLgKyJKypG;

		private readonly ulong qHUGZRrvOHZoktDWDMtGZqvrAkeC;

		private ulong OUQKVqUtUaXzFFosEnbCjWEbpTKi;

		private ulong nHnWuOUmyOrlXpCPOEuouXMVPNIe;

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		public MeasuredPerformanceState CurrentPerformanceState
		{
			[CompilerGenerated]
			get
			{
				return KLrVAGHEiwFyXlZNHWxzFUMgtJKu;
			}
			[CompilerGenerated]
			private set
			{
				KLrVAGHEiwFyXlZNHWxzFUMgtJKu = kLrVAGHEiwFyXlZNHWxzFUMgtJKu;
			}
		}

		public float CurrentRenderScale
		{
			[CompilerGenerated]
			get
			{
				return hDLlHLgyPyBxxRaHXznEBstYBvUeA;
			}
			[CompilerGenerated]
			private set
			{
				hDLlHLgyPyBxxRaHXznEBstYBvUeA = num;
			}
		}

		public float? OverrideRenderScale
		{
			[CompilerGenerated]
			get
			{
				return ewzKIWaDaftTMGtAgwnrYDsjPuUB;
			}
			[CompilerGenerated]
			set
			{
				ewzKIWaDaftTMGtAgwnrYDsjPuUB = value;
			}
		}

		public DynamicResolutionManager([NotNull] IServiceUpdater serviceUpdater, [NotNull] IPerformanceDetector performanceDetector, float minScale = 0.8f, float maxScale = 1f, float scaleChangeAmountPerFrame = 0.01f, ulong increaseScaleFrameInterval = 2uL, ulong decreaseScaleFrameInterval = 2uL, float cpuScalingFactor = 0.5f)
		{
			while (true)
			{
				int num = 1848988096;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5A5D2F50)) % 22)
					{
					case 18u:
						break;
					default:
						return;
					case 4u:
						UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = serviceUpdater ?? throw new ArgumentNullException("serviceUpdater");
						FAyHPlBtQBCaxgvxCIJOlKLJQbOA = performanceDetector ?? throw new ArgumentNullException("performanceDetector");
						num = 125477791;
						continue;
					case 14u:
					{
						int num13;
						int num14;
						if (scaleChangeAmountPerFrame <= 0f)
						{
							num13 = -2103209794;
							num14 = num13;
						}
						else
						{
							num13 = -162702720;
							num14 = num13;
						}
						num = num13 ^ (int)(num2 * 2051046689);
						continue;
					}
					case 16u:
					{
						int num4;
						if (minScale > maxScale)
						{
							num = 1022319230;
							num4 = num;
						}
						else
						{
							num = 1324488019;
							num4 = num;
						}
						continue;
					}
					case 6u:
					{
						int num12;
						if (!(cpuScalingFactor <= 1f))
						{
							num = 1571285899;
							num12 = num;
						}
						else
						{
							num = 630623636;
							num12 = num;
						}
						continue;
					}
					case 5u:
						nHnWuOUmyOrlXpCPOEuouXMVPNIe = 0uL;
						CurrentRenderScale = maxScale;
						num = ((int)num2 * -180039209) ^ -873719174;
						continue;
					case 2u:
					{
						int num5;
						int num6;
						if (!(minScale < 1f))
						{
							num5 = -1440722306;
							num6 = num5;
						}
						else
						{
							num5 = -2112731425;
							num6 = num5;
						}
						num = num5 ^ (int)(num2 * 240886056);
						continue;
					}
					case 20u:
					{
						int num8;
						int num9;
						if (cpuScalingFactor <= 0f)
						{
							num8 = 616243843;
							num9 = num8;
						}
						else
						{
							num8 = 146304538;
							num9 = num8;
						}
						num = num8 ^ ((int)num2 * -1293703454);
						continue;
					}
					case 11u:
						throw new ArgumentOutOfRangeException("maxScale", "Parameter must be greater than 0 and less than or equal to 1.");
					case 8u:
					{
						int num15;
						int num16;
						if (maxScale > 0f)
						{
							num15 = 1154463200;
							num16 = num15;
						}
						else
						{
							num15 = 1599772297;
							num16 = num15;
						}
						num = num15 ^ (int)(num2 * 1610339115);
						continue;
					}
					case 7u:
					{
						int num10;
						int num11;
						if (minScale > 0f)
						{
							num10 = 482371829;
							num11 = num10;
						}
						else
						{
							num10 = 1409019192;
							num11 = num10;
						}
						num = num10 ^ ((int)num2 * -41395750);
						continue;
					}
					case 0u:
						throw new ArgumentOutOfRangeException("minScale", "Parameter must be greater than 0 and less than 1.");
					case 3u:
						OUQKVqUtUaXzFFosEnbCjWEbpTKi = 0uL;
						num = (int)((num2 * 678731826) ^ 0x7048822F);
						continue;
					case 12u:
						throw new ArgumentOutOfRangeException("minScale", "Parameter must be less than maxScale.");
					case 17u:
					{
						int num7;
						if (!(maxScale <= 1f))
						{
							num = 1087266667;
							num7 = num;
						}
						else
						{
							num = 1950530550;
							num7 = num;
						}
						continue;
					}
					case 10u:
						throw new ArgumentOutOfRangeException("scaleChangeAmountPerFrame", "Parameter must be greater than 0 and less than or equal to 1.");
					case 1u:
						throw new ArgumentOutOfRangeException("cpuScalingFactor", "Parameter must be greater than 0 and less or equal to 1.");
					case 9u:
						rvlKNIhjoMhysuEQQExwsaBdJBrU = maxScale;
						num = ((int)num2 * -1787143533) ^ -1777772446;
						continue;
					case 13u:
						iZeIVBautqgEhaqsWnHHQeVEQYSS = minScale;
						num = (int)(num2 * 1764839337) ^ -1884356730;
						continue;
					case 21u:
						QVpoWpfjGMBVgHZGsNjwjYBANikK = scaleChangeAmountPerFrame;
						VRVESvJQqaziaZakxRrLgKyJKypG = increaseScaleFrameInterval;
						qHUGZRrvOHZoktDWDMtGZqvrAkeC = decreaseScaleFrameInterval;
						oYzzyKPFXDfzfqlJjDooSiBdOPyd = 1f - (1f - minScale) * cpuScalingFactor;
						serviceUpdater.RegisterService(this);
						num = ((int)num2 * -116431829) ^ -346147876;
						continue;
					case 15u:
					{
						int num3;
						if (!(scaleChangeAmountPerFrame <= 1f))
						{
							num = 791898134;
							num3 = num;
						}
						else
						{
							num = 696079608;
							num3 = num;
						}
						continue;
					}
					case 19u:
						return;
					}
					break;
				}
			}
		}

		public void Update()
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			bool flag = default(bool);
			float num8 = default(float);
			while (true)
			{
				int num = 2023967050;
				while (true)
				{
					uint num2;
					float num5;
					switch ((num2 = (uint)(num ^ 0x2651D779)) % 21)
					{
					case 13u:
						break;
					case 0u:
					{
						int num4;
						if (!CurrentPerformanceState.HasFlag(MeasuredPerformanceState.GpuSurplus))
						{
							num = 954130386;
							num4 = num;
						}
						else
						{
							num = 1319373283;
							num4 = num;
						}
						continue;
					}
					case 10u:
						CurrentRenderScale = OverrideRenderScale.Value;
						flag = true;
						num = (int)(num2 * 630311084) ^ -1802366146;
						continue;
					case 12u:
						CurrentPerformanceState = FAyHPlBtQBCaxgvxCIJOlKLJQbOA.State;
						num = (int)((num2 * 1353088155) ^ 0x5A15C45D);
						continue;
					case 5u:
					{
						int num11;
						int num12;
						if (CurrentRenderScale < rvlKNIhjoMhysuEQQExwsaBdJBrU)
						{
							num11 = -1013667352;
							num12 = num11;
						}
						else
						{
							num11 = -743976514;
							num12 = num11;
						}
						num = num11 ^ (int)(num2 * 1444590862);
						continue;
					}
					case 16u:
						CurrentRenderScale = Mathf.Max(num8, CurrentRenderScale - QVpoWpfjGMBVgHZGsNjwjYBANikK);
						num = (int)((num2 * 629025378) ^ 0x1F2F4E51);
						continue;
					case 17u:
					{
						int num15;
						int num16;
						if (OUQKVqUtUaXzFFosEnbCjWEbpTKi - nHnWuOUmyOrlXpCPOEuouXMVPNIe >= qHUGZRrvOHZoktDWDMtGZqvrAkeC)
						{
							num15 = -1806829005;
							num16 = num15;
						}
						else
						{
							num15 = -509725955;
							num16 = num15;
						}
						num = num15 ^ (int)(num2 * 1289762528);
						continue;
					}
					case 4u:
						num5 = iZeIVBautqgEhaqsWnHHQeVEQYSS;
						goto IL_017f;
					case 20u:
						flag = true;
						num = ((int)num2 * -1052528993) ^ 0x2FD019F9;
						continue;
					case 3u:
					{
						int num6;
						int num7;
						if (!OverrideRenderScale.HasValue)
						{
							num6 = 1267035112;
							num7 = num6;
						}
						else
						{
							num6 = 531823022;
							num7 = num6;
						}
						num = num6 ^ ((int)num2 * -1226740195);
						continue;
					}
					case 11u:
					{
						int num17;
						if (!CurrentPerformanceState.HasFlag(MeasuredPerformanceState.GpuConstrained))
						{
							num = 581761533;
							num17 = num;
						}
						else
						{
							num = 945398715;
							num17 = num;
						}
						continue;
					}
					case 9u:
						nHnWuOUmyOrlXpCPOEuouXMVPNIe = OUQKVqUtUaXzFFosEnbCjWEbpTKi;
						num = 1142629649;
						continue;
					case 19u:
					{
						int num13;
						int num14;
						if (CurrentRenderScale <= num8)
						{
							num13 = -1879721965;
							num14 = num13;
						}
						else
						{
							num13 = -167859457;
							num14 = num13;
						}
						num = num13 ^ (int)(num2 * 1984760435);
						continue;
					}
					case 6u:
					{
						int num9;
						int num10;
						if (OUQKVqUtUaXzFFosEnbCjWEbpTKi - nHnWuOUmyOrlXpCPOEuouXMVPNIe < VRVESvJQqaziaZakxRrLgKyJKypG)
						{
							num9 = -78681409;
							num10 = num9;
						}
						else
						{
							num9 = -1953527359;
							num10 = num9;
						}
						num = num9 ^ (int)(num2 * 505608497);
						continue;
					}
					case 15u:
						return;
					case 8u:
						flag = false;
						num = ((int)num2 * -1479702713) ^ 0x60778C2D;
						continue;
					case 18u:
						if (CurrentPerformanceState.HasFlag(MeasuredPerformanceState.CpuConstrained))
						{
							num5 = oYzzyKPFXDfzfqlJjDooSiBdOPyd;
							goto IL_017f;
						}
						num = (int)(num2 * 430135584) ^ -358929153;
						continue;
					case 14u:
						OUQKVqUtUaXzFFosEnbCjWEbpTKi++;
						num = ((int)num2 * -1666520478) ^ -1918505995;
						continue;
					case 1u:
					{
						int num3;
						if (!flag)
						{
							num = 1090211180;
							num3 = num;
						}
						else
						{
							num = 2056665555;
							num3 = num;
						}
						continue;
					}
					case 7u:
						CurrentRenderScale = Mathf.Min(rvlKNIhjoMhysuEQQExwsaBdJBrU, CurrentRenderScale + QVpoWpfjGMBVgHZGsNjwjYBANikK);
						flag = true;
						num = ((int)num2 * -1261065838) ^ -1868315736;
						continue;
					default:
						{
							ScalableBufferManager.ResizeBuffers(CurrentRenderScale, CurrentRenderScale);
							return;
						}
						IL_017f:
						num8 = num5;
						num = 2071258019;
						continue;
					}
					break;
				}
			}
		}

		private void UrWwwkEVqlsCwuqAxyNaOnyUzodO()
		{
			if (!tlRiOzSchvnYldbxynOFisSYraBV)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 67174036u) % 3)
				{
				case 2u:
					break;
				default:
					return;
				case 1u:
					throw new ObjectDisposedException(GetType().FullName);
				case 0u:
					return;
				}
			}
		}

		public void Dispose()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				goto IL_0008;
			}
			goto IL_0042;
			IL_0008:
			int num = -966581186;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -232963581)) % 5)
				{
				case 4u:
					break;
				default:
					return;
				case 1u:
					return;
				case 3u:
					goto IL_0042;
				case 2u:
					UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
					num = (int)(num2 * 1017059907) ^ -265662558;
					continue;
				case 0u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0042:
			tlRiOzSchvnYldbxynOFisSYraBV = true;
			num = -278994233;
			goto IL_000d;
		}
	}
}
