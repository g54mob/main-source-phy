using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class VolumetricLightBeamHD2D : VolumetricLightBeamHD
{
	private int m_SortingLayerID;

	private int m_SortingOrder;

	public int sortingLayerID
	{
		get
		{
			return m_SortingLayerID;
		}
		set
		{
			m_SortingLayerID = value;
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
			return SortingLayer.IDToName(m_SortingLayerID);
		}
		set
		{
			int num = SortingLayer.NameToID(value);
			sortingLayerID = num;
		}
	}

	public int sortingOrder
	{
		get
		{
			return m_SortingOrder;
		}
		set
		{
			m_SortingOrder = value;
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.sortingOrder = value;
			}
		}
	}

	public override Dimensions GetDimensions()
	{
		return Dimensions.Dim2D;
	}

	public override bool DoesSupportSorting2D()
	{
		return true;
	}

	public override int GetSortingLayerID()
	{
		return m_SortingLayerID;
	}

	public override int GetSortingOrder()
	{
		return m_SortingOrder;
	}

	public override void CopyPropsFrom(VolumetricLightBeamAbstractBase beamSrc, BeamProps beamProps)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0160: Expected I, but got O
		//IL_0170: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_01ac: Expected O, but got I
		//IL_00a4: Expected O, but got I
		//IL_01e1: Expected I, but got O
		//IL_01f1: Expected O, but got I
		//IL_0201: Expected O, but got I
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_00e0: Expected O, but got I
		//IL_0105: Expected O, but got I4
		base.CopyPropsFrom(beamSrc, beamProps);
		if ((object)beamSrc == null)
		{
			return;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)beamSrc;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		VolumetricLightBeamAbstractBase volumetricLightBeamAbstractBase;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rax_v17+FFFFFFF8+v53 @ rax_v4*8]");
			if (0 == (nint)typeof(VolumetricLightBeamSD))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
				if (num4 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rax_v23+FFFFFFF8+v181 @ rax_v18*8]");
					bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
					volumetricLightBeamAbstractBase = (VolumetricLightBeamAbstractBase)1;
					if (flag)
					{
						goto IL_02aa;
					}
				}
				volumetricLightBeamAbstractBase = null;
				goto IL_02aa;
			}
		}
		nint num5 = (nint)typeof(VolumetricLightBeamHD2D);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamHD2D>)+130]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamHD2D>)+130]");
		int num8;
		if (num6 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v3 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rax_v11+FFFFFFF8+v132 @ rax_v10*8]");
			if (0 == (nint)typeof(VolumetricLightBeamHD2D))
			{
				nint num7 = (nint)beamSrc;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r8_v6 (Il2CppClass<VLB.VolumetricLightBeamHD2D>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v280 @ rax_v12 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rcx_v9+FFFFFFF8+v214 @ rdx_v7*8]");
				object obj9 = 0 - typeof(VolumetricLightBeamHD2D);
				bool flag2 = obj9 == null;
				bool flag3 = !flag2;
				VolumetricLightBeamAbstractBase volumetricLightBeamAbstractBase2 = null;
				if (!flag3)
				{
					volumetricLightBeamAbstractBase2 = beamSrc;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt ebp,0Ah\"");
				if ((flag2 ? 1 : 0) < (false ? 1 : 0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rbx_v5 (VLB.VolumetricLightBeamAbstractBase)+D0]");
					sortingLayerID = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rbx_v5 (VLB.VolumetricLightBeamAbstractBase)+D4]");
					num8 = 0;
					goto IL_02f6;
				}
				return;
			}
			return;
		}
		return;
		IL_02aa:
		bool flag4 = (object)volumetricLightBeamAbstractBase == null;
		VolumetricLightBeamAbstractBase volumetricLightBeamAbstractBase3 = null;
		if (!flag4)
		{
			volumetricLightBeamAbstractBase3 = beamSrc;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt ebp,0Ah\"");
		if ((nint)volumetricLightBeamAbstractBase < 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rbx_v10 (VLB.VolumetricLightBeamAbstractBase)+124]");
			sortingLayerID = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rbx_v10 (VLB.VolumetricLightBeamAbstractBase)+128]");
			num8 = 0;
			goto IL_02f6;
		}
		return;
		IL_02f6:
		sortingOrder = num8;
	}

	public VolumetricLightBeamHD2D()
	{
		//IL_012e: Expected I4, but got I8
		//IL_0147: Expected I, but got O
		//IL_0077: Expected I, but got O
		base.m_ColorFromLight = true;
		base.m_Intensity = 1f;
		base.m_IntensityMultiplier = 1f;
		base.m_ColorFlat = Consts.Beam.FlatColor;
		base.m_SpotAngle = 35f;
		base.m_SpotAngleMultiplier = 1f;
		base.m_ConeRadiusStart = 0.1f;
		base.m_Scalable = true;
		base.m_FallOffEnd = 3f;
		base.m_FallOffEndMultiplier = 1f;
		base.m_AttenuationEquation = AttenuationEquationHD.Quadratic;
		base.m_SideSoftness = 1f;
		base.m_RaymarchingQualityID = -1;
		base.m_JitteringFrameRate = 60;
		nint num = (nint)typeof(Consts.Beam.HD);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v6 (Il2CppClass<VLB.Consts+Beam+HD>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v7 (Il2CppStaticFields<VLB.Consts+Beam+HD>)+4]");
		_ = 0;
		base.m_JitteringLerpRange = Consts.Beam.HD.JitteringLerpRange;
		base.m_NoiseIntensity = 0.5f;
		base.m_NoiseScaleUseGlobal = true;
		base.m_NoiseScaleLocal = 0.5f;
		base.m_NoiseVelocityUseGlobal = true;
		nint num3 = (nint)typeof(Consts.Beam);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v9 (Il2CppClass<VLB.Consts+Beam>)+B8]");
		nint num4 = 0;
		base.m_NoiseVelocityLocal = Consts.Beam.NoiseVelocityDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v4 (Il2CppStaticFields<VLB.Consts+Beam>)+18]");
		_ = 0;
		((VolumetricLightBeamAbstractBase)this)._002Ector();
	}
}
