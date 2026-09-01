using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	public class FileDownloadHandler : DownloadHandlerScript
	{
		private int _contentLength;

		private int _received;

		private FileStream _stream;

		public int contentLength
		{
			get
			{
				return (_received <= _contentLength) ? _contentLength : _received;
			}
		}

		public FileDownloadHandler(string localFilePath, int bufferSize = 65536, FileShare fileShare = FileShare.ReadWrite)
			: base(new byte[bufferSize])
		{
			string directoryName = Path.GetDirectoryName(localFilePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			_contentLength = -1;
			_received = 0;
			_stream = new FileStream(localFilePath, FileMode.OpenOrCreate, FileAccess.Write, fileShare, bufferSize);
		}

		protected override float GetProgress()
		{
			return (contentLength > 0) ? Mathf.Clamp01((float)_received / (float)contentLength) : 0f;
		}

		protected override void ReceiveContentLength(int contentLength)
		{
			_contentLength = contentLength;
		}

		protected override bool ReceiveData(byte[] data, int dataLength)
		{
			if (data == null || data.Length == 0)
			{
				return false;
			}
			_received += dataLength;
			_stream.Write(data, 0, dataLength);
			return true;
		}

		protected override void CompleteContent()
		{
			CloseStream();
		}

		public new void Dispose()
		{
			CloseStream();
			base.Dispose();
		}

		private void CloseStream()
		{
			if (_stream != null)
			{
				_stream.Dispose();
				_stream = null;
			}
		}

		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		protected override string GetText()
		{
			return string.Empty;
		}
	}
}
