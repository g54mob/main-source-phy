using System;
using UnityEngine;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class TextureSetting
	{
		public Texture2D texture;

		public Color color = new Color(1f, 1f, 1f, 1f);

		public bool useTextureAlpha = true;

		[Range(0f, 1f)]
		public float textureAlpha = 1f;

		public Vector2 scale = Vector2.one;

		public Vector2 offset = Vector2.zero;

		public VectorType scrollType;

		public Vector2 vector = Vector2.zero;

		public AnimationCurve vectorXAnimationCurve;

		public AnimationCurve vectorYAnimationCurve;

		public float curveMultiplier = 1f;

		public float positionOffsetTime;

		public TextureSetting()
		{
		}

		public TextureSetting(TextureSetting src)
		{
			texture = src.texture;
			color = src.color;
			useTextureAlpha = src.useTextureAlpha;
			textureAlpha = src.textureAlpha;
			scale = src.scale;
			offset = src.offset;
			vector = src.vector;
			positionOffsetTime = src.positionOffsetTime;
		}
	}
}
