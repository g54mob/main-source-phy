using System;
using UnityEngine;
using qCVMBNQoPHQLSopPMGeTrkCZHFjc;

namespace BitCode.Profiles
{
	[Serializable]
	public class PlatformCapabilities
	{
		internal const string PlatformFieldName = "platform";

		internal const string CapabilityLevelsFieldName = "capabilityLevels";

		[SerializeField]
		private RuntimePlatform platform;

		[SerializeReference]
		private fUdQvkFNWbBnFFOHiJxGYrhKXTGY[] capabilityLevels;

		public RuntimePlatform Platform => platform;

		internal fUdQvkFNWbBnFFOHiJxGYrhKXTGY[] TBmdkriJOXaQAYxjkuZiSUXOxIvPA => capabilityLevels;

		public TCapabilityLevel GetLevelForCapability<TCapabilityLevel>(TCapabilityLevel fallback) where TCapabilityLevel : Enum
		{
			Type typeFromHandle = typeof(TCapabilityLevel);
			fUdQvkFNWbBnFFOHiJxGYrhKXTGY[] array = capabilityLevels;
			int num3 = default(int);
			fUdQvkFNWbBnFFOHiJxGYrhKXTGY fUdQvkFNWbBnFFOHiJxGYrhKXTGY2 = default(fUdQvkFNWbBnFFOHiJxGYrhKXTGY);
			while (true)
			{
				int num = 1029636709;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x1DFE7067)) % 8)
					{
					case 0u:
						break;
					case 2u:
						num3 = 0;
						num = (int)(num2 * 802164912) ^ -305357024;
						continue;
					case 7u:
						num = (int)(num2 * 75929970) ^ -1613370944;
						continue;
					case 5u:
						return (TCapabilityLevel)fUdQvkFNWbBnFFOHiJxGYrhKXTGY2.ATjVgisKIXHkpJIDbEthnFOVJjPCb;
					case 1u:
					{
						int num5;
						if (num3 >= array.Length)
						{
							num = 1334229012;
							num5 = num;
						}
						else
						{
							num = 474762619;
							num5 = num;
						}
						continue;
					}
					case 4u:
					{
						fUdQvkFNWbBnFFOHiJxGYrhKXTGY2 = array[num3];
						int num4;
						if (!(fUdQvkFNWbBnFFOHiJxGYrhKXTGY2.zkxFfreEscZhwkGunsMAEdgwsdjjA == typeFromHandle))
						{
							num = 1016068937;
							num4 = num;
						}
						else
						{
							num = 1417179306;
							num4 = num;
						}
						continue;
					}
					case 6u:
						num3++;
						num = 354268574;
						continue;
					default:
						return fallback;
					}
					break;
				}
			}
		}
	}
}
