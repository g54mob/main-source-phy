using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class VolumetricLightBeamSD : VolumetricLightBeamAbstractBase
{
	public delegate void OnWillCameraRenderCB(Camera cam);

	public delegate void OnBeamGeometryInitialized();

	private sealed class _003CCoPlaytimeUpdate_003Ed__199 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VolumetricLightBeamSD _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoPlaytimeUpdate_003Ed__199(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_005a: Expected I4, but got I8
			//IL_0109: Expected I4, but got O
			VolumetricLightBeamSD volumetricLightBeamSD = _003C_003E4__this;
			if (_003C_003E1__state == 0 || _003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				if (volumetricLightBeamSD._TrackChangesDuringPlaytime && _003C_003E4__this.enabled)
				{
					_003C_003E4__this.UpdateAfterManualPropertyChange();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				volumetricLightBeamSD.m_CoPlaytimeUpdate = null;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public new const string ClassName = "VolumetricLightBeamSD";

	public bool colorFromLight;

	public ColorMode colorMode;

	public Color color;

	public Gradient colorGradient;

	public bool intensityFromLight;

	public bool intensityModeAdvanced;

	public float intensityInside;

	public float intensityOutside;

	public float intensityMultiplier;

	public float hdrpExposureWeight;

	public BlendingMode blendingMode;

	public bool spotAngleFromLight;

	public float spotAngle;

	public float spotAngleMultiplier;

	public float coneRadiusStart;

	public ShaderAccuracy shaderAccuracy;

	public MeshType geomMeshType;

	public int geomCustomSides;

	public int geomCustomSegments;

	public Vector3 skewingLocalForwardDirection;

	public Transform clippingPlaneTransform;

	public bool geomCap;

	public AttenuationEquation attenuationEquation;

	public float attenuationCustomBlending;

	public float fallOffStart;

	public float fallOffEnd;

	public bool fallOffEndFromLight;

	public float fallOffEndMultiplier;

	public float depthBlendDistance;

	public float cameraClippingDistance;

	public float glareFrontal;

	public float glareBehind;

	public float fresnelPow;

	public NoiseMode noiseMode;

	public float noiseIntensity;

	public bool noiseScaleUseGlobal;

	public float noiseScaleLocal;

	public bool noiseVelocityUseGlobal;

	public Vector3 noiseVelocityLocal;

	public Dimensions dimensions;

	public Vector2 tiltFactor;

	private MaterialManager.SD.DynamicOcclusion m_INTERNAL_DynamicOcclusionMode;

	private bool m_INTERNAL_DynamicOcclusionMode_Runtime;

	private OnWillCameraRenderCB m_onWillCameraRenderThisBeam;

	private OnBeamGeometryInitialized m_OnBeamGeometryInitialized;

	private bool _TrackChangesDuringPlaytime;

	private int _SortingLayerID;

	private int _SortingOrder;

	private float _FadeOutBegin;

	private float _FadeOutEnd;

	private uint _003C_INTERNAL_InstancedMaterialGroupID_003Ek__BackingField;

	private BeamGeometrySD m_BeamGeom;

	private Coroutine m_CoPlaytimeUpdate;

	public ColorMode usedColorMode
	{
		get
		{
			//IL_0063: Expected I4, but got O
			Config instance = Config.Instance;
			if ((object)instance != null)
			{
				if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
				{
					return colorMode;
				}
				return ColorMode.Flat;
			}
			NullReferenceException ex = new NullReferenceException();
			return (ColorMode)ex;
		}
	}

	private bool useColorFromAttachedLightSpot
	{
		get
		{
			if (!colorFromLight)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
	}

	private bool useColorTemperatureFromAttachedLightSpot
	{
		get
		{
			//IL_00be: Expected I4, but got O
			if (colorFromLight && m_CachedLightSpot != null)
			{
				if ((object)m_CachedLightSpot != null)
				{
					if (!m_CachedLightSpot.useColorTemperature)
					{
						goto IL_00aa;
					}
					Config instance = Config.Instance;
					if ((object)instance != null)
					{
						return instance.useLightColorTemperature;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00aa;
			IL_00aa:
			return false;
		}
	}

	public float alphaInside
	{
		get
		{
			return intensityInside;
		}
		set
		{
			intensityInside = value;
		}
	}

	public float alphaOutside
	{
		get
		{
			return intensityOutside;
		}
		set
		{
			intensityOutside = value;
		}
	}

	public float intensityGlobal
	{
		get
		{
			return intensityOutside;
		}
		set
		{
			intensityInside = value;
			intensityOutside = value;
		}
	}

	public bool useIntensityFromAttachedLightSpot
	{
		get
		{
			if (!intensityFromLight)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
	}

	public bool useSpotAngleFromAttachedLightSpot
	{
		get
		{
			if (!spotAngleFromLight)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
	}

	public float coneAngle
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0046: Expected O, but got I
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Expected O, but got Unknown
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			//IL_00dc: Expected O, but got I
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Expected O, but got Unknown
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector & 0;
			float num = spotAngle * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			float num3 = num * 0.5f;
			object obj3 = obj + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
			Vector2 vector2 = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj4 = vector2 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num4 & 0;
			float num5 = (float)obj3 * num3;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
			{
				obj4 = obj5;
			}
			float num6 = num5 - coneRadiusStart;
			object obj6 = obj4 + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num7 = num6 * 57.29578f;
			return num7 + num7;
		}
	}

	public float coneRadiusEnd
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0046: Expected O, but got I
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector & 0;
			float num = spotAngle * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			float num3 = num * 0.5f;
			object obj3 = obj + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
			return (float)obj3 * num3;
		}
		set
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0034: Expected O, but got I
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			object obj3 = obj + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num2 = value * 57.29578f;
			float num3 = num2 + num2;
			spotAngle = num3;
		}
	}

	public float coneVolume
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0046: Expected O, but got I
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector & 0;
			float num = spotAngle * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			float num3 = num * 0.5f;
			object obj3 = obj + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
			float num4 = (float)obj3 * num3;
			float num5 = coneRadiusStart * coneRadiusStart;
			float num6 = coneRadiusStart * num4;
			float num7 = num4 * num4;
			float num8 = num5 + num6;
			float num9 = num8 + num7;
			float num10 = num9 * ((float)Math.PI / 3f);
			return num10 * fallOffEnd;
		}
	}

	public float coneApexOffsetZ
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0034: Expected O, but got I
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			//IL_00aa: Expected O, but got I
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Expected O, but got Unknown
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			float num2 = spotAngle * ((float)Math.PI / 180f);
			float num3 = num2 * 0.5f;
			object obj3 = obj + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
			float num4 = (float)obj3 * num3;
			float num5 = coneRadiusStart / num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803A1F0Bh\"");
			if (num5 == 1f)
			{
				return 3.4028235E+38f;
			}
			float num6 = 1f - num5;
			Vector2 vector2 = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj4 = vector2 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num7 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
			{
				obj4 = obj5;
			}
			object obj6 = obj4 + fallOffEnd;
			float num8 = (float)obj6 * num5;
			return num8 / num6;
		}
	}

	public unsafe Vector3 coneApexPositionLocal
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected F4, but got Unknown
			//IL_0028: Expected native int or pointer, but got O
			//IL_0035: Expected native int or pointer, but got O
			float num = coneApexOffsetZ;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			float z = num ^ 0;
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
	}

	public unsafe Vector3 coneApexPositionGlobal
	{
		get
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_0137: Expected native int or pointer, but got O
			//IL_0144: Expected native int or pointer, but got O
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
				float num = coneApexOffsetZ;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				object obj = num ^ 0;
				float num3 = default(float);
				float num2 = num3 * 0f;
				float num4 = num3 * 0f;
				float num5 = num3 * 0f;
				float num6 = num3 * 0f;
				float num7 = num2 + num5;
				float num8 = num4 + num6;
				float num9 = (float)obj * num3;
				float num10 = num7 + num9;
				float num11 = (float)obj * num3;
				float num12 = num8 + num11;
				float num13 = num10 + num3;
				float num14 = num12 + num3;
				float num15 = 1f / num14;
				float z = num15 * num13;
				Vector3 vector = default(Vector3);
				((Vector3*)(nint)vector)->x = num3;
				((Vector3*)(nint)vector)->z = z;
				return vector;
			}
			return (Vector3)new NullReferenceException();
		}
	}

	public int geomSides
	{
		get
		{
			//IL_0067: Expected I4, but got O
			if (geomMeshType == MeshType.Custom)
			{
				return geomCustomSides;
			}
			Config instance = Config.Instance;
			if ((object)instance != null)
			{
				return instance.sharedMeshSides;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			//IL_001e: Expected I, but got O
			//IL_007b: Expected O, but got I
			//IL_0034: Expected I, but got O
			//IL_0064: Expected I, but got O
			geomCustomSides = value;
			object[] array = new object[1];
			bool flag = "VolumetricLightBeamSD" == null;
			nint num = unchecked((nint)"VolumetricLightBeamSD");
			if (!flag)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj2 = default(object);
					throw obj2;
				}
				num = unchecked((nint)"VolumetricLightBeamSD");
			}
			array[0] = num;
			Debug.LogWarningFormat("The setter VLB.{0}.geomSides is OBSOLETE and has been renamed to geomCustomSides.", array);
		}
	}

	public int geomSegments
	{
		get
		{
			//IL_0067: Expected I4, but got O
			if (geomMeshType == MeshType.Custom)
			{
				return geomCustomSegments;
			}
			Config instance = Config.Instance;
			if ((object)instance != null)
			{
				return instance.sharedMeshSegments;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			//IL_001e: Expected I, but got O
			//IL_007b: Expected O, but got I
			//IL_0034: Expected I, but got O
			//IL_0064: Expected I, but got O
			geomCustomSegments = value;
			object[] array = new object[1];
			bool flag = "VolumetricLightBeamSD" == null;
			nint num = unchecked((nint)"VolumetricLightBeamSD");
			if (!flag)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj2 = default(object);
					throw obj2;
				}
				num = unchecked((nint)"VolumetricLightBeamSD");
			}
			array[0] = num;
			Debug.LogWarningFormat("The setter VLB.{0}.geomSegments is OBSOLETE and has been renamed to geomCustomSegments.", array);
		}
	}

	public unsafe Vector3 skewingLocalForwardDirectionNormalized
	{
		get
		{
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Expected O, but got Unknown
			//IL_0193: Invalid comparison between I and F4
			//IL_00d0: Expected I, but got O
			//IL_0056: Expected I, but got O
			//IL_0074: Expected F4, but got O
			//IL_006f: Expected native int or pointer, but got O
			//IL_0089: Expected F4, but got I
			//IL_0084: Expected native int or pointer, but got O
			//IL_0031: Expected native int or pointer, but got O
			//IL_003e: Expected native int or pointer, but got O
			//IL_01db: Expected I, but got O
			//IL_01f9: Expected F4, but got O
			//IL_01f4: Expected native int or pointer, but got O
			//IL_020e: Expected F4, but got I
			//IL_0209: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			Vector3 vector = default(Vector3);
			if (obj == null)
			{
				object obj2 = this + 144;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (VLB.VolumetricLightBeamSD)+98]");
				if (0f > 1E-05f)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (VLB.VolumetricLightBeamSD)+98]");
					float num = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (VLB.VolumetricLightBeamSD)+98]");
					float z = num / 0f;
					float x = default(float);
					((Vector3*)(nint)vector)->x = x;
					((Vector3*)(nint)vector)->z = z;
					return vector;
				}
				nint num2 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rax_v25 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num3 = 0;
				((Vector3*)(nint)vector)->x = (float)Vector3.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rcx_v16 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			object[] array = new object[1];
			string text = base.name;
			if (text != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				if (obj3 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj4 = default(object);
					throw obj4;
				}
			}
			if (array.Length > 0)
			{
				array[0] = text;
				Debug.LogErrorFormat("Beam {0} has a skewingLocalForwardDirection with a null Z, which is forbidden", array);
				nint num5 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num6 = 0;
				((Vector3*)(nint)vector)->x = (float)Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rax_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			return (Vector3)new IndexOutOfRangeException();
		}
	}

	public bool canHaveMeshSkewing
	{
		get
		{
			//IL_0010: Expected O, but got I4
			object obj = geomMeshType - 1;
			return obj == null;
		}
	}

	public bool hasMeshSkewing
	{
		get
		{
			//IL_0093: Expected I4, but got O
			//IL_00a1: Expected I, but got O
			Config instance = Config.Instance;
			if ((object)instance != null)
			{
				if (instance.featureEnabledMeshSkewing && geomMeshType == MeshType.Custom)
				{
					Vector3 vector = skewingLocalForwardDirectionNormalized;
					nint num = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ rax_v7 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num2 = 0;
					float num3 = vector.x * (float)Vector3.forwardVector;
					object obj2 = default(object);
					object obj3 = default(object);
					object obj = obj2 * obj3;
					float num4 = vector.z;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v5 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
					float num5 = num4 * 0f;
					float num6 = (float)obj + num3;
					float num7 = num6 + num5;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					object obj4 = default(object);
					if (obj4 == null)
					{
						return true;
					}
				}
				return false;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public unsafe Vector4 additionalClippingPlane
	{
		get
		{
			//IL_00c2: Expected F4, but got O
			//IL_00bd: Expected native int or pointer, but got O
			//IL_0097: Expected native int or pointer, but got O
			Vector4 vector = default(Vector4);
			if (!(clippingPlaneTransform == null))
			{
				if ((object)clippingPlaneTransform != null)
				{
					Vector3 forward = clippingPlaneTransform.forward;
					if ((object)clippingPlaneTransform != null)
					{
						Vector3 position = clippingPlaneTransform.position;
						float x = default(float);
						((Vector4*)(nint)vector)->x = x;
						return vector;
					}
				}
				return (Vector4)new NullReferenceException();
			}
			((Vector4*)(nint)vector)->x = (float)Vector4.zeroVector;
			return vector;
		}
	}

	public float attenuationLerpLinearQuad
	{
		get
		{
			//IL_0051: Expected F4, but got I4
			if (attenuationEquation != AttenuationEquation.Linear)
			{
				if (attenuationEquation != AttenuationEquation.Quadratic)
				{
					return attenuationCustomBlending;
				}
				return 1f;
			}
			return 0f;
		}
	}

	public float fadeStart
	{
		get
		{
			return fallOffStart;
		}
		set
		{
			fallOffStart = value;
		}
	}

	public float fadeEnd
	{
		get
		{
			return fallOffEnd;
		}
		set
		{
			fallOffEnd = value;
		}
	}

	public bool fadeEndFromLight
	{
		get
		{
			return fallOffEndFromLight;
		}
		set
		{
			fallOffEndFromLight = value;
		}
	}

	public bool useFallOffEndFromAttachedLightSpot
	{
		get
		{
			if (!fallOffEndFromLight)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
	}

	public float maxGeometryDistance
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0034: Expected O, but got I
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			return (float)obj + fallOffEnd;
		}
	}

	public bool isNoiseEnabled
	{
		get
		{
			bool flag = noiseMode < NoiseMode.Disabled;
			bool flag2 = noiseMode == NoiseMode.Disabled;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public bool noiseEnabled
	{
		get
		{
			bool flag = noiseMode < NoiseMode.Disabled;
			bool flag2 = noiseMode == NoiseMode.Disabled;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
		set
		{
			noiseMode = (value ? NoiseMode.WorldSpace : NoiseMode.Disabled);
		}
	}

	public unsafe float fadeOutBegin
	{
		get
		{
			return _FadeOutBegin;
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected Ref, but got Unknown
			SetFadeOutValue(ref *(float*)(this + 300), value);
		}
	}

	public unsafe float fadeOutEnd
	{
		get
		{
			return _FadeOutEnd;
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected Ref, but got Unknown
			SetFadeOutValue(ref *(float*)(this + 304), value);
		}
	}

	public bool isFadeOutEnabled
	{
		get
		{
			//IL_000b: Invalid comparison between F4 and I4
			//IL_0033: Invalid comparison between F4 and I4
			if (_FadeOutBegin < 0f)
			{
				return false;
			}
			bool flag = _FadeOutEnd < 0f;
			return !flag;
		}
	}

	public bool isTilted
	{
		get
		{
			//IL_0013: Expected I, but got O
			//IL_0040: Expected O, but got I
			//IL_0080: Invalid comparison between F4 and O
			//IL_009f: Invalid comparison between F4 and I4
			nint num = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v2 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
			object obj = num3 - 0;
			object obj2 = tiltFactor - Vector2.zeroVector;
			object obj3 = obj * obj;
			object obj4 = obj2 * obj2;
			object obj5 = obj3 + obj4;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
			float num4 = 1E-05f - (float)obj5;
			bool flag2 = num4 == 0f;
			return flag | flag2;
		}
	}

	public int sortingLayerID
	{
		get
		{
			return _SortingLayerID;
		}
		set
		{
			_SortingLayerID = value;
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.sortingLayerID = value;
			}
		}
	}

	public string sortingLayerName
	{
		get
		{
			return SortingLayer.IDToName(_SortingLayerID);
		}
		set
		{
			int num = (_SortingLayerID = SortingLayer.NameToID(value));
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.sortingLayerID = num;
			}
		}
	}

	public int sortingOrder
	{
		get
		{
			return _SortingOrder;
		}
		set
		{
			_SortingOrder = value;
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.sortingOrder = value;
			}
		}
	}

	public bool trackChangesDuringPlaytime
	{
		get
		{
			return _TrackChangesDuringPlaytime;
		}
		set
		{
			_TrackChangesDuringPlaytime = value;
			StartPlaytimeUpdateIfNeeded();
		}
	}

	public bool isCurrentlyTrackingChanges
	{
		get
		{
			bool flag = (nint)m_CoPlaytimeUpdate < 0;
			bool flag2 = m_CoPlaytimeUpdate == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public int blendingModeAsInt
	{
		get
		{
			//IL_00bf: Expected I4, but got O
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(BlendingMode));
			Array values = Enum.GetValues(typeFromHandle);
			if (values != null)
			{
				int num = values.System_002ECollections_002EICollection_002ECount;
				int result;
				if (blendingMode >= BlendingMode.Additive)
				{
					bool flag = (int)blendingMode <= num;
					result = (int)blendingMode;
					if (!flag)
					{
						return num;
					}
				}
				else
				{
					result = 0;
				}
				return result;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public unsafe Quaternion beamInternalLocalRotation
	{
		get
		{
			//IL_0062: Expected native int or pointer, but got O
			//IL_003a: Expected F4, but got O
			//IL_0035: Expected native int or pointer, but got O
			Quaternion quaternion = default(Quaternion);
			if (dimensions == Dimensions.Dim3D)
			{
				((Quaternion*)(nint)quaternion)->x = (float)Quaternion.identityQuaternion;
				return quaternion;
			}
			Vector3 forward = default(Vector3);
			Vector3 upwards = default(Vector3);
			((Quaternion*)(nint)quaternion)->x = Quaternion.Internal_LookRotation(ref forward, ref upwards).x;
			return quaternion;
		}
	}

	public unsafe Vector3 beamLocalForward
	{
		get
		{
			//IL_0080: Expected I, but got O
			//IL_009e: Expected F4, but got O
			//IL_0099: Expected native int or pointer, but got O
			//IL_00b3: Expected F4, but got I
			//IL_00ae: Expected native int or pointer, but got O
			//IL_003a: Expected I, but got O
			//IL_0058: Expected F4, but got O
			//IL_0053: Expected native int or pointer, but got O
			//IL_006d: Expected F4, but got I
			//IL_0068: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			if (dimensions == Dimensions.Dim3D)
			{
				nint num = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num2 = 0;
				((Vector3*)(nint)vector)->x = (float)Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			((Vector3*)(nint)vector)->x = (float)Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
	}

	public unsafe Vector3 beamGlobalForward
	{
		get
		{
			//IL_0043: Expected O, but got Ref
			//IL_0054: Expected native int or pointer, but got O
			//IL_0066: Expected native int or pointer, but got O
			Transform transform = base.transform;
			if (dimensions == Dimensions.Dim3D)
			{
			}
			if ((object)transform != null)
			{
				object obj = default(object);
				Vector3 vector = transform.TransformDirection((Vector3)(&obj));
				Vector3 vector2 = default(Vector3);
				((Vector3*)(nint)vector2)->x = vector.x;
				((Vector3*)(nint)vector2)->z = vector.z;
				return vector2;
			}
			return (Vector3)new NullReferenceException();
		}
	}

	public float raycastDistance
	{
		get
		{
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00e9: Expected O, but got I
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0070: Expected O, but got I
			if (hasMeshSkewing)
			{
				Vector3 vector = skewingLocalForwardDirectionNormalized;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				Vector2 vector2 = tiltFactor;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj = vector2 & 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj2 = num & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
				{
					obj = obj2;
				}
				float num2 = fallOffEnd + (float)obj;
				object obj3 = default(object);
				if (obj3 == null)
				{
					num2 /= vector.z;
				}
				return num2;
			}
			Vector2 vector3 = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj4 = vector3 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num3 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
			{
				obj4 = obj5;
			}
			return (float)obj4 + fallOffEnd;
		}
	}

	public unsafe Vector3 raycastGlobalForward
	{
		get
		{
			//IL_0041: Expected O, but got Ref
			//IL_0052: Expected native int or pointer, but got O
			//IL_0064: Expected native int or pointer, but got O
			if (hasMeshSkewing)
			{
				Vector3 vector = skewingLocalForwardDirectionNormalized;
			}
			if ((object)this != null)
			{
				object obj = default(object);
				Vector3 vector2 = ComputeRaycastGlobalVector((Vector3)(&obj));
				Vector3 vector3 = default(Vector3);
				((Vector3*)(nint)vector3)->x = vector2.x;
				((Vector3*)(nint)vector3)->z = vector2.z;
				return vector3;
			}
			return (Vector3)new NullReferenceException();
		}
	}

	public unsafe Vector3 raycastGlobalUp
	{
		get
		{
			//IL_000f: Expected O, but got Ref
			//IL_0020: Expected native int or pointer, but got O
			//IL_0032: Expected native int or pointer, but got O
			object obj = default(object);
			Vector3 vector = ComputeRaycastGlobalVector((Vector3)(&obj));
			Vector3 vector2 = default(Vector3);
			((Vector3*)(nint)vector2)->x = vector.x;
			((Vector3*)(nint)vector2)->z = vector.z;
			return vector2;
		}
	}

	public unsafe Vector3 raycastGlobalRight
	{
		get
		{
			//IL_000f: Expected O, but got Ref
			//IL_0020: Expected native int or pointer, but got O
			//IL_0032: Expected native int or pointer, but got O
			object obj = default(object);
			Vector3 vector = ComputeRaycastGlobalVector((Vector3)(&obj));
			Vector3 vector2 = default(Vector3);
			((Vector3*)(nint)vector2)->x = vector.x;
			((Vector3*)(nint)vector2)->z = vector.z;
			return vector2;
		}
	}

	public MaterialManager.SD.DynamicOcclusion _INTERNAL_DynamicOcclusionMode
	{
		get
		{
			//IL_0063: Expected I4, but got O
			Config instance = Config.Instance;
			if ((object)instance != null)
			{
				if (instance.featureEnabledDynamicOcclusion)
				{
					return m_INTERNAL_DynamicOcclusionMode;
				}
				return MaterialManager.SD.DynamicOcclusion.Off;
			}
			NullReferenceException ex = new NullReferenceException();
			return (MaterialManager.SD.DynamicOcclusion)ex;
		}
		set
		{
			m_INTERNAL_DynamicOcclusionMode = value;
		}
	}

	public MaterialManager.SD.DynamicOcclusion _INTERNAL_DynamicOcclusionMode_Runtime
	{
		get
		{
			//IL_0082: Expected I4, but got O
			if (m_INTERNAL_DynamicOcclusionMode_Runtime)
			{
				Config instance = Config.Instance;
				if ((object)instance == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (MaterialManager.SD.DynamicOcclusion)ex;
				}
				if (instance.featureEnabledDynamicOcclusion)
				{
					return m_INTERNAL_DynamicOcclusionMode;
				}
			}
			return MaterialManager.SD.DynamicOcclusion.Off;
		}
	}

	public uint _INTERNAL_InstancedMaterialGroupID
	{
		get
		{
			return _003C_INTERNAL_InstancedMaterialGroupID_003Ek__BackingField;
		}
		protected set
		{
			_003C_INTERNAL_InstancedMaterialGroupID_003Ek__BackingField = value;
		}
	}

	public string meshStats
	{
		get
		{
			//IL_00c2: Expected O, but got I
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Expected O, but got Unknown
			//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01af: Expected O, but got Unknown
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Expected O, but got Unknown
			//IL_01ed: Expected O, but got I
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_023b: Expected O, but got Unknown
			UnityEngine.Object obj;
			if ((bool)m_BeamGeom)
			{
				BeamGeometrySD beamGeom = m_BeamGeom;
				if ((object)m_BeamGeom == null)
				{
					return (string)(object)new NullReferenceException();
				}
				obj = ((BeamGeometryAbstractBase)beamGeom)._003CconeMesh_003Ek__BackingField;
			}
			else
			{
				obj = null;
			}
			if (!obj)
			{
				return "no mesh available";
			}
			float num = spotAngle * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			Vector2 vector = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj3 = vector & 0;
			float num3 = num * 0.5f;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj3 = obj2;
			}
			object obj4 = obj3 + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
			Vector2 vector2 = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = vector2 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj6 = num4 & 0;
			float num5 = (float)obj4 * num3;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
			{
				obj5 = obj6;
			}
			float num6 = num5 - coneRadiusStart;
			object obj7 = obj5 + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num7 = num6 * 57.29578f;
			float num8 = num7 + num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			int vertexCount = ((Mesh)obj).vertexCount;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			int[] triangles = ((Mesh)obj).triangles;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rcx+18h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			return $"Cone angle: {arg:0.0} degrees\nMesh: {arg2} vertices, {arg3} triangles";
		}
	}

	public int meshVerticesCount
	{
		get
		{
			//IL_00fc: Expected I4, but got O
			if ((bool)m_BeamGeom)
			{
				BeamGeometrySD beamGeom = m_BeamGeom;
				if ((object)m_BeamGeom != null)
				{
					if (!((BeamGeometryAbstractBase)beamGeom)._003CconeMesh_003Ek__BackingField)
					{
						goto IL_00e8;
					}
					BeamGeometrySD beamGeom2 = m_BeamGeom;
					if ((object)m_BeamGeom != null && (object)((BeamGeometryAbstractBase)beamGeom2)._003CconeMesh_003Ek__BackingField != null)
					{
						return ((BeamGeometryAbstractBase)beamGeom2)._003CconeMesh_003Ek__BackingField.vertexCount;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			goto IL_00e8;
			IL_00e8:
			return 0;
		}
	}

	public int meshTrianglesCount
	{
		get
		{
			//IL_0124: Expected I4, but got O
			if ((bool)m_BeamGeom)
			{
				BeamGeometrySD beamGeom = m_BeamGeom;
				if ((object)m_BeamGeom != null)
				{
					if (!((BeamGeometryAbstractBase)beamGeom)._003CconeMesh_003Ek__BackingField)
					{
						goto IL_0110;
					}
					BeamGeometrySD beamGeom2 = m_BeamGeom;
					if ((object)m_BeamGeom != null && (object)((BeamGeometryAbstractBase)beamGeom2)._003CconeMesh_003Ek__BackingField != null)
					{
						int[] triangles = ((BeamGeometryAbstractBase)beamGeom2)._003CconeMesh_003Ek__BackingField.triangles;
						if (triangles != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rcx+18h]\"");
							return 0;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			goto IL_0110;
			IL_0110:
			return 0;
		}
	}

	public event OnWillCameraRenderCB onWillCameraRenderThisBeam
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 272;
			Delegate obj2 = this.m_onWillCameraRenderThisBeam;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(OnWillCameraRenderCB);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 272;
			Delegate obj2 = this.m_onWillCameraRenderThisBeam;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(OnWillCameraRenderCB);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public unsafe void GetInsideAndOutsideIntensity(out float inside, out float outside)
	{
		//IL_0041: Expected Ref, but got F4
		//IL_004b: Expected Ref, but got F4
		//IL_002c: Expected Ref, but got F4
		//IL_0036: Expected Ref, but got F4
		if (!intensityModeAdvanced)
		{
			ref float reference = ref *(float*)intensityOutside;
			ref float reference2 = ref *(float*)intensityOutside;
		}
		else
		{
			ref float reference2 = ref *(float*)intensityInside;
			ref float reference = ref *(float*)intensityOutside;
		}
	}

	public override bool IsScalable()
	{
		return true;
	}

	public override BeamGeometryAbstractBase GetBeamGeometry()
	{
		return m_BeamGeom;
	}

	protected override void SetBeamGeometryNull()
	{
		m_BeamGeom = null;
	}

	public unsafe override Vector3 GetLossyScale()
	{
		//IL_0070: Expected native int or pointer, but got O
		//IL_016c: Expected native int or pointer, but got O
		//IL_012d: Expected native int or pointer, but got O
		//IL_013f: Expected native int or pointer, but got O
		float z;
		Vector3 vector = default(Vector3);
		if (dimensions == Dimensions.Dim3D)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 lossyScale = transform.lossyScale;
				z = lossyScale.z;
				((Vector3*)(nint)vector)->x = lossyScale.x;
				goto IL_0164;
			}
		}
		else
		{
			Transform transform2 = base.transform;
			if ((object)transform2 != null)
			{
				Vector3 lossyScale2 = transform2.lossyScale;
				Transform transform3 = base.transform;
				if ((object)transform3 != null)
				{
					Vector3 lossyScale3 = transform3.lossyScale;
					Transform transform4 = base.transform;
					if ((object)transform4 != null)
					{
						Vector3 lossyScale4 = transform4.lossyScale;
						((Vector3*)(nint)vector)->x = lossyScale2.z;
						((Vector3*)(nint)vector)->y = lossyScale3.y;
						z = lossyScale4.x;
						goto IL_0164;
					}
				}
			}
		}
		return (Vector3)new NullReferenceException();
		IL_0164:
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	private unsafe Vector3 ComputeRaycastGlobalVector(Vector3 localVec)
	{
		//IL_0051: Expected O, but got Ref
		//IL_0051: Expected O, but got Ref
		//IL_0062: Expected native int or pointer, but got O
		//IL_0074: Expected native int or pointer, but got O
		Transform transform = base.transform;
		if ((object)transform != null)
		{
			Quaternion rotation = transform.rotation;
			Quaternion quaternion = beamInternalLocalRotation;
			object obj = default(object);
			float num = default(float);
			Vector3 vector = (Quaternion)(&obj) * (Vector3)(&num);
			Vector3 vector2 = default(Vector3);
			((Vector3*)(nint)vector2)->x = vector.x;
			((Vector3*)(nint)vector2)->z = vector.z;
			return vector2;
		}
		return (Vector3)new NullReferenceException();
	}

	public void _INTERNAL_SetDynamicOcclusionCallback(string shaderKeyword, MaterialModifier.Callback cb)
	{
		bool flag = cb == null;
		bool iNTERNAL_DynamicOcclusionMode_Runtime = !flag;
		m_INTERNAL_DynamicOcclusionMode_Runtime = iNTERNAL_DynamicOcclusionMode_Runtime;
		if ((bool)m_BeamGeom)
		{
			m_BeamGeom.SetDynamicOcclusionCallback(shaderKeyword, cb);
		}
	}

	public void _INTERNAL_OnWillCameraRenderThisBeam(Camera cam)
	{
		if (this.m_onWillCameraRenderThisBeam != null)
		{
			OnWillCameraRenderCB onWillCameraRenderCB = this.m_onWillCameraRenderThisBeam;
			IntPtr invoke_impl = ((Delegate)onWillCameraRenderCB).invoke_impl;
			IntPtr method = ((Delegate)onWillCameraRenderCB).method;
			IntPtr method_code = ((Delegate)onWillCameraRenderCB).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v12 @ rax_v1 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public void RegisterOnBeamGeometryInitializedCallback(OnBeamGeometryInitialized cb)
	{
		Delegate obj = Delegate.Combine(m_OnBeamGeometryInitialized, cb);
		if ((object)obj == null)
		{
			m_OnBeamGeometryInitialized = (OnBeamGeometryInitialized)obj;
			goto IL_009b;
		}
		bool flag = (object)obj.GetType() != typeof(OnBeamGeometryInitialized);
		Delegate obj2 = null;
		if (!flag)
		{
			obj2 = obj;
		}
		if ((object)obj2 != null)
		{
			m_OnBeamGeometryInitialized = (OnBeamGeometryInitialized)obj2;
			bool flag2 = (object)obj.GetType() != typeof(OnBeamGeometryInitialized);
			Delegate obj3 = null;
			if (!flag2)
			{
				obj3 = obj;
			}
			bool flag3 = (object)obj3 == null;
			Delegate obj4 = obj;
			Delegate typeFromHandle = (Delegate)(object)typeof(OnBeamGeometryInitialized);
			if (!flag3)
			{
				goto IL_009b;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			Delegate typeFromHandle = obj;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_009b:
		if ((bool)m_BeamGeom && m_OnBeamGeometryInitialized != null)
		{
			OnBeamGeometryInitialized onBeamGeometryInitialized = m_OnBeamGeometryInitialized;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v188.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			m_OnBeamGeometryInitialized = null;
		}
	}

	private void CallOnBeamGeometryInitializedCallback()
	{
		if (m_OnBeamGeometryInitialized != null)
		{
			OnBeamGeometryInitialized onBeamGeometryInitialized = m_OnBeamGeometryInitialized;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v14.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			m_OnBeamGeometryInitialized = null;
		}
	}

	private unsafe void SetFadeOutValue(ref float propToChange, float value)
	{
		//IL_000b: Invalid comparison between F4 and I4
		//IL_003b: Invalid comparison between F4 and I4
		//IL_00ea: Expected Ref, but got F4
		//IL_00f5: Invalid comparison between F4 and I4
		//IL_006a: Invalid comparison between F4 and I4
		//IL_0133: Invalid comparison between F4 and I4
		//IL_0090: Invalid comparison between F4 and I4
		bool flag;
		if (_FadeOutBegin < 0f)
		{
			flag = false;
		}
		else
		{
			bool flag2 = _FadeOutEnd < 0f;
			flag = !flag2;
		}
		ref float reference = ref *(float*)value;
		bool flag3;
		if (_FadeOutBegin < 0f)
		{
			flag3 = false;
		}
		else
		{
			bool flag4 = _FadeOutEnd < 0f;
			flag3 = !flag4;
		}
		if (flag3 != flag && !(_FadeOutBegin < 0f) && !(_FadeOutEnd < 0f) && (bool)m_BeamGeom)
		{
			m_BeamGeom.RestartFadeOutCoroutine();
		}
	}

	private void OnFadeOutStateChanged()
	{
		//IL_0069: Invalid comparison between F4 and I4
		//IL_0010: Invalid comparison between F4 and I4
		if (!(_FadeOutBegin < 0f) && !(_FadeOutEnd < 0f) && (bool)m_BeamGeom)
		{
			m_BeamGeom.RestartFadeOutCoroutine();
		}
	}

	public unsafe float GetInsideBeamFactor(Vector3 posWS)
	{
		//IL_001d: Expected O, but got Ref
		//IL_002b: Expected O, but got Ref
		Transform transform = base.transform;
		float num = default(float);
		Vector3 vector = transform.InverseTransformPoint((Vector3)(&num));
		return GetInsideBeamFactorFromObjectSpacePos((Vector3)(&num));
	}

	public unsafe float GetInsideBeamFactorFromObjectSpacePos(Vector3 posOS)
	{
		//IL_01a4: Invalid comparison between I4 and F4
		//IL_002f: Expected native int or pointer, but got O
		//IL_0041: Expected native int or pointer, but got O
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0121: Expected O, but got I
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_025c: Expected O, but got I
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Expected O, but got Unknown
		if (dimensions == Dimensions.Dim2D)
		{
			((Vector3*)(nint)posOS)->z = posOS.x;
			((Vector3*)(nint)posOS)->x = posOS.z;
		}
		float num19;
		if (!(0f > posOS.z))
		{
			bool flag = hasMeshSkewing;
			bool flag2 = !flag;
			float num2 = default(float);
			float num = num2;
			float num3 = num2;
			if (!flag2)
			{
				Vector3 vector = skewingLocalForwardDirectionNormalized;
				float num4 = posOS.z / vector.z;
				float num5 = num4 * vector.x;
				float num6 = num4 * num2;
				float num7 = posOS.x - num5;
				num = num7;
				num3 = num2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			float num8 = coneApexOffsetZ;
			Vector2 value = default(Vector2);
			Vector2 vector2 = Vector2.Normalize(ref value);
			Vector2 vector3 = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = vector3 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num9 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				obj = obj2;
			}
			float num10 = spotAngle * ((float)Math.PI / 180f);
			float num11 = num10 * 0.5f;
			object obj3 = obj + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
			Vector2 vector4 = tiltFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj4 = vector4 & 0;
			float num12 = (float)obj3 * num11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamSD)+100]");
			nint num13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num13 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
			{
				obj4 = obj5;
			}
			float num14 = num12 - coneRadiusStart;
			object obj6 = obj4 + fallOffEnd;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num15 = num14 * 57.29578f;
			float num16 = num15 + num15;
			float num17 = num16 * ((float)Math.PI / 180f);
			float num18 = num17 * 0.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj7 = num18 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj8 = vector2 & 0;
			object obj9 = obj7 - obj8;
			num19 = (float)obj9 / 0.1f;
			bool flag3 = -1f > num19;
			float result = -1f;
			if (!flag3)
			{
				bool flag4 = !(num19 > 1f);
				result = 1f;
				if (flag4)
				{
					goto IL_035b;
				}
			}
			return result;
		}
		num19 = -1f;
		goto IL_035b;
		IL_035b:
		return num19;
	}

	public void Generate()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<VLB.VolumetricLightBeamSD>)+198]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<VLB.VolumetricLightBeamSD>)+1A0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void GenerateGeometry()
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected I4, but got Unknown
		float num;
		float num3;
		float num4 = default(float);
		float num2 = default(float);
		if (pluginVersion != -1 && pluginVersion != 20205)
		{
			if (pluginVersion < 1301)
			{
				attenuationEquation = AttenuationEquation.Linear;
			}
			else if (pluginVersion >= 1501)
			{
				if (pluginVersion < 1610)
				{
					goto IL_00b5;
				}
				bool flag = pluginVersion >= 1910;
				num = num2;
				num3 = num4;
				if (!flag)
				{
					goto IL_012d;
				}
				goto IL_02bc;
			}
			geomMeshType = MeshType.Custom;
			geomCustomSegments = 5;
			goto IL_00b5;
		}
		goto IL_02bc;
		IL_012d:
		bool flag2 = intensityModeAdvanced;
		num2 = num;
		num4 = num3;
		if (!flag2)
		{
			num4 = intensityOutside;
			num2 = intensityInside;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj == null)
			{
				intensityInside = intensityOutside;
			}
		}
		goto IL_02bc;
		IL_02bc:
		pluginVersion = 20205;
		AssignPropertiesFromAttachedSpotLight();
		ClampProperties();
		if (m_BeamGeom == null)
		{
			BeamGeometrySD beamGeom = Utils.NewWithComponent<BeamGeometrySD>("Beam Geometry");
			m_BeamGeom = beamGeom;
			m_BeamGeom.Initialize(this);
			if (m_OnBeamGeometryInitialized != null)
			{
				OnBeamGeometryInitialized onBeamGeometryInitialized = m_OnBeamGeometryInitialized;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v338.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				m_OnBeamGeometryInitialized = null;
			}
		}
		bool masterEnabled = base.enabled;
		m_BeamGeom.RegenerateMesh(masterEnabled);
		if (base.BeamGeometryGeneratedEvent != null)
		{
			BeamGeometryGeneratedHandler beamGeometryGeneratedEvent = base.BeamGeometryGeneratedEvent;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v306.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			base.BeamGeometryGeneratedEvent = null;
		}
		return;
		IL_00b5:
		num3 = intensityOutside;
		num = intensityInside;
		intensityFromLight = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj2 = default(object);
		bool flag3 = (byte)(obj2 ^ 1) != 0;
		intensityModeAdvanced = flag3;
		goto IL_012d;
	}

	public virtual void UpdateAfterManualPropertyChange()
	{
		AssignPropertiesFromAttachedSpotLight();
		ClampProperties();
		if ((bool)m_BeamGeom)
		{
			m_BeamGeom.UpdateMaterialAndBounds();
		}
	}

	private void Start()
	{
		//IL_0084: Expected I, but got O
		//IL_0094: Expected O, but got I
		//IL_00a4: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		UnityEngine.Object cachedLightSpot;
		if (!obj)
		{
			cachedLightSpot = null;
			goto IL_0075;
		}
		LightType type = ((Light)obj).type;
		bool flag = type == LightType.Spot;
		UnityEngine.Object obj2 = obj;
		if (!flag)
		{
			obj2 = null;
		}
		goto IL_00ae;
		IL_0075:
		m_CachedLightSpot = (Light)cachedLightSpot;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rdx_v4 (Il2CppClass<VLB.VolumetricLightBeamSD>)+198]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rdx_v4 (Il2CppClass<VLB.VolumetricLightBeamSD>)+1A0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v109 @ rax_v7 (should have been resolved before IL gen)");
		goto IL_00ae;
		IL_00ae:
		cachedLightSpot = obj2;
		goto IL_0075;
	}

	private void OnEnable()
	{
		if ((bool)m_BeamGeom)
		{
			m_BeamGeom.OnMasterEnable();
		}
		StartPlaytimeUpdateIfNeeded();
	}

	private void OnDisable()
	{
		if ((bool)m_BeamGeom)
		{
			m_BeamGeom.OnMasterDisable();
		}
		m_CoPlaytimeUpdate = null;
	}

	private void StartPlaytimeUpdateIfNeeded()
	{
		if (Application.isPlaying && _TrackChangesDuringPlaytime && m_CoPlaytimeUpdate == null)
		{
			_003CCoPlaytimeUpdate_003Ed__199 obj = new _003CCoPlaytimeUpdate_003Ed__199(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coPlaytimeUpdate = StartCoroutine(obj);
			m_CoPlaytimeUpdate = coPlaytimeUpdate;
		}
	}

	private IEnumerator CoPlaytimeUpdate()
	{
		_003CCoPlaytimeUpdate_003Ed__199 obj = new _003CCoPlaytimeUpdate_003Ed__199(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void AssignPropertiesFromAttachedSpotLight()
	{
		//IL_00a2: Expected F4, but got I4
		//IL_0120: Expected F4, but got I4
		//IL_00f2: Expected F4, but got I4
		//IL_0307: Expected O, but got F4
		//IL_02e4: Expected O, but got F4
		if (!m_CachedLightSpot)
		{
			return;
		}
		if (intensityFromLight)
		{
			intensityModeAdvanced = false;
			float num = ((!(m_CachedLightSpot != null)) ? 0f : m_CachedLightSpot.intensity);
			intensityOutside = (intensityInside = num * intensityMultiplier);
		}
		if (fallOffEndFromLight)
		{
			float num2 = ((!(m_CachedLightSpot != null)) ? 0f : m_CachedLightSpot.range);
			float num3 = num2 * fallOffEndMultiplier;
			fallOffEnd = num3;
		}
		float num6;
		if (spotAngleFromLight)
		{
			bool flag = m_CachedLightSpot != null;
			bool flag2 = !flag;
			float num4 = 0f;
			if (!flag2)
			{
				float num5 = m_CachedLightSpot.spotAngle;
				num4 = num5;
			}
			num6 = num4 * spotAngleMultiplier;
			bool flag3 = 0.1f > num6;
			float num7 = 0.1f;
			if (!flag3)
			{
				bool flag4 = !(num6 > 179.9f);
				num7 = 179.9f;
				if (flag4)
				{
					goto IL_03c1;
				}
			}
			num6 = num7;
			goto IL_03c1;
		}
		goto IL_040e;
		IL_03c1:
		spotAngle = num6;
		goto IL_040e;
		IL_040e:
		if (!colorFromLight)
		{
			return;
		}
		colorMode = ColorMode.Flat;
		if (colorFromLight && m_CachedLightSpot != null && m_CachedLightSpot.useColorTemperature)
		{
			Config instance = Config.Instance;
			if (instance.useLightColorTemperature)
			{
				float colorTemperature = m_CachedLightSpot.colorTemperature;
				Color color = Mathf.CorrelatedColorTemperatureToRGB(colorTemperature);
				float num8 = Mathf.GammaToLinearSpace(m_CachedLightSpot.color.r);
				float num10 = default(float);
				float num9 = Mathf.GammaToLinearSpace(num10);
				float num11 = Mathf.GammaToLinearSpace(num10);
				float value = num8 * color.r;
				float num12 = Mathf.LinearToGammaSpace(value);
				float value2 = num9 * num10;
				float num13 = Mathf.LinearToGammaSpace(value2);
				float value3 = num11 * num10;
				float num14 = Mathf.LinearToGammaSpace(value3);
				this.color = (Color)num10;
				return;
			}
		}
		this.color = (Color)m_CachedLightSpot.color.r;
	}

	private void ClampProperties()
	{
		//IL_0015: Invalid comparison between F4 and I4
		//IL_02bc: Invalid comparison between F4 and I4
		//IL_04f9: Invalid comparison between F4 and I4
		//IL_0035: Expected F4, but got I4
		//IL_02e6: Invalid comparison between I4 and F4
		//IL_0043: Expected F4, but got I4
		//IL_008d: Expected F4, but got I4
		//IL_0051: Expected F4, but got I4
		//IL_053f: Invalid comparison between I4 and F4
		//IL_00d6: Expected F4, but got I4
		//IL_0358: Invalid comparison between F4 and I4
		//IL_00e4: Expected F4, but got I4
		//IL_038e: Invalid comparison between F4 and I4
		//IL_05a2: Invalid comparison between F4 and I4
		//IL_03c4: Invalid comparison between F4 and I4
		//IL_0128: Expected F4, but got I4
		//IL_05e2: Invalid comparison between F4 and I4
		//IL_0136: Expected F4, but got I4
		//IL_0144: Expected F4, but got I4
		//IL_0152: Expected F4, but got I4
		//IL_0454: Invalid comparison between I4 and F4
		//IL_0466: Expected F4, but got I4
		//IL_060e: Invalid comparison between I4 and F4
		//IL_0215: Expected F4, but got I4
		//IL_0493: Invalid comparison between I4 and F4
		//IL_0251: Expected F4, but got I4
		//IL_04bc: Invalid comparison between I4 and F4
		float num = intensityInside;
		if (intensityInside < 0f)
		{
			num = 0f;
		}
		intensityInside = num;
		float num2 = intensityOutside;
		if (intensityOutside < 0f)
		{
			num2 = 0f;
		}
		intensityOutside = num2;
		float num3 = intensityMultiplier;
		float num4 = attenuationCustomBlending;
		if (intensityMultiplier < 0f)
		{
			num3 = 0f;
		}
		intensityMultiplier = num3;
		if (!(0f > num4))
		{
			if (num4 > 1f)
			{
				num4 = 1f;
			}
		}
		else
		{
			num4 = 0f;
		}
		attenuationCustomBlending = num4;
		bool flag = !(0.01f < fallOffEnd);
		float num5 = 0.01f;
		if (!flag)
		{
			num5 = fallOffEnd;
		}
		float num6 = fallOffStart;
		fallOffEnd = num5;
		float num7 = num5 - 0.01f;
		if (!(0f > fallOffStart))
		{
			if (num6 > num7)
			{
				num6 = num7;
			}
		}
		else
		{
			num6 = 0f;
		}
		fallOffStart = num6;
		float num8 = fallOffEndMultiplier;
		float num9 = spotAngle;
		if (fallOffEndMultiplier < 0f)
		{
			num8 = 0f;
		}
		fallOffEndMultiplier = num8;
		bool flag2 = 0.1f > num9;
		float num10 = 0.1f;
		if (!flag2)
		{
			bool flag3 = !(num9 > 179.9f);
			num10 = 179.9f;
			if (flag3)
			{
				goto IL_036f;
			}
		}
		num9 = num10;
		goto IL_036f;
		IL_036f:
		spotAngle = num9;
		float num11 = spotAngleMultiplier;
		if (spotAngleMultiplier < 0f)
		{
			num11 = 0f;
		}
		spotAngleMultiplier = num11;
		float num12 = coneRadiusStart;
		if (coneRadiusStart < 0f)
		{
			num12 = 0f;
		}
		coneRadiusStart = num12;
		float num13 = depthBlendDistance;
		if (depthBlendDistance < 0f)
		{
			num13 = 0f;
		}
		depthBlendDistance = num13;
		float num14 = cameraClippingDistance;
		int num15 = geomCustomSides;
		if (cameraClippingDistance < 0f)
		{
			num14 = 0f;
		}
		cameraClippingDistance = num14;
		if (num15 >= 3)
		{
			if (num15 > 256)
			{
				num15 = 256;
			}
		}
		else
		{
			num15 = 3;
		}
		geomCustomSides = num15;
		int num16 = geomCustomSegments;
		if (geomCustomSegments >= 0)
		{
			if (num16 > 64)
			{
				num16 = 64;
			}
		}
		else
		{
			num16 = 0;
		}
		geomCustomSegments = num16;
		float num17 = glareBehind;
		bool flag4 = !(0f < fresnelPow);
		float num18 = 0f;
		if (!flag4)
		{
			num18 = fresnelPow;
		}
		fresnelPow = num18;
		if (!(0f > fresnelPow))
		{
			if (num17 > 1f)
			{
				num17 = 1f;
			}
		}
		else
		{
			num17 = 0f;
		}
		float num19 = glareFrontal;
		glareBehind = num17;
		if (!(0f > glareFrontal))
		{
			if (num19 > 1f)
			{
				num19 = 1f;
			}
		}
		else
		{
			num19 = 0f;
		}
		glareFrontal = num19;
		if (!(0f > noiseIntensity))
		{
			if (noiseIntensity > 1f)
			{
				noiseIntensity = 1f;
			}
			else
			{
				noiseIntensity = noiseIntensity;
			}
		}
		else
		{
			noiseIntensity = 0f;
		}
	}

	private void ValidateProperties()
	{
		AssignPropertiesFromAttachedSpotLight();
		ClampProperties();
	}

	private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected I4, but got Unknown
		if (serializedVersion == -1 || serializedVersion == newVersion)
		{
			return;
		}
		if (serializedVersion < 1301)
		{
			attenuationEquation = AttenuationEquation.Linear;
		}
		else if (serializedVersion >= 1501)
		{
			if (serializedVersion < 1610)
			{
				goto IL_00c4;
			}
			if (serializedVersion < 1910)
			{
				goto IL_0116;
			}
			return;
		}
		geomMeshType = MeshType.Custom;
		geomCustomSegments = 5;
		goto IL_00c4;
		IL_0116:
		if (!intensityModeAdvanced)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj == null)
			{
				intensityInside = intensityOutside;
			}
		}
		return;
		IL_00c4:
		intensityFromLight = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj2 = default(object);
		bool flag = (byte)(obj2 ^ 1) != 0;
		intensityModeAdvanced = flag;
		goto IL_0116;
	}

	public VolumetricLightBeamSD()
	{
		//IL_01f6: Expected I, but got O
		//IL_00e5: Expected I, but got O
		//IL_011b: Expected I, but got O
		//IL_0168: Expected I4, but got I8
		colorFromLight = true;
		intensityFromLight = true;
		intensityInside = 1f;
		color = Consts.Beam.FlatColor;
		intensityOutside = 1f;
		intensityMultiplier = 1f;
		spotAngleFromLight = true;
		spotAngle = 35f;
		spotAngleMultiplier = 1f;
		coneRadiusStart = 0.1f;
		geomCustomSides = 18;
		geomCustomSegments = 5;
		nint num = (nint)typeof(Consts.Beam.SD);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v6 (Il2CppClass<VLB.Consts+Beam+SD>)+B8]");
		nint num2 = 0;
		skewingLocalForwardDirection = Consts.Beam.SD.SkewingLocalForwardDirectionDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v7 (Il2CppStaticFields<VLB.Consts+Beam+SD>)+10]");
		_ = 0;
		attenuationEquation = AttenuationEquation.Quadratic;
		attenuationCustomBlending = 0.5f;
		fallOffEnd = 3f;
		fallOffEndFromLight = true;
		fallOffEndMultiplier = 1f;
		depthBlendDistance = 2f;
		cameraClippingDistance = 0.5f;
		glareFrontal = 0.5f;
		glareBehind = 0.5f;
		fresnelPow = 8f;
		noiseIntensity = 0.5f;
		noiseScaleUseGlobal = true;
		noiseScaleLocal = 0.5f;
		noiseVelocityUseGlobal = true;
		nint num3 = (nint)typeof(Consts.Beam);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v8 (Il2CppClass<VLB.Consts+Beam>)+B8]");
		nint num4 = 0;
		noiseVelocityLocal = Consts.Beam.NoiseVelocityDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v5 (Il2CppStaticFields<VLB.Consts+Beam>)+18]");
		_ = 0;
		nint num5 = (nint)typeof(Consts.Beam.SD);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rax_v10 (Il2CppClass<VLB.Consts+Beam+SD>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v11 (Il2CppStaticFields<VLB.Consts+Beam+SD>)+4]");
		_ = 0;
		tiltFactor = Consts.Beam.SD.TiltDefault;
		_FadeOutBegin = -150f;
		_FadeOutEnd = -200f;
		pluginVersion = -1;
		((MonoBehaviour)this)._002Ector();
	}
}
