using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BitCode.Graphics
{
	public class ImageData
	{
		public static readonly ImageData Empty = new ImageData(Array.Empty<byte>(), 0u, 0u, ImageDataFormat.Unsupported);

		[CompilerGenerated]
		private byte[] krLBtVKOILuKudRZCWKTeSbtEFSDb;

		[CompilerGenerated]
		private uint QObAuFajloRFXeZCeABpKDXafqWI;

		[CompilerGenerated]
		private uint oMeUFuUAcdOOXPMriBpaHowlSjqsA;

		[CompilerGenerated]
		private ImageDataFormat XBBHtBDHKqMMGyQTuVkCFXxMsOnIA;

		public byte[] Data
		{
			[CompilerGenerated]
			get
			{
				return krLBtVKOILuKudRZCWKTeSbtEFSDb;
			}
			[CompilerGenerated]
			protected set
			{
				krLBtVKOILuKudRZCWKTeSbtEFSDb = value;
			}
		}

		public uint Width
		{
			[CompilerGenerated]
			get
			{
				return QObAuFajloRFXeZCeABpKDXafqWI;
			}
			[CompilerGenerated]
			protected set
			{
				QObAuFajloRFXeZCeABpKDXafqWI = value;
			}
		}

		public uint Height
		{
			[CompilerGenerated]
			get
			{
				return oMeUFuUAcdOOXPMriBpaHowlSjqsA;
			}
			[CompilerGenerated]
			protected set
			{
				oMeUFuUAcdOOXPMriBpaHowlSjqsA = value;
			}
		}

		public ImageDataFormat DataFormat
		{
			[CompilerGenerated]
			get
			{
				return XBBHtBDHKqMMGyQTuVkCFXxMsOnIA;
			}
			[CompilerGenerated]
			protected set
			{
				XBBHtBDHKqMMGyQTuVkCFXxMsOnIA = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (Width != 0)
				{
					while (true)
					{
						uint num;
						switch ((num = 1693905236u) % 3)
						{
						case 0u:
							continue;
						case 2u:
							return Height == 0;
						}
						break;
					}
				}
				return true;
			}
		}

		public ImageData(byte[] data, uint width, uint height, ImageDataFormat dataFormat)
		{
			Data = data;
			Width = width;
			Height = height;
			DataFormat = dataFormat;
		}

		public static ImageData CreateFromPngData(byte[] pngData)
		{
			int num = 16;
			byte b = default(byte);
			byte[] array = default(byte[]);
			byte[] array2 = default(byte[]);
			uint width = default(uint);
			uint height = default(uint);
			ImageDataFormat dataFormat = default(ImageDataFormat);
			while (true)
			{
				int num2 = -845451558;
				while (true)
				{
					uint num3;
					int num4;
					byte[] value2;
					byte[] value;
					switch ((num3 = (uint)(num2 ^ -310494318)) % 15)
					{
					case 6u:
						break;
					case 13u:
						b = pngData[num + 9];
						num2 = (int)((num3 * 760240609) ^ 0x1F24170E);
						continue;
					case 7u:
					{
						int num5;
						int num6;
						if (b != 6)
						{
							num5 = 1915275633;
							num6 = num5;
						}
						else
						{
							num5 = 384315312;
							num6 = num5;
						}
						num2 = num5 ^ ((int)num3 * -904396033);
						continue;
					}
					case 9u:
						Buffer.BlockCopy(pngData, num, array, 0, 4);
						num2 = ((int)num3 * -1321873090) ^ 0x1983BAA0;
						continue;
					case 2u:
						num4 = 1;
						goto IL_00b4;
					case 1u:
						if (b == 2)
						{
							num4 = 0;
							goto IL_00b4;
						}
						num2 = -1068354381;
						continue;
					case 8u:
						value2 = array2;
						goto IL_00db;
					case 3u:
						array = new byte[4];
						num2 = (int)(num3 * 895372761) ^ -24599004;
						continue;
					case 10u:
						throw new InvalidOperationException($"Unsupported colour type for image data: {b}. " + "Only Rgb and Rgba are currently supported.");
					case 4u:
						array2 = new byte[4];
						Buffer.BlockCopy(pngData, num, array2, 0, 4);
						num2 = ((int)num3 * -139164313) ^ -2143314773;
						continue;
					case 14u:
					{
						int num7;
						int num8;
						if (b == 2)
						{
							num7 = 673443411;
							num8 = num7;
						}
						else
						{
							num7 = 2097324175;
							num8 = num7;
						}
						num2 = num7 ^ (int)(num3 * 971413873);
						continue;
					}
					case 5u:
						if (BitConverter.IsLittleEndian)
						{
							value2 = array2.Reverse().ToArray();
							goto IL_00db;
						}
						num2 = ((int)num3 * -516344084) ^ -1028961662;
						continue;
					case 12u:
						value = array;
						goto IL_01b1;
					case 11u:
						if (BitConverter.IsLittleEndian)
						{
							value = array.Reverse().ToArray();
							goto IL_01b1;
						}
						num2 = ((int)num3 * -635347558) ^ 0x770D49A6;
						continue;
					default:
						{
							return new ImageData(pngData, width, height, dataFormat);
						}
						IL_00db:
						width = BitConverter.ToUInt32(value2, 0);
						num += 4;
						num2 = -1638644016;
						continue;
						IL_01b1:
						height = BitConverter.ToUInt32(value, 0);
						num2 = -1535945195;
						continue;
						IL_00b4:
						dataFormat = (ImageDataFormat)num4;
						num2 = -1875316736;
						continue;
					}
					break;
				}
			}
		}
	}
}
