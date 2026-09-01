using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class CloudShadowsRenderer : PostProcessEffectRenderer<CloudShadows>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Cloud Shadows");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			_ = context.command;
			Camera camera = context.camera;
			CloudShadows.isOrtho = context.camera.orthographic;
			propertySheet.properties.SetTexture("_NoiseTex", base.settings.texture.value ? ((Texture)base.settings.texture) : Texture2D.whiteTexture);
			Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, renderIntoTexture: false);
			float value = (gPUProjectionMatrix[3, 2] = 0f);
			gPUProjectionMatrix[2, 3] = value;
			gPUProjectionMatrix[3, 3] = 1f;
			Matrix4x4 value2 = Matrix4x4.Inverse(gPUProjectionMatrix * camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, 0f - gPUProjectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value2);
			float num2 = (float)base.settings.speed * 0.1f;
			propertySheet.properties.SetVector("_CloudParams", new Vector4((float)base.settings.size * 0.01f, base.settings.direction.value.x * num2, base.settings.direction.value.y * num2, base.settings.density));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}

		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}
	}
}
