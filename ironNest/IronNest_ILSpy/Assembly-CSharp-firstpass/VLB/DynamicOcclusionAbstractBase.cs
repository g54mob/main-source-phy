using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public abstract class DynamicOcclusionAbstractBase : MonoBehaviour
{
	protected enum ProcessOcclusionSource
	{
		RenderLoop,
		OnEnable,
		EditorUpdate,
		User
	}

	public const string ClassName = "DynamicOcclusionAbstractBase";

	public DynamicOcclusionUpdateRate updateRate;

	public int waitXFrames;

	private Action m_onOcclusionProcessed;

	public static bool _INTERNAL_ApplyRandomFrameOffset = true;

	private TransformUtils.Packed m_TransformPacked;

	private int m_LastFrameRendered;

	protected VolumetricLightBeamSD m_Master;

	protected MaterialModifier.Callback m_MaterialModifierCallbackCached;

	public int _INTERNAL_LastFrameRendered => m_LastFrameRendered;

	public event Action onOcclusionProcessed
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_onOcclusionProcessed;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
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
			object obj = this + 40;
			Delegate obj2 = this.m_onOcclusionProcessed;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
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

	public void ProcessOcclusionManually()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 2 Invalid \"Jump target not found in method: 0x18038E4F0\"");
	}

	protected void ProcessOcclusion(ProcessOcclusionSource source)
	{
		//IL_0167: Expected O, but got I4
		//IL_01ce: Expected O, but got F4
		Config instance = Config.Instance;
		if (!instance.featureEnabledDynamicOcclusion)
		{
			return;
		}
		int frameCount = Time.frameCount;
		if (m_LastFrameRendered != frameCount || !Application.isPlaying || source != ProcessOcclusionSource.OnEnable)
		{
			bool flag = OnProcessOcclusion(source);
			if (this.m_onOcclusionProcessed != null)
			{
				Action action = this.m_onOcclusionProcessed;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v310.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
			if ((bool)m_Master)
			{
				string shaderKeyword = GetShaderKeyword();
				MaterialModifier.Callback cb = ((!flag) ? null : m_MaterialModifierCallbackCached);
				m_Master._INTERNAL_SetDynamicOcclusionCallback(shaderKeyword, cb);
			}
			object obj = updateRate & DynamicOcclusionUpdateRate.OnBeamMove;
			if (obj != null)
			{
				Transform transform = base.transform;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				Vector3 lossyScale = transform.lossyScale;
				m_TransformPacked = (TransformUtils.Packed)position.x;
				_ = 0;
				_ = 0;
			}
			int frameCount2 = Time.frameCount;
			m_LastFrameRendered = frameCount2;
			if (m_LastFrameRendered < 0 && _INTERNAL_ApplyRandomFrameOffset)
			{
				int num = UnityEngine.Random.Range(0, waitXFrames);
				int lastFrameRendered = num + m_LastFrameRendered;
				m_LastFrameRendered = lastFrameRendered;
			}
		}
	}

	protected abstract string GetShaderKeyword();

	protected abstract MaterialManager.SD.DynamicOcclusion GetDynamicOcclusionMode();

	protected abstract bool OnProcessOcclusion(ProcessOcclusionSource source);

	protected abstract void OnModifyMaterialCallback(MaterialModifier.Interface owner);

	protected abstract void OnEnablePostValidate();

	protected virtual void OnValidateProperties()
	{
		if (waitXFrames >= 1)
		{
			if (waitXFrames > 60)
			{
				waitXFrames = 60;
			}
			else
			{
				waitXFrames = waitXFrames;
			}
		}
		else
		{
			waitXFrames = 1;
		}
	}

	protected virtual void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamSD master = default(VolumetricLightBeamSD);
		m_Master = master;
		VolumetricLightBeamSD master2 = m_Master;
		MaterialManager.SD.DynamicOcclusion dynamicOcclusionMode = GetDynamicOcclusionMode();
		master2.m_INTERNAL_DynamicOcclusionMode = dynamicOcclusionMode;
	}

	protected virtual void OnDestroy()
	{
		VolumetricLightBeamSD master = m_Master;
		master.m_INTERNAL_DynamicOcclusionMode = MaterialManager.SD.DynamicOcclusion.Off;
		DisableOcclusion();
	}

	protected unsafe virtual void OnEnable()
	{
		//IL_0447: Expected I, but got O
		//IL_000a: Expected I, but got O
		//IL_00a0: Expected I, but got O
		//IL_014d: Expected I, but got O
		//IL_046c: Expected I, but got I8
		//IL_0486: Expected O, but got I4
		//IL_048e: Expected I, but got O
		//IL_0136: Expected I, but got I8
		//IL_00fc: Expected I, but got I8
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		//IL_01f1: Expected O, but got I4
		//IL_020b: Expected O, but got I4
		//IL_04c6: Expected O, but got I4
		//IL_026e: Expected I, but got O
		//IL_02e1: Expected I, but got O
		//IL_0535: Expected I, but got I8
		//IL_054f: Expected O, but got I4
		//IL_0557: Expected I, but got O
		//IL_055f: Expected I, but got O
		//IL_02ca: Expected I, but got I8
		//IL_05c0: Expected I, but got O
		//IL_05c9: Expected O, but got I4
		//IL_05f8: Expected O, but got I4
		//IL_0617: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r8_v5 (Il2CppClass<VLB.DynamicOcclusionAbstractBase>)+1B0]");
		MaterialModifier.Callback callback = new MaterialModifier.Callback(this, (IntPtr)0);
		bool flag = (object)this == null;
		nint num = (nint)typeof(MaterialModifier.Callback);
		if (flag)
		{
			goto IL_0408;
		}
		nint num2 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r8_v5 (Il2CppClass<VLB.DynamicOcclusionAbstractBase>)+1B0]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r8_v5 (Il2CppClass<VLB.DynamicOcclusionAbstractBase>)+1B0]");
		callback._002Ector(this, (IntPtr)0);
		m_MaterialModifierCallbackCached = callback;
		OnValidateProperties();
		OnEnablePostValidate();
		VolumetricLightBeamSD master = m_Master;
		VolumetricLightBeamSD.OnWillCameraRenderCB onWillCameraRenderCB = null;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rbx_v6 (Il2CppMethodInfo)+8]");
		((Delegate)onWillCameraRenderCB).method_ptr = (IntPtr)0;
		((Delegate)onWillCameraRenderCB).method = (nint)__ldftn(DynamicOcclusionAbstractBase.OnWillCameraRender);
		((Delegate)onWillCameraRenderCB).m_target = this;
		((Delegate)onWillCameraRenderCB).method_code = (IntPtr)onWillCameraRenderCB;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
		object obj = default(object);
		nint num5;
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rbx_v6 (Il2CppMethodInfo)+52]");
			if ((nint)0 != 1)
			{
				goto IL_013b;
			}
			num5 = unchecked((nint)6442459232L);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rbx_v6 (Il2CppMethodInfo)+52]");
			if ((nint)0 != 0)
			{
				goto IL_013b;
			}
			num5 = unchecked((nint)6442459120L);
		}
		goto IL_0455;
		IL_051e:
		VolumetricLightBeamSD.OnBeamGeometryInitialized onBeamGeometryInitialized;
		((Delegate)onBeamGeometryInitialized).extra_arg = unchecked((nint)6442464128L);
		bool flag2 = (object)m_Master == null;
		object obj2 = 0;
		Delegate obj3;
		num3 = (nint)obj3;
		num = (nint)onBeamGeometryInitialized;
		Delegate obj4 = default(Delegate);
		VolumetricLightBeamSD master2;
		nint num6;
		NullReferenceException ex;
		if (!flag2)
		{
			obj4 = Delegate.Combine(master2.m_OnBeamGeometryInitialized, onBeamGeometryInitialized);
			if ((object)obj4 == null)
			{
				master2.m_OnBeamGeometryInitialized = (VolumetricLightBeamSD.OnBeamGeometryInitialized)obj4;
			}
			else
			{
				bool flag3 = (object)obj4.GetType() != typeof(VolumetricLightBeamSD.OnBeamGeometryInitialized);
				Delegate obj5 = null;
				if (!flag3)
				{
					obj5 = obj4;
				}
				bool flag4 = (object)obj5 == null;
				num6 = (nint)typeof(VolumetricLightBeamSD.OnBeamGeometryInitialized);
				obj2 = 0;
				num3 = 0;
				if (flag4)
				{
					goto IL_0630;
				}
				master2.m_OnBeamGeometryInitialized = (VolumetricLightBeamSD.OnBeamGeometryInitialized)obj5;
				bool flag5 = (object)obj4.GetType() != typeof(VolumetricLightBeamSD.OnBeamGeometryInitialized);
				Delegate obj6 = null;
				if (!flag5)
				{
					obj6 = obj4;
				}
				bool flag6 = (object)obj6 == null;
				obj2 = 0;
				num3 = 0;
				ex = (NullReferenceException)(object)obj4;
				num = (nint)typeof(VolumetricLightBeamSD.OnBeamGeometryInitialized);
				if (flag6)
				{
					goto IL_0648;
				}
			}
			if ((bool)master2.m_BeamGeom && master2.m_OnBeamGeometryInitialized != null)
			{
				VolumetricLightBeamSD.OnBeamGeometryInitialized onBeamGeometryInitialized2 = master2.m_OnBeamGeometryInitialized;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v607.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				master2.m_OnBeamGeometryInitialized = null;
			}
			return;
		}
		goto IL_0408;
		IL_0630:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		Delegate obj7 = obj4;
		goto IL_0625;
		IL_0648:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		num6 = num;
		goto IL_0630;
		IL_0625:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_013b:
		((Delegate)onWillCameraRenderCB).method_code = (IntPtr)((Delegate)onWillCameraRenderCB).m_target;
		num5 = ((Delegate)onWillCameraRenderCB).method_ptr;
		goto IL_0455;
		IL_0408:
		ex = new NullReferenceException();
		goto IL_0648;
		IL_0455:
		((Delegate)onWillCameraRenderCB).extra_arg = unchecked((nint)6442458752L);
		bool flag7 = (object)m_Master == null;
		obj2 = 0;
		num = (nint)onWillCameraRenderCB;
		if (flag7)
		{
			goto IL_0408;
		}
		obj3 = master.onWillCameraRenderThisBeam;
		Delegate obj11 = default(Delegate);
		while (true)
		{
			Delegate obj8 = Delegate.Combine(obj3, onWillCameraRenderCB);
			bool flag8 = (object)obj8 == null;
			Delegate obj9 = null;
			if (!flag8)
			{
				bool flag9 = (object)obj8.GetType() != typeof(VolumetricLightBeamSD.OnWillCameraRenderCB);
				obj9 = null;
				if (!flag9)
				{
					obj9 = obj8;
				}
				bool flag10 = (object)obj9 == null;
				obj2 = 0;
				num3 = 0;
				obj7 = (Delegate)(object)typeof(VolumetricLightBeamSD.OnWillCameraRenderCB);
				if (flag10)
				{
					break;
				}
			}
			object obj10 = m_Master + 272;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag11 = (object)obj11 != obj3;
			obj3 = obj11;
			if (flag11)
			{
				continue;
			}
			goto IL_01e1;
		}
		goto IL_0625;
		IL_01e1:
		object obj12 = updateRate & DynamicOcclusionUpdateRate.Never;
		bool flag12 = obj12 == null;
		object obj13 = !flag12;
		if (obj13 != null)
		{
			return;
		}
		master2 = m_Master;
		onBeamGeometryInitialized = null;
		nint num7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rbx_v12 (Il2CppMethodInfo)+8]");
		((Delegate)onBeamGeometryInitialized).method_ptr = (IntPtr)0;
		((Delegate)onBeamGeometryInitialized).method = (nint)__ldftn(DynamicOcclusionAbstractBase._003COnEnable_003Eb__24_0);
		((Delegate)onBeamGeometryInitialized).m_target = this;
		((Delegate)onBeamGeometryInitialized).method_code = (IntPtr)onBeamGeometryInitialized;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
		object obj14 = default(object);
		nint num8;
		if (obj14 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rbx_v12 (Il2CppMethodInfo)+52]");
			if ((nint)0 == 0)
			{
				num8 = unchecked((nint)6442464208L);
				goto IL_051e;
			}
		}
		((Delegate)onBeamGeometryInitialized).method_code = (IntPtr)((Delegate)onBeamGeometryInitialized).m_target;
		num8 = ((Delegate)onBeamGeometryInitialized).method_ptr;
		goto IL_051e;
	}

	protected unsafe virtual void OnDisable()
	{
		//IL_019a: Expected I, but got O
		//IL_00a8: Expected I, but got O
		//IL_01c8: Expected I, but got I8
		//IL_0091: Expected I, but got I8
		//IL_0057: Expected I, but got I8
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		VolumetricLightBeamSD master = m_Master;
		VolumetricLightBeamSD.OnWillCameraRenderCB onWillCameraRenderCB = null;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rbx_v1 (Il2CppMethodInfo)+8]");
		((Delegate)onWillCameraRenderCB).method_ptr = (IntPtr)0;
		((Delegate)onWillCameraRenderCB).method = (nint)__ldftn(DynamicOcclusionAbstractBase.OnWillCameraRender);
		((Delegate)onWillCameraRenderCB).m_target = this;
		((Delegate)onWillCameraRenderCB).method_code = (IntPtr)onWillCameraRenderCB;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
		object obj = default(object);
		nint invoke_impl;
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rbx_v1 (Il2CppMethodInfo)+52]");
			if ((nint)0 != 1)
			{
				goto IL_0096;
			}
			invoke_impl = unchecked((nint)6442459232L);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rbx_v1 (Il2CppMethodInfo)+52]");
			if ((nint)0 != 0)
			{
				goto IL_0096;
			}
			invoke_impl = unchecked((nint)6442459120L);
		}
		goto IL_01a9;
		IL_0096:
		((Delegate)onWillCameraRenderCB).method_code = (IntPtr)((Delegate)onWillCameraRenderCB).m_target;
		invoke_impl = ((Delegate)onWillCameraRenderCB).method_ptr;
		goto IL_01a9;
		IL_01a9:
		((Delegate)onWillCameraRenderCB).invoke_impl = invoke_impl;
		((Delegate)onWillCameraRenderCB).extra_arg = unchecked((nint)6442458752L);
		if ((object)m_Master == null)
		{
			NullReferenceException ex = new NullReferenceException();
			VolumetricLightBeamSD.OnWillCameraRenderCB onWillCameraRenderCB2 = onWillCameraRenderCB;
		}
		else
		{
			Delegate obj2 = master.onWillCameraRenderThisBeam;
			object obj3 = m_Master + 272;
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Remove(obj2, onWillCameraRenderCB);
				bool flag = (object)obj4 == null;
				Delegate obj5 = null;
				if (!flag)
				{
					bool flag2 = (object)obj4.GetType() != typeof(VolumetricLightBeamSD.OnWillCameraRenderCB);
					obj5 = null;
					if (!flag2)
					{
						obj5 = obj4;
					}
					bool flag3 = (object)obj5 == null;
					NullReferenceException ex = (NullReferenceException)(object)obj4;
					VolumetricLightBeamSD.OnWillCameraRenderCB onWillCameraRenderCB2 = (VolumetricLightBeamSD.OnWillCameraRenderCB)(object)typeof(VolumetricLightBeamSD.OnWillCameraRenderCB);
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj6 != obj2;
				obj2 = obj6;
				if (!flag4)
				{
					DisableOcclusion();
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private unsafe void OnWillCameraRender(Camera cam)
	{
		//IL_0089: Expected O, but got I4
		//IL_00fa: Expected O, but got I4
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_0131: Expected O, but got I4
		if (!(cam != null) || !cam.enabled)
		{
			return;
		}
		int frameCount = Time.frameCount;
		if (frameCount == m_LastFrameRendered)
		{
			return;
		}
		object obj = updateRate & DynamicOcclusionUpdateRate.OnBeamMove;
		if (obj != null)
		{
			Transform transf = base.transform;
			TransformUtils.Packed packed = (TransformUtils.Packed)(this + 48);
			if (!((TransformUtils.Packed*)packed)->IsSame(transf))
			{
				goto IL_014d;
			}
		}
		object obj2 = updateRate & DynamicOcclusionUpdateRate.EveryXFrames;
		if (obj2 != null)
		{
			int frameCount2 = Time.frameCount;
			object obj3 = m_LastFrameRendered + waitXFrames;
			if (frameCount2 >= (nint)obj3)
			{
				goto IL_014d;
			}
			return;
		}
		return;
		IL_014d:
		ProcessOcclusion(ProcessOcclusionSource.RenderLoop);
	}

	private void DisableOcclusion()
	{
		VolumetricLightBeamSD master = m_Master;
		string shaderKeyword = GetShaderKeyword();
		master.m_INTERNAL_DynamicOcclusionMode_Runtime = false;
		if ((bool)master.m_BeamGeom)
		{
			master.m_BeamGeom.SetDynamicOcclusionCallback(shaderKeyword, null);
		}
	}

	protected DynamicOcclusionAbstractBase()
	{
		//IL_0025: Expected I4, but got I8
		updateRate = DynamicOcclusionUpdateRate.EveryXFrames;
		waitXFrames = 3;
		m_LastFrameRendered = -2147483648;
		base._002Ector();
	}

	private void _003COnEnable_003Eb__24_0()
	{
		ProcessOcclusion(ProcessOcclusionSource.OnEnable);
	}
}
