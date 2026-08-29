using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GLTFast
{
	internal static class StreamExtension
	{
		public static async Task<bool> ReadToArrayAsync(this Stream stream, byte[] destination, int offset, int length, CancellationToken cancellationToken)
		{
			int pendingBytes = length;
			int num;
			do
			{
				num = await stream.ReadAsync(destination, offset, pendingBytes, cancellationToken);
				pendingBytes -= num;
				offset += num;
			}
			while (pendingBytes > 0 && num > 0);
			return pendingBytes <= 0;
		}
	}
}
