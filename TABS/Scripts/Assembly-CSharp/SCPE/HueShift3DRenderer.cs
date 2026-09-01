using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class HueShift3DRenderer : PostProcessEffectRenderer<HueShift3D>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/3D Hue Shift");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			HueShift3D.isOrtho = context.camera.orthographic;
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.speed, base.settings.size, base.settings.geoInfluence, base.settings.intensity));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}

		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.DepthNormals;
		}
	}
}
