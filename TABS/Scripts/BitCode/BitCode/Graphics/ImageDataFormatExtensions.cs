using System;

namespace BitCode.Graphics
{
	public static class ImageDataFormatExtensions
	{
		public static uint NumBytesPerPixel(this ImageDataFormat format)
		{
			switch (format)
			{
			default:
				while (true)
				{
					int num = -1215406608;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -395142571)) % 5)
						{
						case 2u:
							break;
						case 1u:
							num = (int)(num2 * 1406637922) ^ -1659265101;
							continue;
						case 0u:
							goto end_IL_001a;
						case 4u:
							goto end_IL_0001;
						default:
							throw new ArgumentException("ImageDataFormat not supported");
						}
						break;
					}
					continue;
					end_IL_001a:
					break;
				}
				goto case ImageDataFormat.Rgba;
			case ImageDataFormat.Rgba:
			case ImageDataFormat.Argb:
			case ImageDataFormat.Brga:
				return 4u;
			case ImageDataFormat.Rgb:
			case ImageDataFormat.Brg:
				break;
				end_IL_0001:
				break;
			}
			return 3u;
		}
	}
}
