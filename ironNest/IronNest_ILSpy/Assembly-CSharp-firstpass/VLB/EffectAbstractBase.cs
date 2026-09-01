using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class EffectAbstractBase : MonoBehaviour
{
	public enum ComponentsToChange
	{
		UnityLight = 1,
		VolumetricLightBeam = 2,
		VolumetricDustParticles = 4
	}

	public const string ClassName = "EffectAbstractBase";

	public ComponentsToChange componentsToChange = (ComponentsToChange)2147483647;

	public bool restoreIntensityOnDisable = true;

	protected VolumetricLightBeamAbstractBase m_Beam;

	protected Light m_Light;

	protected VolumetricDustParticles m_Particles;

	protected float m_BaseIntensityBeamInside;

	protected float m_BaseIntensityBeamOutside;

	protected float m_BaseIntensityLight;

	public bool restoreBaseIntensity
	{
		get
		{
			return restoreIntensityOnDisable;
		}
		set
		{
			restoreIntensityOnDisable = value;
		}
	}

	public virtual void InitFrom(EffectAbstractBase Source)
	{
		if ((bool)Source)
		{
			componentsToChange = Source.componentsToChange;
			restoreIntensityOnDisable = Source.restoreIntensityOnDisable;
		}
	}

	private void GetIntensity(VolumetricLightBeamSD beam)
	{
		if ((bool)beam)
		{
			m_BaseIntensityBeamInside = beam.intensityInside;
			m_BaseIntensityBeamOutside = beam.intensityOutside;
		}
	}

	private void GetIntensity(VolumetricLightBeamHD beam)
	{
		if ((bool)beam)
		{
			m_BaseIntensityBeamOutside = beam.m_Intensity;
		}
	}

	private void SetIntensity(VolumetricLightBeamSD beam, float additive)
	{
		//IL_0045: Invalid comparison between I4 and F4
		//IL_0057: Expected F4, but got I4
		//IL_00ac: Invalid comparison between I4 and F4
		//IL_00be: Expected F4, but got I4
		if ((bool)beam)
		{
			float num = additive + m_BaseIntensityBeamInside;
			bool flag = !(0f < num);
			float intensityInside = 0f;
			if (!flag)
			{
				intensityInside = num;
			}
			beam.intensityInside = intensityInside;
			float num2 = additive + m_BaseIntensityBeamOutside;
			bool flag2 = !(0f < num2);
			float intensityOutside = 0f;
			if (!flag2)
			{
				intensityOutside = num2;
			}
			beam.intensityOutside = intensityOutside;
		}
	}

	private void SetIntensity(VolumetricLightBeamHD beam, float additive)
	{
		//IL_0045: Invalid comparison between I4 and F4
		//IL_0057: Expected F4, but got I4
		if ((bool)beam)
		{
			float num = additive + m_BaseIntensityBeamOutside;
			bool flag = !(0f < num);
			float num2 = 0f;
			if (!flag)
			{
				num2 = num;
			}
			bool flag2 = beam.m_Intensity == num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037B3F4h\"");
			if (!flag2)
			{
				beam.m_Intensity = num2;
				beam.AssignPropertiesFromAttachedSpotLight();
				beam.ClampProperties();
				beam.SetPropertyDirty(DirtyProps.Intensity);
			}
		}
	}

	protected void SetAdditiveIntensity(float additive)
	{
		//IL_0438: Expected O, but got I4
		//IL_0318: Expected O, but got I4
		//IL_03b9: Expected O, but got I4
		//IL_037c: Invalid comparison between I4 and F4
		//IL_038e: Expected F4, but got I4
		//IL_0076: Expected I, but got O
		//IL_007e: Expected I, but got O
		//IL_008e: Expected O, but got I
		//IL_00ca: Expected O, but got I
		//IL_00ef: Expected O, but got I4
		//IL_0156: Invalid comparison between I4 and F4
		//IL_0168: Expected F4, but got I4
		//IL_019e: Expected I, but got O
		//IL_01a6: Expected I, but got O
		//IL_01b6: Expected O, but got I
		//IL_04c9: Invalid comparison between I4 and F4
		//IL_04db: Expected F4, but got I4
		//IL_01f2: Expected O, but got I
		//IL_0217: Expected O, but got I4
		//IL_027e: Invalid comparison between I4 and F4
		//IL_0290: Expected F4, but got I4
		//IL_02bb: Invalid comparison between I and F4
		object obj = componentsToChange & ComponentsToChange.VolumetricLightBeam;
		if (obj == null || !m_Beam)
		{
			goto IL_0308;
		}
		VolumetricLightBeamAbstractBase beam = m_Beam;
		Object obj2;
		if ((object)m_Beam == null)
		{
			obj2 = null;
			goto IL_0116;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)beam;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ r8_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v347 @ r9_v6 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ r8_v11 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		Object obj5;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v347 @ r9_v6 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ rax_v40+FFFFFFF8+v348 @ rax_v36*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj5 = (Object)1;
			if (flag)
			{
				goto IL_045a;
			}
		}
		obj5 = null;
		goto IL_045a;
		IL_045a:
		bool flag2 = (object)obj5 == null;
		obj2 = null;
		if (!flag2)
		{
			obj2 = m_Beam;
		}
		goto IL_0116;
		IL_04e9:
		Object obj6;
		bool flag3 = (object)obj6 == null;
		Object obj7 = null;
		if (!flag3)
		{
			obj7 = m_Beam;
		}
		goto IL_023e;
		IL_0116:
		if ((bool)obj2)
		{
			float num4 = additive + m_BaseIntensityBeamInside;
			bool flag4 = !(0f < num4);
			float num5 = 0f;
			if (!flag4)
			{
				num5 = num4;
			}
			float num6 = additive + m_BaseIntensityBeamOutside;
			bool flag5 = !(0f < num6);
			float num7 = 0f;
			if (!flag5)
			{
				num7 = num6;
			}
		}
		VolumetricLightBeamAbstractBase beam2 = m_Beam;
		bool flag6 = (object)m_Beam == null;
		obj7 = null;
		if (!flag6)
		{
			nint num8 = (nint)typeof(VolumetricLightBeamHD);
			nint num9 = (nint)beam2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v551 @ r8_v10 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v552 @ r9_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v551 @ r8_v10 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num10 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v552 @ r9_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v609 @ rax_v33+FFFFFFF8+v553 @ rax_v30*8]");
				bool flag7 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj6 = (Object)1;
				if (flag7)
				{
					goto IL_04e9;
				}
			}
			obj6 = null;
			goto IL_04e9;
		}
		goto IL_023e;
		IL_023e:
		if ((bool)obj7)
		{
			float num11 = additive + m_BaseIntensityBeamOutside;
			bool flag8 = !(0f < num11);
			float num12 = 0f;
			if (!flag8)
			{
				num12 = num11;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rdi_v10 (UnityEngine.Object)+5C]");
			bool flag9 = 0f == num12;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037B28Eh\"");
			if (!flag9)
			{
				((VolumetricLightBeamHD)obj7).AssignPropertiesFromAttachedSpotLight();
				((VolumetricLightBeamHD)obj7).ClampProperties();
				((VolumetricLightBeamHD)obj7).SetPropertyDirty(DirtyProps.Intensity);
			}
		}
		goto IL_0308;
		IL_0308:
		object obj10 = componentsToChange & ComponentsToChange.UnityLight;
		if (obj10 != null && (bool)m_Light)
		{
			float num13 = additive + m_BaseIntensityLight;
			bool flag10 = !(0f < num13);
			float intensity = 0f;
			if (!flag10)
			{
				intensity = num13;
			}
			m_Light.intensity = intensity;
		}
		object obj11 = componentsToChange & ComponentsToChange.VolumetricDustParticles;
		if (obj11 != null && (bool)m_Particles)
		{
			float alphaAdditionalRuntime = additive + 1f;
			m_Particles.alphaAdditionalRuntime = alphaAdditionalRuntime;
		}
	}

	private void Awake()
	{
		//IL_006c: Expected I, but got O
		//IL_0074: Expected I, but got O
		//IL_0084: Expected O, but got I
		//IL_00c0: Expected O, but got I
		//IL_00e5: Expected O, but got I4
		//IL_0144: Expected F4, but got I
		//IL_0156: Expected F4, but got I
		//IL_0169: Expected I, but got O
		//IL_0171: Expected I, but got O
		//IL_0181: Expected O, but got I
		//IL_01bd: Expected O, but got I
		//IL_01e2: Expected O, but got I4
		//IL_0241: Expected F4, but got I
		//IL_028b: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamAbstractBase beam = default(VolumetricLightBeamAbstractBase);
		m_Beam = beam;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Light light = default(Light);
		m_Light = light;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricDustParticles particles = default(VolumetricDustParticles);
		m_Particles = particles;
		VolumetricLightBeamAbstractBase beam2 = m_Beam;
		Object obj;
		if ((object)m_Beam == null)
		{
			obj = null;
			goto IL_010c;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)beam2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v16 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r8_v9 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v16 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r8_v9 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v39+FFFFFFF8+v81 @ rax_v35*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (Object)1;
			if (flag)
			{
				goto IL_02b8;
			}
		}
		obj4 = null;
		goto IL_02b8;
		IL_0308:
		Object obj5;
		bool flag2 = (object)obj5 == null;
		Object obj6 = null;
		if (!flag2)
		{
			obj6 = m_Beam;
		}
		goto IL_0209;
		IL_0209:
		if ((bool)obj6)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rbx_v2 (UnityEngine.Object)+5C]");
			m_BaseIntensityBeamOutside = 0f;
		}
		float baseIntensityLight = ((!m_Light) ? 0f : m_Light.intensity);
		m_BaseIntensityLight = baseIntensityLight;
		return;
		IL_010c:
		if ((bool)obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v1 (UnityEngine.Object)+5C]");
			m_BaseIntensityBeamInside = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v1 (UnityEngine.Object)+60]");
			m_BaseIntensityBeamOutside = 0f;
		}
		VolumetricLightBeamAbstractBase beam3 = m_Beam;
		bool flag3 = (object)m_Beam == null;
		obj6 = null;
		if (!flag3)
		{
			nint num4 = (nint)typeof(VolumetricLightBeamHD);
			nint num5 = (nint)beam3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r9_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r9_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rax_v27+FFFFFFF8+v291 @ rax_v24*8]");
				bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj5 = (Object)1;
				if (flag4)
				{
					goto IL_0308;
				}
			}
			obj5 = null;
			goto IL_0308;
		}
		goto IL_0209;
		IL_02b8:
		bool flag5 = (object)obj4 == null;
		obj = null;
		if (!flag5)
		{
			obj = m_Beam;
		}
		goto IL_010c;
	}

	protected virtual void OnEnable()
	{
		StopAllCoroutines();
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		if (restoreIntensityOnDisable)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 23 Invalid \"Jump target not found in method: 0x18037B0A0\"");
		}
	}
}
