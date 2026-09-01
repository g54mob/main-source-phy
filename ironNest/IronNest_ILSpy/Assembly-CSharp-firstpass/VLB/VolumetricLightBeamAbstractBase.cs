using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public abstract class VolumetricLightBeamAbstractBase : MonoBehaviour
{
	public delegate void BeamGeometryGeneratedHandler(VolumetricLightBeamAbstractBase beam);

	public enum AttachedLightType
	{
		NoLight,
		OtherLight,
		SpotLight
	}

	public const string ClassName = "VolumetricLightBeamAbstractBase";

	private BeamGeometryGeneratedHandler m_BeamGeometryGeneratedEvent;

	protected int pluginVersion;

	protected Light m_CachedLightSpot;

	public bool hasGeometry
	{
		get
		{
			BeamGeometryAbstractBase beamGeometry = GetBeamGeometry();
			return beamGeometry != null;
		}
	}

	public unsafe Bounds bounds
	{
		get
		{
			//IL_00db: Expected I, but got O
			//IL_00f9: Expected I, but got O
			//IL_0117: Expected native int or pointer, but got O
			//IL_015c: Expected O, but got F4
			//IL_0157: Expected native int or pointer, but got O
			//IL_0094: Expected native int or pointer, but got O
			BeamGeometryAbstractBase beamGeometry = GetBeamGeometry();
			Bounds bounds = default(Bounds);
			if (beamGeometry != null)
			{
				BeamGeometryAbstractBase beamGeometry2 = GetBeamGeometry();
				if ((object)beamGeometry2 != null && (object)beamGeometry2._003CmeshRenderer_003Ek__BackingField != null)
				{
					((Bounds*)(nint)bounds)->m_Center = beamGeometry2._003CmeshRenderer_003Ek__BackingField.bounds.m_Center;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ rax_v16 (UnityEngine.Bounds)+10]");
					_ = 0;
					return bounds;
				}
				return (Bounds)new NullReferenceException();
			}
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rdx_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rdx_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			_ = 0;
			object obj = default(object);
			float num5 = (float)obj * 0.5f;
			float num6 = (float)Vector3.zeroVector * 0.5f;
			((Bounds*)(nint)bounds)->m_Extents = (Vector3)num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			float num7 = 0f * 0.5f;
			return bounds;
		}
	}

	public int _INTERNAL_pluginVersion => pluginVersion;

	public Light lightSpotAttached => m_CachedLightSpot;

	private event BeamGeometryGeneratedHandler BeamGeometryGeneratedEvent
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 32;
			Delegate obj2 = this.m_BeamGeometryGeneratedEvent;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(BeamGeometryGeneratedHandler);
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
			object obj = this + 32;
			Delegate obj2 = this.m_BeamGeometryGeneratedEvent;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(BeamGeometryGeneratedHandler);
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

	public abstract BeamGeometryAbstractBase GetBeamGeometry();

	protected abstract void SetBeamGeometryNull();

	public void RegisterBeamGeometryGeneratedCallback(BeamGeometryGeneratedHandler callback)
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_00f5: Expected O, but got I4
		//IL_0143: Expected O, but got I4
		BeamGeometryAbstractBase beamGeometry = GetBeamGeometry();
		if (!(beamGeometry == null))
		{
			if (callback != null)
			{
				IntPtr invoke_impl = ((Delegate)callback).invoke_impl;
				IntPtr method = ((Delegate)callback).method;
				IntPtr method_code = ((Delegate)callback).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v96 @ rax_v9 (System.IntPtr) (should have been resolved before IL gen)");
			}
			NullReferenceException ex = new NullReferenceException();
			object obj = 0;
			UnityEngine.Object obj2 = beamGeometry;
		}
		else
		{
			object obj3 = this + 32;
			Delegate obj4 = this.m_BeamGeometryGeneratedEvent;
			Delegate obj7 = default(Delegate);
			while (true)
			{
				Delegate obj5 = Delegate.Combine(obj4, callback);
				bool flag = (object)obj5 == null;
				Delegate obj6 = null;
				if (!flag)
				{
					bool flag2 = (object)obj5.GetType() != typeof(BeamGeometryGeneratedHandler);
					obj6 = null;
					if (!flag2)
					{
						obj6 = obj5;
					}
					bool flag3 = (object)obj6 == null;
					object obj = 0;
					NullReferenceException ex = (NullReferenceException)(object)obj5;
					UnityEngine.Object obj2 = (UnityEngine.Object)(object)typeof(BeamGeometryGeneratedHandler);
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj7 != obj4;
				obj4 = obj7;
				if (!flag4)
				{
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public virtual void GenerateGeometry()
	{
		if (this.m_BeamGeometryGeneratedEvent != null)
		{
			BeamGeometryGeneratedHandler beamGeometryGeneratedEvent = this.m_BeamGeometryGeneratedEvent;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v14.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			this.m_BeamGeometryGeneratedEvent = null;
		}
	}

	public abstract bool IsScalable();

	public abstract Vector3 GetLossyScale();

	public unsafe virtual void CopyPropsFrom(VolumetricLightBeamAbstractBase beamSrc, BeamProps beamProps)
	{
		//IL_000e: Expected O, but got I4
		//IL_00ff: Expected O, but got I4
		//IL_0146: Expected I, but got O
		//IL_014b: Expected I, but got O
		//IL_015b: Expected O, but got I
		//IL_006c: Expected O, but got Ref
		//IL_0197: Expected O, but got I
		//IL_01bc: Expected O, but got I4
		//IL_00ac: Expected O, but got Ref
		//IL_00ec: Expected O, but got Ref
		//IL_0226: Expected I, but got O
		//IL_022b: Expected I, but got O
		//IL_023b: Expected O, but got I
		//IL_0277: Expected O, but got I
		//IL_029c: Expected O, but got I4
		object obj = beamProps & BeamProps.Transform;
		if (obj != null)
		{
			Transform transform = base.transform;
			Transform transform2 = beamSrc.transform;
			Vector3 position = transform2.position;
			float num = default(float);
			transform.position = (Vector3)(&num);
			Transform transform3 = base.transform;
			Transform transform4 = beamSrc.transform;
			Quaternion rotation = transform4.rotation;
			object obj2 = default(object);
			transform3.rotation = (Quaternion)(&obj2);
			Transform transform5 = base.transform;
			Transform transform6 = beamSrc.transform;
			Vector3 localScale = transform6.localScale;
			transform5.localScale = (Vector3)(&num);
		}
		object obj3 = beamProps & BeamProps.SideSoftness;
		if (obj3 == null)
		{
			return;
		}
		float thickness = UtilsBeamProps.GetThickness(beamSrc);
		UnityEngine.Object obj4;
		if ((object)this == null)
		{
			obj4 = null;
			goto IL_01de;
		}
		nint num2 = (nint)typeof(VolumetricLightBeamSD);
		nint num3 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rdx_v10 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj7;
		if (num4 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ r8_v8 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rax_v23+FFFFFFF8+v335 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj7 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_0363;
			}
		}
		obj7 = null;
		goto IL_0363;
		IL_01de:
		UnityEngine.Object obj8;
		UnityEngine.Object obj11;
		if (!obj4)
		{
			bool flag2 = (object)this == null;
			obj8 = null;
			if (!flag2)
			{
				nint num5 = (nint)typeof(VolumetricLightBeamHD);
				nint num6 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v452 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v453 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v452 @ rdx_v9 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
				if (num7 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v453 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v509 @ rax_v17+FFFFFFF8+v454 @ rax_v14*8]");
					bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
					obj11 = (UnityEngine.Object)1;
					if (flag3)
					{
						goto IL_038a;
					}
				}
				obj11 = null;
				goto IL_038a;
			}
			goto IL_02be;
		}
		float num8 = 1f - thickness;
		float num9 = num8 * 10f;
		return;
		IL_02be:
		if ((bool)obj8)
		{
			float num10 = 1f - thickness;
			float sideSoftness = num10 * 10f;
			((VolumetricLightBeamHD)obj8).sideSoftness = sideSoftness;
		}
		return;
		IL_0363:
		bool flag4 = (object)obj7 == null;
		obj4 = null;
		if (!flag4)
		{
			obj4 = this;
		}
		goto IL_01de;
		IL_038a:
		bool flag5 = (object)obj11 == null;
		obj8 = null;
		if (!flag5)
		{
			obj8 = this;
		}
		goto IL_02be;
	}

	public unsafe Light GetLightSpotAttachedSlow(out AttachedLightType lightType)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!obj)
		{
			ref AttachedLightType reference = ref *(AttachedLightType*)null;
			return null;
		}
		if ((object)obj != null)
		{
			ref AttachedLightType reference;
			if (((Light)obj).type != LightType.Spot)
			{
				reference = ref *(AttachedLightType*)1;
				return null;
			}
			reference = ref *(AttachedLightType*)2;
			return (Light)obj;
		}
		return (Light)(object)new NullReferenceException();
	}

	protected void InitLightSpotAttachedCached()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		UnityEngine.Object cachedLightSpot;
		if (!obj)
		{
			cachedLightSpot = null;
		}
		else
		{
			LightType type = ((Light)obj).type;
			bool flag = type == LightType.Spot;
			UnityEngine.Object obj2 = obj;
			if (!flag)
			{
				obj2 = null;
			}
			cachedLightSpot = obj2;
		}
		m_CachedLightSpot = (Light)cachedLightSpot;
	}

	private void OnDestroy()
	{
		//IL_0044: Expected I, but got O
		//IL_0054: Expected O, but got I
		//IL_0064: Expected O, but got I
		while (true)
		{
			if (Application.isPlaying)
			{
				BeamGeometryAbstractBase beamGeometry = GetBeamGeometry();
				BeamGeometryAbstractBase.DestroyBeamGeometryGameObject(beamGeometry);
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v66 @ rax_v5 (should have been resolved before IL gen)");
		}
	}

	protected void DestroyBeam()
	{
		//IL_0044: Expected I, but got O
		//IL_0054: Expected O, but got I
		//IL_0064: Expected O, but got I
		while (true)
		{
			if (Application.isPlaying)
			{
				BeamGeometryAbstractBase beamGeometry = GetBeamGeometry();
				BeamGeometryAbstractBase.DestroyBeamGeometryGameObject(beamGeometry);
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v66 @ rax_v5 (should have been resolved before IL gen)");
		}
	}

	protected VolumetricLightBeamAbstractBase()
	{
		//IL_000f: Expected I4, but got I8
		pluginVersion = -1;
		base._002Ector();
	}
}
