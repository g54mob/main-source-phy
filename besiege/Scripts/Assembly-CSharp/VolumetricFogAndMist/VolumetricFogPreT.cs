using UnityEngine;

namespace VolumetricFogAndMist
{
	[ExecuteInEditMode]
	[ImageEffectAllowedInSceneView]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("")]
	public class VolumetricFogPreT : MonoBehaviour, IVolumetricFogRenderComponent
	{
		private RenderTexture opaqueFrame;

		public VolumetricFog fog { get; set; }

		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (fog == null || !fog.enabled)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (fog.renderBeforeTransparent)
			{
				fog.DoOnRenderImage(source, destination);
				return;
			}
			opaqueFrame = RenderTexture.GetTemporary(source.width, source.height, source.depth, source.format, RenderTextureReadWrite.Default, source.antiAliasing);
			fog.DoOnRenderImage(source, opaqueFrame);
			Shader.SetGlobalTexture("_VolumetricFog_OpaqueFrame", opaqueFrame);
			Graphics.Blit(opaqueFrame, destination);
		}

		private void OnPostRender()
		{
			if (opaqueFrame != null)
			{
				RenderTexture.ReleaseTemporary(opaqueFrame);
				opaqueFrame = null;
			}
		}

		public void DestroySelf()
		{
			Object.DestroyImmediate(this);
		}
	}
}
