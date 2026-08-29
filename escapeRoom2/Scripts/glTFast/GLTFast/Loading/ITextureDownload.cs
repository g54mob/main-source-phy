using System;
using UnityEngine;

namespace GLTFast.Loading
{
	public interface ITextureDownload : IDownload, IDisposable
	{
		Texture2D Texture { get; }
	}
}
