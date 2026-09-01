using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class VolumetricLightBeamHD : VolumetricLightBeamAbstractBase
{
	public new const string ClassName = "VolumetricLightBeamHD";

	private bool m_ColorFromLight;

	private ColorMode m_ColorMode;

	private Color m_ColorFlat;

	private Gradient m_ColorGradient;

	private BlendingMode m_BlendingMode;

	private float m_Intensity;

	private float m_IntensityMultiplier;

	private float m_HDRPExposureWeight;

	private float m_SpotAngle;

	private float m_SpotAngleMultiplier;

	private float m_ConeRadiusStart;

	private bool m_Scalable;

	private float m_FallOffStart;

	private float m_FallOffEnd;

	private float m_FallOffEndMultiplier;

	private AttenuationEquationHD m_AttenuationEquation;

	private float m_SideSoftness;

	private int m_RaymarchingQualityID;

	private float m_JitteringFactor;

	private int m_JitteringFrameRate;

	private MinMaxRangeFloat m_JitteringLerpRange;

	private NoiseMode m_NoiseMode;

	private float m_NoiseIntensity;

	private bool m_NoiseScaleUseGlobal;

	private float m_NoiseScaleLocal;

	private bool m_NoiseVelocityUseGlobal;

	private Vector3 m_NoiseVelocityLocal;

	private uint _003C_INTERNAL_InstancedMaterialGroupID_003Ek__BackingField;

	protected BeamGeometryHD m_BeamGeom;

	public bool colorFromLight
	{
		get
		{
			return m_ColorFromLight;
		}
		set
		{
			if (m_ColorFromLight != value)
			{
				m_ColorFromLight = value;
				ValidateProperties();
			}
		}
	}

	public ColorMode colorMode
	{
		get
		{
			//IL_0068: Expected I4, but got O
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
				{
					return m_ColorMode;
				}
				return ColorMode.Flat;
			}
			NullReferenceException ex = new NullReferenceException();
			return (ColorMode)ex;
		}
		set
		{
			if (m_ColorMode != value)
			{
				m_ColorMode = value;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.ColorMode);
			}
		}
	}

	public unsafe Color colorFlat
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)m_ColorFlat;
			return color;
		}
		set
		{
			//IL_00c8: Invalid comparison between F4 and I4
			//IL_00f1: Expected O, but got I4
			//IL_0120: Expected O, but got F4
			object obj2 = default(object);
			object obj = obj2 - obj2;
			float num = (float)m_ColorFlat - value.r;
			object obj3 = obj2 - obj2;
			object obj4 = obj2 - obj2;
			object obj5 = obj * obj;
			float num2 = num * num;
			object obj6 = obj3 * obj3;
			float num3 = (float)obj5 + num2;
			object obj7 = obj4 * obj4;
			float num4 = num3 + (float)obj6;
			float num5 = num4 + (float)obj7;
			bool flag = 9.9999994E-11f < num5;
			float num6 = 9.9999994E-11f - num5;
			bool flag2 = num6 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			object obj8 = flag4 & flag3;
			if (obj8 == null)
			{
				m_ColorFlat = (Color)value.r;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Color);
			}
		}
	}

	public Gradient colorGradient
	{
		get
		{
			return m_ColorGradient;
		}
		set
		{
			if (m_ColorGradient != value)
			{
				m_ColorGradient = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.Color);
			}
		}
	}

	private bool useColorFromAttachedLightSpot
	{
		get
		{
			if (!m_ColorFromLight)
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
			//IL_00c3: Expected I4, but got O
			if (m_ColorFromLight && m_CachedLightSpot != null)
			{
				if ((object)m_CachedLightSpot != null)
				{
					if (!m_CachedLightSpot.useColorTemperature)
					{
						goto IL_00af;
					}
					Config instance = Config.GetInstance(true);
					if ((object)instance != null)
					{
						return instance.useLightColorTemperature;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00af;
			IL_00af:
			return false;
		}
	}

	public float intensity
	{
		get
		{
			return m_Intensity;
		}
		set
		{
			bool flag = m_Intensity == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388BB5h\"");
			if (!flag)
			{
				m_Intensity = value;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Intensity);
			}
		}
	}

	public float intensityMultiplier
	{
		get
		{
			return m_IntensityMultiplier;
		}
		set
		{
			bool flag = m_IntensityMultiplier == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388B8Ch\"");
			if (!flag)
			{
				m_IntensityMultiplier = value;
				ValidateProperties();
			}
		}
	}

	public bool useIntensityFromAttachedLightSpot
	{
		get
		{
			//IL_0030: Invalid comparison between F4 and I4
			if (m_IntensityMultiplier < 0f)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
		set
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			float num = ((!value) ? (-1f) : 1f);
			float num2 = m_IntensityMultiplier;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num2 & 0;
			float num3 = (float)obj * num;
			bool flag = m_IntensityMultiplier == num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018038913Bh\"");
			if (!flag)
			{
				m_IntensityMultiplier = num3;
				ValidateProperties();
			}
		}
	}

	public float hdrpExposureWeight
	{
		get
		{
			return m_HDRPExposureWeight;
		}
		set
		{
			bool flag = m_HDRPExposureWeight == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388B55h\"");
			if (!flag)
			{
				m_HDRPExposureWeight = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.HDRPExposureWeight);
			}
		}
	}

	public BlendingMode blendingMode
	{
		get
		{
			return m_BlendingMode;
		}
		set
		{
			if (m_BlendingMode != value)
			{
				m_BlendingMode = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.BlendingMode);
			}
		}
	}

	public float spotAngle
	{
		get
		{
			return m_SpotAngle;
		}
		set
		{
			bool flag = m_SpotAngle == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180389065h\"");
			if (!flag)
			{
				m_SpotAngle = value;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Cone);
			}
		}
	}

	public float spotAngleMultiplier
	{
		get
		{
			return m_SpotAngleMultiplier;
		}
		set
		{
			bool flag = m_SpotAngleMultiplier == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018038903Ch\"");
			if (!flag)
			{
				m_SpotAngleMultiplier = value;
				ValidateProperties();
			}
		}
	}

	public bool useSpotAngleFromAttachedLightSpot
	{
		get
		{
			//IL_0030: Invalid comparison between F4 and I4
			if (m_SpotAngleMultiplier < 0f)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
		set
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			float num = ((!value) ? (-1f) : 1f);
			float num2 = m_SpotAngleMultiplier;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num2 & 0;
			float num3 = (float)obj * num;
			bool flag = m_SpotAngleMultiplier == num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018038919Bh\"");
			if (!flag)
			{
				m_SpotAngleMultiplier = num3;
				ValidateProperties();
			}
		}
	}

	public float coneAngle
	{
		get
		{
			float num = Utils.ComputeConeRadiusEnd(m_FallOffEnd, m_SpotAngle);
			float num2 = num - m_ConeRadiusStart;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num3 = num2 * 57.29578f;
			return num3 + num3;
		}
	}

	public float coneRadiusStart
	{
		get
		{
			return m_ConeRadiusStart;
		}
		set
		{
			bool flag = m_ConeRadiusStart == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388A65h\"");
			if (!flag)
			{
				m_ConeRadiusStart = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.Cone);
			}
		}
	}

	public float coneRadiusEnd
	{
		get
		{
			return Utils.ComputeConeRadiusEnd(m_FallOffEnd, m_SpotAngle);
		}
		set
		{
			float num = Utils.ComputeSpotAngle(m_FallOffEnd, value);
			bool flag = m_SpotAngle == num;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388A12h\"");
			if (!flag)
			{
				m_SpotAngle = num;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Cone);
			}
		}
	}

	public float coneVolume
	{
		get
		{
			float num = Utils.ComputeConeRadiusEnd(m_FallOffEnd, m_SpotAngle);
			float num2 = m_ConeRadiusStart * m_ConeRadiusStart;
			float num3 = m_ConeRadiusStart * num;
			float num4 = num * num;
			float num5 = num2 + num3;
			float num6 = num5 + num4;
			float num7 = num6 * ((float)Math.PI / 3f);
			return num7 * m_FallOffEnd;
		}
	}

	public bool scalable
	{
		get
		{
			return m_Scalable;
		}
		set
		{
			if (m_Scalable != value)
			{
				m_Scalable = value;
				SetPropertyDirty(DirtyProps.Attenuation);
			}
		}
	}

	public AttenuationEquationHD attenuationEquation
	{
		get
		{
			return m_AttenuationEquation;
		}
		set
		{
			if (m_AttenuationEquation != value)
			{
				m_AttenuationEquation = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.Attenuation);
			}
		}
	}

	public float fallOffStart
	{
		get
		{
			return m_FallOffStart;
		}
		set
		{
			bool flag = m_FallOffStart == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388B15h\"");
			if (!flag)
			{
				m_FallOffStart = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.Cone);
			}
		}
	}

	public float fallOffEnd
	{
		get
		{
			return m_FallOffEnd;
		}
		set
		{
			bool flag = m_FallOffEnd == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388AC5h\"");
			if (!flag)
			{
				m_FallOffEnd = value;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Cone);
			}
		}
	}

	public float maxGeometryDistance => m_FallOffEnd;

	public float fallOffEndMultiplier
	{
		get
		{
			return m_FallOffEndMultiplier;
		}
		set
		{
			bool flag = m_FallOffEndMultiplier == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388A9Fh\"");
			if (!flag)
			{
				m_FallOffEndMultiplier = value;
				ValidateProperties();
			}
		}
	}

	public bool useFallOffEndFromAttachedLightSpot
	{
		get
		{
			//IL_0030: Invalid comparison between F4 and I4
			if (m_FallOffEndMultiplier < 0f)
			{
				return false;
			}
			return m_CachedLightSpot != null;
		}
		set
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			float num = ((!value) ? (-1f) : 1f);
			float num2 = m_FallOffEndMultiplier;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num2 & 0;
			float num3 = (float)obj * num;
			bool flag = m_FallOffEndMultiplier == num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803890E1h\"");
			if (!flag)
			{
				m_FallOffEndMultiplier = num3;
				ValidateProperties();
			}
		}
	}

	public float sideSoftness
	{
		get
		{
			return m_SideSoftness;
		}
		set
		{
			bool flag = m_SideSoftness == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388FF8h\"");
			if (!flag)
			{
				m_SideSoftness = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.SideSoftness);
			}
		}
	}

	public float jitteringFactor
	{
		get
		{
			return m_JitteringFactor;
		}
		set
		{
			bool flag = m_JitteringFactor == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388C08h\"");
			if (!flag)
			{
				m_JitteringFactor = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.Jittering);
			}
		}
	}

	public int jitteringFrameRate
	{
		get
		{
			return m_JitteringFrameRate;
		}
		set
		{
			if (m_JitteringFrameRate != value)
			{
				m_JitteringFrameRate = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.Jittering);
			}
		}
	}

	public MinMaxRangeFloat jitteringLerpRange
	{
		get
		{
			MinMaxRangeFloat result = default(MinMaxRangeFloat);
			return result;
		}
		set
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388CB4h\"");
			if ((object)m_JitteringLerpRange == (object)value)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamHD)+9C]");
				object obj = default(object);
				bool flag = 0 == (nint)obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388CB4h\"");
				if (flag)
				{
					return;
				}
			}
			m_JitteringLerpRange = value;
			ValidateProperties();
			SetPropertyDirty(DirtyProps.Jittering);
		}
	}

	public NoiseMode noiseMode
	{
		get
		{
			return m_NoiseMode;
		}
		set
		{
			if (m_NoiseMode != value)
			{
				m_NoiseMode = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.NoiseMode);
			}
		}
	}

	public bool isNoiseEnabled
	{
		get
		{
			bool flag = m_NoiseMode < NoiseMode.Disabled;
			bool flag2 = m_NoiseMode == NoiseMode.Disabled;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public float noiseIntensity
	{
		get
		{
			return m_NoiseIntensity;
		}
		set
		{
			bool flag = m_NoiseIntensity == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388CF8h\"");
			if (!flag)
			{
				m_NoiseIntensity = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.NoiseIntensity);
			}
		}
	}

	public bool noiseScaleUseGlobal
	{
		get
		{
			return m_NoiseScaleUseGlobal;
		}
		set
		{
			if (m_NoiseScaleUseGlobal != value)
			{
				m_NoiseScaleUseGlobal = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.NoiseVelocityAndScale);
			}
		}
	}

	public float noiseScaleLocal
	{
		get
		{
			return m_NoiseScaleLocal;
		}
		set
		{
			bool flag = m_NoiseScaleLocal == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180388D88h\"");
			if (!flag)
			{
				m_NoiseScaleLocal = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.NoiseVelocityAndScale);
			}
		}
	}

	public bool noiseVelocityUseGlobal
	{
		get
		{
			return m_NoiseVelocityUseGlobal;
		}
		set
		{
			if (m_NoiseVelocityUseGlobal != value)
			{
				m_NoiseVelocityUseGlobal = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.NoiseVelocityAndScale);
			}
		}
	}

	public unsafe Vector3 noiseVelocityLocal
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)m_NoiseVelocityLocal;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (VLB.VolumetricLightBeamHD)+BC]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			//IL_00ac: Invalid comparison between F4 and I4
			//IL_00d5: Expected O, but got I4
			//IL_0104: Expected O, but got F4
			float num = (float)m_NoiseVelocityLocal - value.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricLightBeamHD)+BC]");
			object obj = 0 - value.z;
			object obj3 = default(object);
			object obj2 = obj3 - obj3;
			object obj4 = obj2 * obj2;
			float num2 = num * num;
			object obj5 = obj * obj;
			float num3 = (float)obj4 + num2;
			float num4 = num3 + (float)obj5;
			bool flag = 9.9999994E-11f < num4;
			float num5 = 9.9999994E-11f - num4;
			bool flag2 = num5 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			object obj6 = flag4 & flag3;
			if (obj6 == null)
			{
				m_NoiseVelocityLocal = (Vector3)value.x;
				_ = value.z;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.NoiseVelocityAndScale);
			}
		}
	}

	public int raymarchingQualityID
	{
		get
		{
			return m_RaymarchingQualityID;
		}
		set
		{
			if (m_RaymarchingQualityID != value)
			{
				m_RaymarchingQualityID = value;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.RaymarchingQuality);
			}
		}
	}

	public int raymarchingQualityIndex
	{
		get
		{
			//IL_0051: Expected I4, but got O
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				return instance.GetRaymarchingQualityIndexForUniqueID(m_RaymarchingQualityID);
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			Config instance = Config.GetInstance(true);
			Config instance2 = Config.GetInstance(true);
			int raymarchingQualityIndexForUniqueID = instance2.GetRaymarchingQualityIndexForUniqueID(m_RaymarchingQualityID);
			RaymarchingQuality[] raymarchingQualities = instance.m_RaymarchingQualities;
			RaymarchingQuality raymarchingQuality = raymarchingQualities[raymarchingQualityIndexForUniqueID];
			if (m_RaymarchingQualityID != raymarchingQuality._UniqueID)
			{
				m_RaymarchingQualityID = raymarchingQuality._UniqueID;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.RaymarchingQuality);
			}
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
				if (m_BlendingMode >= BlendingMode.Additive)
				{
					bool flag = (int)m_BlendingMode <= num;
					result = (int)m_BlendingMode;
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
			//IL_006a: Expected native int or pointer, but got O
			//IL_0042: Expected F4, but got O
			//IL_003d: Expected native int or pointer, but got O
			Quaternion quaternion = default(Quaternion);
			if (GetDimensions() == Dimensions.Dim3D)
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
			//IL_0088: Expected I, but got O
			//IL_00a6: Expected F4, but got O
			//IL_00a1: Expected native int or pointer, but got O
			//IL_00bb: Expected F4, but got I
			//IL_00b6: Expected native int or pointer, but got O
			//IL_0042: Expected I, but got O
			//IL_0060: Expected F4, but got O
			//IL_005b: Expected native int or pointer, but got O
			//IL_0075: Expected F4, but got I
			//IL_0070: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			if (GetDimensions() == Dimensions.Dim3D)
			{
				nint num = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rax_v10 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num2 = 0;
				((Vector3*)(nint)vector)->x = (float)Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v11 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			((Vector3*)(nint)vector)->x = (float)Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v5 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
	}

	public unsafe Vector3 beamGlobalForward
	{
		get
		{
			//IL_004b: Expected O, but got Ref
			//IL_005c: Expected native int or pointer, but got O
			//IL_006e: Expected native int or pointer, but got O
			Transform transform = base.transform;
			if (GetDimensions() == Dimensions.Dim3D)
			{
			}
			if ((object)transform != null)
			{
				Vector3 vector2 = default(Vector3);
				Vector3 vector = transform.TransformDirection((Vector3)(&vector2));
				Vector3 vector3 = default(Vector3);
				((Vector3*)(nint)vector3)->x = vector.x;
				((Vector3*)(nint)vector3)->z = vector.z;
				return vector3;
			}
			return (Vector3)new NullReferenceException();
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

	public float GetConeApexOffsetZ(bool counterApplyScaleForUnscalableBeam)
	{
		float num = Utils.ComputeConeRadiusEnd(m_FallOffEnd, m_SpotAngle);
		float num2 = m_ConeRadiusStart / num;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803877D7h\"");
		if (num2 == 1f)
		{
			return 3.4028235E+38f;
		}
		float num3 = 1f - num2;
		float num4 = num2 * m_FallOffEnd;
		float num5 = num4 / num3;
		if (counterApplyScaleForUnscalableBeam && !m_Scalable)
		{
			num5 /= GetLossyScale().z;
		}
		return num5;
	}

	public override bool IsScalable()
	{
		return m_Scalable;
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
		//IL_0078: Expected native int or pointer, but got O
		//IL_0174: Expected native int or pointer, but got O
		//IL_0135: Expected native int or pointer, but got O
		//IL_0147: Expected native int or pointer, but got O
		float z;
		Vector3 vector = default(Vector3);
		if (GetDimensions() == Dimensions.Dim3D)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 lossyScale = transform.lossyScale;
				z = lossyScale.z;
				((Vector3*)(nint)vector)->x = lossyScale.x;
				goto IL_016c;
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
						goto IL_016c;
					}
				}
			}
		}
		return (Vector3)new NullReferenceException();
		IL_016c:
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	public VolumetricCookieHD GetAdditionalComponentCookie()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricCookieHD result = default(VolumetricCookieHD);
		return result;
	}

	public VolumetricShadowHD GetAdditionalComponentShadow()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricShadowHD result = default(VolumetricShadowHD);
		return result;
	}

	public void SetPropertyDirty(DirtyProps flags)
	{
		//IL_0096: Expected I4, but got O
		//IL_00a3: Expected I4, but got O
		if ((bool)m_BeamGeom)
		{
			BeamGeometryHD beamGeom = m_BeamGeom;
			DirtyProps dirtyProps = beamGeom.m_DirtyProps | flags;
			beamGeom.m_DirtyProps = dirtyProps;
			object obj = default(object);
			Enum mask = (DirtyProps)obj;
			object obj2 = default(object);
			Enum flags2 = (DirtyProps)obj2;
			if (Utils.HasAtLeastOneFlag(mask, flags2))
			{
				beamGeom.UpdateMaterialAndBounds();
			}
		}
	}

	public virtual Dimensions GetDimensions()
	{
		return Dimensions.Dim3D;
	}

	public virtual bool DoesSupportSorting2D()
	{
		return false;
	}

	public virtual int GetSortingLayerID()
	{
		return 0;
	}

	public virtual int GetSortingOrder()
	{
		return 0;
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
		//IL_023b: Invalid comparison between I4 and F4
		//IL_0037: Expected native int or pointer, but got O
		//IL_0049: Expected native int or pointer, but got O
		//IL_025d: Expected I, but got O
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		Dimensions dimensions = GetDimensions();
		if (dimensions == Dimensions.Dim2D)
		{
			((Vector3*)(nint)posOS)->z = posOS.x;
			((Vector3*)(nint)posOS)->x = posOS.z;
		}
		float num9;
		if (!(0f > posOS.z))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037FE00");
			nint num = (nint)typeof(Math);
			object obj2 = default(object);
			object obj = obj2 * obj2;
			object obj4 = default(object);
			object obj3 = obj4 * obj4;
			double d = (double)obj + (double)obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v138 @ rcx_v4 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
			}
			else
			{
				double num2 = Math.Sqrt(d);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm0\"");
			float coneApexOffsetZ = GetConeApexOffsetZ(counterApplyScaleForUnscalableBeam: true);
			Vector2 value = default(Vector2);
			Vector2 vector = Vector2.Normalize(ref value);
			float num3 = Utils.ComputeConeRadiusEnd(m_FallOffEnd, m_SpotAngle);
			float num4 = num3 - m_ConeRadiusStart;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num5 = num4 * 57.29578f;
			float num6 = num5 + num5;
			float num7 = num6 * ((float)Math.PI / 180f);
			float num8 = num7 * 0.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num8 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj6 = vector & 0;
			object obj7 = obj5 - obj6;
			num9 = (float)obj7 / 0.1f;
			bool flag = -1f > num9;
			float result = -1f;
			if (!flag)
			{
				bool flag2 = !(num9 > 1f);
				result = 1f;
				if (flag2)
				{
					goto IL_0262;
				}
			}
			return result;
		}
		num9 = -1f;
		goto IL_0262;
		IL_0262:
		return num9;
	}

	public override void GenerateGeometry()
	{
		//IL_00f7: Expected I, but got O
		//IL_03b8: Expected O, but got I
		//IL_0463: Expected O, but got I
		//IL_0463: Expected O, but got I
		//IL_01db: Expected I, but got O
		//IL_01eb: Expected O, but got I
		//IL_020d: Expected O, but got I4
		if (pluginVersion == -1)
		{
			Config instance = Config.GetInstance(true);
			if (m_RaymarchingQualityID != instance.m_DefaultRaymarchingQualityUniqueID)
			{
				m_RaymarchingQualityID = instance.m_DefaultRaymarchingQualityUniqueID;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.RaymarchingQuality);
			}
		}
		Config instance2 = Config.GetInstance(true);
		int raymarchingQualityIndexForUniqueID = instance2.GetRaymarchingQualityIndexForUniqueID(m_RaymarchingQualityID);
		if (raymarchingQualityIndexForUniqueID < 0)
		{
			GameObject context = base.gameObject;
			object[] array = new object[2];
			string text = base.name;
			if (text != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text2 = default(string);
					throw text2;
				}
			}
			array[0] = text;
			Config instance3 = Config.GetInstance(true);
			Config instance4 = Config.GetInstance(true);
			int raymarchingQualityIndexForUniqueID2 = instance3.GetRaymarchingQualityIndexForUniqueID(instance4.m_DefaultRaymarchingQualityUniqueID);
			if (raymarchingQualityIndexForUniqueID2 < 0)
			{
				throw new NullReferenceException();
			}
			RaymarchingQuality[] raymarchingQualities = instance3.m_RaymarchingQualities;
			RaymarchingQuality raymarchingQuality = raymarchingQualities[raymarchingQualityIndexForUniqueID2];
			if (raymarchingQuality.name != null)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v694 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag = obj3 == null;
				object obj4 = 0;
				string text3 = raymarchingQuality.name;
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj5 = default(object);
					throw obj5;
				}
			}
			array[1] = raymarchingQuality.name;
			Debug.LogErrorFormat(context, "HD Beam '{0}': fallback to default quality '{1}'", array);
			Config instance5 = Config.GetInstance(true);
			bool flag2 = m_RaymarchingQualityID == instance5.m_DefaultRaymarchingQualityUniqueID;
			object[] array2 = array;
			DirtyProps dirtyProps = DirtyProps.None;
			if (!flag2)
			{
				m_RaymarchingQualityID = instance5.m_DefaultRaymarchingQualityUniqueID;
				ValidateProperties();
				SetPropertyDirty(DirtyProps.RaymarchingQuality);
				array2 = null;
				dirtyProps = DirtyProps.RaymarchingQuality;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
		pluginVersion = 20205;
		ValidateProperties();
		if (m_BeamGeom == null)
		{
			BeamGeometryHD beamGeom = Utils.NewWithComponent<BeamGeometryHD>("Beam Geometry");
			m_BeamGeom = beamGeom;
			m_BeamGeom.Initialize(this);
		}
		Component beamGeom2 = m_BeamGeom;
		Config instance6 = Config.GetInstance(true);
		int layer;
		GameObject gameObject3;
		if (!instance6.geometryOverrideLayer)
		{
			GameObject gameObject = m_BeamGeom.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rdi_v5 (UnityEngine.Component)+80]");
			GameObject gameObject2 = ((Component)0).gameObject;
			layer = gameObject2.layer;
			gameObject3 = gameObject;
		}
		else
		{
			GameObject gameObject4 = m_BeamGeom.gameObject;
			Config instance7 = Config.GetInstance(true);
			layer = instance7.geometryLayerID;
			gameObject3 = gameObject4;
		}
		gameObject3.layer = layer;
		GameObject gameObject5 = m_BeamGeom.gameObject;
		Config instance8 = Config.GetInstance(true);
		gameObject5.tag = instance8.geometryTag;
		Mesh mesh = GlobalMeshHD.Get();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rdi_v5 (UnityEngine.Component)+28]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rdi_v5 (UnityEngine.Component)+30]");
		((MeshFilter)num3).sharedMesh = (Mesh)0;
		m_BeamGeom.UpdateMaterialAndBounds();
		bool visible = base.enabled;
		m_BeamGeom.visible = visible;
		base.GenerateGeometry();
	}

	public virtual void UpdateAfterManualPropertyChange()
	{
		ValidateProperties();
		SetPropertyDirty(DirtyProps.All);
	}

	private void Start()
	{
		//IL_000b: Expected I, but got O
		//IL_001b: Expected O, but got I
		//IL_002b: Expected O, but got I
		InitLightSpotAttachedCached();
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamHD>)+198]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamHD>)+1A0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v7 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	private void OnEnable()
	{
		if ((bool)m_BeamGeom)
		{
			m_BeamGeom.visible = true;
		}
	}

	private void OnDisable()
	{
		if ((bool)m_BeamGeom)
		{
			m_BeamGeom.visible = false;
		}
	}

	private void OnDidApplyAnimationProperties()
	{
		//IL_000b: Expected I, but got O
		//IL_001b: Expected O, but got I
		//IL_002b: Expected O, but got I
		AssignPropertiesFromAttachedSpotLight();
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamHD>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamHD>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v7 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public unsafe void AssignPropertiesFromAttachedSpotLight()
	{
		//IL_0454: Invalid comparison between F4 and I4
		//IL_0473: Invalid comparison between F4 and I4
		//IL_0492: Invalid comparison between F4 and I4
		//IL_0439: Expected O, but got Ref
		//IL_041b: Expected O, but got Ref
		if (!m_CachedLightSpot)
		{
			return;
		}
		if (!(m_IntensityMultiplier < 0f) && m_CachedLightSpot != null)
		{
			float num = SpotLightHelper.GetIntensity(m_CachedLightSpot);
			float num2 = num * m_IntensityMultiplier;
			bool flag = m_Intensity == num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180386D75h\"");
			if (!flag)
			{
				m_Intensity = num2;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Intensity);
			}
		}
		if (!(m_FallOffEndMultiplier < 0f) && m_CachedLightSpot != null)
		{
			float num3 = SpotLightHelper.GetFallOffEnd(m_CachedLightSpot);
			float num4 = num3 * m_FallOffEndMultiplier;
			bool flag2 = m_FallOffEnd == num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180386E0Fh\"");
			if (!flag2)
			{
				m_FallOffEnd = num4;
				AssignPropertiesFromAttachedSpotLight();
				ClampProperties();
				SetPropertyDirty(DirtyProps.Cone);
			}
		}
		if (m_SpotAngleMultiplier < 0f || !(m_CachedLightSpot != null))
		{
			goto IL_0253;
		}
		float num5 = SpotLightHelper.GetSpotAngle(m_CachedLightSpot);
		float num6 = num5 * m_SpotAngleMultiplier;
		bool flag3 = 0.1f > num6;
		float num7 = 0.1f;
		if (!flag3)
		{
			bool flag4 = !(num6 > 179.9f);
			num7 = 179.9f;
			if (flag4)
			{
				goto IL_04a6;
			}
		}
		num6 = num7;
		goto IL_04a6;
		IL_04a6:
		bool flag5 = m_SpotAngle == num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180386EC7h\"");
		if (!flag5)
		{
			m_SpotAngle = num6;
			AssignPropertiesFromAttachedSpotLight();
			ClampProperties();
			SetPropertyDirty(DirtyProps.Cone);
		}
		goto IL_0253;
		IL_0253:
		if (!m_ColorFromLight)
		{
			return;
		}
		if (m_ColorMode != ColorMode.Flat)
		{
			m_ColorMode = ColorMode.Flat;
			AssignPropertiesFromAttachedSpotLight();
			ClampProperties();
			SetPropertyDirty(DirtyProps.ColorMode);
		}
		object obj = default(object);
		if (m_ColorFromLight && m_CachedLightSpot != null && m_CachedLightSpot.useColorTemperature)
		{
			Config instance = Config.GetInstance(true);
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
				colorFlat = (Color)(&obj);
				return;
			}
		}
		Color color2 = m_CachedLightSpot.color;
		colorFlat = (Color)(&obj);
	}

	private void ClampProperties()
	{
		//IL_0015: Invalid comparison between F4 and I4
		//IL_02d9: Invalid comparison between I4 and F4
		//IL_0035: Expected F4, but got I4
		//IL_007e: Expected F4, but got I4
		//IL_0220: Invalid comparison between F4 and I4
		//IL_00c4: Expected F4, but got I4
		//IL_0260: Invalid comparison between F4 and I4
		//IL_0108: Expected F4, but got I4
		//IL_028c: Invalid comparison between I4 and F4
		float num = m_Intensity;
		if (m_Intensity < 0f)
		{
			num = 0f;
		}
		bool flag = !(0.01f < m_FallOffEnd);
		float num2 = 0.01f;
		if (!flag)
		{
			num2 = m_FallOffEnd;
		}
		m_Intensity = num;
		float num3 = m_FallOffStart;
		m_FallOffEnd = num2;
		float num4 = num2 - 0.01f;
		if (!(0f > m_FallOffStart))
		{
			if (num3 > num4)
			{
				num3 = num4;
			}
		}
		else
		{
			num3 = 0f;
		}
		m_FallOffStart = num3;
		float num5 = m_SpotAngle;
		bool flag2 = 0.1f > m_SpotAngle;
		float num6 = 0.1f;
		if (!flag2)
		{
			bool flag3 = !(m_SpotAngle > 179.9f);
			num6 = 179.9f;
			if (flag3)
			{
				goto IL_01f7;
			}
		}
		num5 = num6;
		goto IL_01f7;
		IL_0237:
		float num7;
		m_SideSoftness = num7;
		float num8 = m_JitteringFactor;
		int num9 = m_JitteringFrameRate;
		if (m_JitteringFactor < 0f)
		{
			num8 = 0f;
		}
		m_JitteringFactor = num8;
		if (num9 >= 0)
		{
			if (num9 > 120)
			{
				num9 = 120;
			}
		}
		else
		{
			num9 = 0;
		}
		m_JitteringFrameRate = num9;
		if (!(0f > m_NoiseIntensity))
		{
			if (m_NoiseIntensity > 1f)
			{
				m_NoiseIntensity = 1f;
			}
			else
			{
				m_NoiseIntensity = m_NoiseIntensity;
			}
		}
		else
		{
			m_NoiseIntensity = 0f;
		}
		return;
		IL_01f7:
		m_SpotAngle = num5;
		float num10 = m_ConeRadiusStart;
		num7 = m_SideSoftness;
		if (m_ConeRadiusStart < 0f)
		{
			num10 = 0f;
		}
		m_ConeRadiusStart = num10;
		bool flag4 = 0.0001f > num7;
		float num11 = 0.0001f;
		if (!flag4)
		{
			bool flag5 = !(num7 > 10f);
			num11 = 10f;
			if (flag5)
			{
				goto IL_0237;
			}
		}
		num7 = num11;
		goto IL_0237;
	}

	private void ValidateProperties()
	{
		//IL_001b: Invalid comparison between F4 and I4
		//IL_02df: Invalid comparison between I4 and F4
		//IL_003b: Expected F4, but got I4
		//IL_0084: Expected F4, but got I4
		//IL_0226: Invalid comparison between F4 and I4
		//IL_00ca: Expected F4, but got I4
		//IL_0266: Invalid comparison between F4 and I4
		//IL_010e: Expected F4, but got I4
		//IL_0292: Invalid comparison between I4 and F4
		AssignPropertiesFromAttachedSpotLight();
		float num = m_Intensity;
		if (m_Intensity < 0f)
		{
			num = 0f;
		}
		bool flag = !(0.01f < m_FallOffEnd);
		float num2 = 0.01f;
		if (!flag)
		{
			num2 = m_FallOffEnd;
		}
		m_Intensity = num;
		float num3 = m_FallOffStart;
		m_FallOffEnd = num2;
		float num4 = num2 - 0.01f;
		if (!(0f > m_FallOffStart))
		{
			if (num3 > num4)
			{
				num3 = num4;
			}
		}
		else
		{
			num3 = 0f;
		}
		m_FallOffStart = num3;
		float num5 = m_SpotAngle;
		bool flag2 = 0.1f > m_SpotAngle;
		float num6 = 0.1f;
		if (!flag2)
		{
			bool flag3 = !(m_SpotAngle > 179.9f);
			num6 = 179.9f;
			if (flag3)
			{
				goto IL_01fd;
			}
		}
		num5 = num6;
		goto IL_01fd;
		IL_023d:
		float num7;
		m_SideSoftness = num7;
		float num8 = m_JitteringFactor;
		int num9 = m_JitteringFrameRate;
		if (m_JitteringFactor < 0f)
		{
			num8 = 0f;
		}
		m_JitteringFactor = num8;
		if (num9 >= 0)
		{
			if (num9 > 120)
			{
				num9 = 120;
			}
		}
		else
		{
			num9 = 0;
		}
		m_JitteringFrameRate = num9;
		if (!(0f > m_NoiseIntensity))
		{
			if (m_NoiseIntensity > 1f)
			{
				m_NoiseIntensity = 1f;
			}
			else
			{
				m_NoiseIntensity = m_NoiseIntensity;
			}
		}
		else
		{
			m_NoiseIntensity = 0f;
		}
		return;
		IL_01fd:
		m_SpotAngle = num5;
		float num10 = m_ConeRadiusStart;
		num7 = m_SideSoftness;
		if (m_ConeRadiusStart < 0f)
		{
			num10 = 0f;
		}
		m_ConeRadiusStart = num10;
		bool flag4 = 0.0001f > num7;
		float num11 = 0.0001f;
		if (!flag4)
		{
			bool flag5 = !(num7 > 10f);
			num11 = 10f;
			if (flag5)
			{
				goto IL_023d;
			}
		}
		num7 = num11;
		goto IL_023d;
	}

	private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
	{
	}

	public VolumetricLightBeamHD()
	{
		//IL_012e: Expected I4, but got I8
		//IL_0147: Expected I, but got O
		//IL_0077: Expected I, but got O
		m_ColorFromLight = true;
		m_Intensity = 1f;
		m_IntensityMultiplier = 1f;
		m_ColorFlat = Consts.Beam.FlatColor;
		m_SpotAngle = 35f;
		m_SpotAngleMultiplier = 1f;
		m_ConeRadiusStart = 0.1f;
		m_Scalable = true;
		m_FallOffEnd = 3f;
		m_FallOffEndMultiplier = 1f;
		m_AttenuationEquation = AttenuationEquationHD.Quadratic;
		m_SideSoftness = 1f;
		m_RaymarchingQualityID = -1;
		m_JitteringFrameRate = 60;
		nint num = (nint)typeof(Consts.Beam.HD);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v6 (Il2CppClass<VLB.Consts+Beam+HD>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v7 (Il2CppStaticFields<VLB.Consts+Beam+HD>)+4]");
		_ = 0;
		m_JitteringLerpRange = Consts.Beam.HD.JitteringLerpRange;
		m_NoiseIntensity = 0.5f;
		m_NoiseScaleUseGlobal = true;
		m_NoiseScaleLocal = 0.5f;
		m_NoiseVelocityUseGlobal = true;
		nint num3 = (nint)typeof(Consts.Beam);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v9 (Il2CppClass<VLB.Consts+Beam>)+B8]");
		nint num4 = 0;
		m_NoiseVelocityLocal = Consts.Beam.NoiseVelocityDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v4 (Il2CppStaticFields<VLB.Consts+Beam>)+18]");
		_ = 0;
		base._002Ector();
	}
}
