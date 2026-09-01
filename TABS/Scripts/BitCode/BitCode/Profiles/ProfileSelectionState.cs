using System;
using System.Collections.Generic;

namespace BitCode.Profiles
{
	public class ProfileSelectionState : IProfileSelectionState
	{
		private readonly Dictionary<Type, object> ovHebzDwsFVMpkIQlYaGTAHktiyd = new Dictionary<Type, object>();

		public void RegisterCapability<TCapabilityLevel>(ICapability<TCapabilityLevel> newCapability) where TCapabilityLevel : Enum
		{
			ovHebzDwsFVMpkIQlYaGTAHktiyd.Add(typeof(TCapabilityLevel), newCapability);
		}

		public ICapability<TCapabilityLevel> GetCapability<TCapabilityLevel>() where TCapabilityLevel : Enum
		{
			Type typeFromHandle = typeof(TCapabilityLevel);
			if (!ovHebzDwsFVMpkIQlYaGTAHktiyd.TryGetValue(typeFromHandle, out var value))
			{
				while (true)
				{
					uint num;
					switch ((num = 2103038557u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						throw new CapabilityNotFoundException(typeFromHandle);
					}
					break;
				}
			}
			return (ICapability<TCapabilityLevel>)value;
		}

		public bool SetCapabilityLevel<TCapabilityLevel>(TCapabilityLevel newLevel) where TCapabilityLevel : Enum
		{
			ICapability<TCapabilityLevel> capability = GetCapability<TCapabilityLevel>();
			TCapabilityLevel level = default(TCapabilityLevel);
			while (true)
			{
				int num = -193741451;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -257037458)) % 5)
					{
					case 3u:
						break;
					case 4u:
						level = capability.Level;
						num = ((int)num2 * -1388637770) ^ -362381781;
						continue;
					case 0u:
						return false;
					case 1u:
					{
						int num3;
						int num4;
						if (!level.Equals(newLevel))
						{
							num3 = -1514980533;
							num4 = num3;
						}
						else
						{
							num3 = -1204766825;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 605345317);
						continue;
					}
					default:
						capability.Level = newLevel;
						return true;
					}
					break;
				}
			}
		}
	}
}
