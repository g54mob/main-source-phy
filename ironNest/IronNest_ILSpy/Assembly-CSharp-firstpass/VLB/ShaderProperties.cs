using UnityEngine;

namespace VLB;

public static class ShaderProperties
{
	public static class SD
	{
		public static readonly int FadeOutFactor;

		public static readonly int ConeSlopeCosSin;

		public static readonly int AlphaInside;

		public static readonly int AlphaOutside;

		public static readonly int AttenuationLerpLinearQuad;

		public static readonly int DistanceCamClipping;

		public static readonly int FresnelPow;

		public static readonly int GlareBehind;

		public static readonly int GlareFrontal;

		public static readonly int DrawCap;

		public static readonly int DepthBlendDistance;

		public static readonly int CameraParams;

		public static readonly int DynamicOcclusionClippingPlaneWS;

		public static readonly int DynamicOcclusionClippingPlaneProps;

		public static readonly int DynamicOcclusionDepthTexture;

		public static readonly int DynamicOcclusionDepthProps;

		public static readonly int LocalForwardDirection;

		public static readonly int TiltVector;

		public static readonly int AdditionalClippingPlaneWS;

		static SD()
		{
			int fadeOutFactor = Shader.PropertyToID("_FadeOutFactor");
			FadeOutFactor = fadeOutFactor;
			int coneSlopeCosSin = Shader.PropertyToID("_ConeSlopeCosSin");
			ConeSlopeCosSin = coneSlopeCosSin;
			int alphaInside = Shader.PropertyToID("_AlphaInside");
			AlphaInside = alphaInside;
			int alphaOutside = Shader.PropertyToID("_AlphaOutside");
			AlphaOutside = alphaOutside;
			int attenuationLerpLinearQuad = Shader.PropertyToID("_AttenuationLerpLinearQuad");
			AttenuationLerpLinearQuad = attenuationLerpLinearQuad;
			int distanceCamClipping = Shader.PropertyToID("_DistanceCamClipping");
			DistanceCamClipping = distanceCamClipping;
			int fresnelPow = Shader.PropertyToID("_FresnelPow");
			FresnelPow = fresnelPow;
			int glareBehind = Shader.PropertyToID("_GlareBehind");
			GlareBehind = glareBehind;
			int glareFrontal = Shader.PropertyToID("_GlareFrontal");
			GlareFrontal = glareFrontal;
			int drawCap = Shader.PropertyToID("_DrawCap");
			DrawCap = drawCap;
			int depthBlendDistance = Shader.PropertyToID("_DepthBlendDistance");
			DepthBlendDistance = depthBlendDistance;
			int cameraParams = Shader.PropertyToID("_CameraParams");
			CameraParams = cameraParams;
			int dynamicOcclusionClippingPlaneWS = Shader.PropertyToID("_DynamicOcclusionClippingPlaneWS");
			DynamicOcclusionClippingPlaneWS = dynamicOcclusionClippingPlaneWS;
			int dynamicOcclusionClippingPlaneProps = Shader.PropertyToID("_DynamicOcclusionClippingPlaneProps");
			DynamicOcclusionClippingPlaneProps = dynamicOcclusionClippingPlaneProps;
			int dynamicOcclusionDepthTexture = Shader.PropertyToID("_DynamicOcclusionDepthTexture");
			DynamicOcclusionDepthTexture = dynamicOcclusionDepthTexture;
			int dynamicOcclusionDepthProps = Shader.PropertyToID("_DynamicOcclusionDepthProps");
			DynamicOcclusionDepthProps = dynamicOcclusionDepthProps;
			int localForwardDirection = Shader.PropertyToID("_LocalForwardDirection");
			LocalForwardDirection = localForwardDirection;
			int tiltVector = Shader.PropertyToID("_TiltVector");
			TiltVector = tiltVector;
			int additionalClippingPlaneWS = Shader.PropertyToID("_AdditionalClippingPlaneWS");
			AdditionalClippingPlaneWS = additionalClippingPlaneWS;
		}
	}

	public static class HD
	{
		public static readonly int Intensity;

		public static readonly int SideSoftness;

		public static readonly int CameraForwardOS;

		public static readonly int CameraForwardWS;

		public static readonly int TransformScale;

		public static readonly int ShadowDepthTexture;

		public static readonly int ShadowProps;

		public static readonly int Jittering;

		public static readonly int CookieTexture;

		public static readonly int CookieProperties;

		public static readonly int CookiePosAndScale;

		public static readonly int GlobalCameraBlendingDistance;

		public static readonly int GlobalJitteringNoiseTex;

