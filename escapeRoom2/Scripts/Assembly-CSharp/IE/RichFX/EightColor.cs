using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Color Effects/Eight Color")]
	public sealed class EightColor : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		private static class IDs
		{
			internal static readonly int Dithering = Shader.PropertyToID("_Dithering");

			internal static readonly int Downsampling = Shader.PropertyToID("_Downsampling");

			internal static readonly int InputTexture = Shader.PropertyToID("_InputTexture");

			internal static readonly int Opacity = Shader.PropertyToID("_Opacity");

			internal static readonly int Palette = Shader.PropertyToID("_Palette");
		}

		public ColorParameter color1 = new ColorParameter(new Color(0f, 0f, 0f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color2 = new ColorParameter(new Color(1f, 0f, 0f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color3 = new ColorParameter(new Color(0f, 1f, 0f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color4 = new ColorParameter(new Color(1f, 1f, 0f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color5 = new ColorParameter(new Color(0f, 0f, 1f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color6 = new ColorParameter(new Color(1f, 0f, 1f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color7 = new ColorParameter(new Color(0f, 1f, 1f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ColorParameter color8 = new ColorParameter(new Color(1f, 1f, 1f, 0f), hdr: false, showAlpha: true, showEyeDropper: true);

		public ClampedFloatParameter dithering = new ClampedFloatParameter(0.05f, 0f, 0.5f);

		public ClampedIntParameter downsampling = new ClampedIntParameter(1, 1, 32);

		public ClampedFloatParameter opacity = new ClampedFloatParameter(0f, 0f, 1f);

		private static Vector4[] _palette = new Vector4[8];

		private Material _material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (_material != null)
			{
				return opacity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			_material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/EightColor");
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle srcRT, RTHandle destRT)
		{
			if (!(_material == null))
			{
				_palette[0] = color1.value;
				_palette[1] = color2.value;
				_palette[2] = color3.value;
				_palette[3] = color4.value;
				_palette[4] = color5.value;
				_palette[5] = color6.value;
				_palette[6] = color7.value;
				_palette[7] = color8.value;
				_material.SetVectorArray(IDs.Palette, _palette);
				_material.SetFloat(IDs.Dithering, dithering.value);
				_material.SetInt(IDs.Downsampling, downsampling.value);
				_material.SetFloat(IDs.Opacity, opacity.value);
				_material.SetTexture(IDs.InputTexture, srcRT);
				HDUtils.DrawFullScreen(cmd, _material, destRT);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(_material);
		}
	}
}
