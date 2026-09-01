using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Shapes;

public abstract class ShapeRenderer : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ShapeGroup, bool> _003C_003E9__152_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CSetColor_003Eb__152_0(ShapeGroup g)
		{
			//IL_0035: Expected I4, but got O
			if ((object)g != null)
			{
				return g._003CIsEnabled_003Ek__BackingField;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass139_0
	{
		public Material[] sourceMats;

		public ShapeRenderer _003C_003E4__this;
	}

	private bool initializedComponents;

	private MeshRenderer rnd;

	private MeshFilter mf;

	private int meshOwnerID;

	private MaterialPropertyBlock mpb;

	private Material[] instancedMaterials;

	[NonSerialized]
	public bool meshOutOfDate;

	private ShapesBlendMode blendMode;

	private ScaleMode scaleMode;

	private protected Color color;

	private protected DetailLevel detailLevel;

	private protected ShapeCulling culling;

	private protected float boundsPadding;

	private int renderQueue;

	public const int DEFAULT_RENDER_QUEUE_AUTO = -1;

	public const CompareFunction DEFAULT_ZTEST = CompareFunction.LessEqual;

	public const float DEFAULT_ZOFS_FACTOR = 0f;

	public const int DEFAULT_ZOFS_UNITS = 0;

	public const ColorWriteMask DEFAULT_COLOR_MASK = ColorWriteMask.All;

	private CompareFunction zTest;

	private float zOffsetFactor;

	private int zOffsetUnits;

	private ColorWriteMask colorMask;

	public const CompareFunction DEFAULT_STENCIL_COMP = CompareFunction.Always;

	public const StencilOp DEFAULT_STENCIL_OP = StencilOp.Keep;

	public const byte DEFAULT_STENCIL_REF_ID = 0;

	public const byte DEFAULT_STENCIL_MASK = 255;

	private CompareFunction stencilComp;

	private StencilOp stencilOpPass;

	private byte stencilRefID;

	private byte stencilReadMask;

	private byte stencilWriteMask;

	private bool shouldUpdateMaterialPropertiesInEditor;

	private Material[] mats;

	private MaterialPropertyBlock Mpb
	{
		get
		{
			MaterialPropertyBlock result = mpb;
			if (mpb == null)
			{
				result = (mpb = new MaterialPropertyBlock());
			}
			return result;
		}
	}

	internal bool IsUsingUniqueMaterials
	{
		get
		{
			//IL_0037: Invalid comparison between F4 and I4
			//IL_014c: Expected O, but got I4
			if (zTest == CompareFunction.LessEqual)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018105190Fh\"");
				if (zOffsetFactor == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All)
				{
					object obj = renderQueue - -1;
					bool flag = obj == null;
					return !flag;
				}
			}
			return true;
		}
	}

	public Mesh Mesh
	{
		get
		{
			if ((object)mf != null)
			{
				return mf.sharedMesh;
			}
			return (Mesh)(object)new NullReferenceException();
		}
		private set
		{
			mf.sharedMesh = value;
		}
	}

	public unsafe int SortingLayerID
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected Ref, but got Unknown
			//IL_0025: Expected I4, but got O
			MeshRenderer meshRenderer = MakeSureComponentExists(ref *(MeshRenderer*)(this + 40), out var _);
			if ((object)meshRenderer != null)
			{
				return meshRenderer.sortingLayerID;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected Ref, but got Unknown
			MeshRenderer meshRenderer = MakeSureComponentExists(ref *(MeshRenderer*)(this + 40), out var _);
			meshRenderer.sortingLayerID = value;
		}
	}

	public unsafe int SortingOrder
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected Ref, but got Unknown
			//IL_0025: Expected I4, but got O
			MeshRenderer meshRenderer = MakeSureComponentExists(ref *(MeshRenderer*)(this + 40), out var _);
			if ((object)meshRenderer != null)
			{
				return meshRenderer.sortingOrder;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected Ref, but got Unknown
			MeshRenderer meshRenderer = MakeSureComponentExists(ref *(MeshRenderer*)(this + 40), out var _);
			meshRenderer.sortingOrder = value;
		}
	}

	public unsafe string SortingLayerName
	{
		get
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected Ref, but got Unknown
			MeshRenderer meshRenderer = MakeSureComponentExists(ref *(MeshRenderer*)(this + 40), out var _);
			if ((object)meshRenderer != null)
			{
				int sortingLayerID = meshRenderer.sortingLayerID;
				return SortingLayer.IDToName(sortingLayerID);
			}
			return (string)(object)new NullReferenceException();
		}
	}

	public ShapesBlendMode BlendMode
	{
		get
		{
			return blendMode;
		}
		set
		{
			blendMode = value;
			UpdateMaterial();
		}
	}

	public ScaleMode ScaleMode
	{
		get
		{
			return scaleMode;
		}
		set
		{
			scaleMode = value;
			SetIntNow(ShapesMaterialUtils.propScaleMode, (int)value);
		}
	}

	public unsafe virtual Color Color
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)this.color;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			color = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&obj));
			ApplyProperties();
		}
	}

	public virtual DetailLevel DetailLevel
	{
		get
		{
			return detailLevel;
		}
		set
		{
			detailLevel = value;
			UpdateMesh(force: true);
		}
	}

	public ShapeCulling Culling
	{
		get
		{
			return culling;
		}
		set
		{
			culling = value;
			UpdateBounds();
		}
	}

	public float BoundsPadding
	{
		get
		{
			return boundsPadding;
		}
		set
		{
			boundsPadding = value;
			UpdateBounds();
		}
	}

	private bool IsInstanced
	{
		get
		{
			//IL_0037: Invalid comparison between F4 and I4
			//IL_014c: Expected O, but got I4
			if (zTest == CompareFunction.LessEqual)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018105189Fh\"");
				if (zOffsetFactor == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All)
				{
					object obj = renderQueue - -1;
					return obj == null;
				}
			}
			return false;
		}
	}

	private bool UsingDefaultRenderQueue
	{
		get
		{
			//IL_0010: Expected O, but got I4
			object obj = renderQueue - -1;
			return obj == null;
		}
	}

	public int RenderQueue
	{
		get
		{
			return renderQueue;
		}
		set
		{
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Expected O, but got Unknown
			//IL_0191: Expected O, but got I4
			//IL_019a: Expected O, but got I4
			//IL_0041: Invalid comparison between F4 and I4
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Expected O, but got Unknown
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ca: Expected O, but got Unknown
			renderQueue = value;
			if (zTest == CompareFunction.LessEqual)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181051D78h\"");
				if (zOffsetFactor == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All && value == -1)
				{
					return;
				}
			}
			UpdateMaterial();
			Material[] array = instancedMaterials;
			object obj = instancedMaterials + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < array.Length)
			{
				((Material)obj).renderQueue = renderQueue;
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
		}
	}

	private bool UsingDefaultZTests
	{
		get
		{
			//IL_0037: Invalid comparison between F4 and I4
			if (zTest == CompareFunction.LessEqual)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181051B93h\"");
				if (zOffsetFactor == 0f)
				{
					return zOffsetUnits == 0;
				}
			}
			return false;
		}
	}

	public CompareFunction ZTest
	{
		get
		{
			return zTest;
		}
		set
		{
			zTest = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propZTest, (int)value);
		}
	}

	public float ZOffsetFactor
	{
		get
		{
			return zOffsetFactor;
		}
		set
		{
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Expected O, but got Unknown
			//IL_016a: Expected O, but got I4
			//IL_0173: Expected O, but got I4
			//IL_0018: Invalid comparison between F4 and I4
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Expected O, but got Unknown
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Expected O, but got Unknown
			zOffsetFactor = value;
			if (zTest == CompareFunction.LessEqual)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181052287h\"");
				if (value == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All && renderQueue == -1)
				{
					return;
				}
			}
			UpdateMaterial();
			Material[] array = instancedMaterials;
			object obj = instancedMaterials + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj2 < array.Length)
			{
				((Material)obj).SetFloat(ShapesMaterialUtils.propZOffsetFactor, value);
				obj3++;
				obj += 8;
				obj2 = obj3;
			}
		}
	}

	public int ZOffsetUnits
	{
		get
		{
			return zOffsetUnits;
		}
		set
		{
			zOffsetUnits = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propZOffsetUnits, value);
		}
	}

	public ColorWriteMask ColorMask
	{
		get
		{
			return colorMask;
		}
		set
		{
			colorMask = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propColorMask, (int)value);
		}
	}

	private bool UsingDefaultMasking
	{
		get
		{
			//IL_00ba: Expected O, but got I4
			if (stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255)
			{
				object obj = colorMask - 15;
				return obj == null;
			}
			return false;
		}
	}

	public CompareFunction StencilComp
	{
		get
		{
			return stencilComp;
		}
		set
		{
			stencilComp = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propStencilComp, (int)value);
		}
	}

	public StencilOp StencilOpPass
	{
		get
		{
			return stencilOpPass;
		}
		set
		{
			stencilOpPass = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propStencilOpPass, (int)value);
		}
	}

	public byte StencilRefID
	{
		get
		{
			return stencilRefID;
		}
		set
		{
			stencilRefID = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propStencilID, value);
		}
	}

	public byte StencilReadMask
	{
		get
		{
			return stencilReadMask;
		}
		set
		{
			stencilReadMask = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propStencilReadMask, value);
		}
	}

	public byte StencilWriteMask
	{
		get
		{
			return stencilWriteMask;
		}
		set
		{
			stencilWriteMask = value;
			SetIntOnAllInstancedMaterials(ShapesMaterialUtils.propStencilWriteMask, value);
		}
	}

	public bool ShouldUpdateMaterialPropertiesInEditor
	{
		get
		{
			return shouldUpdateMaterialPropertiesInEditor;
		}
		set
		{
			shouldUpdateMaterialPropertiesInEditor = value;
		}
	}

	private bool HasGeneratedOrCopyOfMesh
	{
		get
		{
			//IL_0048: Expected O, but got I4
			MeshUpdateMode meshUpdateMode = MeshUpdateMode;
			if (meshUpdateMode == MeshUpdateMode.SelfGenerated)
			{
				return true;
			}
			MeshUpdateMode meshUpdateMode2 = MeshUpdateMode;
			object obj = meshUpdateMode2 - 1;
			return obj == null;
		}
	}

	private protected virtual int MaterialCount => 1;

	private protected virtual MeshUpdateMode MeshUpdateMode => MeshUpdateMode.UseAsset;

	internal virtual bool HasScaleModes => true;

	internal virtual bool HasDetailLevels => true;

	private protected virtual bool UseCamOnPreCull => false;

	private unsafe T MakeSureComponentExists<T>(ref T field, out bool created)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		if (!(System.Runtime.CompilerServices.Unsafe.As<T, UnityEngine.Object>(ref field) == null))
		{
			goto IL_011c;
		}
		ref bool reference2;
		if ((object)this != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			object obj = default(object);
			ref T reference = ref *(T*)obj;
			if (System.Runtime.CompilerServices.Unsafe.As<T, UnityEngine.Object>(ref field) == null)
			{
				GameObject gameObject = base.gameObject;
				if ((object)gameObject == null)
				{
					goto IL_012b;
				}
				T val = gameObject.AddComponent<T>();
				reference = ref *(T*)val;
				reference2 = ref *(bool*)1;
			}
			if (System.Runtime.CompilerServices.Unsafe.As<T, object>(ref field) != null)
			{
				System.Runtime.CompilerServices.Unsafe.As<T, UnityEngine.Object>(ref field).hideFlags = HideFlags.HideInInspector;
				goto IL_011c;
			}
		}
		goto IL_012b;
		IL_011c:
		reference2 = ref *(bool*)null;
		return (T)System.Runtime.CompilerServices.Unsafe.As<T, object>(ref field);
		IL_012b:
		return (T)new NullReferenceException();
	}

	private unsafe void VerifyComponents()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected Ref, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected Ref, but got Unknown
		if (!initializedComponents)
		{
			initializedComponents = true;
			MeshFilter meshFilter = MakeSureComponentExists(ref *(MeshFilter*)(this + 48), out var _);
			MeshRenderer meshRenderer = MakeSureComponentExists(ref *(MeshRenderer*)(this + 40), out var _);
		}
		if (rnd.receiveShadows)
		{
			rnd.receiveShadows = false;
		}
		if (rnd.shadowCastingMode != ShadowCastingMode.Off)
		{
			rnd.shadowCastingMode = ShadowCastingMode.Off;
		}
		if (rnd.lightProbeUsage != LightProbeUsage.Off)
		{
			rnd.lightProbeUsage = LightProbeUsage.Off;
		}
		if (rnd.reflectionProbeUsage != ReflectionProbeUsage.Off)
		{
			rnd.reflectionProbeUsage = ReflectionProbeUsage.Off;
		}
	}

	public virtual void Awake()
	{
		VerifyComponents();
		UpdateMaterial();
		UpdateMesh();
		UpdateAllMaterialProperties();
	}

	public virtual void OnEnable()
	{
		//IL_019f: Expected I, but got O
		UpdateMesh();
		if ((object)rnd != null)
		{
			rnd.enabled = true;
			if (!UseCamOnPreCull)
			{
				return;
			}
			if (UnityInfo.UsingSRP)
			{
				Action<ScriptableRenderContext, Camera> value = OnPreCamCullWithCam;
				RenderPipelineManager.beginCameraRendering += value;
				return;
			}
			Camera.CameraCallback b = OnPreCamCullWithCam;
			Delegate obj = Delegate.Combine(Camera.onPreCull, b);
			if ((object)obj == null)
			{
				Camera.onPreCull = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(Camera.CameraCallback);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			nint num = (nint)typeof(Camera.CameraCallback);
			Delegate obj3 = obj;
			if (flag2)
			{
				goto IL_01e8;
			}
			Camera.onPreCull = (Camera.CameraCallback)obj2;
			bool flag3 = (object)obj.GetType() != typeof(Camera.CameraCallback);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj;
			}
			bool flag4 = (object)obj4 == null;
			NullReferenceException typeFromHandle = (NullReferenceException)(object)typeof(Camera.CameraCallback);
			obj3 = obj;
			if (!flag4)
			{
				return;
			}
		}
		else
		{
			NullReferenceException typeFromHandle = new NullReferenceException();
			Delegate obj3 = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_01e8;
		IL_01e8:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void OnDisable()
	{
		//IL_01d9: Expected I, but got O
		if (rnd != null)
		{
			if ((object)rnd == null)
			{
				NullReferenceException ex = new NullReferenceException();
				Delegate obj = null;
				goto IL_0225;
			}
			rnd.enabled = false;
		}
		if (!UseCamOnPreCull)
		{
			return;
		}
		if (!UnityInfo.UsingSRP)
		{
			Camera.CameraCallback value = OnPreCamCullWithCam;
			Delegate obj2 = Delegate.Remove(Camera.onPreCull, value);
			if ((object)obj2 == null)
			{
				Camera.onPreCull = null;
				return;
			}
			bool flag = (object)obj2.GetType() != typeof(Camera.CameraCallback);
			Delegate obj3 = null;
			if (!flag)
			{
				obj3 = obj2;
			}
			bool flag2 = (object)obj3 == null;
			Delegate obj = obj2;
			nint num = (nint)typeof(Camera.CameraCallback);
			if (flag2)
			{
				goto IL_021a;
			}
			Camera.onPreCull = (Camera.CameraCallback)obj3;
			bool flag3 = (object)obj2.GetType() != typeof(Camera.CameraCallback);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj2;
			}
			bool flag4 = (object)obj4 == null;
			obj = obj2;
			NullReferenceException ex = (NullReferenceException)(object)typeof(Camera.CameraCallback);
			if (!flag4)
			{
				return;
			}
			goto IL_0225;
		}
		Action<ScriptableRenderContext, Camera> value2 = OnPreCamCullWithCam;
		RenderPipelineManager.beginCameraRendering -= value2;
		return;
		IL_0225:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_021a;
		IL_021a:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void OnPreCamCullWithCam(Camera cam)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Shapes.ShapeRenderer>)+288]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Shapes.ShapeRenderer>)+290]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	private void OnPreCamCullWithCam(ScriptableRenderContext ctx, Camera cam)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Shapes.ShapeRenderer>)+288]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Shapes.ShapeRenderer>)+290]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	private void SubscribeCamPreCull()
	{
		//IL_013e: Expected I, but got O
		if (!UnityInfo.UsingSRP)
		{
			Camera.CameraCallback b = OnPreCamCullWithCam;
			Delegate obj = Delegate.Combine(Camera.onPreCull, b);
			if ((object)obj == null)
			{
				Camera.onPreCull = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(Camera.CameraCallback);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			nint num = (nint)typeof(Camera.CameraCallback);
			if (!flag2)
			{
				Camera.onPreCull = (Camera.CameraCallback)obj2;
				bool flag3 = (object)obj.GetType() != typeof(Camera.CameraCallback);
				Delegate obj3 = null;
				if (!flag3)
				{
					obj3 = obj;
				}
				if ((object)obj3 != null)
				{
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		else
		{
			Action<ScriptableRenderContext, Camera> value = OnPreCamCullWithCam;
			RenderPipelineManager.beginCameraRendering += value;
		}
	}

	private void UnsubscribeCamPreCull()
	{
		//IL_013e: Expected I, but got O
		if (!UnityInfo.UsingSRP)
		{
			Camera.CameraCallback value = OnPreCamCullWithCam;
			Delegate obj = Delegate.Remove(Camera.onPreCull, value);
			if ((object)obj == null)
			{
				Camera.onPreCull = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(Camera.CameraCallback);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			nint num = (nint)typeof(Camera.CameraCallback);
			if (!flag2)
			{
				Camera.onPreCull = (Camera.CameraCallback)obj2;
				bool flag3 = (object)obj.GetType() != typeof(Camera.CameraCallback);
				Delegate obj3 = null;
				if (!flag3)
				{
					obj3 = obj;
				}
				if ((object)obj3 != null)
				{
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		else
		{
			Action<ScriptableRenderContext, Camera> value2 = OnPreCamCullWithCam;
			RenderPipelineManager.beginCameraRendering -= value2;
		}
	}

	private void Reset()
	{
		UpdateAllMaterialProperties();
		UpdateMesh(force: true);
	}

	private void OnDestroy()
	{
		MeshUpdateMode meshUpdateMode = MeshUpdateMode;
		if (meshUpdateMode != MeshUpdateMode.SelfGenerated)
		{
			MeshUpdateMode meshUpdateMode2 = MeshUpdateMode;
			if (meshUpdateMode2 != MeshUpdateMode.UseAssetCopy)
			{
				goto IL_008c;
			}
		}
		Mesh sharedMesh = mf.sharedMesh;
		if (sharedMesh != null)
		{
			Mesh sharedMesh2 = mf.sharedMesh;
			UnityEngine.Object.DestroyImmediate(sharedMesh2);
		}
		goto IL_008c;
		IL_008c:
		ShapesExtensions.TryDestroyInOnDestroy(this, rnd);
		ShapesExtensions.TryDestroyInOnDestroy(this, mf);
		TryDestroyInstancedMaterials(inOnDestroy: true);
	}

	private protected abstract Bounds GetUnpaddedLocalBounds_Internal();

	private protected abstract void SetAllMaterialProperties();

	private protected virtual void ShapeClampRanges()
	{
	}

	private protected abstract void GetMaterials(Material[] mats);

	private protected virtual void GenerateMesh()
	{
	}

	private protected virtual Mesh GetInitialMeshAsset()
	{
		//IL_000e: Expected I, but got O
		//IL_003d: Expected O, but got I
		//IL_0067: Expected O, but got I4
		//IL_009e: Expected O, but got I
		//IL_007e: Expected O, but got I4
		Mesh[] quadMesh = ShapesMeshUtils.QuadMesh;
		nint num = (nint)this;
		bool hasDetailLevels = HasDetailLevels;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb edx,edx\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Shapes.ShapeRenderer>)+270]");
		object obj = (nint)0 & (nint)2;
		if ((nint)obj < quadMesh.Length)
		{
			object obj2 = 48;
			if (!hasDetailLevels)
			{
				obj2 = 32;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v7+v7 @ rax_v1 (UnityEngine.Mesh[])]");
			return (Mesh)0;
		}
		return (Mesh)(object)new IndexOutOfRangeException();
	}

	internal virtual void CamOnPreCull()
	{
	}

	private unsafe void UpdateBounds()
	{
		//IL_00b2: Expected O, but got Ref
		//IL_00d4: Expected O, but got Ref
		Bounds unpaddedLocalBounds_Internal = GetUnpaddedLocalBounds_Internal();
		Bounds bounds = default(Bounds);
		bounds.Expand(boundsPadding);
		MeshUpdateMode meshUpdateMode = MeshUpdateMode;
		object obj = default(object);
		if (meshUpdateMode == MeshUpdateMode.UseAssetCopy || meshUpdateMode == MeshUpdateMode.SelfGenerated)
		{
			Mesh sharedMesh = mf.sharedMesh;
			if (!(sharedMesh == null))
			{
				Mesh sharedMesh2 = mf.sharedMesh;
				sharedMesh2.bounds = (Bounds)(&obj);
				goto IL_0096;
			}
		}
		if (culling != ShapeCulling.CalculatedLocal)
		{
			if (culling == ShapeCulling.SimpleGlobal)
			{
				goto IL_0096;
			}
			return;
		}
		rnd.localBounds = (Bounds)(&obj);
		return;
		IL_0096:
		rnd.ResetLocalBounds();
	}

	private void TryDestroyInstancedMaterials(bool inOnDestroy = false)
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_0055: Expected O, but got I
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00b2: Expected O, but got I
		//IL_009b: Expected O, but got I
		if (instancedMaterials == null)
		{
			return;
		}
		Material[] array = instancedMaterials;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			Material[] array2 = instancedMaterials;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rsi_v3+v167 @ rax_v5 (UnityEngine.Material[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Material[] array3 = instancedMaterials;
				if (!inOnDestroy)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rsi_v3+v197 @ rax_v14 (UnityEngine.Material[])]");
					ShapesExtensions.DestroyBranched((UnityEngine.Object)0);
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rsi_v3+v197 @ rax_v14 (UnityEngine.Material[])]");
					ShapesExtensions.TryDestroyInOnDestroy(this, (UnityEngine.Object)0);
				}
			}
			array = instancedMaterials;
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}

	private unsafe void MakeSureMaterialInstancesAreGood(Material[] sourceMats)
	{
		//IL_0075: Expected O, but got I4
		//IL_00cb: Expected O, but got I
		//IL_00fd: Expected I, but got O
		//IL_0112: Expected O, but got I
		//IL_0462: Expected O, but got I4
		//IL_0472: Expected O, but got I4
		//IL_013c: Expected I, but got O
		//IL_0151: Expected O, but got I
		//IL_049d: Expected O, but got I4
		//IL_0170: Expected O, but got I
		//IL_0188: Expected I, but got O
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Expected O, but got Unknown
		//IL_04c3: Expected I, but got O
		//IL_04d3: Expected O, but got I
		//IL_04f5: Expected O, but got I4
		//IL_050e: Expected O, but got I4
		//IL_01bf: Expected I, but got O
		//IL_01d4: Expected O, but got I
		//IL_01f3: Expected O, but got I
		//IL_032e: Expected I, but got O
		//IL_0239: Expected I, but got O
		//IL_0355: Expected O, but got I
		//IL_0390: Expected O, but got I4
		//IL_03a0: Expected O, but got I4
		//IL_0263: Expected I, but got O
		//IL_03cb: Expected O, but got I4
		//IL_0289: Expected O, but got I
		//IL_02a5: Expected I, but got O
		//IL_03f1: Expected I, but got O
		//IL_02e1: Expected I, but got O
		//IL_0421: Expected O, but got I4
		//IL_030b: Expected I, but got O
		_003C_003Ec__DisplayClass139_0 obj2 = default(_003C_003Ec__DisplayClass139_0);
		if (instancedMaterials != null)
		{
			UnityEngine.Object obj = (UnityEngine.Object)(object)instancedMaterials;
			bool flag = (object)obj2 == null;
			_003C_003Ec__DisplayClass139_0 obj3 = obj2;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rdx_v12 (UnityEngine.Object)+18]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ stack_-18_v2 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)+18]");
				if (num != 0)
				{
					TryDestroyInstancedMaterials();
					_003CMakeSureMaterialInstancesAreGood_003Eg__PopulateAll_007C139_1(ref obj2);
					return;
				}
				int num2 = 0;
				object obj4 = 32;
				int num3 = 0;
				obj3 = obj2;
				UnityEngine.Object obj7 = default(UnityEngine.Object);
				object obj8 = default(object);
				_003C_003Ec__DisplayClass139_0 obj9 = default(_003C_003Ec__DisplayClass139_0);
				object obj10 = default(object);
				object obj11 = default(object);
				object obj6 = default(object);
				while (true)
				{
					int num4 = num3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v426 @ rcx_v6 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)+18]");
					if ((nint)num4 >= (nint)0)
					{
						return;
					}
					Material[] array = instancedMaterials;
					if (instancedMaterials == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v124 @ rax_v18 (UnityEngine.Material[])]");
					bool flag2 = (UnityEngine.Object)0 != null;
					Material[] array2 = instancedMaterials;
					object obj5;
					nint num6;
					Material material2;
					nint num5;
					if (flag2)
					{
						bool flag3 = instancedMaterials == null;
						num5 = unchecked((nint)null);
						obj = null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v124 @ rax_v18 (UnityEngine.Material[])]");
						obj3 = (_003C_003Ec__DisplayClass139_0)0;
						if (flag3)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v492 @ rdi_v12 (UnityEngine.Material[])]");
						bool flag4 = (nint)0 == 0;
						num5 = unchecked((nint)null);
						obj = null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v492 @ rdi_v12 (UnityEngine.Material[])]");
						obj3 = (_003C_003Ec__DisplayClass139_0)0;
						if (flag4)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v492 @ rdi_v12 (UnityEngine.Material[])]");
						Shader shader = ((Material)0).shader;
						bool flag5 = (object)obj2 == null;
						num5 = unchecked((nint)null);
						obj = null;
						obj3 = obj2;
						if (flag5)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v37 @ stack_-18_v2 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
						bool flag6 = (nint)0 == 0;
						num5 = unchecked((nint)null);
						obj = null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v37 @ stack_-18_v2 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
						obj3 = (_003C_003Ec__DisplayClass139_0)0;
						if (flag6)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v37 @ stack_-18_v2 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
						Shader shader2 = ((Material)0).shader;
						bool flag7 = shader != shader2;
						obj3 = (_003C_003Ec__DisplayClass139_0)instancedMaterials;
						if (!flag7)
						{
							bool flag8 = (object)obj3 == null;
							num5 = unchecked((nint)null);
							obj = shader2;
							if (flag8)
							{
								break;
							}
							bool flag9 = (object)obj2 == null;
							num5 = unchecked((nint)null);
							obj = shader2;
							if (flag9)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v37 @ stack_-18_v2 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
							obj3 = (_003C_003Ec__DisplayClass139_0)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v37 @ stack_-18_v2 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
							bool flag10 = (nint)0 == 0;
							num5 = unchecked((nint)null);
							obj = shader2;
							if (flag10)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D8A9B0");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v426 @ rcx_v6 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
							bool flag11 = (nint)0 == 0;
							num5 = unchecked((nint)null);
							obj = null;
							if (flag11)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D8CC90");
							obj5 = obj6;
							num6 = unchecked((nint)null);
							obj = obj7;
							goto IL_053e;
						}
						bool flag12 = instancedMaterials == null;
						num5 = unchecked((nint)null);
						obj = shader2;
						if (flag12)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rsi_v7+v426 @ rcx_v6 (Shapes.ShapeRenderer+<>c__DisplayClass139_0)]");
						ShapesExtensions.DestroyBranched((UnityEngine.Object)0);
						array2 = instancedMaterials;
						Material material = _003CMakeSureMaterialInstancesAreGood_003Eg__InstantiateMaterial_007C139_0(num2, ref obj2);
						bool flag13 = instancedMaterials == null;
						obj6 = 0;
						num5 = (nint)(&obj2);
						obj = (UnityEngine.Object)num2;
						obj3 = (_003C_003Ec__DisplayClass139_0)this;
						if (flag13)
						{
							break;
						}
						bool flag14 = (object)material == null;
						obj5 = 0;
						material2 = material;
						num6 = (nint)(&obj2);
						if (!flag14)
						{
							nint num7 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							if (obj8 == null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj9;
							}
							obj5 = 0;
							material2 = material;
							num6 = (nint)(&obj2);
						}
					}
					else
					{
						Material material3 = _003CMakeSureMaterialInstancesAreGood_003Eg__InstantiateMaterial_007C139_0(num2, ref obj2);
						bool flag15 = instancedMaterials == null;
						obj6 = 0;
						num5 = (nint)(&obj2);
						obj = (UnityEngine.Object)num2;
						obj3 = (_003C_003Ec__DisplayClass139_0)this;
						if (flag15)
						{
							break;
						}
						bool flag16 = (object)material3 == null;
						obj5 = 0;
						material2 = material3;
						num6 = (nint)(&obj2);
						if (!flag16)
						{
							nint num8 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v575 @ rdx_v18 (Il2CppClass<UnityEngine.Material[]>)+40]");
							obj = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag17 = obj10 == null;
							obj5 = 0;
							material2 = material3;
							num6 = (nint)(&obj2);
							obj6 = 0;
							num5 = (nint)(&obj2);
							obj3 = (_003C_003Ec__DisplayClass139_0)material3;
							if (flag17)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj11;
							}
						}
					}
					obj = material2;
					goto IL_053e;
					IL_053e:
					num2++;
					obj4 += 8;
					bool flag18 = (object)obj2 == null;
					obj6 = obj5;
					num5 = num6;
					obj3 = obj2;
					if (flag18)
					{
						break;
					}
					obj6 = obj5;
					num5 = num6;
					num3 = num2;
					obj3 = obj2;
				}
			}
			throw new NullReferenceException();
		}
		_003CMakeSureMaterialInstancesAreGood_003Eg__PopulateAll_007C139_1(ref obj2);
	}

	private protected void UpdateMaterial()
	{
		//IL_0062: Expected I, but got O
		//IL_0072: Expected O, but got I
		//IL_0281: Expected O, but got I4
		//IL_00bf: Invalid comparison between F4 and I4
		//IL_00d1: Expected O, but got I4
		//IL_029d: Expected O, but got I4
		//IL_04df: Expected O, but got I4
		//IL_00fc: Expected O, but got I4
		//IL_02da: Expected O, but got I4
		//IL_0127: Expected O, but got I4
		//IL_0302: Expected O, but got I4
		//IL_0152: Expected O, but got I4
		//IL_017d: Expected O, but got I4
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Expected O, but got Unknown
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Expected O, but got Unknown
		//IL_03da: Expected O, but got I4
		//IL_03ea: Expected O, but got I
		//IL_033d: Expected I, but got O
		//IL_034d: Expected O, but got I
		//IL_036f: Expected O, but got I4
		//IL_037f: Expected O, but got I
		//IL_01a8: Expected O, but got I4
		//IL_0410: Expected O, but got I
		//IL_01d3: Expected O, but got I4
		//IL_01fe: Expected O, but got I4
		//IL_0226: Expected O, but got I4
		//IL_022f: Expected O, but got I4
		if (mats != null)
		{
			Material[] array = mats;
			int materialCount = MaterialCount;
			if (array.Length == materialCount)
			{
				goto IL_005d;
			}
		}
		int materialCount2 = MaterialCount;
		Material[] array2 = new Material[materialCount2];
		mats = array2;
		goto IL_005d;
		IL_041d:
		VerifyComponents();
		MeshRenderer meshRenderer = rnd;
		bool flag = (object)rnd == null;
		object obj2;
		object obj = obj2;
		float num2;
		float num = num2;
		Material[] array3 = null;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
			return;
		}
		goto IL_046c;
		IL_046c:
		throw new NullReferenceException();
		IL_005d:
		nint num3 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ r8_v1 (Il2CppClass<Shapes.ShapeRenderer>)+210]");
		object obj3 = 0;
		GetMaterials(mats);
		if (zTest == CompareFunction.LessEqual)
		{
			num = zOffsetFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018105126Dh\"");
			bool flag2 = zOffsetFactor != 0f;
			obj = 0;
			if (!flag2)
			{
				bool flag3 = zOffsetUnits != 0;
				obj = 0;
				if (!flag3)
				{
					bool flag4 = stencilComp != CompareFunction.Always;
					obj = 0;
					if (!flag4)
					{
						bool flag5 = stencilOpPass != StencilOp.Keep;
						obj = 0;
						if (!flag5)
						{
							bool flag6 = stencilRefID != 0;
							obj = 0;
							if (!flag6)
							{
								bool flag7 = stencilReadMask != 255;
								obj = 0;
								if (!flag7)
								{
									bool flag8 = stencilWriteMask != 255;
									obj = 0;
									if (!flag8)
									{
										bool flag9 = colorMask != ColorWriteMask.All;
										obj = 0;
										if (!flag9)
										{
											bool flag10 = renderQueue == -1;
											obj = 0;
											obj2 = 0;
											num2 = zOffsetFactor;
											if (flag10)
											{
												goto IL_041d;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		array3 = mats;
		MakeSureMaterialInstancesAreGood(mats);
		Material[] array4 = mats;
		bool flag11 = mats == null;
		obj3 = 0;
		meshRenderer = null;
		if (!flag11)
		{
			object obj4 = 32;
			MeshRenderer meshRenderer2 = null;
			meshRenderer = null;
			object obj5 = default(object);
			object obj6 = default(object);
			while (true)
			{
				bool flag12 = (nint)meshRenderer >= array4.Length;
				obj2 = obj;
				num2 = num;
				obj3 = 0;
				if (flag12)
				{
					break;
				}
				Material[] array5 = instancedMaterials;
				Material[] array6 = mats;
				bool flag13 = instancedMaterials == null;
				obj3 = 0;
				if (!flag13)
				{
					bool flag14 = mats == null;
					obj3 = 0;
					if (!flag14)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ r14_v7+v273 @ rax_v18 (UnityEngine.Material[])]");
						if ((nint)0 != 0)
						{
							nint num4 = (nint)array6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v474 @ rdx_v14 (Il2CppClass<UnityEngine.Material[]>)+40]");
							array3 = (Material[])0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag15 = obj5 == null;
							obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ r14_v7+v273 @ rax_v18 (UnityEngine.Material[])]");
							meshRenderer = (MeshRenderer)0;
							if (flag15)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj6;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ r14_v7+v273 @ rax_v18 (UnityEngine.Material[])]");
						_ = 0;
						array4 = mats;
						meshRenderer2 = (MeshRenderer)(meshRenderer2 + 1);
						obj4 += 8;
						bool flag16 = mats == null;
						obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ r14_v7+v273 @ rax_v18 (UnityEngine.Material[])]");
						array3 = (Material[])0;
						meshRenderer = meshRenderer2;
						if (!flag16)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ r14_v7+v273 @ rax_v18 (UnityEngine.Material[])]");
							array3 = (Material[])0;
							meshRenderer = meshRenderer2;
							continue;
						}
					}
				}
				goto IL_046c;
			}
			goto IL_041d;
		}
		goto IL_046c;
	}

	public void UpdateMesh(bool force = false)
	{
		MeshUpdateMode meshUpdateMode = MeshUpdateMode;
		if (meshUpdateMode == MeshUpdateMode.UseAsset)
		{
			Mesh sharedMesh = mf.sharedMesh;
			if (sharedMesh != null)
			{
				Mesh sharedMesh2 = mf.sharedMesh;
				Mesh initialMeshAsset = GetInitialMeshAsset();
				if (!(sharedMesh2 != initialMeshAsset))
				{
					goto IL_00a1;
				}
			}
			Mesh initialMeshAsset2 = GetInitialMeshAsset();
			mf.sharedMesh = initialMeshAsset2;
			return;
		}
		goto IL_00a1;
		IL_00a1:
		GameObject gameObject = base.gameObject;
		int instanceID = gameObject.GetInstanceID();
		Mesh sharedMesh3 = mf.sharedMesh;
		if (sharedMesh3 != null && meshOwnerID == instanceID)
		{
			if (force && meshUpdateMode == MeshUpdateMode.SelfGenerated)
			{
				GenerateMesh();
				UpdateBounds();
				return;
			}
		}
		else
		{
			meshOwnerID = instanceID;
			switch (meshUpdateMode)
			{
			case MeshUpdateMode.SelfGenerated:
			{
				Mesh mesh = new Mesh();
				mesh.hideFlags = HideFlags.HideAndDontSave;
				mf.sharedMesh = mesh;
				Mesh sharedMesh7 = mf.sharedMesh;
				sharedMesh7.MarkDynamic();
				GenerateMesh();
				UpdateBounds();
				return;
			}
			case MeshUpdateMode.UseAssetCopy:
			{
				Mesh initialMeshAsset3 = GetInitialMeshAsset();
				Mesh sharedMesh4 = UnityEngine.Object.Instantiate(initialMeshAsset3);
				mf.sharedMesh = sharedMesh4;
				Mesh sharedMesh5 = mf.sharedMesh;
				sharedMesh5.hideFlags = HideFlags.HideAndDontSave;
				Mesh sharedMesh6 = mf.sharedMesh;
				sharedMesh6.MarkDynamic();
				break;
			}
			}
		}
		UpdateBounds();
	}

	public unsafe Bounds GetBounds()
	{
		//IL_000e: Expected O, but got I4
		//IL_0009: Expected native int or pointer, but got O
		//IL_002b: Expected native int or pointer, but got O
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
		_ = 0;
		((Bounds*)(nint)bounds)->m_Center = GetUnpaddedLocalBounds_Internal().m_Center;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rax_v3 (UnityEngine.Bounds)+10]");
		_ = 0;
		((Bounds*)bounds)->Expand(boundsPadding);
		return bounds;
	}

	public unsafe Bounds GetWorldBounds()
	{
		//IL_026d: Expected I, but got O
		//IL_02c4: Expected I, but got O
		//IL_03f2: Expected O, but got I8
		//IL_038d: Expected O, but got I8
		//IL_035c: Expected O, but got I8
		//IL_006c: Expected O, but got Ref
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_01f3: Expected O, but got F4
		//IL_01ee: Expected native int or pointer, but got O
		//IL_022a: Expected O, but got F4
		//IL_0225: Expected native int or pointer, but got O
		Bounds unpaddedLocalBounds_Internal = GetUnpaddedLocalBounds_Internal();
		Bounds bounds = default(Bounds);
		bounds.Expand(boundsPadding);
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdx_v1 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		object obj = default(object);
		float num3 = (float)obj * 3.4028235E+38f;
		float num4 = (float)Vector3.oneVector * 3.4028235E+38f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		float num5 = 0f * 3.4028235E+38f;
		nint num6 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdx_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num7 = 0;
		float num8 = (float)obj * -3.4028235E+38f;
		float num9 = (float)Vector3.oneVector * -3.4028235E+38f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		float num10 = 0f * -3.4028235E+38f;
		Transform transform = base.transform;
		object obj2 = 4294967295L;
		object obj4 = default(object);
		object obj8 = default(object);
		float num11 = default(float);
		float num12;
		float num13 = default(float);
		float num14;
		bool flag5;
		do
		{
			object obj3 = obj2 * obj4;
			object obj5 = obj3 + (object)unpaddedLocalBounds_Internal.m_Center;
			object obj6 = 4294967295L;
			bool flag4;
			do
			{
				object obj7 = 4294967295L;
				bool flag3;
				do
				{
					if ((object)transform != null)
					{
						Vector3 vector = transform.TransformPoint((Vector3)(&obj8));
						if (!(vector.x > num4))
						{
							num4 = vector.x;
						}
						bool flag = !(num11 > num3);
						num12 = num11;
						if (!flag)
						{
							num12 = num3;
						}
						if (!(vector.z > num5))
						{
							num5 = vector.z;
						}
						if (!(num9 > vector.x))
						{
							num9 = vector.x;
						}
						bool flag2 = !(num8 > num13);
						num14 = num13;
						if (!flag2)
						{
							num14 = num8;
						}
						if (!(num10 > vector.z))
						{
							num10 = vector.z;
						}
						obj7 += 2;
						flag3 = (nint)obj7 <= 1;
						num8 = num14;
						num3 = num12;
						obj8 = obj5;
						continue;
					}
					return (Bounds)new NullReferenceException();
				}
				while (flag3);
				obj6 += 2;
				flag4 = (nint)obj6 <= 1;
				num8 = num14;
				num3 = num12;
				obj8 = obj5;
			}
			while (flag4);
			obj2 += 2;
			flag5 = (nint)obj2 <= 1;
			num8 = num14;
			num3 = num12;
			obj8 = obj5;
		}
		while (flag5);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181061150");
		float num15 = num9 + num4;
		float num16 = num14 + num12;
		float num17 = num10 + num5;
		object obj9 = default(object);
		float num18 = (float)obj9 * 0.5f;
		float num19 = num15 * 0.5f;
		float num20 = num16 * 0.5f;
		Bounds bounds2 = default(Bounds);
		((Bounds*)(nint)bounds2)->m_Center = (Vector3)num19;
		float num21 = num17 * 0.5f;
		float num22 = (float)obj * 0.5f;
		((Bounds*)(nint)bounds2)->m_Extents = (Vector3)num18;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v504 @ rax_v16+8]");
		float num23 = 0f * 0.5f;
		return bounds2;
	}

	private void OnDidApplyAnimationProperties()
	{
		UpdateAllMaterialProperties();
	}

	private void SetIntOnAllInstancedMaterials(int property, int value)
	{
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0189: Expected O, but got I4
		//IL_0192: Expected O, but got I4
		//IL_0037: Invalid comparison between F4 and I4
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		if (zTest == CompareFunction.LessEqual)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181050166h\"");
			if (zOffsetFactor == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All && renderQueue == -1)
			{
				return;
			}
		}
		UpdateMaterial();
		Material[] array = instancedMaterials;
		object obj = instancedMaterials + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < array.Length)
		{
			((Material)obj).SetInt(property, value);
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	private void SetFloatOnAllInstancedMaterials(int property, float value)
	{
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0189: Expected O, but got I4
		//IL_0192: Expected O, but got I4
		//IL_0037: Invalid comparison between F4 and I4
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		if (zTest == CompareFunction.LessEqual)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018104FF24h\"");
			if (zOffsetFactor == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All && renderQueue == -1)
			{
				return;
			}
		}
		UpdateMaterial();
		Material[] array = instancedMaterials;
		object obj = instancedMaterials + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < array.Length)
		{
			((Material)obj).SetFloat(property, value);
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	internal unsafe void UpdateAllMaterialProperties()
	{
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01c6: Expected O, but got I4
		//IL_01cf: Expected O, but got I4
		//IL_007a: Invalid comparison between F4 and I4
		//IL_0378: Expected O, but got Ref
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		GameObject gameObject = base.gameObject;
		Scene scene = gameObject.scene;
		Scene scene2 = default(Scene);
		if (!scene2.IsValid())
		{
			return;
		}
		UpdateMaterial();
		if (zTest == CompareFunction.LessEqual)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181050CF9h\"");
			if (zOffsetFactor == 0f && zOffsetUnits == 0 && stencilComp == CompareFunction.Always && stencilOpPass == StencilOp.Keep && stencilRefID == 0 && stencilReadMask == 255 && stencilWriteMask == 255 && colorMask == ColorWriteMask.All && renderQueue == -1)
			{
				goto IL_0369;
			}
		}
		Material[] array = instancedMaterials;
		object obj = instancedMaterials + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			((Material)obj).SetInt(ShapesMaterialUtils.propZTest, (int)zTest);
			((Material)obj).SetFloat(ShapesMaterialUtils.propZOffsetFactor, zOffsetFactor);
			((Material)obj).SetInt(ShapesMaterialUtils.propZOffsetUnits, zOffsetUnits);
			((Material)obj).SetInt(ShapesMaterialUtils.propColorMask, (int)colorMask);
			((Material)obj).SetInt(ShapesMaterialUtils.propStencilComp, (int)stencilComp);
			((Material)obj).SetInt(ShapesMaterialUtils.propStencilOpPass, (int)stencilOpPass);
			((Material)obj).SetInt(ShapesMaterialUtils.propStencilID, (int)stencilRefID);
			((Material)obj).SetInt(ShapesMaterialUtils.propStencilReadMask, (int)stencilReadMask);
			((Material)obj).SetInt(ShapesMaterialUtils.propStencilWriteMask, (int)stencilWriteMask);
			((Material)obj).renderQueue = renderQueue;
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		goto IL_0369;
		IL_0369:
		object obj4 = default(object);
		SetColor(ShapesMaterialUtils.propColor, (Color)(&obj4));
		if (HasScaleModes)
		{
			MaterialPropertyBlock materialPropertyBlock = mpb;
			if (mpb == null)
			{
				materialPropertyBlock = (mpb = new MaterialPropertyBlock());
			}
			materialPropertyBlock.SetInt(ShapesMaterialUtils.propScaleMode, (int)scaleMode);
		}
		SetAllMaterialProperties();
		ApplyProperties();
	}

	private protected void ApplyProperties()
	{
		VerifyComponents();
		MaterialPropertyBlock properties = mpb;
		if (mpb == null)
		{
			properties = (mpb = new MaterialPropertyBlock());
		}
		((Renderer)rnd).Internal_SetPropertyBlock(properties);
		UpdateBounds();
	}

	private protected unsafe void SetAllDashValues(DashStyle style, bool dashed, bool matchSpacingToSize, float thickness, bool setType, bool now)
	{
		//IL_00fa: Expected O, but got Ref
		float thickness2 = default(float);
		float netAbsoluteSize = ((DashStyle*)style)->GetNetAbsoluteSize(dashed, thickness2);
		if (dashed)
		{
			object obj = default(object);
			float thickness3 = default(float);
			float netDashSpacing = GetNetDashSpacing((DashStyle)(&obj), dashed: true, matchSpacingToSize, thickness3);
			SetFloat(ShapesMaterialUtils.propDashSpacing, netDashSpacing);
			SetFloat(ShapesMaterialUtils.propDashOffset, style.offset);
			int value = (int)style.type >> 32;
			SetInt(ShapesMaterialUtils.propDashSpace, value);
			SetInt(ShapesMaterialUtils.propDashSnap, (int)style.snap);
			object obj2 = default(object);
			if (obj2 != null)
			{
				SetInt(ShapesMaterialUtils.propDashType, (int)style.type);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181056A80");
				object obj3 = default(object);
				if (obj3 != null)
				{
					SetFloat(ShapesMaterialUtils.propDashShapeModifier, style.shapeModifier);
				}
			}
		}
		object obj4 = default(object);
		if (obj4 == null)
		{
			SetFloat(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
		else
		{
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	private protected unsafe float GetNetDashSpacing(DashStyle style, bool dashed, bool matchSpacingToSize, float thickness)
	{
		//IL_0018: Expected O, but got I4
		float thickness2 = default(float);
		if (matchSpacingToSize)
		{
			object obj = (int)style.type >> 32;
			if ((nint)obj != -2)
			{
				return ((DashStyle*)style)->GetNetAbsoluteSize(dashed, thickness2);
			}
			return 0.5f;
		}
		return ((DashStyle*)style)->GetNetAbsoluteSpacing(dashed, thickness2);
	}

	private protected unsafe void SetColor(int prop, Color value)
	{
		//IL_0276: Expected O, but got Ref
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected O, but got Unknown
		//IL_0076: Expected O, but got Ref
		//IL_0306: Expected I, but got O
		//IL_00d1: Expected I, but got O
		//IL_015c: Expected O, but got I4
		//IL_0109: Expected O, but got I
		//IL_0112: Expected O, but got I4
		//IL_018f: Expected native int or pointer, but got O
		//IL_01a2: Expected I, but got O
		//IL_01ca: Expected O, but got I
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		if (ShapeGroup.shapeGroupsInScene > 0)
		{
			ShapeGroup[] componentsInParent = GetComponentsInParent<ShapeGroup>();
			if (componentsInParent != null)
			{
				Func<ShapeGroup, bool> predicate = _003C_003Ec._003C_003E9__152_0;
				if (_003C_003Ec._003C_003E9__152_0 == null)
				{
					Func<ShapeGroup, bool> func = (_003C_003Ec._003C_003E9__152_0 = delegate(ShapeGroup g)
					{
						//IL_0035: Expected I4, but got O
						if ((object)g == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						return g._003CIsEnabled_003Ek__BackingField;
					});
					nint num = unchecked((nint)null);
					predicate = func;
				}
				IEnumerable<ShapeGroup> enumerable = Enumerable.Where(componentsInParent, predicate);
				if (enumerable == null)
				{
					goto IL_0277;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IEnumerable<ShapeGroup> enumerable2 = default(IEnumerable<ShapeGroup>);
				object obj = (object)(&enumerable2);
				IEnumerable<ShapeGroup> enumerable3 = null;
				object obj2 = default(object);
				object obj11 = default(object);
				float num4 = default(float);
				object obj12 = default(object);
				while (true)
				{
					object obj3;
					object obj10;
					if (enumerable2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj2 == null)
						{
							break;
						}
						bool flag = enumerable2 == null;
						enumerable3 = null;
						if (!flag)
						{
							nint num2 = (nint)enumerable2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v462 @ r10_v7 (Il2CppClass<System.Collections.Generic.IEnumerable`1<Shapes.ShapeGroup>>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0149;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v462 @ r10_v7 (Il2CppClass<System.Collections.Generic.IEnumerable`1<Shapes.ShapeGroup>>)+B0]");
							obj3 = 0;
							object obj4 = 0;
							while (true)
							{
								object obj5 = obj4 + obj4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v468 @ r8_v14+v619 @ rax_v37*8]");
								if (0 == (nint)typeof(IEnumerator<ShapeGroup>))
								{
									break;
								}
								obj4++;
								object obj6 = obj4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v462 @ r10_v7 (Il2CppClass<System.Collections.Generic.IEnumerable`1<Shapes.ShapeGroup>>)+12E]");
								if ((nint)obj6 < 0)
								{
									continue;
								}
								goto IL_0149;
							}
							object obj7 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v468 @ r8_v14+8+v675 @ rcx_v32*8]");
							object obj8 = (nint)0 << 4;
							object obj9 = obj8 + 312;
							obj10 = obj9 + num2;
							goto IL_037d;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
					IL_037d:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v680 @ rdx_v19] (should have been resolved before IL gen)");
					if (obj11 != null)
					{
						float num3 = num4 * num4;
						float num5 = num4 * num4;
						((Color*)(nint)value)->r = num4;
						nint num = (nint)typeof(IEnumerator<ShapeGroup>);
						continue;
					}
					throw new NullReferenceException();
					IL_0149:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj3 = 0;
					obj10 = obj12;
					goto IL_037d;
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			MaterialPropertyBlock materialPropertyBlock2 = (mpb = new MaterialPropertyBlock());
			IEnumerable<ShapeGroup> enumerable3 = (IEnumerable<ShapeGroup>)(this + 64);
			bool flag2 = materialPropertyBlock2 == null;
			materialPropertyBlock = materialPropertyBlock2;
			if (flag2)
			{
				goto IL_0277;
			}
		}
		float num6 = default(float);
		materialPropertyBlock.SetColor(prop, (Color)(&num6));
		return;
		IL_0277:
		throw new NullReferenceException();
	}

	private protected void SetFloat(int prop, float value)
	{
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
		}
		materialPropertyBlock.SetFloatImpl(prop, value);
	}

	private protected void SetInt(int prop, int value)
	{
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
		}
		materialPropertyBlock.SetInt(prop, value);
	}

	private protected unsafe void SetVector3(int prop, Vector3 value)
	{
		//IL_005b: Expected O, but got Ref
		MaterialPropertyBlock materialPropertyBlock = mpb;
		float x;
		if (mpb != null)
		{
			x = value.x;
		}
		else
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
			x = value.x;
		}
		materialPropertyBlock.SetVector(prop, (Vector4)(&x));
	}

	private protected unsafe void SetVector4(int prop, Vector4 value)
	{
		//IL_003c: Expected O, but got Ref
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
		}
		object obj = default(object);
		materialPropertyBlock.SetVector(prop, (Vector4)(&obj));
	}

	private protected unsafe void SetColorNow(int prop, Color value)
	{
		//IL_000e: Expected O, but got Ref
		object obj = default(object);
		SetColor(prop, (Color)(&obj));
		ApplyProperties();
	}

	private protected void SetFloatNow(int prop, float value)
	{
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
		}
		materialPropertyBlock.SetFloatImpl(prop, value);
		ApplyProperties();
	}

	private protected void SetIntNow(int prop, int value)
	{
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
		}
		materialPropertyBlock.SetInt(prop, value);
		ApplyProperties();
	}

	private protected unsafe void SetVector3Now(int prop, Vector3 value)
	{
		//IL_005b: Expected O, but got Ref
		MaterialPropertyBlock materialPropertyBlock = mpb;
		float x;
		if (mpb != null)
		{
			x = value.x;
		}
		else
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
			x = value.x;
		}
		materialPropertyBlock.SetVector(prop, (Vector4)(&x));
		ApplyProperties();
	}

	private protected unsafe void SetVector4Now(int prop, Vector4 value)
	{
		//IL_003c: Expected O, but got Ref
		MaterialPropertyBlock materialPropertyBlock = mpb;
		if (mpb == null)
		{
			materialPropertyBlock = (mpb = new MaterialPropertyBlock());
		}
		object obj = default(object);
		materialPropertyBlock.SetVector(prop, (Vector4)(&obj));
		ApplyProperties();
	}

	protected ShapeRenderer()
	{
		//IL_0012: Expected O, but got I
		//IL_0042: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		meshOutOfDate = true;
		blendMode = ShapesBlendMode.Transparent;
		detailLevel = DetailLevel.Medium;
		renderQueue = -1;
		zTest = CompareFunction.LessEqual;
		colorMask = ColorWriteMask.All;
		stencilComp = CompareFunction.Always;
		stencilReadMask = 255;
		shouldUpdateMaterialPropertiesInEditor = true;
		base._002Ector();
	}

	private Material _003CMakeSureMaterialInstancesAreGood_003Eg__InstantiateMaterial_007C139_0(int index, ref _003C_003Ec__DisplayClass139_0 P_1)
	{
		//IL_003d: Expected O, but got I
		//IL_0091: Expected O, but got I
		object obj = P_1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rbx_v1+18]");
		if ((nint)index < (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rbx_v1+20+index @ rdx (System.Int32)*8]");
			Material material = new Material((Material)0);
			object obj2 = P_1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v8+18]");
			if ((nint)index < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v8+20+index @ rdx (System.Int32)*8]");
				string text = ((UnityEngine.Object)0).name;
				string text2 = text + " (instance)";
				material.name = text2;
				return material;
			}
		}
		return (Material)(object)new IndexOutOfRangeException();
	}

	private unsafe void _003CMakeSureMaterialInstancesAreGood_003Eg__PopulateAll_007C139_1(ref _003C_003Ec__DisplayClass139_0 P_0)
	{
		//IL_01e3: Expected I4, but got O
		//IL_0052: Expected I4, but got O
		//IL_006e: Expected O, but got I4
		//IL_0095: Expected I4, but got O
		//IL_00bb: Expected O, but got I4
		//IL_00c3: Expected I4, but got O
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_019e: Expected O, but got I4
		//IL_01a6: Expected I4, but got O
		//IL_00fb: Expected I, but got O
		//IL_0135: Expected O, but got I4
		int num = (int)P_0;
		bool flag = (object)P_0 == null;
		ShapeRenderer shapeRenderer = this;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rdx_v3 (System.Int32)+18]");
			Material[] array = (instancedMaterials = new Material[0]);
			object obj = P_0;
			bool flag2 = (object)P_0 == null;
			num = (int)array;
			shapeRenderer = null;
			if (!flag2)
			{
				object obj2 = 32;
				ShapeRenderer shapeRenderer2 = null;
				ShapeRenderer shapeRenderer3 = null;
				object obj4 = default(object);
				object obj5 = default(object);
				while (true)
				{
					ShapeRenderer shapeRenderer4 = shapeRenderer3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v12+18]");
					if ((nint)shapeRenderer4 >= 0)
					{
						return;
					}
					Material[] array2 = instancedMaterials;
					Material material = _003CMakeSureMaterialInstancesAreGood_003Eg__InstantiateMaterial_007C139_0((int)shapeRenderer2, ref P_0);
					bool flag3 = instancedMaterials == null;
					nint num2 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref P_0);
					object obj3 = 0;
					num = (int)shapeRenderer2;
					shapeRenderer = this;
					if (flag3)
					{
						break;
					}
					if ((object)material != null)
					{
						nint num3 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rdx_v12 (Il2CppClass<UnityEngine.Material[]>)+40]");
						num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						bool flag4 = obj4 == null;
						num2 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref P_0);
						obj3 = 0;
						shapeRenderer = (ShapeRenderer)(object)material;
						if (flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							throw obj5;
						}
					}
					obj = P_0;
					shapeRenderer2 = (ShapeRenderer)(shapeRenderer2 + 1);
					obj2 += 8;
					bool flag5 = (object)P_0 == null;
					num2 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref P_0);
					obj3 = 0;
					num = (int)material;
					shapeRenderer = shapeRenderer2;
					if (flag5)
					{
						break;
					}
					shapeRenderer3 = shapeRenderer2;
				}
			}
		}
		throw new NullReferenceException();
	}
}
