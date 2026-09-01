using System;
using System.Text;

namespace BitCode.Extensions
{
	public static class StringExtensions
	{
		public static string ToBase64(this string text)
		{
			return text.ToBase64(Encoding.UTF8);
		}

		public static string ToBase64(this string text, Encoding encoding)
		{
			return Convert.ToBase64String(encoding.GetBytes(text));
		}

		public static bool TryParseFromBase64(this string text, out string decoded)
		{
			return text.TryParseFromBase64(Encoding.UTF8, out decoded);
		}

		public static bool TryParseFromBase64(this string text, Encoding encoding, out string decoded)
		{
			bool result = default(bool);
			try
			{
				byte[] bytes = Convert.FromBase64String(text);
				while (true)
				{
					IL_0007:
					int num = 1540592375;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x77901A1E)) % 4)
						{
						case 3u:
							break;
						default:
							goto end_IL_000c;
						case 1u:
							decoded = encoding.GetString(bytes);
							num = (int)((num2 * 583982201) ^ 0x6306313F);
							continue;
						case 0u:
							result = true;
							num = ((int)num2 * -2005043510) ^ -2062375108;
							continue;
						case 2u:
							goto end_IL_000c;
						}
						goto IL_0007;
						continue;
						end_IL_000c:
						break;
					}
					break;
				}
			}
			catch (FormatException)
			{
				decoded = null;
				result = false;
			}
			catch (ArgumentException)
			{
				while (true)
				{
					IL_0061:
					int num3 = 1469222319;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num3 ^ 0x77901A1E)) % 4)
						{
						case 2u:
							break;
						default:
							goto end_IL_0066;
						case 1u:
							decoded = null;
							num3 = (int)((num2 * 1546530070) ^ 0x6E5DB07B);
							continue;
						case 3u:
							result = false;
							num3 = ((int)num2 * -1396273119) ^ -1379493319;
							continue;
						case 0u:
							goto end_IL_0066;
						}
						goto IL_0061;
						continue;
						end_IL_0066:
						break;
					}
					break;
				}
			}
			return result;
		}
	}
}
