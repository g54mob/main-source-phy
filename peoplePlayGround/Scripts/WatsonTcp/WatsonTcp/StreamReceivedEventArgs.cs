using System;
using System.Collections.Generic;
using System.IO;

namespace WatsonTcp
{
	public class StreamReceivedEventArgs : EventArgs
	{
		private Dictionary<object, object> _Metadata = new Dictionary<object, object>();

		private byte[] _Data;

		private int _BufferSize = 65536;

		public string IpPort { get; }

		public Dictionary<object, object> Metadata
		{
			get
			{
				return _Metadata;
			}
			set
			{
				if (value == null)
				{
					_Metadata = new Dictionary<object, object>();
				}
				else
				{
					_Metadata = value;
				}
			}
		}

		public long ContentLength { get; }

		public Stream DataStream { get; }

		public byte[] Data
		{
			get
			{
				if (_Data != null)
				{
					return _Data;
				}
				if (ContentLength <= 0)
				{
					return null;
				}
				_Data = ReadFromStream(DataStream, ContentLength);
				return _Data;
			}
		}

		internal StreamReceivedEventArgs(string ipPort, Dictionary<object, object> metadata, long contentLength, Stream stream)
		{
			IpPort = ipPort;
			Metadata = metadata;
			ContentLength = contentLength;
			DataStream = stream;
		}

		private byte[] ReadFromStream(Stream stream, long count)
		{
			if (count <= 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[_BufferSize];
			int num = 0;
			long num2 = count;
			MemoryStream memoryStream = new MemoryStream();
			while (num2 > 0)
			{
				if (_BufferSize > num2)
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
	}
}
