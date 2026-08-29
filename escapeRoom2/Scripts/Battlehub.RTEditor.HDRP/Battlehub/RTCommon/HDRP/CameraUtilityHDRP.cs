using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace Battlehub.RTCommon.HDRP
{
	public class CameraUtilityHDRP : MonoBehaviour, IRenderPipelineCameraUtility
	{
		public event Action<Camera, bool> PostProcessingEnabled;

		private void Awake()
		{
			if (RenderPipelineInfo.Type != RPType.HDRP)
			{
				UnityEngine.Object.Destroy(this);
			}
			else
			{
				IOC.RegisterFallback((IRenderPipelineCameraUtility)this);
			}
		}

		private void OnDestroy()
		{
			IOC.UnregisterFallback((IRenderPipelineCameraUtility)this);
		}

		public void EnablePostProcessing(Camera camera, bool value)
		{
			HDAdditionalCameraData hDAdditionalCameraData = camera.GetComponent<HDAdditionalCameraData>();
			if (hDAdditionalCameraData == null)
			{
				hDAdditionalCameraData = camera.gameObject.AddComponent<HDAdditionalCameraData>();
			}
			hDAdditionalCameraData.customRenderingSettings = true;
			FrameSettingsOverrideMask renderingPathCustomFrameSettingsOverrideMask = hDAdditionalCameraData.renderingPathCustomFrameSettingsOverrideMask;
			FrameSettings renderingPathCustomFrameSettings = hDAdditionalCameraData.renderingPathCustomFrameSettings;
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.Postprocess, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.AfterPostprocess, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.TransparentPostpass, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.TransparentPostpass, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.LowResTransparent, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ShadowMaps, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ContactShadows, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ScreenSpaceShadows, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.Transmission, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ExposureControl, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ReflectionProbe, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.PlanarProbe, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ReplaceDiffuseForIndirect, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.SkyReflection, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.DirectSpecularLighting, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.Volumetrics, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ReprojectionForVolumetrics, value);
			renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.CustomPostProcess, value);
			renderingPathCustomFrameSettingsOverrideMask.mask[15u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[17u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[9u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[9u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[18u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[20u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[21u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[34u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[46u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[26u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[32u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[33u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[35u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[36u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[37u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[38u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[28u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[29u] = true;
			renderingPathCustomFrameSettingsOverrideMask.mask[39u] = true;
			hDAdditionalCameraData.renderingPathCustomFrameSettingsOverrideMask = renderingPathCustomFrameSettingsOverrideMask;
			hDAdditionalCameraData.renderingPathCustomFrameSettings = renderingPathCustomFrameSettings;
			if (this.PostProcessingEnabled != null)
			{
				this.PostProcessingEnabled(camera, value);
			}
		}

		public bool IsPostProcessingEnabled(Camera camera)
		{
			HDAdditionalCameraData component = camera.GetComponent<HDAdditionalCameraData>();
			if (component == null)
			{
				return false;
			}
			FrameSettings renderingPathCustomFrameSettings = component.renderingPathCustomFrameSettings;
			return renderingPathCustomFrameSettings.IsEnabled(FrameSettingsField.Postprocess);
		}

		public void RequiresDepthTexture(Camera camera, bool value)
		{
		}

		public void Stack(Camera baseCamera, Camera overlayCamera)
		{
			overlayCamera.clearFlags = CameraClearFlags.Nothing;
		}

		public void SetBackgroundColor(Camera camera, Color color)
		{
			HDAdditionalCameraData hDAdditionalCameraData = camera.GetComponent<HDAdditionalCameraData>();
			if (hDAdditionalCameraData == null)
			{
				hDAdditionalCameraData = camera.gameObject.AddComponent<HDAdditionalCameraData>();
			}
			hDAdditionalCameraData.backgroundColorHDR = color;
			hDAdditionalCameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
		}

		public void ResetCullingMask(Camera camera)
		{
			camera.cullingMask = LayerMask.GetMask("TransparentFX");
		}
	}
}
