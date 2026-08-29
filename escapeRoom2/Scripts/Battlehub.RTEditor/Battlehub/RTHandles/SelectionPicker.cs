using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTHandles
{
	public class SelectionPicker
	{
		private RuntimeWindow m_window;

		private Action<FilteringArgs> m_filterCallback;

		private static RenderTextureFormat s_renderTextureFormat;

		private static bool s_initialized;

		private static RenderTextureFormat[] s_preferredFormats = new RenderTextureFormat[2]
		{
			RenderTextureFormat.ARGB32,
			RenderTextureFormat.ARGBFloat
		};

		private static Shader s_objectSelectionShader;

		private static RenderTextureFormat RenderTextureFormat
		{
			get
			{
				Init();
				return s_renderTextureFormat;
			}
		}

		private static TextureFormat TextureFormat => TextureFormat.ARGB32;

		private static Shader ObjectSelectionShader
		{
			get
			{
				Init();
				return s_objectSelectionShader;
			}
		}

		public SelectionPicker(RuntimeWindow window, Action<FilteringArgs> filterCallback = null)
		{
			m_window = window;
			m_filterCallback = filterCallback;
		}

		protected virtual Renderer[] GetRenderers()
		{
			return (from r in UnityObjectExt.FindObjectsByType<Renderer>()
				where r.hideFlags == HideFlags.None && r.isVisible
				select r).ToArray();
		}

		protected virtual HashSet<Renderer> FilterObjects(FilteringArgs filteringArgs, IEnumerable<Renderer> renderers)
		{
			HashSet<Renderer> hashSet = new HashSet<Renderer>();
			foreach (Renderer renderer in renderers)
			{
				if (hashSet.Contains(renderer))
				{
					continue;
				}
				if (m_filterCallback != null)
				{
					filteringArgs.Object = renderer.gameObject;
					m_filterCallback(filteringArgs);
					if (!filteringArgs.Cancel)
					{
						hashSet.Add(renderer);
					}
					filteringArgs.Reset();
				}
				else
				{
					hashSet.Add(renderer);
				}
			}
			return hashSet;
		}

		public IEnumerable<Renderer> PixelPerfectDepthTest(Renderer[] renderers, Bounds bounds)
		{
			Canvas componentInParent = m_window.GetComponentInParent<Canvas>();
			RectTransform rectTransform = m_window.GetComponent<RectTransform>();
			if (rectTransform.childCount > 0)
			{
				rectTransform = (RectTransform)m_window.GetComponent<RectTransform>().GetChild(0);
			}
			Rect selectionRect = SelectionBoundsToSelectionRect(m_window.Camera, bounds, componentInParent, rectTransform);
			return PickRenderersInRect(m_window.Camera, selectionRect, renderers, Mathf.RoundToInt(componentInParent.pixelRect.width), Mathf.RoundToInt(componentInParent.pixelRect.height));
		}

		public Renderer[] Pick(Renderer[] renderers = null, bool filterObjects = true)
		{
			return Pick(renderers, new Bounds(m_window.Pointer.ScreenPoint, Vector2.zero), filterObjects);
		}

		public Renderer[] Pick(Renderer[] renderers, Bounds bounds, bool filterObjects = true)
		{
			if (renderers == null)
			{
				renderers = GetRenderers();
			}
			IEnumerable<Renderer> enumerable = PixelPerfectDepthTest(renderers, bounds);
			if (filterObjects)
			{
				FilteringArgs filteringArgs = new FilteringArgs();
				return FilterObjects(filteringArgs, enumerable).ToArray();
			}
			return enumerable.ToArray();
		}

		public Color32[] BeginPick(out Vector2Int texSize, Renderer[] renderers = null)
		{
			if (renderers == null)
			{
				renderers = GetRenderers();
			}
			Canvas componentInParent = m_window.GetComponentInParent<Canvas>();
			return Render(m_window.Camera, renderers, new Vector2Int(Mathf.RoundToInt(componentInParent.pixelRect.width), Mathf.RoundToInt(componentInParent.pixelRect.height)), out texSize);
		}

		public Renderer[] EndPick(Color32[] texPixels, Vector2Int texSize, Renderer[] renderers = null)
		{
			if (renderers == null)
			{
				renderers = (from r in UnityObjectExt.FindObjectsByType<Renderer>()
					where r.hideFlags == HideFlags.None && r.isVisible
					select r).ToArray();
			}
			Bounds bounds = new Bounds(m_window.Pointer.ScreenPoint, Vector2.zero);
			return EndPick(texPixels, texSize, renderers, bounds);
		}

		private Renderer[] EndPick(Color32[] texPixels, Vector2Int texSize, Renderer[] renderers, Bounds bounds)
		{
			Canvas componentInParent = m_window.GetComponentInParent<Canvas>();
			RectTransform rectTransform = m_window.GetComponent<RectTransform>();
			if (rectTransform.childCount > 0)
			{
				rectTransform = (RectTransform)m_window.GetComponent<RectTransform>().GetChild(0);
			}
			Rect selectionRect = SelectionBoundsToSelectionRect(m_window.Camera, bounds, componentInParent, rectTransform);
			return PickRenderersInRect(m_window.Camera, selectionRect, renderers, texPixels, texSize);
		}

		private static Rect SelectionBoundsToSelectionRect(Camera sceneCamera, Bounds bounds, Canvas canvas, RectTransform sceneOutput)
		{
			Vector2 localPoint = bounds.min;
			Vector2 localPoint2 = bounds.max;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(sceneOutput, localPoint, canvas.worldCamera, out localPoint);
			localPoint.y = sceneOutput.rect.height - localPoint.y;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(sceneOutput, localPoint2, canvas.worldCamera, out localPoint2);
			localPoint2.y = sceneOutput.rect.height - localPoint2.y;
			localPoint *= canvas.scaleFactor;
			localPoint2 *= canvas.scaleFactor;
			Rect result = new Rect(new Vector2(Mathf.Min(localPoint.x, localPoint2.x), Mathf.Min(localPoint.y, localPoint2.y)), new Vector2(Mathf.Max(Mathf.Abs(localPoint2.x - localPoint.x), 1f), Mathf.Max(Mathf.Abs(localPoint2.y - localPoint.y), 1f)));
			result.x += sceneCamera.pixelRect.x;
			result.y += canvas.pixelRect.height - (sceneCamera.pixelRect.y + sceneCamera.pixelRect.height);
			return result;
		}

		private static void Init()
		{
			if (s_initialized)
			{
				return;
			}
			s_initialized = true;
			s_objectSelectionShader = Shader.Find("Battlehub/RTHandles/BoxSelectionShader");
			for (int i = 0; i < s_preferredFormats.Length; i++)
			{
				if (SystemInfo.SupportsRenderTextureFormat(s_preferredFormats[i]))
				{
					s_renderTextureFormat = s_preferredFormats[i];
					break;
				}
			}
		}

		public static Color32[] Render(Camera camera, Renderer[] renderers, Vector2Int reqiestedTexSize, out Vector2Int texSize)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				Renderer renderer = renderers[i];
				if (!renderer.isPartOfStaticBatch)
				{
					Material[] sharedMaterials = renderer.sharedMaterials;
					for (int j = 0; j < sharedMaterials.Length; j++)
					{
						MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
						renderer.GetPropertyBlock(materialPropertyBlock, j);
						materialPropertyBlock.SetColor("_SelectionColor", EncodeRGBA((uint)(i + 1)));
						renderer.SetPropertyBlock(materialPropertyBlock, j);
					}
				}
			}
			Texture2D texture2D = Render(camera, ObjectSelectionShader, renderers, reqiestedTexSize.x, reqiestedTexSize.y);
			Color32[] pixels = texture2D.GetPixels32();
			texSize = new Vector2Int(texture2D.width, texture2D.height);
			UnityEngine.Object.DestroyImmediate(texture2D);
			return pixels;
		}

		public static Renderer[] PickRenderersInRect(Camera camera, Rect selectionRect, Renderer[] renderers, Color32[] texPixels, Vector2Int texSize)
		{
			selectionRect.width /= camera.rect.width;
			selectionRect.height /= camera.rect.height;
			selectionRect.x = (selectionRect.x - camera.pixelRect.x) / camera.rect.width;
			selectionRect.y = (selectionRect.y - ((float)texSize.y - (camera.pixelRect.y + camera.pixelRect.height))) / camera.rect.height;
			int num = Math.Max(0, Mathf.FloorToInt(selectionRect.x));
			int num2 = Math.Max(0, Mathf.FloorToInt((float)texSize.y - selectionRect.y - selectionRect.height));
			int num3 = Mathf.FloorToInt(selectionRect.width);
			int num4 = Mathf.FloorToInt(selectionRect.height);
			List<Renderer> list = new List<Renderer>();
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = num2; i < Math.Min(num2 + num4, texSize.y); i++)
			{
				for (int j = num; j < Math.Min(num + num3, texSize.x); j++)
				{
					int num5 = (int)(DecodeRGBA(texPixels[i * texSize.x + j]) - 1);
					if (num5 >= 0 && num5 < renderers.Length && hashSet.Add(num5))
					{
						Renderer item = renderers[num5];
						list.Add(item);
						if (list.Count == renderers.Length)
						{
							return list.ToArray();
						}
					}
				}
			}
			return list.ToArray();
		}

		public static Renderer[] PickRenderersInRect(Camera camera, Rect selectionRect, Renderer[] renderers, int renderTextureWidth = -1, int renderTextureHeight = -1)
		{
			Vector2Int texSize;
			Color32[] texPixels = Render(camera, renderers, new Vector2Int(renderTextureWidth, renderTextureHeight), out texSize);
			return PickRenderersInRect(camera, selectionRect, renderers, texPixels, texSize);
		}

		private static uint DecodeRGBA(Color32 color)
		{
			uint r = color.r;
			uint g = color.g;
			uint b = color.b;
			if (BitConverter.IsLittleEndian)
			{
				return (r << 16) | (g << 8) | b;
			}
			return (r << 24) | (g << 16) | (b << 8);
		}

		private static Color32 EncodeRGBA(uint hash)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new Color32((byte)((hash >> 16) & 0xFF), (byte)((hash >> 8) & 0xFF), (byte)(hash & 0xFF), byte.MaxValue);
			}
			return new Color32((byte)((hash >> 24) & 0xFF), (byte)((hash >> 16) & 0xFF), (byte)((hash >> 8) & 0xFF), byte.MaxValue);
		}

		private static Texture2D Render(Camera camera, Shader shader, Renderer[] renderers, int width = -1, int height = -1)
		{
			int num;
			int num2;
			if (width >= 0)
			{
				num = ((height < 0) ? 1 : 0);
				if (num == 0)
				{
					num2 = width;
					goto IL_0022;
				}
			}
			else
			{
				num = 1;
			}
			num2 = (int)camera.pixelRect.width;
			goto IL_0022;
			IL_0022:
			int num3 = num2;
			int num4 = ((num != 0) ? ((int)camera.pixelRect.height) : height);
			GameObject gameObject = new GameObject();
			Camera camera2 = gameObject.AddComponent<Camera>();
			camera2.CopyFrom(camera);
			camera2.renderingPath = RenderingPath.Forward;
			camera2.enabled = false;
			camera2.clearFlags = CameraClearFlags.Color;
			camera2.backgroundColor = Color.white;
			IRenderPipelineCameraUtility renderPipelineCameraUtility = IOC.Resolve<IRenderPipelineCameraUtility>();
			if (renderPipelineCameraUtility != null)
			{
				renderPipelineCameraUtility.ResetCullingMask(camera2);
				renderPipelineCameraUtility.EnablePostProcessing(camera2, value: false);
				renderPipelineCameraUtility.SetBackgroundColor(camera2, Color.white);
			}
			else
			{
				camera2.cullingMask = 0;
			}
			camera2.allowHDR = false;
			camera2.allowMSAA = false;
			camera2.forceIntoRenderTexture = true;
			float aspect = camera2.aspect;
			camera2.rect = new Rect(Vector2.zero, Vector2.one);
			camera2.aspect = aspect;
			RenderTexture temporary = RenderTexture.GetTemporary(new RenderTextureDescriptor
			{
				width = num3,
				height = num4,
				colorFormat = RenderTextureFormat,
				autoGenerateMips = false,
				depthBufferBits = 16,
				dimension = TextureDimension.Tex2D,
				enableRandomWrite = false,
				memoryless = RenderTextureMemoryless.None,
				sRGB = true,
				useMipMap = false,
				volumeDepth = 1,
				msaaSamples = 1
			});
			RenderTexture active = RenderTexture.active;
			camera2.targetTexture = temporary;
			RenderTexture.active = temporary;
			Material material = new Material(shader);
			IRTECamera iRTECamera = IOC.Resolve<IRTEGraphics>().CreateCamera(camera2, CameraEvent.AfterForwardAlpha, meshesCache: false, renderersCache: true);
			iRTECamera.RenderersCache.MaterialOverride = material;
			iRTECamera.Camera.name = "BoxSelectionCamera";
			foreach (Renderer renderer in renderers)
			{
				if (renderer.isPartOfStaticBatch)
				{
					continue;
				}
				Material[] sharedMaterials = renderer.sharedMaterials;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					if (sharedMaterials[j] != null)
					{
						iRTECamera.RenderersCache.Add(renderer);
					}
				}
			}
			iRTECamera.RefreshCommandBuffer();
			if (RenderPipelineInfo.Type != RPType.Standard)
			{
				bool invertCulling = GL.invertCulling;
				GL.invertCulling = true;
				camera2.projectionMatrix *= Matrix4x4.Scale(new Vector3(1f, -1f, 1f));
				camera2.Render();
				GL.invertCulling = invertCulling;
			}
			else
			{
				camera2.Render();
			}
			Texture2D texture2D = new Texture2D(num3, num4, TextureFormat, mipChain: false, linear: false);
			texture2D.ReadPixels(new Rect(0f, 0f, num3, num4), 0, 0);
			texture2D.Apply();
			RenderTexture.active = active;
			RenderTexture.ReleaseTemporary(temporary);
			UnityEngine.Object.DestroyImmediate(gameObject);
			UnityEngine.Object.Destroy(material);
			iRTECamera.Destroy();
			return texture2D;
		}
	}
}
