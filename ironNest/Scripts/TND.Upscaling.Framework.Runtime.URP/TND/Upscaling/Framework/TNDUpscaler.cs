using System.Collections.Generic;
using TND.Upscaling.Framework.URP;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera), typeof(UniversalAdditionalCameraData))]
	public class TNDUpscaler : UpscalerController_URP
	{
		public bool SetUpscaler(UpscalerName upscalerName)
		{
			return false;
		}

		public UpscalerName GetSelectedUpscaler()
		{
			return default(UpscalerName);
		}

		public UpscalerName GetActiveUpscaler()
		{
			return default(UpscalerName);
		}

		public void SetQuality(UpscalerQuality value)
		{
		}

		public UpscalerQuality GetQuality()
		{
			return default(UpscalerQuality);
		}

		public float GetScaling()
		{
			return 0f;
		}

		public float GetRenderScale()
		{
			return 0f;
		}

		public void SetSharpening(bool value)
		{
		}

		public bool GetSharpening()
		{
			return false;
		}

		public void SetSharpness(float value)
		{
		}

		public float GetSharpness()
		{
			return 0f;
		}

		public void SetAutoReactive(bool value)
		{
		}

		public bool GetAutoReactive()
		{
			return false;
		}

		public void SetInjectionPoint(UpscalerInjectionPoint upscalerInjectionPoint)
		{
		}

		public UpscalerInjectionPoint GetInjectionPoint()
		{
			return default(UpscalerInjectionPoint);
		}

		public UpscalerSettingsBase GetUpscalerSettings(UpscalerName upscalerName)
		{
			return null;
		}

		public TSettings GetUpscalerSettings<TSettings>(UpscalerName upscalerName)
		{
			return default(TSettings);
		}

		public void ResetCamera()
		{
		}

		public static List<UpscalerName> GetSupported()
		{
			return null;
		}

		public static void GetSupported(List<UpscalerName> supported)
		{
		}

		public static bool IsSupported(UpscalerName upscalerName)
		{
			return false;
		}
	}
}
