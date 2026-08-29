using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public abstract class TextureVignettableBase : VignettableBase
	{
		public FlexibleTextureParameter texture = new FlexibleTextureParameter(null, overrideState: true);

		private const string ENABLE_VIGNETTE_TEXTURE = "ENABLE_VIGNETTE_TEXTURE";

		private Vector2 textureRenderOffset = Vector2.zero;

		public float prevRenderTime;

		public float startRenderTime;

		public float timeOffset;

		public override void Setup()
		{
			base.Setup();
			timeOffset = 0f;
			timeOffset = GetTime();
			prevRenderTime = GetTime();
			startRenderTime = GetTime();
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			SetVignetteTextureSetting(m_Material, texture.value);
			float time = GetTime();
			float num = time - prevRenderTime;
			Vector2 vector = Vector2.zero;
			switch (texture.value.scrollType)
			{
			case VectorType.Constant:
				vector = texture.value.vector;
				break;
			case VectorType.AnimationCurve:
			{
				float time2 = (time - startRenderTime) * texture.value.curveMultiplier;
				vector = new Vector2((texture.value.vectorXAnimationCurve == null) ? 0f : texture.value.vectorXAnimationCurve.Evaluate(time2), (texture.value.vectorYAnimationCurve == null) ? 0f : texture.value.vectorYAnimationCurve.Evaluate(time2));
				break;
			}
			}
			textureRenderOffset += vector * num;
			prevRenderTime = time;
			m_Material.SetFloat("_VignetteTexturePositionX", textureRenderOffset.x);
			m_Material.SetFloat("_VignetteTexturePositionY", textureRenderOffset.y);
			base.Render(cmd, camera, source, destination);
		}

		private float GetTime()
		{
			return Time.time - timeOffset;
		}

		public static void SetVignetteTextureSetting(Material mat, TextureSetting vignetteTextureSetting)
		{
			if (vignetteTextureSetting.texture == null)
			{
				mat.DisableKeyword("ENABLE_VIGNETTE_TEXTURE");
				return;
			}
			mat.EnableKeyword("ENABLE_VIGNETTE_TEXTURE");
			mat.SetTexture("_VignetteTexture", vignetteTextureSetting.texture);
			mat.SetInt("_VignetteTextureUseTextureAlpha", vignetteTextureSetting.useTextureAlpha ? 1 : 0);
			mat.SetFloat("_VignetteTextureTextureAlpha", vignetteTextureSetting.textureAlpha);
			mat.SetFloat("_VignetteTextureScaleX", vignetteTextureSetting.scale.x);
			mat.SetFloat("_VignetteTextureScaleY", vignetteTextureSetting.scale.y);
			mat.SetColor("_VignetteTextureColor", vignetteTextureSetting.color);
			mat.SetFloat("_VignetteTextureOffsetX", vignetteTextureSetting.offset.x);
			mat.SetFloat("_VignetteTextureOffsetY", vignetteTextureSetting.offset.y);
			mat.SetFloat("_VignetteTextureOffsetTime", vignetteTextureSetting.positionOffsetTime);
		}
	}
}
