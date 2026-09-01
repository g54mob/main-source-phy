using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public class DebugScreen
	{
		private static readonly DebugScreen unkCMXdDaHlgFnStRuNbxzrbnMID = new DebugScreen();

		[DebugCommand(Name = "Screen", Description = "Push the Screen context onto the stack.")]
		public static DebugScreen PushScreen()
		{
			return unkCMXdDaHlgFnStRuNbxzrbnMID;
		}

		[DebugCommand(Description = "Set the screen height.")]
		public void SetHeight(int height, bool preserveAspectRatio = true)
		{
			int num = Screen.width;
			if (preserveAspectRatio)
			{
				goto IL_0009;
			}
			goto IL_004c;
			IL_0009:
			int num2 = -916241404;
			goto IL_000e;
			IL_000e:
			while (true)
			{
				uint num3;
				switch ((num3 = (uint)(num2 ^ -1529825521)) % 4)
				{
				case 0u:
					break;
				default:
					return;
				case 3u:
					num = (int)((float)height / (float)Screen.height * (float)num);
					num2 = ((int)num3 * -531145609) ^ 0x6E725B00;
					continue;
				case 2u:
					goto IL_004c;
				case 1u:
					return;
				}
				break;
			}
			goto IL_0009;
			IL_004c:
			Screen.SetResolution(num, height, Screen.fullScreenMode);
			num2 = -2127563190;
			goto IL_000e;
		}

		[DebugCommand(Description = "Set the screen height.")]
		public void SetWidth(int width, bool preserveAspectRatio = true)
		{
			int num = Screen.height;
			while (true)
			{
				int num2 = 1894253048;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x3DA3E86A)) % 5)
					{
					case 2u:
						break;
					default:
						return;
					case 0u:
						Screen.SetResolution(width, num, Screen.fullScreenMode);
						num2 = 732371812;
						continue;
					case 4u:
						num = (int)((float)width / (float)Screen.width * (float)num);
						num2 = (int)((num3 * 2075740052) ^ 0x1BDD2B0B);
						continue;
					case 1u:
					{
						int num4;
						int num5;
						if (preserveAspectRatio)
						{
							num4 = -60803152;
							num5 = num4;
						}
						else
						{
							num4 = -2040494437;
							num5 = num4;
						}
						num2 = num4 ^ ((int)num3 * -1754976324);
						continue;
					}
					case 3u:
						return;
					}
					break;
				}
			}
		}

		[DebugCommand(Description = "Set the screen resolution.")]
		public void SetResolution(int width, int height, FullScreenMode fullscreenMode = FullScreenMode.FullScreenWindow, int refreshRate = 0)
		{
			if (refreshRate != 0)
			{
				goto IL_0004;
			}
			goto IL_0047;
			IL_0004:
			int num = 846058952;
			goto IL_0009;
			IL_0009:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x3344DE98)) % 5)
				{
				case 4u:
					break;
				default:
					return;
				case 3u:
					Screen.SetResolution(width, height, fullscreenMode, refreshRate);
					num = ((int)num2 * -1314733421) ^ 0x55683E92;
					continue;
				case 1u:
					goto IL_0047;
				case 2u:
					return;
				case 0u:
					return;
				}
				break;
			}
			goto IL_0004;
			IL_0047:
			Screen.SetResolution(width, height, fullscreenMode);
			num = 1810206467;
			goto IL_0009;
		}
	}
}
