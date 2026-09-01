using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class DitheringRenderer : PostProcessEffectRenderer<Dithering>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Dithering");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			if ((bool)base.settings.lut.value)
			{
				propertySheet.properties.SetTexture("_LUT", base.settings.lut.value);
			}
			float z = ((QualitySettings.activeColorSpace == ColorSpace.Gamma) ? Mathf.LinearToGammaSpace(base.settings.luminanceThreshold.value) : base.settings.luminanceThreshold.value);
			Vector4 value = new Vector4(0f, base.settings.tiling, z, base.settings.intensity);
			propertySheet.properties.SetVector("_Dithering_Coords", value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}
	}
}
