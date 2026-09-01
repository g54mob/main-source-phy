using System;

namespace BitCode.Extensions
{
	internal static class ObjectExtensions
	{
		internal static void TryDispose(this object obj)
		{
			IDisposable disposable = obj as IDisposable;
			while (true)
			{
				int num = 348626837;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5DF45C90)) % 4)
					{
					case 3u:
						break;
					default:
						return;
					case 1u:
					{
						int num3;
						int num4;
						if (disposable == null)
						{
							num3 = -1546015620;
							num4 = num3;
						}
						else
						{
							num3 = -1613506846;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -866460798);
						continue;
					}
					case 0u:
						disposable.Dispose();
						num = (int)((num2 * 1815338624) ^ 0x6DC1EFF6);
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}
	}
}
