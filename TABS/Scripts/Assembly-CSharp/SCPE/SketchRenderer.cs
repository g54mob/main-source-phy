using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class SketchRenderer : PostProcessEffectRenderer<Sketch>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Sketch");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, renderIntoTexture: false);
			float value = (gPUProjectionMatrix[3, 2] = 0f);
			gPUProjectionMatrix[2, 3] = value;
			gPUProjectionMatrix[3, 3] = 1f;
			Matrix4x4 value2 = Matrix4x4.Inverse(gPUProjectionMatrix * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, 0f - gPUProjectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value2);
			if ((bool)base.settings.strokeTex.value)
			{
				propertySheet.properties.SetTexture("_Strokes", base.settings.strokeTex);
			}
			propertySheet.properties.SetVector("_Params", new Vector4(0f, (float)base.settings.blendMode.value, base.settings.intensity, (base.settings.projectionMode.value == Sketch.SketchProjectionMode.ScreenSpace) ? ((float)base.settings.tiling * 0.1f) : ((float)base.settings.tiling)));
			propertySheet.properties.SetVector("_Brightness", base.settings.brightness);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.projectionMode.value);
		}
	}
}
