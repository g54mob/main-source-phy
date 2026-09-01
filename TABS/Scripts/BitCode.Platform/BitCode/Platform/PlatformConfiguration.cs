using UnityEngine;

namespace BitCode.Platform
{
	[CreateAssetMenu(fileName = "PlatformConfiguration", menuName = "BitCode/Platform/Configuration")]
	public class PlatformConfiguration : ScriptableObject, IPlatformConfiguration
	{
		[Tooltip("Check to clamp the device's maximum render resolution by height.")]
		[SerializeField]
		protected bool clampMaximumHeight;

		[Tooltip("The height to clamp the device's maximum render resolution to.")]
		[SerializeField]
		protected int maximumScreenHeight = 1440;

		[Tooltip("Check to limit the device's minimum mip level.")]
		[SerializeField]
		protected bool clampMipLevel;

		[SerializeField]
		[Tooltip("The lowest mip level the device will use.")]
		protected int minimumMipLevel = 1;

		public void Apply()
		{
			if (clampMaximumHeight)
			{
				goto IL_000b;
			}
			goto IL_0172;
			IL_000b:
			int num = -728986556;
			goto IL_0010;
			IL_0010:
			float num7 = default(float);
			int num3 = default(int);
			int num4 = default(int);
			int height = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -763134202)) % 12)
				{
				case 4u:
					break;
				default:
					return;
				case 1u:
					goto IL_0056;
				case 3u:
					num7 = (float)num3 / (float)num4;
					height = maximumScreenHeight;
					num = ((int)num2 * -932343322) ^ 0x51C80F1A;
					continue;
				case 10u:
					num3 = Screen.height;
					num4 = Screen.width;
					num = ((int)num2 * -1851346430) ^ 0x663AC541;
					continue;
				case 7u:
					num = (int)((num2 * 212711251) ^ 0x3E088036);
					continue;
				case 2u:
					Screen.SetResolution(Mathf.RoundToInt((float)maximumScreenHeight * num7 * 0.25f) * 4, height, fullscreen: true);
					num = ((int)num2 * -1329959014) ^ -922231590;
					continue;
				case 6u:
				{
					int num8;
					int num9;
					if (maximumScreenHeight <= 0)
					{
						num8 = 464816064;
						num9 = num8;
					}
					else
					{
						num8 = 671977459;
						num9 = num8;
					}
					num = num8 ^ ((int)num2 * -780292845);
					continue;
				}
				case 11u:
				{
					int num5;
					int num6;
					if (Screen.height <= Screen.width)
					{
						num5 = -303226627;
						num6 = num5;
					}
					else
					{
						num5 = -494243066;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -259699898);
					continue;
				}
				case 5u:
					num3 = Screen.width;
					num4 = Screen.height;
					num = -119479697;
					continue;
				case 9u:
					QualitySettings.masterTextureLimit = minimumMipLevel;
					num = (int)(num2 * 731595534) ^ -2120522240;
					continue;
				case 0u:
					goto IL_0172;
				case 8u:
					return;
				}
				break;
				IL_0056:
				int num10;
				if (num4 > maximumScreenHeight)
				{
					num = -349350351;
					num10 = num;
				}
				else
				{
					num = -126345178;
					num10 = num;
				}
			}
			goto IL_000b;
			IL_0172:
			int num11;
			if (!clampMipLevel)
			{
				num = -1174887770;
				num11 = num;
			}
			else
			{
				num = -164125485;
				num11 = num;
			}
			goto IL_0010;
		}
	}
}
