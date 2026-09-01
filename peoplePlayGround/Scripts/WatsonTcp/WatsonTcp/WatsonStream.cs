using System;
using System.IO;

namespace WatsonTcp
{
	public class WatsonStream : Stream
	{
		private readonly object _Lock = new object();

		private Stream _Stream;

		private long _Length;

		private long _Position;

		public override bool CanRead => true;

		public override bool CanSeek => false;

		public override bool CanWrite => false;

		public override long Length => _Length;

		public override long Position
		{
			get
			{
				return _Position;
			}
			set
			{
				throw new InvalidOperationException("Position may not be modified.");
			}
		}

		private long _BytesRemaining => _Length - _Position;

		internal WatsonStream(long contentLength, Stream stream)
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			_Length = contentLength;
			_Stream = stream;
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentException("Offset must be zero or greater.");
			}
			if (offset >= buffer.Length)
			{
				throw new IndexOutOfRangeException("Offset must be less than the buffer length of " + buffer.Length + ".");
			}
			if (count < 0)
			{
				throw new ArgumentException("Count must be zero or greater.");
			}
			if (count == 0)
			{
				return 0;
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("Offset and count must sum to a value less than the buffer length of " + buffer.Length + ".");
			}
			lock (_Lock)
			{
				byte[] array = null;
				if (_BytesRemaining == 0L)
				{
					return 0;
				}
				array = ((count <= _BytesRemaining) ? new byte[count] : new byte[_BytesRemaining]);
				int num = _Stream.Read(array, 0, array.Length);
				Buffer.BlockCopy(array, 0, buffer, offset, num);
				_Position += num;
				return num;
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Seek operations are not supported.");
		}

		public override void SetLength(long value)
		{
			throw new InvalidOperationException("Length may not be modified.");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Stream is not writeable.");
		}
	}
}
