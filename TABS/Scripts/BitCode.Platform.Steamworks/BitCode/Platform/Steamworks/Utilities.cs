using System;
using BitCode.Graphics;
using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public static class Utilities
	{
		public static UserAccountOnlineStatus ConvertToOnlineStatus(EPersonaState personaState)
		{
			switch (personaState)
			{
			default:
				while (true)
				{
					int num = -579626438;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -851321645)) % 8)
						{
						case 3u:
							break;
						case 4u:
							goto end_IL_0026;
						case 2u:
							goto IL_0065;
						case 1u:
							num = ((int)num2 * -668685154) ^ -696874683;
							continue;
						case 6u:
							goto IL_007d;
						case 5u:
							goto IL_0086;
						case 7u:
							goto end_IL_0001;
						default:
							return UserAccountOnlineStatus.Offline;
						}
						break;
					}
					continue;
					end_IL_0026:
					break;
				}
				goto case EPersonaState.k_EPersonaStateMax;
			case EPersonaState.k_EPersonaStateMax:
				return UserAccountOnlineStatus.Invisible;
			case EPersonaState.k_EPersonaStateAway:
			case EPersonaState.k_EPersonaStateSnooze:
				goto IL_0065;
			case EPersonaState.k_EPersonaStateBusy:
			case EPersonaState.k_EPersonaStateLookingToTrade:
				goto IL_007d;
			case EPersonaState.k_EPersonaStateOffline:
				goto IL_0086;
			case EPersonaState.k_EPersonaStateOnline:
			case EPersonaState.k_EPersonaStateLookingToPlay:
				break;
				IL_0086:
				return UserAccountOnlineStatus.Offline;
				IL_007d:
				return UserAccountOnlineStatus.Busy;
				IL_0065:
				return UserAccountOnlineStatus.Away;
				end_IL_0001:
				break;
			}
			return UserAccountOnlineStatus.Online;
		}

		public static ImageData CreateImageDataFromHandle(int imageHandle)
		{
			if (SteamUtils.GetImageSize(imageHandle, out var pnWidth, out var pnHeight))
			{
				byte[] array = default(byte[]);
				uint num3 = default(uint);
				while (true)
				{
					int num = -783278800;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1727518082)) % 6)
						{
						case 0u:
							break;
						case 5u:
							return new ImageData(tgcrLkPWnlAZMdhLZUyTWHHXHMFI(array, pnWidth, pnHeight, num3), pnWidth, pnHeight, ImageDataFormat.Rgba);
						case 1u:
						{
							int num4 = (int)(pnWidth * pnHeight * num3);
							array = new byte[num4];
							int num5;
							int num6;
							if (SteamUtils.GetImageRGBA(imageHandle, array, num4))
							{
								num5 = 1025332701;
								num6 = num5;
							}
							else
							{
								num5 = 1494470749;
								num6 = num5;
							}
							num = num5 ^ (int)(num2 * 1245520522);
							continue;
						}
						case 4u:
							goto end_IL_000f;
						case 2u:
							num3 = ImageDataFormat.Rgba.NumBytesPerPixel();
							num = (int)(num2 * 1807640398) ^ -229991689;
							continue;
						default:
							return null;
						}
						break;
					}
					continue;
					end_IL_000f:
					break;
				}
			}
			throw new ArgumentException($"Could not create image data, dimensions aren't valid! [ {pnWidth}, {pnHeight} ].");
		}

		private static byte[] tgcrLkPWnlAZMdhLZUyTWHHXHMFI(byte[] P_0, uint P_1, uint P_2, uint P_3)
		{
			uint num = P_1 * P_3;
			uint num7 = default(uint);
			uint num5 = default(uint);
			uint num10 = default(uint);
			uint num9 = default(uint);
			uint num4 = default(uint);
			byte[] array = default(byte[]);
			while (true)
			{
				int num2 = -1532065180;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ -1423152317)) % 11)
					{
					case 9u:
						break;
					case 7u:
					{
						int num8;
						if (num7 >= P_2 - 1)
						{
							num2 = -984733772;
							num8 = num2;
						}
						else
						{
							num2 = -1830001271;
							num8 = num2;
						}
						continue;
					}
					case 3u:
						num7 = 0u;
						num2 = ((int)num3 * -482361965) ^ 0x5327B8D1;
						continue;
					case 2u:
					{
						uint num11 = num7 * num;
						num5 = num11 + num;
						num10 = num9 * num;
						num4 = num11;
						num2 = -1529290468;
						continue;
					}
					case 10u:
						num9 = P_2 - 1;
						num2 = ((int)num3 * -755149705) ^ 0x5202A4D7;
						continue;
					case 0u:
						num7++;
						num2 = (int)(num3 * 34428437) ^ -986330615;
						continue;
					case 6u:
						array[num10] = P_0[num4];
						num10++;
						num4++;
						num2 = -1529290468;
						continue;
					case 5u:
						num9--;
						num2 = ((int)num3 * -901280224) ^ 0x2BF6DFBB;
						continue;
					case 1u:
						array = new byte[P_2 * num];
						num2 = ((int)num3 * -1001591942) ^ -1700927783;
						continue;
					case 8u:
					{
						int num6;
						if (num4 >= num5)
						{
							num2 = -509828289;
							num6 = num2;
						}
						else
						{
							num2 = -336114359;
							num6 = num2;
						}
						continue;
					}
					default:
						return array;
					}
					break;
				}
			}
		}
	}
}
