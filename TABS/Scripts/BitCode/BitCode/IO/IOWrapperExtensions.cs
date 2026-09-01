using System;
using BitCode.Threading;

namespace BitCode.IO
{
	public static class IOWrapperExtensions
	{
		private sealed class ZXjJJXdfjUCYdxswWWjOSJImoRgO
		{
			public Action<byte[], long, Exception> wRYPpvdBdjHIboMZuRxppMmSSqgW;

			internal void kPhBJupNJctNpgLOGwTSccsLYjRR((long bytesRead, byte[] readBuffer) P_0, Exception P_1)
			{
				wRYPpvdBdjHIboMZuRxppMmSSqgW(P_0.readBuffer, P_0.bytesRead, P_1);
			}
		}

		private sealed class QclOKXGADUwwpmeYAYwgYnNEKlvJ
		{
			public Action<byte[], long, Exception> wRYPpvdBdjHIboMZuRxppMmSSqgW;

			internal void kPhBJupNJctNpgLOGwTSccsLYjRR((long bytesRead, byte[] readBuffer) P_0, Exception P_1)
			{
				wRYPpvdBdjHIboMZuRxppMmSSqgW(P_0.readBuffer, P_0.bytesRead, P_1);
			}
		}

		public static void WriteToFileAsync(this IIOWrapper ioWrapper, string path, byte[] buffer, Action<Exception> onCompleted)
		{
			ioWrapper.WriteToFileAsync(path, buffer).ContinueWithAsync(onCompleted);
		}

		public static void WriteToFileAsync(this IIOWrapper ioWrapper, string path, byte[] buffer, int offset, int length, Action<Exception> onCompleted)
		{
			ioWrapper.WriteToFileAsync(path, buffer, offset, length).ContinueWithAsync(onCompleted);
		}

		public static void ReadFromFileAsync(this IIOWrapper ioWrapper, string path, byte[] buffer, Action<byte[], long, Exception> onCompleted)
		{
			ZXjJJXdfjUCYdxswWWjOSJImoRgO zXjJJXdfjUCYdxswWWjOSJImoRgO = new ZXjJJXdfjUCYdxswWWjOSJImoRgO();
			while (true)
			{
				int num = -1523110268;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1145587223)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_0028;
					case 2u:
						return;
					}
					break;
					IL_0028:
					zXjJJXdfjUCYdxswWWjOSJImoRgO.wRYPpvdBdjHIboMZuRxppMmSSqgW = onCompleted;
					ioWrapper.ReadFromFileAsync(path, buffer).ContinueWithAsync(zXjJJXdfjUCYdxswWWjOSJImoRgO.kPhBJupNJctNpgLOGwTSccsLYjRR);
					num = (int)((num2 * 2029106166) ^ 0x5F1DCEEF);
				}
			}
		}

		public static void ReadFromFileAsync(this IIOWrapper ioWrapper, string path, byte[] buffer, int offset, Action<byte[], long, Exception> onCompleted)
		{
			QclOKXGADUwwpmeYAYwgYnNEKlvJ qclOKXGADUwwpmeYAYwgYnNEKlvJ = new QclOKXGADUwwpmeYAYwgYnNEKlvJ();
			qclOKXGADUwwpmeYAYwgYnNEKlvJ.wRYPpvdBdjHIboMZuRxppMmSSqgW = onCompleted;
			ioWrapper.ReadFromFileAsync(path, buffer, offset).ContinueWithAsync(qclOKXGADUwwpmeYAYwgYnNEKlvJ.kPhBJupNJctNpgLOGwTSccsLYjRR);
		}
	}
}
