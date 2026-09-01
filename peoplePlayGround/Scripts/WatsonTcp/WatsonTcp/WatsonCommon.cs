using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WatsonTcp
{
	internal static class WatsonCommon
	{
		internal static byte[] ReadStreamFully(Stream input)
		{
			byte[] array = new byte[65536];
			using MemoryStream memoryStream = new MemoryStream();
			int num = 0;
			while (true)
			{
				num = input.Read(array, 0, array.Length);
				if (num <= 0)
				{
					break;
				}
				memoryStream.Write(array, 0, num);
			}
			return memoryStream.ToArray();
		}

		internal static byte[] ReadFromStream(Stream stream, long count, int bufferLen)
		{
			if (count <= 0)
			{
				return new byte[0];
			}
			if (bufferLen <= 0)
			{
				throw new ArgumentException("Buffer must be greater than zero bytes.");
			}
			byte[] array = new byte[bufferLen];
			int num = 0;
			long num2 = count;
			MemoryStream memoryStream = new MemoryStream();
			while (num2 > 0)
			{
				if (bufferLen > num2)
				{
					array = new byte[num2];
				}
				num = stream.Read(array, 0, array.Length);
				if (num > 0)
				{
					memoryStream.Write(array, 0, num);
					num2 -= num;
					continue;
				}
				throw new IOException("Could not read from supplied stream.");
			}
			return memoryStream.ToArray();
		}

		internal static MemoryStream DataStreamToMemoryStream(long contentLength, Stream stream, int bufferLen)
		{
			if (contentLength <= 0)
			{
				return new MemoryStream(new byte[0]);
			}
			if (bufferLen <= 0)
			{
				throw new ArgumentException("Buffer must be greater than zero bytes.");
			}
			byte[] array = new byte[bufferLen];
			int num = 0;
			long num2 = contentLength;
			MemoryStream memoryStream = new MemoryStream();
			while (num2 > 0)
			{
				if (bufferLen > num2)
				{
					array = new byte[num2];
				}
				num = stream.Read(array, 0, array.Length);
				if (num > 0)
				{
					memoryStream.Write(array, 0, num);
					num2 -= num;
					continue;
				}
				throw new IOException("Could not read from supplied stream.");
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		internal static async Task<byte[]> ReadFromStreamAsync(Stream stream, long count, int bufferLen)
		{
			if (count <= 0)
			{
				return null;
			}
			if (bufferLen <= 0)
			{
				throw new ArgumentException("Buffer must be greater than zero bytes.");
			}
			byte[] buffer = new byte[bufferLen];
			long bytesRemaining = count;
			using MemoryStream ms = new MemoryStream();
			while (bytesRemaining > 0)
			{
				if (bufferLen > bytesRemaining)
				{
					buffer = new byte[bytesRemaining];
				}
				int num = await stream.ReadAsync(buffer, 0, buffer.Length);
				if (num > 0)
				{
					ms.Write(buffer, 0, num);
					bytesRemaining -= num;
					continue;
				}
				throw new IOException("Could not read from supplied stream.");
			}
			return ms.ToArray();
		}

		internal static async Task<byte[]> ReadMessageDataAsync(WatsonMessage msg, int bufferLen)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (msg.ContentLength == 0L)
			{
				return new byte[0];
			}
			return await ReadFromStreamAsync(msg.DataStream, msg.ContentLength, bufferLen);
		}

		internal static byte[] AppendBytes(byte[] head, byte[] tail)
		{
			byte[] array = new byte[head.Length + tail.Length];
			Array.Copy(head, 0, array, 0, head.Length);
			Array.Copy(tail, 0, array, head.Length, tail.Length);
			return array;
		}

		internal static string ByteArrayToHex(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder(data.Length * 2);
			foreach (byte b in data)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		internal static void BytesToStream(byte[] data, int start, out int contentLength, out Stream stream)
		{
			contentLength = 0;
			stream = new MemoryStream(new byte[0]);
			if (data != null && data.Length != 0)
			{
				contentLength = data.Length - start;
				stream = new MemoryStream();
				stream.Write(data, start, contentLength);
				stream.Seek(0L, SeekOrigin.Begin);
			}
		}

		internal static DateTime GetExpirationTimestamp(WatsonMessage msg)
		{
			DateTime result = msg.Expiration.Value;
			if (msg.SenderTimestamp.HasValue)
			{
				result = result.AddMilliseconds((DateTime.Now - msg.SenderTimestamp.Value).TotalMilliseconds);
			}
			return result;
		}
	}
}
