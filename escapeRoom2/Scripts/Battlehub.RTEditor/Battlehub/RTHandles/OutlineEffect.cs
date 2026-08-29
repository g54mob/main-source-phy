using System.Collections.Generic;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTHandles
{
	public class OutlineEffect : MonoBehaviour
	{
		public Color OutlineColor = new Color(1f, 0.35f, 0f, 0.05f);

		public CameraEvent BufferDrawEvent = CameraEvent.BeforeImageEffects;

		[Range(0f, 1f)]
		public int Downsample;

		[Range(0f, 3f)]
		public float BlurSize = 0.9f;

		public bool UseDepth;

		private CommandBuffer m_commandBuffer;

		private int m_outlineRTID;

		private int m_blurredRTID;

		private int m_temporaryRTID;

		private int m_depthRTID;

		private int m_idRTID;

		private List<Renderer> m_objectRenderers;

		private HashSet<Renderer> m_objectRenderersHs;

		private List<ICustomOutlinePrepass> m_customObjectRenderers;

		private HashSet<ICustomOutlinePrepass> m_customObjectRenderersHs;

		private Material m_outlineMaterial;

		private Camera m_camera;

		private int m_rtWidth = 512;

		private int m_rtHeight = 512;

		private Rect m_prevRect;

		public bool ContainsRenderer(Renderer renderer)
		{
			return m_objectRenderersHs.Contains(renderer);
		}

		public void AddRenderers(Renderer[] renderers)
		{
			foreach (Renderer item in renderers)
			{
				if (!m_objectRenderersHs.Contains(item))
				{
					m_objectRenderers.Add(item);
					m_objectRenderersHs.Add(item);
				}
			}
			RecreateCommandBuffer();
		}

		public void RemoveRenderers(Renderer[] renderers)
		{
			foreach (Renderer item in renderers)
			{
				m_objectRenderers.Remove(item);
				m_objectRenderersHs.Remove(item);
			}
			RecreateCommandBuffer();
		}

		public void AddRenderers(ICustomOutlinePrepass[] renderers)
		{
			foreach (ICustomOutlinePrepass item in renderers)
			{
				if (!m_customObjectRenderersHs.Contains(item))
				{
					m_customObjectRenderers.Add(item);
					m_customObjectRenderersHs.Add(item);
				}
			}
			RecreateCommandBuffer();
		}

		public void RemoveRenderers(ICustomOutlinePrepass[] renderers)
		{
			foreach (ICustomOutlinePrepass item in renderers)
			{
				m_customObjectRenderers.Remove(item);
				m_customObjectRenderersHs.Remove(item);
			}
			RecreateCommandBuffer();
		}

		public void ClearOutlineData()
		{
			m_objectRenderers.Clear();
			m_objectRenderersHs.Clear();
			RecreateCommandBuffer();
		}

		private void Awake()
		{
			m_objectRenderers = new List<Renderer>();
			m_objectRenderersHs = new HashSet<Renderer>();
			m_customObjectRenderers = new List<ICustomOutlinePrepass>();
			m_customObjectRenderersHs = new HashSet<ICustomOutlinePrepass>();
			m_commandBuffer = new CommandBuffer();
			m_commandBuffer.name = "UnityOutlineFX Command Buffer";
			m_depthRTID = Shader.PropertyToID("_DepthRT");
			m_outlineRTID = Shader.PropertyToID("_OutlineRT");
			m_blurredRTID = Shader.PropertyToID("_BlurredRT");
			m_temporaryRTID = Shader.PropertyToID("_TemporaryRT");
			m_idRTID = Shader.PropertyToID("_idRT");
			m_outlineMaterial = new Material(Shader.Find("Hidden/UnityOutline"));
			m_camera = GetComponent<Camera>();
			m_camera.depthTextureMode = DepthTextureMode.Depth;
			m_camera.AddCmdBuffer(BufferDrawEvent, m_commandBuffer);
		}

		private void OnDestroy()
		{
			if (m_camera != null)
			{
				m_camera.RemoveCmdBuffer(BufferDrawEvent, m_commandBuffer);
			}
		}

		public void RecreateCommandBuffer()
		{
			if (m_camera == null || m_commandBuffer == null)
			{
				return;
			}
			int num = ((!m_camera.allowMSAA) ? 1 : Mathf.Max(1, RenderPipelineInfo.MSAASampleCount));
			RenderTargetIdentifier depth = BuiltinRenderTextureType.Depth;
			if (m_camera.targetTexture != null && RenderPipelineInfo.Type == RPType.Standard)
			{
				m_rtWidth = Screen.width;
				m_rtHeight = Screen.height;
				depth = BuiltinRenderTextureType.CurrentActive;
			}
			else
			{
				m_rtWidth = m_camera.pixelWidth;
				m_rtHeight = m_camera.pixelHeight;
				if (num != 1)
				{
					depth = BuiltinRenderTextureType.CurrentActive;
				}
			}
			m_commandBuffer.Clear();
			if (m_objectRenderers.Count == 0 && m_customObjectRenderers.Count == 0)
			{
				return;
			}
			FilterMode filter = FilterMode.Point;
			m_commandBuffer.GetTemporaryRT(m_depthRTID, m_rtWidth, m_rtHeight, 0, filter, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, num);
			m_commandBuffer.GetTemporaryRT(m_depthRTID, m_rtWidth, m_rtHeight, 0, filter, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, num);
			if (UseDepth)
			{
				m_commandBuffer.SetRenderTarget(m_depthRTID, depth);
			}
			else
			{
				m_commandBuffer.SetRenderTarget(m_depthRTID);
			}
			m_commandBuffer.ClearRenderTarget(clearDepth: false, clearColor: true, Color.clear);
			if (m_camera.targetTexture != null && RenderPipelineInfo.Type == RPType.Standard)
			{
				m_commandBuffer.SetViewport(m_camera.pixelRect);
			}
			float num2 = 0f;
			for (int num3 = m_objectRenderers.Count - 1; num3 >= 0; num3--)
			{
				Renderer renderer = m_objectRenderers[num3];
				if (renderer != null)
				{
					if (((1 << renderer.gameObject.layer) & m_camera.cullingMask) != 0 && renderer.enabled)
					{
						num2 += 0.25f;
						m_commandBuffer.SetGlobalFloat("_ObjectId", num2);
						int num4 = renderer.sharedMaterials.Length;
						for (int i = 0; i < num4; i++)
						{
							m_commandBuffer.DrawRenderer(renderer, m_outlineMaterial, i, 1);
							m_commandBuffer.DrawRenderer(renderer, m_outlineMaterial, i, 0);
						}
					}
				}
				else
				{
					m_objectRenderers.Remove(renderer);
					m_objectRenderersHs.Remove(renderer);
				}
			}
			for (int num5 = m_customObjectRenderers.Count - 1; num5 >= 0; num5--)
			{
				ICustomOutlinePrepass customOutlinePrepass = m_customObjectRenderers[num5];
				if (customOutlinePrepass != null && customOutlinePrepass.GetRenderer() != null)
				{
					if (((1 << customOutlinePrepass.GetRenderer().gameObject.layer) & m_camera.cullingMask) != 0 && customOutlinePrepass.GetRenderer().enabled)
					{
						num2 += 0.25f;
						m_commandBuffer.SetGlobalFloat("_ObjectId", num2);
						int num6 = customOutlinePrepass.GetRenderer().sharedMaterials.Length;
						for (int j = 0; j < num6; j++)
						{
							m_commandBuffer.DrawRenderer(customOutlinePrepass.GetRenderer(), customOutlinePrepass.GetOutlinePrepassMaterial(), j);
						}
					}
				}
				else
				{
					m_customObjectRenderers.Remove(customOutlinePrepass);
					m_customObjectRenderersHs.Remove(customOutlinePrepass);
				}
			}
			m_commandBuffer.GetTemporaryRT(m_idRTID, m_rtWidth, m_rtHeight, 0, filter, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, num);
			m_commandBuffer.Blit(m_depthRTID, m_idRTID, m_outlineMaterial, 3);
			int width = m_rtWidth >> Downsample;
			int height = m_rtHeight >> Downsample;
			m_commandBuffer.GetTemporaryRT(m_temporaryRTID, width, height, 0, filter, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, num);
			m_commandBuffer.GetTemporaryRT(m_blurredRTID, width, height, 0, filter, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, num);
			m_commandBuffer.Blit(m_idRTID, m_blurredRTID);
			m_commandBuffer.SetGlobalVector("_BlurDirection", new Vector2(BlurSize, 0f));
			m_commandBuffer.Blit(m_blurredRTID, m_temporaryRTID, m_outlineMaterial, 2);
			m_commandBuffer.SetGlobalVector("_BlurDirection", new Vector2(0f, BlurSize));
			m_commandBuffer.Blit(m_temporaryRTID, m_blurredRTID, m_outlineMaterial, 2);
			m_commandBuffer.SetGlobalColor("_OutlineColor", OutlineColor);
			m_commandBuffer.Blit(m_blurredRTID, BuiltinRenderTextureType.CameraTarget, m_outlineMaterial, 4);
			m_commandBuffer.ReleaseTemporaryRT(m_blurredRTID);
			m_commandBuffer.ReleaseTemporaryRT(m_outlineRTID);
			m_commandBuffer.ReleaseTemporaryRT(m_temporaryRTID);
			m_commandBuffer.ReleaseTemporaryRT(m_depthRTID);
		}

		private void OnPreRender()
		{
			if (m_camera.pixelRect != m_prevRect)
			{
				m_prevRect = m_camera.pixelRect;
				RecreateCommandBuffer();
			}
		}
	}
}
