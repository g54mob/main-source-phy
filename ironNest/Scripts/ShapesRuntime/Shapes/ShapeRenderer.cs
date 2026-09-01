using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes
{
	[DisallowMultipleComponent]
	public abstract class ShapeRenderer : MonoBehaviour
	{
		private bool initializedComponents;

		private MeshRenderer rnd;

		private MeshFilter mf;

		private int meshOwnerID;

		private MaterialPropertyBlock mpb;

		private Material[] instancedMaterials;

		[NonSerialized]
		public bool meshOutOfDate;

		[SerializeField]
		private ShapesBlendMode blendMode;

		[SerializeField]
		private ScaleMode scaleMode;

		[SerializeField]
		[ShapesColorField(true)]
		private protected Color color;

		[SerializeField]
		private protected DetailLevel detailLevel;

		[SerializeField]
		private protected ShapeCulling culling;

		[SerializeField]
		private protected float boundsPadding;

		[SerializeField]
		private int renderQueue;

		public const int DEFAULT_RENDER_QUEUE_AUTO = -1;

		public const CompareFunction DEFAULT_ZTEST = CompareFunction.LessEqual;

		public const float DEFAULT_ZOFS_FACTOR = 0f;

		public const int DEFAULT_ZOFS_UNITS = 0;

		public const ColorWriteMask DEFAULT_COLOR_MASK = ColorWriteMask.All;

		[SerializeField]
		private CompareFunction zTest;

		[SerializeField]
		private float zOffsetFactor;

		[SerializeField]
		private int zOffsetUnits;

		[SerializeField]
		private ColorWriteMask colorMask;

		public const CompareFunction DEFAULT_STENCIL_COMP = CompareFunction.Always;

		public const StencilOp DEFAULT_STENCIL_OP = StencilOp.Keep;

		public const byte DEFAULT_STENCIL_REF_ID = 0;

		public const byte DEFAULT_STENCIL_MASK = 255;

		[SerializeField]
		private CompareFunction stencilComp;

		[SerializeField]
		private StencilOp stencilOpPass;

		[SerializeField]
		private byte stencilRefID;

		[SerializeField]
		private byte stencilReadMask;

		[SerializeField]
		private byte stencilWriteMask;

		[SerializeField]
		private bool shouldUpdateMaterialPropertiesInEditor;

		private Material[] mats;

		private MaterialPropertyBlock Mpb => null;

		internal bool IsUsingUniqueMaterials => false;

		public Mesh Mesh
		{
			get
			{
				return null;
			}
			private set
			{
			}
		}

		public int SortingLayerID
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int SortingOrder
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string SortingLayerName => null;

		public ShapesBlendMode BlendMode
		{
			get
			{
				return default(ShapesBlendMode);
			}
			set
			{
			}
		}

		public ScaleMode ScaleMode
		{
			get
			{
				return default(ScaleMode);
			}
			set
			{
			}
		}

		public virtual Color Color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public virtual DetailLevel DetailLevel
		{
			get
			{
				return default(DetailLevel);
			}
			set
			{
			}
		}

		public ShapeCulling Culling
		{
			get
			{
				return default(ShapeCulling);
			}
			set
			{
			}
		}

		public float BoundsPadding
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		private bool IsInstanced => false;

		private bool UsingDefaultRenderQueue => false;

		public int RenderQueue
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		private bool UsingDefaultZTests => false;

		public CompareFunction ZTest
		{
			get
			{
				return default(CompareFunction);
			}
			set
			{
			}
		}

		public float ZOffsetFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public int ZOffsetUnits
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public ColorWriteMask ColorMask
		{
			get
			{
				return default(ColorWriteMask);
			}
			set
			{
			}
		}

		private bool UsingDefaultMasking => false;

		public CompareFunction StencilComp
		{
			get
			{
				return default(CompareFunction);
			}
			set
			{
			}
		}

		public StencilOp StencilOpPass
		{
			get
			{
				return default(StencilOp);
			}
			set
			{
			}
		}

		public byte StencilRefID
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public byte StencilReadMask
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public byte StencilWriteMask
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public bool ShouldUpdateMaterialPropertiesInEditor
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private bool HasGeneratedOrCopyOfMesh => false;

		private protected virtual int MaterialCount => 0;

		private protected virtual MeshUpdateMode MeshUpdateMode => default(MeshUpdateMode);

		internal virtual bool HasScaleModes => false;

		internal virtual bool HasDetailLevels => false;

		private protected virtual bool UseCamOnPreCull => false;

		private T MakeSureComponentExists<T>(ref T field, out bool created)
		{
			created = default(bool);
			return default(T);
		}

		private void VerifyComponents()
		{
		}

		public virtual void Awake()
		{
		}

		public virtual void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnPreCamCullWithCam(Camera cam)
		{
		}

		private void OnPreCamCullWithCam(ScriptableRenderContext ctx, Camera cam)
		{
		}

		private void SubscribeCamPreCull()
		{
		}

		private void UnsubscribeCamPreCull()
		{
		}

		private void Reset()
		{
		}

		private void OnDestroy()
		{
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
			return null;
		}

		internal virtual void CamOnPreCull()
		{
		}

		private void UpdateBounds()
		{
		}

		private void TryDestroyInstancedMaterials(bool inOnDestroy = false)
		{
		}

		private void MakeSureMaterialInstancesAreGood(Material[] sourceMats)
		{
		}

		private protected void UpdateMaterial()
		{
		}

		public void UpdateMesh(bool force = false)
		{
		}

		public Bounds GetBounds()
		{
			return default(Bounds);
		}

		public Bounds GetWorldBounds()
		{
			return default(Bounds);
		}

		private void OnDidApplyAnimationProperties()
		{
		}

		private void SetIntOnAllInstancedMaterials(int property, int value)
		{
		}

		private void SetFloatOnAllInstancedMaterials(int property, float value)
		{
		}

		internal void UpdateAllMaterialProperties()
		{
		}

		private protected void ApplyProperties()
		{
		}

		private protected void SetAllDashValues(DashStyle style, bool dashed, bool matchSpacingToSize, float thickness, bool setType, bool now)
		{
		}

		private protected float GetNetDashSpacing(DashStyle style, bool dashed, bool matchSpacingToSize, float thickness)
		{
			return 0f;
		}

		private protected void SetColor(int prop, Color value)
		{
		}

		private protected void SetFloat(int prop, float value)
		{
		}

		private protected void SetInt(int prop, int value)
		{
		}

		private protected void SetVector3(int prop, Vector3 value)
		{
		}

		private protected void SetVector4(int prop, Vector4 value)
		{
		}

		private protected void SetColorNow(int prop, Color value)
		{
		}

		private protected void SetFloatNow(int prop, float value)
		{
		}

		private protected void SetIntNow(int prop, int value)
		{
		}

		private protected void SetVector3Now(int prop, Vector3 value)
		{
		}

		private protected void SetVector4Now(int prop, Vector4 value)
		{
		}
	}
}