		static HD()
		{
			int intensity = Shader.PropertyToID("_Intensity");
			Intensity = intensity;
			int sideSoftness = Shader.PropertyToID("_SideSoftness");
			SideSoftness = sideSoftness;
			int cameraForwardOS = Shader.PropertyToID("_CameraForwardOS");
			CameraForwardOS = cameraForwardOS;
			int cameraForwardWS = Shader.PropertyToID("_CameraForwardWS");
			CameraForwardWS = cameraForwardWS;
			int transformScale = Shader.PropertyToID("_TransformScale");
			TransformScale = transformScale;
			int shadowDepthTexture = Shader.PropertyToID("_ShadowDepthTexture");
			ShadowDepthTexture = shadowDepthTexture;
			int shadowProps = Shader.PropertyToID("_ShadowProps");
			ShadowProps = shadowProps;
			int jittering = Shader.PropertyToID("_Jittering");
			Jittering = jittering;
			int cookieTexture = Shader.PropertyToID("_CookieTexture");
			CookieTexture = cookieTexture;
			int cookieProperties = Shader.PropertyToID("_CookieProperties");
			CookieProperties = cookieProperties;
			int cookiePosAndScale = Shader.PropertyToID("_CookiePosAndScale");
			CookiePosAndScale = cookiePosAndScale;
			int globalCameraBlendingDistance = Shader.PropertyToID("_VLB_CameraBlendingDistance");
			GlobalCameraBlendingDistance = globalCameraBlendingDistance;
			int globalJitteringNoiseTex = Shader.PropertyToID("_VLB_JitteringNoiseTex");
			GlobalJitteringNoiseTex = globalJitteringNoiseTex;
		}
	}

	public static readonly int ConeRadius;

	public static readonly int ConeGeomProps;

	public static readonly int ColorFlat;

	public static readonly int DistanceFallOff;

	public static readonly int NoiseVelocityAndScale;

	public static readonly int NoiseParam;

	public static readonly int ColorGradientMatrix;

	public static readonly int LocalToWorldMatrix;

	public static readonly int WorldToLocalMatrix;

	public static readonly int BlendSrcFactor;

	public static readonly int BlendDstFactor;

	public static readonly int ZTest;

	public static readonly int ParticlesTintColor;

	public static readonly int HDRPExposureWeight;

	public static readonly int GlobalUsesReversedZBuffer;

	public static readonly int GlobalNoiseTex3D;

	public static readonly int GlobalNoiseCustomTime;

	public static readonly int GlobalDitheringFactor;

	public static readonly int GlobalDitheringNoiseTex;

	static ShaderProperties()
	{
		int coneRadius = Shader.PropertyToID("_ConeRadius");
		ConeRadius = coneRadius;
		int coneGeomProps = Shader.PropertyToID("_ConeGeomProps");
		ConeGeomProps = coneGeomProps;
		int colorFlat = Shader.PropertyToID("_ColorFlat");
		ColorFlat = colorFlat;
		int distanceFallOff = Shader.PropertyToID("_DistanceFallOff");
		DistanceFallOff = distanceFallOff;
		int noiseVelocityAndScale = Shader.PropertyToID("_NoiseVelocityAndScale");
		NoiseVelocityAndScale = noiseVelocityAndScale;
		int noiseParam = Shader.PropertyToID("_NoiseParam");
		NoiseParam = noiseParam;
		int colorGradientMatrix = Shader.PropertyToID("_ColorGradientMatrix");
		ColorGradientMatrix = colorGradientMatrix;
		int localToWorldMatrix = Shader.PropertyToID("_LocalToWorldMatrix");
		LocalToWorldMatrix = localToWorldMatrix;
		int worldToLocalMatrix = Shader.PropertyToID("_WorldToLocalMatrix");
		WorldToLocalMatrix = worldToLocalMatrix;
		int blendSrcFactor = Shader.PropertyToID("_BlendSrcFactor");
		BlendSrcFactor = blendSrcFactor;
		int blendDstFactor = Shader.PropertyToID("_BlendDstFactor");
		BlendDstFactor = blendDstFactor;
		int zTest = Shader.PropertyToID("_ZTest");
		ZTest = zTest;
		int particlesTintColor = Shader.PropertyToID("_TintColor");
		ParticlesTintColor = particlesTintColor;
		int hDRPExposureWeight = Shader.PropertyToID("_HDRPExposureWeight");
		HDRPExposureWeight = hDRPExposureWeight;
		int globalUsesReversedZBuffer = Shader.PropertyToID("_VLB_UsesReversedZBuffer");
		GlobalUsesReversedZBuffer = globalUsesReversedZBuffer;
		int globalNoiseTex3D = Shader.PropertyToID("_VLB_NoiseTex3D");
		GlobalNoiseTex3D = globalNoiseTex3D;
		int globalNoiseCustomTime = Shader.PropertyToID("_VLB_NoiseCustomTime");
		GlobalNoiseCustomTime = globalNoiseCustomTime;
		int globalDitheringFactor = Shader.PropertyToID("_VLB_DitheringFactor");
		GlobalDitheringFactor = globalDitheringFactor;
		int globalDitheringNoiseTex = Shader.PropertyToID("_VLB_DitheringNoiseTex");
		GlobalDitheringNoiseTex = globalDitheringNoiseTex;
	}
}
