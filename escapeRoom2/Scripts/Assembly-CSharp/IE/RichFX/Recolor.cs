using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Color Effects/Recolor")]
	public sealed class Recolor : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public enum EdgeSource
		{
			Color = 0,
			Depth = 1,
			Normal = 2
		}

		[Serializable]
		public sealed class EdgeSourceParameter : VolumeParameter<EdgeSource>
		{
		}

		public enum DitherType
		{
			Bayer2x2 = 0,
			Bayer3x3 = 1,
			Bayer4x4 = 2,
			Bayer8x8 = 3
		}

		[Serializable]
		public sealed class DitherTypeParameter : VolumeParameter<DitherType>
		{
		}

		private static class ShaderIDs
		{
			internal static readonly int DitherStrength = Shader.PropertyToID("_DitherStrength");

			internal static readonly int DitherTexture = Shader.PropertyToID("_DitherTexture");

			internal static readonly int EdgeColor = Shader.PropertyToID("_EdgeColor");

			internal static readonly int EdgeThresholds = Shader.PropertyToID("_EdgeThresholds");

			internal static readonly int FillOpacity = Shader.PropertyToID("_FillOpacity");

			internal static readonly int InputTexture = Shader.PropertyToID("_InputTexture");
		}

		public ColorParameter edgeColor = new ColorParameter(new Color(0f, 0f, 0f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public EdgeSourceParameter edgeSource = new EdgeSourceParameter
		{
			value = EdgeSource.Depth
		};

		public ClampedFloatParameter edgeThreshold = new ClampedFloatParameter(0.5f, 0f, 1f);

		public ClampedFloatParameter edgeContrast = new ClampedFloatParameter(0.5f, 0f, 1f);

		public GradientParameter fillGradient = new GradientParameter();

		public ClampedFloatParameter fillOpacity = new ClampedFloatParameter(0f, 0f, 1f);

		public DitherTypeParameter ditherType = new DitherTypeParameter
		{
			value = DitherType.Bayer4x4
		};

		public ClampedFloatParameter ditherStrength = new ClampedFloatParameter(0f, 0f, 1f);

		private Material _material;

		private Gradient _cachedGradient;

		private GradientColorKey[] _cachedColorKeys;

		private DitherType _ditherType;

		private Texture2D _ditherTexture;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (_material != null)
			{
				if (!(edgeColor.value.a > 0f))
				{
					return fillOpacity.value > 0f;
				}
				return true;
			}
			return false;
		}

		public override void Setup()
		{
			_material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/Recolor");
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle srcRT, RTHandle destRT)
		{
			if (_ditherType != ditherType.value || _ditherTexture == null)
			{
				CoreUtils.Destroy(_ditherTexture);
				_ditherType = ditherType.value;
				_ditherTexture = GenerateDitherTexture(_ditherType);
			}
			if (_cachedGradient != fillGradient.value)
			{
				_cachedGradient = fillGradient.value;
				_cachedColorKeys = _cachedGradient.colorKeys;
			}
			Vector2 vector;
			if (edgeSource.value == EdgeSource.Depth)
			{
				float num = 1f / Mathf.Lerp(1000f, 1f, edgeThreshold.value);
				float num2 = 1f + 2f / (1.01f - edgeContrast.value);
				vector = new Vector2(num, num * num2);
			}
			else
			{
				float value = edgeThreshold.value;
				float y = value + 1.01f - edgeContrast.value;
				vector = new Vector2(value, y);
			}
			_material.SetColor(ShaderIDs.EdgeColor, edgeColor.value);
			_material.SetVector(ShaderIDs.EdgeThresholds, vector);
			_material.SetFloat(ShaderIDs.FillOpacity, fillOpacity.value);
			GradientUtility.SetColorKeys(_material, _cachedColorKeys);
			_material.SetTexture(ShaderIDs.DitherTexture, _ditherTexture);
			_material.SetFloat(ShaderIDs.DitherStrength, ditherStrength.value);
			int num3 = (int)edgeSource.value;
			if (fillOpacity.value > 0f && _cachedColorKeys.Length > 4)
			{
				num3 += 3;
			}
			if (fillGradient.value.mode == GradientMode.Blend)
			{
				num3 += 6;
			}
			_material.SetTexture(ShaderIDs.InputTexture, srcRT);
			HDUtils.DrawFullScreen(cmd, _material, destRT, null, num3);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(_material);
			CoreUtils.Destroy(_ditherTexture);
		}

		private static Texture2D GenerateDitherTexture(DitherType type)
		{
			switch (type)
			{
			case DitherType.Bayer2x2:
			{
				Texture2D texture2D4 = new Texture2D(2, 2, TextureFormat.R8, mipChain: false, linear: true);
				texture2D4.LoadRawTextureData(new byte[4] { 0, 170, 255, 85 });
				texture2D4.Apply();
				return texture2D4;
			}
			case DitherType.Bayer3x3:
			{
				Texture2D texture2D3 = new Texture2D(3, 3, TextureFormat.R8, mipChain: false, linear: true);
				texture2D3.LoadRawTextureData(new byte[9] { 0, 223, 95, 191, 159, 63, 127, 31, 255 });
				texture2D3.Apply();
				return texture2D3;
			}
			case DitherType.Bayer4x4:
			{
				Texture2D texture2D2 = new Texture2D(4, 4, TextureFormat.R8, mipChain: false, linear: true);
				texture2D2.LoadRawTextureData(new byte[16]
				{
					0, 136, 34, 170, 204, 68, 238, 102, 51, 187,
					17, 153, 255, 119, 221, 85
				});
				texture2D2.Apply();
				return texture2D2;
			}
			case DitherType.Bayer8x8:
			{
				Texture2D texture2D = new Texture2D(8, 8, TextureFormat.R8, mipChain: false, linear: true);
				texture2D.LoadRawTextureData(new byte[64]
				{
					0, 194, 48, 242, 12, 206, 60, 255, 129, 64,
					178, 113, 141, 76, 190, 125, 32, 226, 16, 210,
					44, 238, 28, 222, 161, 97, 145, 80, 174, 109,
					157, 93, 8, 202, 56, 250, 4, 198, 52, 246,
					137, 72, 186, 121, 133, 68, 182, 117, 40, 234,
					24, 218, 36, 230, 20, 214, 170, 105, 153, 89,
					165, 101, 149, 85
				});
				texture2D.Apply();
				return texture2D;
			}
			default:
				return null;
			}
		}
	}
}
