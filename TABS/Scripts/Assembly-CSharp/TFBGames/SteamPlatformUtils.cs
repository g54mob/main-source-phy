using System;
using BitCode.Graphics;
using UnityEngine;

namespace TFBGames
{
	public class SteamPlatformUtils : PlatformImageHandling, IPlatformUtils, IService
	{
		public bool IsUIOpenOrLostFocus => !Application.isFocused;

		public bool IsRunningInBackground => false;

		public override Texture2D CreateTextureFromImageData(ImageData imageData)
		{
			TextureFormat textureFormat;
			switch (imageData.DataFormat)
			{
			case ImageDataFormat.Rgba:
				textureFormat = TextureFormat.RGBA32;
				break;
			case ImageDataFormat.Rgb:
				textureFormat = TextureFormat.RGB24;
				break;
			default:
				throw new ArgumentException();
			}
			Texture2D texture2D = new Texture2D((int)imageData.Width, (int)imageData.Height, textureFormat, mipChain: false);
			texture2D.LoadRawTextureData(imageData.Data);
			texture2D.Apply();
			return texture2D;
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}
	}
}
