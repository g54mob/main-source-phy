using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BitCode.Platform
{
	[CreateAssetMenu(fileName = "RuntimeConfigurationSelector", menuName = "BitCode/Platform/Runtime Configuration Selector")]
	public class RuntimePlatformConfigurationSelector : ScriptableObject, IPlatformConfigurationSelector
	{
		private sealed class tIpbXYwNEkHZBcplVEMnMbSCcEjMA : IEnumerable<IPlatformConfiguration>, IEnumerator<IPlatformConfiguration>, IEnumerable, IEnumerator, IDisposable
		{
			private int IQbzBrmlqjTSpMXNlFTzNDRJPCug;

			private IPlatformConfiguration ELbeoOKgAZKNJfJZeCPObBdBgrkbE;

			private int ruvOnTQEBCXAFZaSIYUsZNfvFHz;

			public RuntimePlatformConfigurationSelector OABKcmuyYYXbmPheABbJQjfupFPv;

			IPlatformConfiguration IEnumerator<IPlatformConfiguration>.Current
			{
				[DebuggerHidden]
				get
				{
					return ELbeoOKgAZKNJfJZeCPObBdBgrkbE;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return ELbeoOKgAZKNJfJZeCPObBdBgrkbE;
				}
			}

			[DebuggerHidden]
			public tIpbXYwNEkHZBcplVEMnMbSCcEjMA(int P_0)
			{
				IQbzBrmlqjTSpMXNlFTzNDRJPCug = P_0;
				ruvOnTQEBCXAFZaSIYUsZNfvFHz = Environment.CurrentManagedThreadId;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int iQbzBrmlqjTSpMXNlFTzNDRJPCug = IQbzBrmlqjTSpMXNlFTzNDRJPCug;
				RuntimePlatformConfigurationSelector oABKcmuyYYXbmPheABbJQjfupFPv = default(RuntimePlatformConfigurationSelector);
				RuntimePlatform platform = default(RuntimePlatform);
				while (true)
				{
					int num = -1036922521;
					while (true)
					{
						uint num2;
						int num12;
						int num17;
						int num20;
						switch ((num2 = (uint)(num ^ -369570679)) % 48)
						{
						case 24u:
							break;
						case 9u:
							num = (int)((num2 * 246145373) ^ 0x483A5E3C);
							continue;
						case 11u:
						{
							int num19;
							if (!(oABKcmuyYYXbmPheABbJQjfupFPv.xboxOnePlatform != null))
							{
								num = -498636439;
								num19 = num;
							}
							else
							{
								num = -240433790;
								num19 = num;
							}
							continue;
						}
						case 4u:
							num = (int)(num2 * 1638518257) ^ -2047105939;
							continue;
						case 2u:
						{
							int num22;
							if (platform <= RuntimePlatform.PS4)
							{
								num = -920433453;
								num22 = num;
							}
							else
							{
								num = -2059612008;
								num22 = num;
							}
							continue;
						}
						case 17u:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							platform = Application.platform;
							num = -935920785;
							continue;
						case 38u:
						{
							int num13;
							int num14;
							if (platform <= RuntimePlatform.LinuxPlayer)
							{
								num13 = 1733011121;
								num14 = num13;
							}
							else
							{
								num13 = 1040587129;
								num14 = num13;
							}
							num = num13 ^ ((int)num2 * -99417861);
							continue;
						}
						case 45u:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 6;
							num = (int)((num2 * 28780087) ^ 0x545EA010);
							continue;
						case 5u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.switchPlatform;
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 5;
							return true;
						case 47u:
							num = (int)((num2 * 1344746636) ^ 0x406BC61D);
							continue;
						case 28u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.ps4Platform;
							num = (int)(num2 * 740699257) ^ -280986808;
							continue;
						case 42u:
							switch (platform)
							{
							case RuntimePlatform.OSXEditor:
							case RuntimePlatform.WindowsEditor:
								goto IL_031d;
							case RuntimePlatform.OSXPlayer:
							case RuntimePlatform.WindowsPlayer:
								goto IL_057a;
							case RuntimePlatform.IPhonePlayer:
								goto IL_059c;
							case RuntimePlatform.OSXWebPlayer:
							case RuntimePlatform.OSXDashboardPlayer:
							case RuntimePlatform.WindowsWebPlayer:
							case (RuntimePlatform)6:
								goto IL_05df;
							}
							num = (int)(num2 * 980374686) ^ -669547727;
							continue;
						case 13u:
							goto IL_0224;
						case 19u:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 7;
							return true;
						case 20u:
							num = (int)(num2 * 1804747025) ^ -1792717299;
							continue;
						case 39u:
						{
							int num5;
							if (!(oABKcmuyYYXbmPheABbJQjfupFPv.ps4Platform != null))
							{
								num = -498636439;
								num5 = num;
							}
							else
							{
								num = -1967374315;
								num5 = num;
							}
							continue;
						}
						case 43u:
							goto IL_0284;
						case 16u:
						{
							int num23;
							if (!(oABKcmuyYYXbmPheABbJQjfupFPv.switchPlatform != null))
							{
								num = -498636439;
								num23 = num;
							}
							else
							{
								num = -639563796;
								num23 = num;
							}
							continue;
						}
						case 14u:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 2;
							num = ((int)num2 * -1402832576) ^ 0x4B2CBA6;
							continue;
						case 44u:
							goto IL_02d0;
						case 37u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.iosPlatform;
							num = ((int)num2 * -1229853773) ^ -1825184049;
							continue;
						case 27u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.xboxOnePlatform;
							num = ((int)num2 * -109744424) ^ 0x1897D352;
							continue;
						case 34u:
							goto IL_031d;
						case 1u:
						{
							int num21;
							if (platform == RuntimePlatform.XboxOne)
							{
								num = -1240262270;
								num21 = num;
							}
							else
							{
								num = -18562105;
								num21 = num;
							}
							continue;
						}
						case 12u:
						{
							int num18;
							if (oABKcmuyYYXbmPheABbJQjfupFPv.androidPlatform != null)
							{
								num = -757015055;
								num18 = num;
							}
							else
							{
								num = -498636439;
								num18 = num;
							}
							continue;
						}
						case 23u:
							goto IL_037a;
						case 35u:
						{
							int num15;
							int num16;
							if (platform == RuntimePlatform.LinuxPlayer)
							{
								num15 = 1875124674;
								num16 = num15;
							}
							else
							{
								num15 = 1023149062;
								num16 = num15;
							}
							num = num15 ^ ((int)num2 * -1190523767);
							continue;
						}
						case 7u:
							num = (int)((num2 * 888248117) ^ 0x7BA2413A);
							continue;
						case 10u:
						{
							int num10;
							int num11;
							if (platform == RuntimePlatform.LinuxEditor)
							{
								num10 = -1657730317;
								num11 = num10;
							}
							else
							{
								num10 = -2088917534;
								num11 = num10;
							}
							num = num10 ^ ((int)num2 * -903059252);
							continue;
						}
						case 26u:
							goto IL_03df;
						case 46u:
							oABKcmuyYYXbmPheABbJQjfupFPv = OABKcmuyYYXbmPheABbJQjfupFPv;
							switch (iQbzBrmlqjTSpMXNlFTzNDRJPCug)
							{
							case 0:
								break;
							case 7:
								goto IL_0224;
							case 5:
								goto IL_0284;
							case 2:
								goto IL_02d0;
							case 6:
								goto IL_037a;
							case 3:
								goto IL_03df;
							default:
								goto IL_041d;
							case 1:
								goto IL_0476;
							case 4:
								goto IL_0542;
							}
							goto case 17u;
						case 36u:
						{
							int num8;
							int num9;
							if (platform == RuntimePlatform.Android)
							{
								num8 = -2076069171;
								num9 = num8;
							}
							else
							{
								num8 = -143609950;
								num9 = num8;
							}
							num = num8 ^ (int)(num2 * 1842583710);
							continue;
						}
						case 31u:
							return true;
						case 25u:
							num = (int)((num2 * 1538107066) ^ 0x1A394083);
							continue;
						case 8u:
							goto IL_0476;
						case 29u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.desktopPlatform;
							num = (int)(num2 * 168363486) ^ -1462890623;
							continue;
						case 41u:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 3;
							return true;
						case 18u:
							return true;
						case 6u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.editorPlatform;
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 1;
							return true;
						case 21u:
							num = (int)(num2 * 11772799) ^ -506950958;
							continue;
						case 30u:
						{
							int num6;
							int num7;
							if (platform != RuntimePlatform.Switch)
							{
								num6 = 127614011;
								num7 = num6;
							}
							else
							{
								num6 = 2013129855;
								num7 = num6;
							}
							num = num6 ^ ((int)num2 * -2077842883);
							continue;
						}
						case 15u:
							return false;
						case 22u:
							goto IL_0542;
						case 40u:
							ELbeoOKgAZKNJfJZeCPObBdBgrkbE = oABKcmuyYYXbmPheABbJQjfupFPv.androidPlatform;
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = 4;
							return true;
						case 32u:
							goto IL_057a;
						case 33u:
							goto IL_059c;
						case 3u:
						{
							int num3;
							int num4;
							if (platform != RuntimePlatform.PS4)
							{
								num3 = 1557668574;
								num4 = num3;
							}
							else
							{
								num3 = 1470193726;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1304425200);
							continue;
						}
						default:
							goto IL_05df;
							IL_0284:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -1347247796;
							continue;
							IL_0224:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -498636439;
							continue;
							IL_0542:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -498636439;
							continue;
							IL_0476:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -498636439;
							continue;
							IL_041d:
							num = (int)(num2 * 1088430251) ^ -1561450420;
							continue;
							IL_03df:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -1804617008;
							continue;
							IL_059c:
							if (oABKcmuyYYXbmPheABbJQjfupFPv.iosPlatform != null)
							{
								num = -625459140;
								num12 = num;
							}
							else
							{
								num = -498636439;
								num12 = num;
							}
							continue;
							IL_05df:
							return false;
							IL_057a:
							if (oABKcmuyYYXbmPheABbJQjfupFPv.desktopPlatform != null)
							{
								num = -354068956;
								num17 = num;
							}
							else
							{
								num = -498636439;
								num17 = num;
							}
							continue;
							IL_037a:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -928645514;
							continue;
							IL_031d:
							if (oABKcmuyYYXbmPheABbJQjfupFPv.editorPlatform != null)
							{
								num = -1566150417;
								num20 = num;
							}
							else
							{
								num = -498636439;
								num20 = num;
							}
							continue;
							IL_02d0:
							IQbzBrmlqjTSpMXNlFTzNDRJPCug = -1;
							num = -783430928;
							continue;
						}
						break;
					}
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			[DebuggerHidden]
			IEnumerator<IPlatformConfiguration> IEnumerable<IPlatformConfiguration>.GetEnumerator()
			{
				if (IQbzBrmlqjTSpMXNlFTzNDRJPCug == -2)
				{
					goto IL_000a;
				}
				goto IL_0085;
				IL_000a:
				int num = 481398758;
				goto IL_000f;
				IL_000f:
				tIpbXYwNEkHZBcplVEMnMbSCcEjMA tIpbXYwNEkHZBcplVEMnMbSCcEjMA2 = default(tIpbXYwNEkHZBcplVEMnMbSCcEjMA);
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x2A780DE9)) % 6)
					{
					case 2u:
						break;
					case 1u:
					{
						int num3;
						int num4;
						if (ruvOnTQEBCXAFZaSIYUsZNfvFHz != Environment.CurrentManagedThreadId)
						{
							num3 = 95684;
							num4 = num3;
						}
						else
						{
							num3 = 449355279;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 621292920);
						continue;
					}
					case 4u:
						IQbzBrmlqjTSpMXNlFTzNDRJPCug = 0;
						num = ((int)num2 * -392072388) ^ 0x99AED8C;
						continue;
					case 3u:
						tIpbXYwNEkHZBcplVEMnMbSCcEjMA2 = this;
						num = (int)((num2 * 824634487) ^ 0x647F5950);
						continue;
					case 5u:
						goto IL_0085;
					default:
						return tIpbXYwNEkHZBcplVEMnMbSCcEjMA2;
					}
					break;
				}
				goto IL_000a;
				IL_0085:
				tIpbXYwNEkHZBcplVEMnMbSCcEjMA2 = new tIpbXYwNEkHZBcplVEMnMbSCcEjMA(0);
				tIpbXYwNEkHZBcplVEMnMbSCcEjMA2.OABKcmuyYYXbmPheABbJQjfupFPv = OABKcmuyYYXbmPheABbJQjfupFPv;
				num = 1164959291;
				goto IL_000f;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<IPlatformConfiguration>)this).GetEnumerator();
			}
		}

		[SerializeField]
		protected PlatformConfiguration editorPlatform;

		[SerializeField]
		protected PlatformConfiguration desktopPlatform;

		[SerializeField]
		protected PlatformConfiguration iosPlatform;

		[SerializeField]
		protected PlatformConfiguration androidPlatform;

		[SerializeField]
		protected PlatformConfiguration switchPlatform;

		[SerializeField]
		protected PlatformConfiguration ps4Platform;

		[SerializeField]
		protected PlatformConfiguration xboxOnePlatform;

		[IteratorStateMachine(typeof(_003CGetActiveConfiguration_003Ed__7))]
		public IEnumerable<IPlatformConfiguration> GetActiveConfiguration()
		{
			return new tIpbXYwNEkHZBcplVEMnMbSCcEjMA(-2)
			{
				OABKcmuyYYXbmPheABbJQjfupFPv = this
			};
		}
	}
}
