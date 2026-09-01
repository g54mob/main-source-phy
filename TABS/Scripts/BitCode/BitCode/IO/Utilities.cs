using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BitCode.IO
{
	public static class Utilities
	{
		private static readonly char[] FBKxahXHIbNgMbibzSGbxRnhTyBs = Path.GetInvalidFileNameChars();

		private static readonly char[] UwuKXyNVPDsEnccOsLoeXNQQFlFGA = Path.GetInvalidPathChars();

		private static readonly string[] eXNgdlZjfDfsJdUEonruZnNxyhuB = new string[22]
		{
			"CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6",
			"COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7",
			"LPT8", "LPT9"
		};

		public static string SanitizeFileName(string fileName, char replacementCharacter = '_', bool replaceWindowsReservedNames = false, string reservedReplacement = "reserved")
		{
			if (replaceWindowsReservedNames)
			{
				int num3 = default(int);
				while (true)
				{
					int num = 1537467196;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x1792EB7E)) % 7)
						{
						case 5u:
							break;
						case 0u:
							goto IL_0038;
						case 2u:
							goto IL_0053;
						case 4u:
							num3++;
							num = 523689436;
							continue;
						case 6u:
							num3 = 0;
							num = (int)((num2 * 335697759) ^ 0x37B2A3A2);
							continue;
						case 1u:
							return reservedReplacement;
						default:
							goto end_IL_0006;
						}
						break;
						IL_0053:
						int num4;
						if (string.Equals(fileName, eXNgdlZjfDfsJdUEonruZnNxyhuB[num3], StringComparison.OrdinalIgnoreCase))
						{
							num = 796091426;
							num4 = num;
						}
						else
						{
							num = 1150926536;
							num4 = num;
						}
						continue;
						IL_0038:
						int num5;
						if (num3 < eXNgdlZjfDfsJdUEonruZnNxyhuB.Length)
						{
							num = 670817150;
							num5 = num;
						}
						else
						{
							num = 1574341586;
							num5 = num;
						}
					}
					continue;
					end_IL_0006:
					break;
				}
			}
			return hzUHOUBlEupRQKuODGPWhzDivQwA(fileName, replacementCharacter, FBKxahXHIbNgMbibzSGbxRnhTyBs);
		}

		public static string SanitizeFilePath(string filePath, char replacementCharacter = '_')
		{
			return hzUHOUBlEupRQKuODGPWhzDivQwA(filePath, replacementCharacter, UwuKXyNVPDsEnccOsLoeXNQQFlFGA);
		}

		private static string hzUHOUBlEupRQKuODGPWhzDivQwA(string P_0, char P_1, char[] P_2)
		{
			StringBuilder stringBuilder = new StringBuilder(P_0.Length);
			int num3 = default(int);
			char value = default(char);
			while (true)
			{
				int num = -470420284;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -29507646)) % 8)
					{
					case 2u:
						break;
					case 1u:
					{
						int num5;
						if (num3 < P_0.Length)
						{
							num = -1478290267;
							num5 = num;
						}
						else
						{
							num = -1089864258;
							num5 = num;
						}
						continue;
					}
					case 7u:
					{
						value = P_0[num3];
						int num4;
						if (P_2.Contains(value))
						{
							num = -468837695;
							num4 = num;
						}
						else
						{
							num = -1306759425;
							num4 = num;
						}
						continue;
					}
					case 6u:
						num3 = 0;
						num = ((int)num2 * -1421739970) ^ -71424689;
						continue;
					case 0u:
						num3++;
						num = -1240736197;
						continue;
					case 5u:
						stringBuilder.Append(value);
						num = -326523606;
						continue;
					case 3u:
						stringBuilder.Append(P_1);
						num = (int)(num2 * 1799778210) ^ -1249567284;
						continue;
					default:
						return stringBuilder.ToString();
					}
					break;
				}
			}
		}
	}
}
