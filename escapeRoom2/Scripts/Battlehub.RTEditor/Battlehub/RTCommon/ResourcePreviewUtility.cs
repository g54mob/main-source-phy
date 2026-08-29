using System;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class ResourcePreviewUtility : MonoBehaviour, IResourcePreviewUtility
	{
		private Shader m_unlitTexShader;

		[SerializeField]
		private ObjectToTexture m_objectToTextureCamera;

		[SerializeField]
		private GameObject m_fallbackPrefab;

		[SerializeField]
		private Vector3 m_previewObjectScale = new Vector3(1f, 1f, 1f);

		[SerializeField]
		private float m_previewScale = 1f;

		private GameObject m_materialPreviewSphere;

		private MeshRenderer m_materialPreviewRenderer;

		public Vector3 PreviewObjectScale
		{
			get
			{
				return m_previewObjectScale;
			}
			set
			{
				m_previewObjectScale = value;
			}
		}

		public float PreviewScale
		{
			get
			{
				return m_previewScale;
			}
			set
			{
				m_previewScale = value;
			}
		}

		public int PreviewWidth
		{
			get
			{
				return m_objectToTextureCamera.snapshotTextureWidth;
			}
			set
			{
				m_objectToTextureCamera.snapshotTextureWidth = value;
			}
		}

		public int PreviewHeight
		{
			get
			{
				return m_objectToTextureCamera.snapshotTextureHeight;
			}
			set
			{
				m_objectToTextureCamera.snapshotTextureHeight = value;
			}
		}

		public virtual Camera Camera
		{
			get
			{
				if (m_objectToTextureCamera != null)
				{
					return m_objectToTextureCamera.GetComponent<Camera>();
				}
				return null;
			}
		}

		protected virtual void Awake()
		{
			IRTE iRTE = IOC.Resolve<IRTE>();
			m_unlitTexShader = Shader.Find("Unlit/Texture");
			if (m_objectToTextureCamera == null)
			{
				GameObject gameObject = new GameObject("Object To Texture");
				gameObject.SetActive(value: false);
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				Camera camera = gameObject.AddComponent<Camera>();
				camera.nearClipPlane = 0.01f;
				camera.orthographic = true;
				camera.clearFlags = CameraClearFlags.Color;
				camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
				if (RenderPipelineInfo.Type == RPType.Standard)
				{
					camera.stereoTargetEye = StereoTargetEyeMask.None;
				}
				camera.cullingMask = 1 << iRTE.CameraLayerSettings.ResourcePreviewLayer;
				m_objectToTextureCamera = gameObject.AddComponent<ObjectToTexture>();
				m_objectToTextureCamera.objectImageLayer = iRTE.CameraLayerSettings.ResourcePreviewLayer;
				Light[] array = UnityObjectExt.FindObjectsByType<Light>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].cullingMask &= ~(1 << iRTE.CameraLayerSettings.ResourcePreviewLayer);
				}
				GameObject obj = new GameObject("Directional light");
				obj.transform.SetParent(gameObject.transform, worldPositionStays: false);
				obj.layer = iRTE.CameraLayerSettings.ResourcePreviewLayer;
				obj.transform.rotation = Quaternion.Euler(30f, 0f, 0f);
				Light light = obj.AddComponent<Light>();
				light.type = LightType.Directional;
				light.cullingMask = 1 << iRTE.CameraLayerSettings.ResourcePreviewLayer;
			}
			m_materialPreviewSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			m_materialPreviewSphere.transform.SetParent(base.transform, worldPositionStays: false);
			m_materialPreviewSphere.transform.position = Vector3.zero;
			m_materialPreviewSphere.SetActive(value: false);
			m_materialPreviewRenderer = m_materialPreviewSphere.GetComponent<MeshRenderer>();
		}

		private void OnDestroy()
		{
			if (m_materialPreviewSphere != null)
			{
				UnityEngine.Object.Destroy(m_materialPreviewSphere);
			}
		}

		public virtual bool CanCreatePreview(UnityEngine.Object obj)
		{
			if (!(obj is GameObject) && !(obj is Material) && !(obj is Texture2D))
			{
				return obj is Sprite;
			}
			return true;
		}

		public virtual Texture2D CreatePreview(UnityEngine.Object obj, bool instantiate)
		{
			Texture2D result = null;
			if (obj is GameObject)
			{
				GameObject obj2 = (GameObject)obj;
				result = CreatePreview(obj2, instantiate);
			}
			else if (obj is Material)
			{
				Material material = (Material)obj;
				Shader shader = material.shader;
				int num;
				if (shader != null)
				{
					num = (shader.name.StartsWith("Particles/") ? 1 : 0);
					if (num != 0)
					{
						material.shader = m_unlitTexShader;
					}
				}
				else
				{
					num = 0;
				}
				m_materialPreviewRenderer.sharedMaterial = material;
				m_materialPreviewSphere.transform.position = Vector3.zero;
				result = CreatePreview(m_materialPreviewSphere, instantiate: false);
				if (num != 0)
				{
					material.shader = shader;
				}
			}
			else if (obj is Texture2D)
			{
				Texture2D texture2D = (Texture2D)obj;
				bool isReadable = texture2D.isReadable;
				bool flag = texture2D.format == TextureFormat.ARGB32 || texture2D.format == TextureFormat.RGBA32 || texture2D.format == TextureFormat.RGB24 || texture2D.format == TextureFormat.Alpha8;
				if (isReadable && flag)
				{
					if (instantiate)
					{
						texture2D = UnityEngine.Object.Instantiate(texture2D);
					}
				}
				else
				{
					texture2D = texture2D.DeCompress();
				}
				float num2 = (float)(texture2D.width * m_objectToTextureCamera.snapshotTextureHeight) / (float)Mathf.Max(1, texture2D.height * m_objectToTextureCamera.snapshotTextureWidth);
				TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)m_objectToTextureCamera.snapshotTextureWidth * num2), m_objectToTextureCamera.snapshotTextureHeight);
				result = texture2D;
			}
			else if (obj is Sprite)
			{
				Sprite sprite = (Sprite)obj;
				result = FromSprite(sprite);
			}
			return result;
		}

		public byte[] CreatePreviewData(UnityEngine.Object obj, bool instantiate = true)
		{
			Texture2D texture2D = CreatePreview(obj, instantiate);
			byte[] result;
			if (texture2D != null)
			{
				result = texture2D.EncodeToPNG();
				UnityEngine.Object.Destroy(texture2D);
			}
			else
			{
				result = new byte[0];
			}
			return result;
		}

		public Texture2D CreatePreview(GameObject obj, bool instantiate)
		{
			m_objectToTextureCamera.gameObject.SetActive(value: true);
			Texture2D result = m_objectToTextureCamera.TakeObjectSnapshot(obj, m_fallbackPrefab, m_objectToTextureCamera.defaultPosition, Quaternion.Euler(m_objectToTextureCamera.defaultRotation), m_previewObjectScale, m_previewScale, instantiate);
			m_objectToTextureCamera.gameObject.SetActive(value: false);
			return result;
		}

		private Texture2D FromSprite(Sprite sprite)
		{
			if (sprite.texture != null && sprite.texture.isReadable)
			{
				Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
				Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
				texture2D.SetPixels(pixels);
				texture2D.Reinitialize(m_objectToTextureCamera.snapshotTextureWidth, m_objectToTextureCamera.snapshotTextureHeight);
				return texture2D;
			}
			return null;
		}

		[Obsolete]
		public Texture2D TakeSnapshot(GameObject go)
		{
			return CreatePreview(go, instantiate: true);
		}
	}
}
