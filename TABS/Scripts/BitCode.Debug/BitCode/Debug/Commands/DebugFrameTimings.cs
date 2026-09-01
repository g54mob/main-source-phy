using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public sealed class DebugFrameTimings
	{
		[DebugCommand(Description = "Prints out current frame timings as captured by the FrameTimingManager.")]
		public static void PrintFrameTimings(IDebugConsoleWriter writer, uint frames = 10u)
		{
			FrameTiming[] array = new FrameTiming[frames];
			FrameTiming frameTiming = default(FrameTiming);
			int num3 = default(int);
			uint latestTimings = default(uint);
			while (true)
			{
				int num = -1194015286;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -2111193157)) % 16)
					{
					case 14u:
						break;
					default:
						return;
					case 5u:
						writer.AppendLine($"  GPU Time: {frameTiming.gpuFrameTime} ms");
						num = ((int)num2 * -1258468361) ^ -1449662176;
						continue;
					case 13u:
						num3++;
						num = ((int)num2 * -1836081036) ^ 0x255E2BDB;
						continue;
					case 0u:
						writer.AppendLine($"  y-Scale: {frameTiming.heightScale}");
						num = ((int)num2 * -449368298) ^ -1400126512;
						continue;
					case 8u:
						writer.AppendLine();
						writer.AppendLine($"  x-Scale: {frameTiming.widthScale}");
						num = (int)((num2 * 645781984) ^ 0x198DA28B);
						continue;
					case 9u:
						writer.AppendLine($"  CPU Time: {frameTiming.cpuFrameTime} ms");
						num = ((int)num2 * -1223443042) ^ 0x6F03BC20;
						continue;
					case 12u:
						writer.AppendLine($"  Present: {frameTiming.cpuTimePresentCalled}");
						num = (int)((num2 * 737463469) ^ 0x463AF46A);
						continue;
					case 11u:
						writer.AppendLine();
						writer.AppendLine($"  vSync interval: {frameTiming.syncInterval}");
						num = ((int)num2 * -171319330) ^ -1901619965;
						continue;
					case 15u:
						writer.AppendLine($"Captured {latestTimings} frames.");
						num = ((int)num2 * -753492463) ^ -1110459934;
						continue;
					case 2u:
						writer.AppendLine($"  FrameComplete: {frameTiming.cpuTimeFrameComplete}");
						num = (int)(num2 * 1336949076) ^ -813114017;
						continue;
					case 6u:
						num3 = 0;
						num = (int)((num2 * 1587204712) ^ 0x748DCD8C);
						continue;
					case 3u:
						frameTiming = array[num3];
						writer.AppendLine($"Frame {num3}:\n---");
						num = -1608226030;
						continue;
					case 4u:
					{
						int num4;
						if (num3 >= latestTimings)
						{
							num = -1727989999;
							num4 = num;
						}
						else
						{
							num = -1785187608;
							num4 = num;
						}
						continue;
					}
					case 7u:
						num = ((int)num2 * -1589030814) ^ 0x31455011;
						continue;
					case 1u:
						FrameTimingManager.CaptureFrameTimings();
						latestTimings = FrameTimingManager.GetLatestTimings(frames, array);
						num = (int)((num2 * 1936865045) ^ 0x7A5698F1);
						continue;
					case 10u:
						return;
					}
					break;
				}
			}
		}
	}
}
