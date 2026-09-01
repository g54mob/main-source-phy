using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class EdgeDetectionRenderer : PostProcessEffectRenderer<EdgeDetection>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Edge Detection");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			_ = context.command;
			Vector2 vector = new Vector2(base.settings.sensitivityDepth, base.settings.sensitivityNormals);
			propertySheet.properties.SetVector("_Sensitivity", vector);
			propertySheet.properties.SetFloat("_BackgroundFade", base.settings.debug ? 1f : 0f);
			propertySheet.properties.SetFloat("_EdgeSize", (int)base.settings.edgeSize);
			propertySheet.properties.SetFloat("_Exponent", base.settings.edgeExp);
			propertySheet.properties.SetFloat("_Threshold", base.settings.lumThreshold);
			propertySheet.properties.SetColor("_EdgeColor", base.settings.edgeColor);
			float x = (context.camera.orthographic ? ((float)base.settings.fadeDistance * 1E-10f) : ((float)base.settings.fadeDistance));
			propertySheet.properties.SetVector("_DistanceParams", new Vector4(x, base.settings.invertFadeDistance ? 1 : 0, 0f, 0f));
			propertySheet.properties.SetVector("_SobelParams", new Vector4(base.settings.sobelThin ? 1 : 0, 0f, 0f, 0f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value);
		}

		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.DepthNormals;
		}
	}
}
