using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace Meta.XR.EnvironmentDepth
{
	public class EnvironmentDepthManager : MonoBehaviour
	{
		private class Mask
		{
			internal readonly Material _maskMaterial;

			private readonly RenderTexture _maskDepthRt;

			private readonly RenderTexture _maskedDepthTexture;

			private readonly CommandBuffer _maskCommandBuffer;

			private readonly Matrix4x4[] _mvpMatrices = new Matrix4x4[2];

			internal Mask(int width, int height, float bias)
			{
				Shader shader = Shader.Find("Meta/EnvironmentDepth/DepthMask");
				_maskMaterial = new Material(shader)
				{
					enableInstancing = true
				};
				_maskMaterial.SetFloat(MaskBiasID, bias);
				_maskDepthRt = new RenderTexture(width, height, GraphicsFormat.R16_UNorm, GraphicsFormat.D16_UNorm)
				{
					dimension = TextureDimension.Tex2DArray,
					volumeDepth = 2
				};
				_maskedDepthTexture = new RenderTexture(width, height, GraphicsFormat.R16_UNorm, GraphicsFormat.None)
				{
					dimension = TextureDimension.Tex2DArray,
					volumeDepth = 2,
					depth = 0
				};
				_maskCommandBuffer = new CommandBuffer();
			}

			internal RenderTexture ApplyMask(RenderTexture depthTexture, List<MeshFilter> meshFilters, Matrix4x4 trackingSpaceWorldToLocal)
			{
				EnvironmentDepthUtils.CalculateDepthCameraMatrices(_provider.GetFrameDesc(0), out var projMatrix, out var viewMatrix);
				EnvironmentDepthUtils.CalculateDepthCameraMatrices(_provider.GetFrameDesc(1), out var projMatrix2, out var viewMatrix2);
				_maskCommandBuffer.SetRenderTarget(new RenderTargetIdentifier(_maskDepthRt, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				_maskCommandBuffer.ClearRenderTarget(clearDepth: true, clearColor: true, Color.white);
				foreach (MeshFilter meshFilter in meshFilters)
				{
					if (meshFilter == null || meshFilter.sharedMesh == null)
					{
						UnityEngine.Debug.LogError("MeshFilter or sharedMesh is null.");
						continue;
					}
					_mvpMatrices[0] = GL.GetGPUProjectionMatrix(projMatrix, renderIntoTexture: true) * viewMatrix * trackingSpaceWorldToLocal * meshFilter.transform.localToWorldMatrix;
					_mvpMatrices[1] = GL.GetGPUProjectionMatrix(projMatrix2, renderIntoTexture: true) * viewMatrix2 * trackingSpaceWorldToLocal * meshFilter.transform.localToWorldMatrix;
					_maskCommandBuffer.SetGlobalMatrixArray(MvpMatricesID, _mvpMatrices);
					_maskCommandBuffer.DrawMeshInstancedProcedural(meshFilter.sharedMesh, 0, _maskMaterial, 0, 2);
				}
				_maskMaterial.SetTexture(DepthTextureID, depthTexture);
				_maskMaterial.SetTexture(MaskTextureID, _maskDepthRt);
				_maskCommandBuffer.SetRenderTarget(new RenderTargetIdentifier(_maskedDepthTexture, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				_maskCommandBuffer.DrawProcedural(Matrix4x4.identity, _maskMaterial, 1, MeshTopology.Triangles, 3, 2);
				Graphics.ExecuteCommandBuffer(_maskCommandBuffer);
				_maskCommandBuffer.Clear();
				return _maskedDepthTexture;
			}

			internal void Dispose()
			{
				Object.Destroy(_maskMaterial);
				Object.Destroy(_maskDepthRt);
				Object.Destroy(_maskedDepthTexture);
				_maskCommandBuffer.Dispose();
			}
		}

		public const string HardOcclusionKeyword = "HARD_OCCLUSION";

		public const string SoftOcclusionKeyword = "SOFT_OCCLUSION";

		private const int numViews = 2;

		private static readonly int DepthTextureID = Shader.PropertyToID("_EnvironmentDepthTexture");

		private static readonly int ReprojectionMatricesID = Shader.PropertyToID("_EnvironmentDepthReprojectionMatrices");

		private static readonly int ZBufferParamsID = Shader.PropertyToID("_EnvironmentDepthZBufferParams");

		private static readonly int PreprocessedEnvironmentDepthTexture = Shader.PropertyToID("_PreprocessedEnvironmentDepthTexture");

		private static readonly int MvpMatricesID = Shader.PropertyToID("_DepthMask_MVP_Matrices");

		private static readonly int MaskTextureID = Shader.PropertyToID("_MaskTexture");

		private static readonly int MaskBiasID = Shader.PropertyToID("_MaskBias");

		[SerializeField]
		private OcclusionShadersMode _occlusionShadersMode = OcclusionShadersMode.SoftOcclusion;

		[SerializeField]
		private bool _removeHands;

		[SerializeField]
		public Transform CustomTrackingSpace;

		private bool _isCameraRigCached;

		[SerializeField]
		[HideInInspector]
		private OVRCameraRig _cameraRig;

		internal static readonly IDepthProvider _provider = CreateProvider();

		private bool _hasPermission;

		private uint? _prevTextureId;

		private Material _preprocessMaterial;

		[CanBeNull]
		private RenderTexture _preprocessTexture;

		private RenderTargetSetup _preprocessRenderTargetSetup;

		private float _maskBias = 0.1f;

		private Mask _mask;

		private readonly Matrix4x4[] _reprojectionMatrices = new Matrix4x4[2];

		private XRDisplaySubsystem _xrDisplay;

		[field: SerializeField]
		public List<MeshFilter> MaskMeshFilters { get; set; } = new List<MeshFilter>();

		public static bool IsSupported => _provider.IsSupported;

		public bool IsDepthAvailable { get; private set; }

		public OcclusionShadersMode OcclusionShadersMode
		{
			get
			{
				return _occlusionShadersMode;
			}
			set
			{
				if (_occlusionShadersMode != value)
				{
					_occlusionShadersMode = value;
					if (IsDepthAvailable)
					{
						SetOcclusionShaderKeywords(value);
					}
				}
			}
		}

		public bool RemoveHands
		{
			get
			{
				return _removeHands;
			}
			set
			{
				if (_removeHands != value)
				{
					_removeHands = value;
					if (base.enabled)
					{
						_provider.RemoveHands = value;
					}
				}
			}
		}

		public float MaskBias
		{
			get
			{
				return _maskBias;
			}
			set
			{
				_maskBias = value;
				if (_mask != null)
				{
					_mask._maskMaterial.SetFloat(MaskBiasID, value);
				}
			}
		}

		[NotNull]
		private static IDepthProvider CreateProvider()
		{
			return new DepthProviderNotSupported();
		}

		private void Awake()
		{
			if (IsSupported)
			{
				List<XRDisplaySubsystem> list = new List<XRDisplaySubsystem>(1);
				SubsystemManager.GetSubsystems(list);
				_xrDisplay = list.Single();
				Shader shader = Shader.Find("Meta/EnvironmentDepth/Preprocessing");
				_preprocessMaterial = new Material(shader);
			}
		}

		private void OnEnable()
		{
			if (!IsSupported)
			{
				UnityEngine.Debug.LogError("Environment Depth is not supported. Please check EnvironmentDepthManager.IsSupported before enabling EnvironmentDepthManager.\nOpen 'Oculus -> Tools -> Project Setup Tool' to see requirements.\n");
				base.enabled = false;
				return;
			}
			_hasPermission = Permission.HasUserAuthorizedPermission("com.oculus.permission.USE_SCENE");
			if (_hasPermission)
			{
				_provider.SetDepthEnabled(isEnabled: true, _removeHands);
			}
		}

		private void ResetDepthTextureIfAvailable()
		{
			if (IsDepthAvailable)
			{
				IsDepthAvailable = false;
				Shader.SetGlobalTexture(DepthTextureID, null);
				if (_occlusionShadersMode != OcclusionShadersMode.None)
				{
					SetOcclusionShaderKeywords(OcclusionShadersMode.None);
				}
			}
		}

		private void OnDisable()
		{
			ResetDepthTextureIfAvailable();
			if (IsSupported && _hasPermission)
			{
				_provider.SetDepthEnabled(isEnabled: false, removeHands: false);
			}
		}

		private void OnDestroy()
		{
			if (_preprocessMaterial != null)
			{
				Object.Destroy(_preprocessMaterial);
			}
			if (_preprocessTexture != null)
			{
				Object.Destroy(_preprocessTexture);
			}
			_mask?.Dispose();
		}

		private void Update()
		{
			if (!_hasPermission)
			{
				if (!Permission.HasUserAuthorizedPermission("com.oculus.permission.USE_SCENE"))
				{
					return;
				}
				_hasPermission = true;
				_provider.SetDepthEnabled(isEnabled: true, _removeHands);
			}
			Matrix4x4 trackingSpaceWorldToLocalMatrix = GetTrackingSpaceWorldToLocalMatrix();
			TryFetchDepthTexture(trackingSpaceWorldToLocalMatrix);
			if (IsDepthAvailable)
			{
				DepthFrameDesc frameDesc = _provider.GetFrameDesc(0);
				DepthFrameDesc frameDesc2 = _provider.GetFrameDesc(1);
				Vector4 value = EnvironmentDepthUtils.ComputeNdcToLinearDepthParameters(frameDesc.nearZ, frameDesc.farZ);
				Shader.SetGlobalVector(ZBufferParamsID, value);
				_reprojectionMatrices[0] = EnvironmentDepthUtils.CalculateReprojection(frameDesc) * trackingSpaceWorldToLocalMatrix;
				_reprojectionMatrices[1] = EnvironmentDepthUtils.CalculateReprojection(frameDesc2) * trackingSpaceWorldToLocalMatrix;
				Shader.SetGlobalMatrixArray(ReprojectionMatricesID, _reprojectionMatrices);
			}
		}

		private void CacheCameraRig()
		{
			if (_cameraRig == null)
			{
				_cameraRig = Object.FindObjectOfType<OVRCameraRig>();
			}
		}

		private static void SetOcclusionShaderKeywords(OcclusionShadersMode mode)
		{
			switch (mode)
			{
			case OcclusionShadersMode.HardOcclusion:
				Shader.DisableKeyword("SOFT_OCCLUSION");
				Shader.EnableKeyword("HARD_OCCLUSION");
				break;
			case OcclusionShadersMode.SoftOcclusion:
				Shader.DisableKeyword("HARD_OCCLUSION");
				Shader.EnableKeyword("SOFT_OCCLUSION");
				break;
			case OcclusionShadersMode.None:
				Shader.DisableKeyword("HARD_OCCLUSION");
				Shader.DisableKeyword("SOFT_OCCLUSION");
				break;
			default:
				UnityEngine.Debug.LogError(string.Format("Environment Depth: unknown {0} {1}", "OcclusionShadersMode", mode));
				break;
			}
		}

		private void TryFetchDepthTexture(Matrix4x4 trackingSpaceWorldToLocal)
		{
			uint textureId = 0u;
			if (!_xrDisplay.running || !_provider.GetDepthTextureId(ref textureId))
			{
				return;
			}
			RenderTexture renderTexture = _xrDisplay.GetRenderTexture(textureId);
			if (renderTexture == null)
			{
				ResetDepthTextureIfAvailable();
			}
			else
			{
				if (_prevTextureId == textureId)
				{
					return;
				}
				_prevTextureId = textureId;
				if (MaskMeshFilters != null && MaskMeshFilters.Count > 0)
				{
					if (_mask == null)
					{
						_mask = new Mask(renderTexture.width, renderTexture.height, _maskBias);
					}
					renderTexture = _mask.ApplyMask(renderTexture, MaskMeshFilters, trackingSpaceWorldToLocal);
				}
				Shader.SetGlobalTexture(DepthTextureID, renderTexture);
				if (!IsDepthAvailable)
				{
					IsDepthAvailable = true;
					if (_occlusionShadersMode != OcclusionShadersMode.None)
					{
						SetOcclusionShaderKeywords(_occlusionShadersMode);
					}
				}
				if (_occlusionShadersMode == OcclusionShadersMode.SoftOcclusion)
				{
					PreprocessDepthTexture(renderTexture);
				}
			}
		}

		internal Matrix4x4 GetTrackingSpaceWorldToLocalMatrix()
		{
			if (CustomTrackingSpace != null)
			{
				return CustomTrackingSpace.worldToLocalMatrix;
			}
			if (!_isCameraRigCached)
			{
				_isCameraRigCached = true;
				CacheCameraRig();
			}
			if (!(_cameraRig != null))
			{
				return Matrix4x4.identity;
			}
			return _cameraRig.trackingSpace.worldToLocalMatrix;
		}

		private void PreprocessDepthTexture(RenderTexture depthTexture)
		{
			if (_preprocessTexture == null)
			{
				_preprocessTexture = new RenderTexture(depthTexture.width, depthTexture.height, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None)
				{
					dimension = TextureDimension.Tex2DArray,
					volumeDepth = 2,
					name = "_preprocessTexture",
					depth = 0
				};
				_preprocessTexture.Create();
				Shader.SetGlobalTexture(PreprocessedEnvironmentDepthTexture, _preprocessTexture);
				_preprocessRenderTargetSetup = new RenderTargetSetup
				{
					color = new RenderBuffer[1] { _preprocessTexture.colorBuffer },
					depth = _preprocessTexture.depthBuffer,
					depthSlice = -1,
					colorLoad = new RenderBufferLoadAction[1] { RenderBufferLoadAction.DontCare },
					colorStore = new RenderBufferStoreAction[1],
					depthLoad = RenderBufferLoadAction.DontCare,
					depthStore = RenderBufferStoreAction.DontCare,
					mipLevel = 0,
					cubemapFace = CubemapFace.Unknown
				};
			}
			Graphics.SetRenderTarget(_preprocessRenderTargetSetup);
			_preprocessMaterial.SetPass(0);
			Graphics.DrawProceduralNow(MeshTopology.Triangles, 3, 2);
		}

		[Conditional("UNITY_ASSERTIONS")]
		private static void Log(LogType type, string msg)
		{
			UnityEngine.Debug.unityLogger.Log(type, msg);
		}
	}
}
