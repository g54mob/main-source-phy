using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	public class RTECamera : MonoBehaviour, IRTECamera
	{
		private Camera m_camera;

		private RTECommandBuffer m_rteCommandBuffer;

		private IRTECommandBuffer m_rteCommandBufferOverride;

		[SerializeField]
		private CameraEvent m_cameraEvent = CameraEvent.BeforeImageEffects;

		private IRenderersCache m_renderersCache;

		private bool m_destroyRenderersCache;

		private IMeshesCache m_meshesCache;

		private bool m_destroyMeshesCache;

		private bool m_initialized;

		public Camera Camera => m_camera;

		public IRTECommandBuffer RTECommandBuffer
		{
			get
			{
				if (m_rteCommandBufferOverride == null)
				{
					return m_rteCommandBuffer;
				}
				return m_rteCommandBufferOverride;
			}
		}

		public IRTECommandBuffer RTECommandBufferOverride
		{
			get
			{
				return m_rteCommandBufferOverride;
			}
			set
			{
				m_rteCommandBufferOverride = value;
				if (m_rteCommandBufferOverride != null)
				{
					RemoveCommandBuffer();
				}
				else
				{
					CreateCommandBuffer();
				}
			}
		}

		public CameraEvent Event
		{
			get
			{
				return m_cameraEvent;
			}
			set
			{
				if (m_rteCommandBufferOverride == null)
				{
					RemoveCommandBuffer();
					m_cameraEvent = value;
					CreateCommandBuffer();
				}
				else
				{
					m_cameraEvent = value;
				}
			}
		}

		public IRenderersCache RenderersCache
		{
			get
			{
				if (m_initialized && m_renderersCache == null)
				{
					CreateRenderersCache();
				}
				return m_renderersCache;
			}
			set
			{
				DestroyRenderersCache();
				m_renderersCache = value;
			}
		}

		public IMeshesCache MeshesCache
		{
			get
			{
				if (m_initialized && m_meshesCache == null)
				{
					CreateMeshesCache();
				}
				return m_meshesCache;
			}
			set
			{
				DestroyMeshesCache();
				m_meshesCache = value;
			}
		}

		private bool IsInitialized
		{
			get
			{
				if (m_initialized)
				{
					return m_camera != null;
				}
				return false;
			}
		}

		public static event Action<IRTECamera> Created;

		public static event Action<IRTECamera> Destroyed;

		public event Action<IRTECamera> CommandBufferRefresh;

		private void Awake()
		{
			Debug.Log("initing RTECamera");
			m_initialized = true;
			m_camera = GetComponent<Camera>();
			if (m_rteCommandBufferOverride == null)
			{
				CreateCommandBuffer();
			}
			RefreshCommandBuffer();
			if (m_renderersCache != null)
			{
				m_renderersCache.Refreshed -= OnCacheRefresh;
				m_renderersCache.Refreshed += OnCacheRefresh;
			}
			if (m_meshesCache != null)
			{
				m_meshesCache.Refreshing -= OnCacheRefresh;
				m_meshesCache.Refreshing += OnCacheRefresh;
			}
			if (RTECamera.Created != null)
			{
				RTECamera.Created(this);
			}
		}

		private void OnDestroy()
		{
			m_initialized = false;
			if (m_renderersCache != null)
			{
				m_renderersCache.Refreshed -= OnCacheRefresh;
				DestroyRenderersCache();
			}
			if (m_meshesCache != null)
			{
				m_meshesCache.Refreshing -= OnCacheRefresh;
				DestroyMeshesCache();
			}
			if (m_camera != null)
			{
				RemoveCommandBuffer();
			}
			if (RTECamera.Destroyed != null)
			{
				RTECamera.Destroyed(this);
			}
		}

		public void CreateRenderersCache()
		{
			DestroyRenderersCache();
			m_destroyRenderersCache = true;
			m_renderersCache = base.gameObject.AddComponent<RenderersCache>();
			if (IsInitialized)
			{
				m_renderersCache.Refreshed += OnCacheRefresh;
			}
		}

		public void CreateMeshesCache()
		{
			DestroyMeshesCache();
			m_destroyMeshesCache = true;
			m_meshesCache = base.gameObject.AddComponent<MeshesCache>();
			if (IsInitialized)
			{
				m_meshesCache.Refreshing += OnCacheRefresh;
			}
		}

		public void DestroyRenderersCache()
		{
			if (m_destroyRenderersCache && m_renderersCache != null)
			{
				m_renderersCache.Refreshed -= OnCacheRefresh;
				m_renderersCache.Destroy();
				m_renderersCache = null;
			}
		}

		public void DestroyMeshesCache()
		{
			if (m_destroyMeshesCache && m_meshesCache != null)
			{
				m_meshesCache.Refreshing -= OnCacheRefresh;
				m_meshesCache.Destroy();
				m_meshesCache = null;
			}
		}

		public void Destroy()
		{
			DestroyMeshesCache();
			DestroyRenderersCache();
			UnityEngine.Object.Destroy(this);
		}

		private void OnCacheRefresh()
		{
			RefreshCommandBuffer();
		}

		private void CreateCommandBuffer()
		{
			if (m_rteCommandBuffer == null && !(m_camera == null) && RenderPipelineInfo.Type != RPType.HDRP && (RenderPipelineInfo.Type != RPType.URP || !RenderPipelineInfo.UseRenderGraph))
			{
				m_rteCommandBuffer = new RTECommandBuffer();
				m_rteCommandBuffer.name = "RTECameraCommandBuffer";
				m_camera.AddCmdBuffer(m_cameraEvent, m_rteCommandBuffer.WrappedCommandBuffer);
			}
		}

		private void RemoveCommandBuffer()
		{
			if (m_rteCommandBuffer != null && RenderPipelineInfo.Type != RPType.HDRP && (RenderPipelineInfo.Type != RPType.URP || !RenderPipelineInfo.UseRenderGraph))
			{
				m_camera.RemoveCmdBuffer(m_cameraEvent, m_rteCommandBuffer.WrappedCommandBuffer);
				m_rteCommandBuffer.Dispose();
				m_rteCommandBuffer = null;
			}
		}

		public void RefreshCommandBuffer()
		{
			if (!IsInitialized)
			{
				return;
			}
			IRTECommandBuffer iRTECommandBuffer;
			if (m_rteCommandBufferOverride == null)
			{
				if (m_rteCommandBuffer == null)
				{
					return;
				}
				m_rteCommandBuffer.Clear();
				if (m_cameraEvent == CameraEvent.AfterImageEffects || m_cameraEvent == CameraEvent.AfterImageEffectsOpaque)
				{
					m_rteCommandBuffer.ClearRenderTarget(clearDepth: true, clearColor: false, Color.black);
				}
				iRTECommandBuffer = m_rteCommandBuffer;
			}
			else
			{
				iRTECommandBuffer = m_rteCommandBufferOverride;
			}
			if (m_meshesCache != null)
			{
				IList<RenderMeshesBatch> batches = m_meshesCache.Batches;
				for (int i = 0; i < batches.Count; i++)
				{
					RenderMeshesBatch renderMeshesBatch = batches[i];
					if (renderMeshesBatch.Material == null)
					{
						continue;
					}
					if (renderMeshesBatch.Material.enableInstancing)
					{
						for (int j = 0; j < renderMeshesBatch.Mesh.subMeshCount; j++)
						{
							if (renderMeshesBatch.Mesh != null)
							{
								iRTECommandBuffer.DrawMeshInstanced(renderMeshesBatch.Mesh, j, renderMeshesBatch.Material, -1, renderMeshesBatch.Matrices, renderMeshesBatch.Matrices.Length);
							}
						}
						continue;
					}
					Matrix4x4[] matrices = renderMeshesBatch.Matrices;
					for (int k = 0; k < matrices.Length; k++)
					{
						for (int l = 0; l < renderMeshesBatch.Mesh.subMeshCount; l++)
						{
							if (renderMeshesBatch.Mesh != null)
							{
								iRTECommandBuffer.DrawMesh(renderMeshesBatch.Mesh, matrices[k], renderMeshesBatch.Material, l, -1);
							}
						}
					}
				}
			}
			if (m_renderersCache != null)
			{
				IList<Renderer> renderers = m_renderersCache.Renderers;
				for (int m = 0; m < renderers.Count; m++)
				{
					Renderer renderer = renderers[m];
					if (renderer == null)
					{
						continue;
					}
					Material[] sharedMaterials = renderer.sharedMaterials;
					for (int n = 0; n < sharedMaterials.Length; n++)
					{
						if (m_renderersCache.MaterialOverride != null)
						{
							iRTECommandBuffer.DrawRenderer(renderer, m_renderersCache.MaterialOverride, n, -1);
							continue;
						}
						Material material = sharedMaterials[n];
						if (material != null)
						{
							iRTECommandBuffer.DrawRenderer(renderer, material, n, -1);
						}
					}
				}
			}
			if (this.CommandBufferRefresh != null)
			{
				this.CommandBufferRefresh(this);
			}
		}
	}
}
