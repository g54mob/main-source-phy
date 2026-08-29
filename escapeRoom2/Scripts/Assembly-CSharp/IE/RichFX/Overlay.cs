using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Color Effects/Overlay")]
	public sealed class Overlay : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public enum SourceType
		{
			Color = 0,
			Gradient = 1,
			Texture = 2
		}

		public enum BlendMode
		{
			Normal = 0,
			Screen = 1,
			Overlay = 2,
			Multiply = 3,
			SoftLight = 4,
			HardLight = 5
		}

		[Serializable]
		public sealed class SourceTypeParameter : VolumeParameter<SourceType>
		{
		}

		[Serializable]
		public sealed class BlendModeParameter : VolumeParameter<BlendMode>
		{
		}

		public SourceTypeParameter sourceType = new SourceTypeParameter
		{
			value = SourceType.Gradient
		};

		public BlendModeParameter blendMode = new BlendModeParameter
		{
			value = BlendMode.Overlay
		};

		public ClampedFloatParameter opacity = new ClampedFloatParameter(0f, 0f, 1f);

		public ColorParameter color = new ColorParameter(Color.red, hdr: false, showAlpha: false, showEyeDropper: true);

		public GradientParameter gradient = new GradientParameter();

		public ClampedFloatParameter angle = new ClampedFloatParameter(0f, -180f, 180f);

		public TextureParameter texture = new TextureParameter(null);

		public BoolParameter sourceAlpha = new BoolParameter(value: true);

		private GradientColorKey[] _gradientCache;

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return opacity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			m_Material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/Overlay");
			_gradientCache = gradient.value.colorKeys;
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			m_Material.SetFloat("_Opacity", opacity.value);
			int num = (int)blendMode.value * 3;
			if (sourceType == SourceType.Color)
			{
				m_Material.SetColor("_Color", color.value);
				m_Material.SetTexture("_OverlayTexture", Texture2D.whiteTexture);
				m_Material.SetFloat("_UseTextureAlpha", 0f);
			}
			else if (sourceType == SourceType.Gradient)
			{
				float f = MathF.PI / 180f * angle.value;
				Vector2 vector = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
				m_Material.SetVector("_Direction", vector);
				GradientUtility.SetColorKeys(m_Material, _gradientCache);
				num += ((_gradientCache.Length <= 3) ? 1 : 2);
			}
			else
			{
				if (texture.value == null)
				{
					return;
				}
				m_Material.SetColor("_Color", Color.white);
				m_Material.SetTexture("_OverlayTexture", texture.value);
				m_Material.SetFloat("_UseTextureAlpha", sourceAlpha.value ? 1 : 0);
			}
			m_Material.SetTexture("_InputTexture", source);
			HDUtils.DrawFullScreen(cmd, m_Material, destination, null, num);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
