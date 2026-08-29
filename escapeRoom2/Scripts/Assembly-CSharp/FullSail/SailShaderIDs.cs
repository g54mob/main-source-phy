using UnityEngine;

namespace FullSail
{
	public class SailShaderIDs
	{
		public static int _Color;

		public static int _MainTex;

		public static int _Speed;

		public static int _RippleNoise;

		public static int _Emblem;

		public static int _EmblemColor;

		public static int _EmblemEMColor;

		public static int _EmblemBackface;

		public static int _Damage;

		public static int _DamageTex;

		public static int _DamageTex1;

		public static int _DamageTex2;

		public static int _BumpMap;

		public static int _SpecColor;

		public static int _Smoothness;

		public static int _AlphaCutoff;

		public static int _WindDir;

		public static int _SailForward;

		public static int _MaskMap;

		public static int _MaskMapRev;

		public static int _SailWind;

		public static int _FillPercent;

		public static int _SailLift;

		public static int _SailLiftArch;

		public static int _SailLiftSideArch;

		public static int _SailReverse;

		public static int _SailTaper;

		public static int _SailShear;

		public static int _SailTilt;

		public static int _SailArch;

		public static int _SailTopArch;

		public static int _SailArchStart;

		public static int _SailSideArch;

		public static int _FullRipple;

		public static int _Furl;

		public static int _FurlOrder;

		public static int _FurlMap;

		public static int _FurledMap;

		public static int _FurledRadius;

		public static int _RippleStrength;

		public static int _RippleSpeed;

		public static int _RippleSeed;

		public static int _RippleScale;

		public static int _RippleSmooth;

		public static int _Thickness;

		public static int _Power;

		public static int _Distortion;

		public static int _Scale;

		public static int _SubColor;

		public static int _SailSideways;

		public static int _ImpactCount;

		public static int _ImpactPoints;

		public static int _ImpactTex;

		public static int _ImpactRipple;

		public static int _ImpactLocation;

		public static int _YScale;

		public static int _SailTiltTop;

		public static int _EmblemTex_ST;

		public static int _Transparent;

		private static bool haveIds;

		public static void MakeIds()
		{
			if (!haveIds)
			{
				_Color = Shader.PropertyToID("_Color");
				_MainTex = Shader.PropertyToID("_MainTex");
				_Speed = Shader.PropertyToID("_Speed");
				_RippleNoise = Shader.PropertyToID("_RippleNoise");
				_Emblem = Shader.PropertyToID("_EmblemTex");
				_EmblemColor = Shader.PropertyToID("_EmblemColor");
				_EmblemEMColor = Shader.PropertyToID("_EmblemEMColor");
				_EmblemBackface = Shader.PropertyToID("_EmblemBackface");
				_Damage = Shader.PropertyToID("_Damage");
				_DamageTex = Shader.PropertyToID("_DamageTex");
				_DamageTex1 = Shader.PropertyToID("_DamageTex1");
				_DamageTex2 = Shader.PropertyToID("_DamageTex2");
				_BumpMap = Shader.PropertyToID("_BumpMap");
				_SpecColor = Shader.PropertyToID("_SpecColor");
				_Smoothness = Shader.PropertyToID("_Smoothness");
				_AlphaCutoff = Shader.PropertyToID("_AlphaCutoff");
				_WindDir = Shader.PropertyToID("_WindDir");
				_SailForward = Shader.PropertyToID("_SailForward");
				_MaskMap = Shader.PropertyToID("_MaskMap");
				_MaskMapRev = Shader.PropertyToID("_MaskMapRev");
				_SailWind = Shader.PropertyToID("_SailWind");
				_FillPercent = Shader.PropertyToID("_FillPercent");
				_SailLift = Shader.PropertyToID("_SailLift");
				_SailLiftArch = Shader.PropertyToID("_SailLiftArch");
				_SailLiftSideArch = Shader.PropertyToID("_SailLiftSideArch");
				_SailReverse = Shader.PropertyToID("_SailReverse");
				_SailTaper = Shader.PropertyToID("_SailTaper");
				_SailShear = Shader.PropertyToID("_SailShear");
				_SailTilt = Shader.PropertyToID("_SailTilt");
				_SailTiltTop = Shader.PropertyToID("_SailTiltTop");
				_SailArch = Shader.PropertyToID("_SailArch");
				_SailTopArch = Shader.PropertyToID("_SailTopArch");
				_SailArchStart = Shader.PropertyToID("_SailArchStart");
				_SailSideArch = Shader.PropertyToID("_SailSideArch");
				_FullRipple = Shader.PropertyToID("_FullRipple");
				_Furl = Shader.PropertyToID("_Furl");
				_FurlOrder = Shader.PropertyToID("_FurlOrder");
				_FurlMap = Shader.PropertyToID("_FurlMap");
				_FurledMap = Shader.PropertyToID("_FurledMap");
				_FurledRadius = Shader.PropertyToID("_FurledRadius");
				_RippleStrength = Shader.PropertyToID("_VorStrength");
				_RippleSpeed = Shader.PropertyToID("_VorSpeed");
				_RippleSeed = Shader.PropertyToID("_VorSeed");
				_RippleScale = Shader.PropertyToID("_VorScale");
				_RippleSmooth = Shader.PropertyToID("_VorSmoothness");
				_Thickness = Shader.PropertyToID("_Thickness");
				_Power = Shader.PropertyToID("_Power");
				_Distortion = Shader.PropertyToID("_Distortion");
				_Scale = Shader.PropertyToID("_Scale");
				_SubColor = Shader.PropertyToID("_SubColor");
				_SailSideways = Shader.PropertyToID("_SailSideways");
				_ImpactCount = Shader.PropertyToID("_ImpactCount");
				_ImpactPoints = Shader.PropertyToID("_ImpactPoints");
				_ImpactTex = Shader.PropertyToID("_ImpactTex");
				_ImpactRipple = Shader.PropertyToID("_ImpactRipple");
				_ImpactLocation = Shader.PropertyToID("_ImpactLocation");
				_YScale = Shader.PropertyToID("_YScale");
				_EmblemTex_ST = Shader.PropertyToID("_EmblemTex_ST");
				_Transparent = Shader.PropertyToID("_Transparent");
				haveIds = true;
			}
		}
	}
}
