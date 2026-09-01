using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class TriggerZone : MonoBehaviour
{
	private enum TriggerZoneUpdateRate
	{
		OnEnable,
		OnOcclusionChange
	}

	public const string ClassName = "TriggerZone";

	public bool setIsTrigger = true;

	public float rangeMultiplier = 1f;

	private const int kMeshColliderNumSides = 8;

	private VolumetricLightBeamAbstractBase m_Beam;

	private DynamicOcclusionRaycasting m_DynamicOcclusionRaycasting;

	private PolygonCollider2D m_PolygonCollider2D;

	private TriggerZoneUpdateRate updateRate
	{
		get
		{
			if (UtilsBeamProps.GetDimensions(m_Beam) != Dimensions.Dim3D && m_DynamicOcclusionRaycasting != null)
			{
				return TriggerZoneUpdateRate.OnOcclusionChange;
			}
			return TriggerZoneUpdateRate.OnEnable;
		}
	}

	private void OnEnable()
	{
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamAbstractBase beam = default(VolumetricLightBeamAbstractBase);
		m_Beam = beam;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		DynamicOcclusionRaycasting dynamicOcclusionRaycasting = default(DynamicOcclusionRaycasting);
		m_DynamicOcclusionRaycasting = dynamicOcclusionRaycasting;
		if (UtilsBeamProps.GetDimensions(m_Beam) != Dimensions.Dim3D && m_DynamicOcclusionRaycasting != null)
		{
			if (!m_DynamicOcclusionRaycasting)
			{
				return;
			}
			DynamicOcclusionRaycasting dynamicOcclusionRaycasting2 = m_DynamicOcclusionRaycasting;
			Action b = OnOcclusionProcessed;
			Delegate obj = ((DynamicOcclusionAbstractBase)dynamicOcclusionRaycasting2).onOcclusionProcessed;
			object obj2 = dynamicOcclusionRaycasting2 + 40;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, b);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				bool flag3 = (object)obj5 != obj;
				obj = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			throw new NullReferenceException();
		}
		ComputeZone();
		base.enabled = false;
	}

	private void OnOcclusionProcessed()
	{
		ComputeZone();
	}

	private unsafe void ComputeZone()
	{
		//IL_0008: Expected O, but got Ref
		//IL_039c: Expected I, but got O
		//IL_03a4: Expected I, but got O
		//IL_03b4: Expected O, but got I
		//IL_014d: Expected O, but got F4
		//IL_0434: Expected O, but got I4
		//IL_016b: Expected O, but got F4
		//IL_03f0: Expected O, but got I
		//IL_0426: Expected O, but got I4
		//IL_0231: Invalid comparison between O and F4
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_026e: Invalid comparison between F4 and O
		//IL_052d: Expected I, but got O
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Expected O, but got Unknown
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Expected O, but got Unknown
		//IL_0603: Expected I, but got O
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Expected O, but got Unknown
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Expected O, but got Unknown
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Expected O, but got Unknown
		//IL_07c6: Invalid comparison between F4 and O
		//IL_02a4: Expected O, but got Ref
		//IL_02c4: Expected O, but got Ref
		//IL_02d5: Expected O, but got Ref
		//IL_02d5: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (!m_Beam)
		{
			return;
		}
		float coneRadiusStart = UtilsBeamProps.GetConeRadiusStart(m_Beam);
		float fallOffEnd = UtilsBeamProps.GetFallOffEnd(m_Beam);
		float lengthZ = fallOffEnd * rangeMultiplier;
		float coneRadiusEnd = UtilsBeamProps.GetConeRadiusEnd(m_Beam);
		float num = coneRadiusEnd - coneRadiusStart;
		float num2 = num * rangeMultiplier;
		float num3 = num2 + coneRadiusStart;
		if (UtilsBeamProps.GetDimensions(m_Beam) != Dimensions.Dim3D)
		{
			if (m_PolygonCollider2D == null)
			{
				GameObject self = base.gameObject;
				PolygonCollider2D orAddComponent = Utils.GetOrAddComponent<PolygonCollider2D>(self);
				m_PolygonCollider2D = orAddComponent;
			}
			Vector2[] array = new Vector2[4];
			object obj3 = coneRadiusStart ^ -0f;
			_ = 0;
			object obj4 = num3 ^ -0f;
			_ = 0;
			bool flag = m_DynamicOcclusionRaycasting;
			bool flag2 = !flag;
			Vector2[] points = array;
			if (!flag2)
			{
				DynamicOcclusionRaycasting dynamicOcclusionRaycasting = m_DynamicOcclusionRaycasting;
				object obj5 = (object)dynamicOcclusionRaycasting._003CplaneEquationWS_003Ek__BackingField * (object)dynamicOcclusionRaycasting._003CplaneEquationWS_003Ek__BackingField;
				object obj7 = default(object);
				object obj6 = obj7 * obj7;
				object obj8 = obj7 * obj7;
				object obj9 = obj6 + obj5;
				object obj10 = obj9 + obj8;
				bool flag3 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f);
				points = array;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj11 = obj7 & 0;
					bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11);
					points = array;
					if (!flag4)
					{
						nint num4 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1112 @ rcx_v41 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num5 = 0;
						object obj12 = obj7 * obj7;
						object obj13 = (object)dynamicOcclusionRaycasting._003CplaneEquationWS_003Ek__BackingField * (object)Vector3.zeroVector;
						object obj14 = obj12 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1130 @ rax_v48 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						object obj15 = obj7 * 0;
						object obj16 = obj14 + obj15;
						object obj17 = obj16 + obj7;
						object obj18 = (object)dynamicOcclusionRaycasting._003CplaneEquationWS_003Ek__BackingField * obj17;
						object obj19 = (object)Vector3.zeroVector - obj18;
						object obj20 = obj7 * obj17;
						object obj21 = obj7 - obj20;
						object obj22 = obj7 * obj17;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1130 @ rax_v48 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						object obj23 = 0 - obj22;
						nint num6 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1184 @ rcx_v42 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num7 = 0;
						object obj24 = (object)dynamicOcclusionRaycasting._003CplaneEquationWS_003Ek__BackingField * (object)Vector3.upVector;
						object obj25 = obj7 * obj7;
						object obj26 = obj25 + obj24;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1186 @ rax_v50 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
						object obj27 = obj7 * 0;
						object obj28 = obj26 + obj27;
						object obj29 = obj28 + obj7;
						object obj30 = (object)dynamicOcclusionRaycasting._003CplaneEquationWS_003Ek__BackingField * obj29;
						object obj31 = (object)Vector3.upVector - obj30;
						object obj32 = obj7 * obj29;
						object obj33 = obj7 - obj32;
						object obj34 = obj19 - obj31;
						object obj35 = obj7 * obj29;
						object obj36 = obj21 - obj33;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1186 @ rax_v50 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
						object obj37 = 0 - obj35;
						object obj38 = obj34 * obj34;
						object obj39 = obj36 * obj36;
						object obj40 = obj23 - obj37;
						object obj41 = obj39 + obj38;
						object obj42 = obj40 * obj40;
						object obj43 = obj41 + obj42;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj44 = obj43 & 0;
						bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj44);
						Vector3 vector = Vector3.upVector;
						if (!flag5)
						{
							vector = Vector3.rightVector;
						}
						Transform transform = base.transform;
						object obj45 = default(object);
						Vector3 vector2 = transform.InverseTransformPoint((Vector3)(&obj45));
						Transform transform2 = base.transform;
						Vector3 vector3 = transform2.InverseTransformPoint((Vector3)(&obj45));
						PolygonHelper.Plane2D plane2D = default(PolygonHelper.Plane2D);
						if ((nint)PolygonHelper.Plane2D.FromPoints((Vector3)(&vector), (Vector3)(&obj45)).normal > 0)
						{
							plane2D.Flip();
						}
						Vector2[] array2 = plane2D.CutConvex(array);
						points = array2;
					}
				}
			}
			m_PolygonCollider2D.points = points;
			m_PolygonCollider2D.isTrigger = setIsTrigger;
			return;
		}
		GameObject self2 = base.gameObject;
		MeshCollider orAddComponent2 = Utils.GetOrAddComponent<MeshCollider>(self2);
		VolumetricLightBeamAbstractBase beam = m_Beam;
		UnityEngine.Object obj46;
		if ((object)m_Beam == null)
		{
			obj46 = null;
			goto IL_0448;
		}
		nint num8 = (nint)typeof(VolumetricLightBeamSD);
		nint num9 = (nint)beam;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v471 @ rdx_v17 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj47 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v472 @ r8_v9 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v471 @ rdx_v17 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj49;
		if (num10 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v472 @ r8_v9 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj48 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v813 @ rax_v32+FFFFFFF8+v473 @ rax_v28*8]");
			if (0 == (nint)typeof(VolumetricLightBeamSD))
			{
				obj49 = 1;
				goto IL_064c;
			}
		}
		obj49 = 0;
		goto IL_064c;
		IL_064c:
		bool flag6 = obj49 == null;
		obj46 = null;
		if (!flag6)
		{
			obj46 = m_Beam;
		}
		goto IL_0448;
		IL_04a1:
		bool inverted = default(bool);
		Mesh mesh = MeshGenerator.GenerateConeZ_Radii_DoubleCaps(lengthZ, coneRadiusStart, num3, 8, inverted);
		HideFlags proceduralObjectsHideFlags = Consts.Internal.ProceduralObjectsHideFlags;
		mesh.hideFlags = proceduralObjectsHideFlags;
		orAddComponent2.sharedMesh = mesh;
		orAddComponent2.convex = setIsTrigger;
		orAddComponent2.isTrigger = setIsTrigger;
		return;
		IL_0448:
		if ((bool)obj46)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v608 @ rdi_v6 (UnityEngine.Object)+84]");
			if ((nint)0 == 1)
			{
				goto IL_04a1;
			}
		}
		Config instance = Config.Instance;
		goto IL_04a1;
	}
}
