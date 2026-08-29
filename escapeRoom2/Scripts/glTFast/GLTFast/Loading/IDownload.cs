using System;

namespace GLTFast.Loading
{
	public interface IDownload : IDisposable
	{
		bool Success { get; }

		string Error { get; }

		byte[] Data { get; }

		string Text { get; }

		bool? IsBinary { get; }
	}
}
