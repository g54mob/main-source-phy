using BitCode.Graphics;
using UnityEngine;

namespace TFBGames
{
	public abstract class PlatformImageHandling
	{
		public abstract Texture2D CreateTextureFromImageData(ImageData imageData);

		public Sprite CreateSpriteFromImageData(ImageData imageData)
		{
			Texture2D texture2D = CreateTextureFromImageData(imageData);
			return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
		}
	}
}
