using System;
using System.Collections.Generic;
using UnityEngine;

public static class RuntimePreviewGenerator
{
	private struct ProjectionPlane
	{
		private readonly Vector3 m_Normal;

		private readonly float m_Distance;

		public ProjectionPlane(Vector3 inNormal, Vector3 inPoint)
		{
			m_Normal = Vector3.Normalize(inNormal);
			m_Distance = 0f - Vector3.Dot(inNormal, inPoint);
		}

		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			float num = Vector3.Dot(m_Normal, point) + m_Distance;
			return point - m_Normal * num;
		}

		public float GetDistanceToPoint(Vector3 point)
		{
			float num = Vector3.Dot(m_Normal, point) + m_Distance;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}
	}

	private class CameraSetup
	{
		private Vector3 position;

		private Quaternion rotation;

		private RenderTexture targetTexture;

		private Color backgroundColor;

		private bool orthographic;

		private float orthographicSize;

		private float nearClipPlane;

		private float farClipPlane;

		private float aspect;

		private CameraClearFlags clearFlags;

		public void GetSetup(Camera camera)
		{
			position = camera.transform.position;
			rotation = camera.transform.rotation;
			targetTexture = camera.targetTexture;
			backgroundColor = camera.backgroundColor;
			orthographic = camera.orthographic;
			orthographicSize = camera.orthographicSize;
			nearClipPlane = camera.nearClipPlane;
			farClipPlane = camera.farClipPlane;
			aspect = camera.aspect;
			clearFlags = camera.clearFlags;
		}

		public void ApplySetup(Camera camera)
		{
			camera.transform.position = position;
			camera.transform.rotation = rotation;
			camera.targetTexture = targetTexture;
			camera.backgroundColor = backgroundColor;
			camera.orthographic = orthographic;
			camera.orthographicSize = orthographicSize;
			camera.nearClipPlane = nearClipPlane;
			camera.farClipPlane = farClipPlane;
			camera.aspect = aspect;
			camera.clearFlags = clearFlags;
			targetTexture = null;
		}
	}

	private const int PREVIEW_LAYER = 22;

	private static Vector3 PREVIEW_POSITION;

	private static Camera renderCamera;

	private static CameraSetup cameraSetup;

	private static List<Renderer> renderersList;

	private static List<int> layersList;

	private static float aspect;

	private static float minX;

	private static float maxX;

	private static float minY;

	private static float maxY;

	private static float maxDistance;

	private static Vector3 boundsCenter;

	private static ProjectionPlane projectionPlaneHorizontal;

	private static ProjectionPlane projectionPlaneVertical;

	private static Camera m_internalCamera;

	private static Camera m_previewRenderCamera;

	private static Vector3 m_previewDirection;

	private static float m_padding;

	private static Color m_backgroundColor;

	private static bool m_orthographicMode;

	private static bool m_markTextureNonReadable;

	private static Camera InternalCamera
	{
		get
		{
			if (m_internalCamera == null)
			{
				m_internalCamera = new GameObject("ModelPreviewGeneratorCamera").AddComponent<Camera>();
				m_internalCamera.enabled = false;
				m_internalCamera.nearClipPlane = 0.01f;
				m_internalCamera.cullingMask = 4194304;
				m_internalCamera.gameObject.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_internalCamera;
		}
	}

	public static Camera PreviewRenderCamera
	{
		get
		{
			return m_previewRenderCamera;
		}
		set
		{
			m_previewRenderCamera = value;
		}
	}

	public static Vector3 PreviewDirection
	{
		get
		{
			return m_previewDirection;
		}
		set
		{
			m_previewDirection = value.normalized;
		}
	}

	public static float Padding
	{
		get
		{
			return m_padding;
		}
		set
		{
			m_padding = Mathf.Clamp(value, -0.25f, 0.25f);
		}
	}

	public static Color BackgroundColor
	{
		get
		{
			return m_backgroundColor;
		}
		set
		{
			m_backgroundColor = value;
		}
	}

	public static bool OrthographicMode
	{
		get
		{
			return m_orthographicMode;
		}
		set
		{
			m_orthographicMode = value;
		}
	}

	public static bool MarkTextureNonReadable
	{
		get
		{
			return m_markTextureNonReadable;
		}
		set
		{
			m_markTextureNonReadable = value;
		}
	}

	static RuntimePreviewGenerator()
	{
		PREVIEW_POSITION = new Vector3(-9245f, 9899f, -9356f);
		cameraSetup = new CameraSetup();
		renderersList = new List<Renderer>(64);
		layersList = new List<int>(64);
		m_internalCamera = null;
		PreviewRenderCamera = null;
		PreviewDirection = new Vector3(-1f, -1f, -1f);
		Padding = 0f;
		BackgroundColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		OrthographicMode = false;
		MarkTextureNonReadable = true;
	}

	public static Texture2D GenerateMaterialPreview(Material material, PrimitiveType previewObject, int width = 64, int height = 64)
	{
		return GenerateMaterialPreviewWithShader(material, previewObject, null, null, width, height);
	}

	public static Texture2D GenerateMaterialPreviewWithShader(Material material, PrimitiveType previewPrimitive, Shader shader, string replacementTag, int width = 64, int height = 64)
	{
		GameObject gameObject = GameObject.CreatePrimitive(previewPrimitive);
		gameObject.gameObject.hideFlags = HideFlags.HideAndDontSave;
		gameObject.GetComponent<Renderer>().sharedMaterial = material;
		try
		{
			return GenerateModelPreviewWithShader(gameObject.transform, shader, replacementTag, width, height);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		finally
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		return null;
	}

	public static Texture2D GenerateModelPreview(Transform model, int width = 64, int height = 64, bool shouldCloneModel = false)
	{
		return GenerateModelPreviewWithShader(model, null, null, width, height, shouldCloneModel);
	}

	public static Texture2D GenerateModelPreviewWithShader(Transform model, Shader shader, string replacementTag, int width = 64, int height = 64, bool shouldCloneModel = false)
	{
		if (model == null || model.Equals(null))
		{
			return null;
		}
		Texture2D texture2D = null;
		if (!model.gameObject.scene.IsValid() || !model.gameObject.scene.isLoaded)
		{
			shouldCloneModel = true;
		}
		Transform transform;
		if (shouldCloneModel)
		{
			transform = UnityEngine.Object.Instantiate(model, null, worldPositionStays: false);
			transform.gameObject.hideFlags = HideFlags.HideAndDontSave;
		}
		else
		{
			transform = model;
			layersList.Clear();
			GetLayerRecursively(transform);
		}
		bool flag = IsStatic(model);
		bool activeSelf = transform.gameObject.activeSelf;
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		try
		{
			SetupCamera();
			SetLayerRecursively(transform);
			if (!flag)
			{
				transform.position = PREVIEW_POSITION;
				transform.rotation = Quaternion.identity;
			}
			if (!activeSelf)
			{
				transform.gameObject.SetActive(value: true);
			}
			Vector3 vector = transform.rotation * m_previewDirection;
			renderersList.Clear();
			transform.GetComponentsInChildren(renderersList);
			Bounds bounds = default(Bounds);
			bool flag2 = false;
			for (int i = 0; i < renderersList.Count; i++)
			{
				if (renderersList[i].enabled)
				{
					if (!flag2)
					{
						bounds = renderersList[i].bounds;
						flag2 = true;
					}
					else
					{
						bounds.Encapsulate(renderersList[i].bounds);
					}
				}
			}
			if (!flag2)
			{
				return null;
			}
			boundsCenter = bounds.center;
			Vector3 extents = bounds.extents;
			Vector3 vector2 = 2f * extents;
			aspect = (float)width / (float)height;
			renderCamera.aspect = aspect;
			renderCamera.transform.rotation = Quaternion.LookRotation(vector, transform.up);
			float num;
			if (m_orthographicMode)
			{
				renderCamera.transform.position = boundsCenter;
				minX = (minY = float.PositiveInfinity);
				maxX = (maxY = float.NegativeInfinity);
				Vector3 point = boundsCenter + extents;
				ProjectBoundingBoxMinMax(point);
				point.x -= vector2.x;
				ProjectBoundingBoxMinMax(point);
				point.y -= vector2.y;
				ProjectBoundingBoxMinMax(point);
				point.x += vector2.x;
				ProjectBoundingBoxMinMax(point);
				point.z -= vector2.z;
				ProjectBoundingBoxMinMax(point);
				point.x -= vector2.x;
				ProjectBoundingBoxMinMax(point);
				point.y += vector2.y;
				ProjectBoundingBoxMinMax(point);
				point.x += vector2.x;
				ProjectBoundingBoxMinMax(point);
				num = extents.magnitude + 1f;
				renderCamera.orthographicSize = (1f + m_padding * 2f) * Mathf.Max(maxY - minY, (maxX - minX) / aspect) * 0.5f;
			}
			else
			{
				projectionPlaneHorizontal = new ProjectionPlane(renderCamera.transform.up, boundsCenter);
				projectionPlaneVertical = new ProjectionPlane(renderCamera.transform.right, boundsCenter);
				maxDistance = float.NegativeInfinity;
				Vector3 point2 = boundsCenter + extents;
				CalculateMaxDistance(point2);
				point2.x -= vector2.x;
				CalculateMaxDistance(point2);
				point2.y -= vector2.y;
				CalculateMaxDistance(point2);
				point2.x += vector2.x;
				CalculateMaxDistance(point2);
				point2.z -= vector2.z;
				CalculateMaxDistance(point2);
				point2.x -= vector2.x;
				CalculateMaxDistance(point2);
				point2.y += vector2.y;
				CalculateMaxDistance(point2);
				point2.x += vector2.x;
				CalculateMaxDistance(point2);
				num = (1f + m_padding * 2f) * Mathf.Sqrt(maxDistance);
			}
			renderCamera.transform.position = boundsCenter - vector * num;
			renderCamera.farClipPlane = num * 4f;
			RenderTexture active = RenderTexture.active;
			RenderTexture renderTexture = (RenderTexture.active = RenderTexture.GetTemporary(width, height, 16));
			if (m_backgroundColor.a < 1f)
			{
				GL.Clear(clearDepth: false, clearColor: true, m_backgroundColor);
			}
			renderCamera.targetTexture = renderTexture;
			if (shader == null)
			{
				renderCamera.Render();
			}
			else
			{
				renderCamera.RenderWithShader(shader, (replacementTag == null) ? string.Empty : replacementTag);
			}
			renderCamera.targetTexture = null;
			texture2D = new Texture2D(width, height, (m_backgroundColor.a < 1f) ? TextureFormat.RGBA32 : TextureFormat.RGB24, mipChain: false);
			texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0, recalculateMipMaps: false);
			texture2D.Apply(updateMipmaps: false, m_markTextureNonReadable);
			RenderTexture.active = active;
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		finally
		{
			if (shouldCloneModel)
			{
				UnityEngine.Object.DestroyImmediate(transform.gameObject);
			}
			else
			{
				if (!activeSelf)
				{
					transform.gameObject.SetActive(value: false);
				}
				if (!flag)
				{
					transform.position = position;
					transform.rotation = rotation;
				}
				int index = 0;
				SetLayerRecursively(transform, ref index);
			}
			if (renderCamera == m_previewRenderCamera)
			{
				cameraSetup.ApplySetup(renderCamera);
			}
		}
		return texture2D;
	}

	private static void SetupCamera()
	{
		if (m_previewRenderCamera != null && !m_previewRenderCamera.Equals(null))
		{
			cameraSetup.GetSetup(m_previewRenderCamera);
			renderCamera = m_previewRenderCamera;
			renderCamera.nearClipPlane = 0.01f;
		}
		else
		{
			renderCamera = InternalCamera;
		}
		renderCamera.backgroundColor = m_backgroundColor;
		renderCamera.orthographic = m_orthographicMode;
		renderCamera.clearFlags = ((m_backgroundColor.a < 1f) ? CameraClearFlags.Depth : CameraClearFlags.Color);
	}

	private static void ProjectBoundingBoxMinMax(Vector3 point)
	{
		Vector3 vector = renderCamera.transform.InverseTransformPoint(point);
		if (vector.x < minX)
		{
			minX = vector.x;
		}
		if (vector.x > maxX)
		{
			maxX = vector.x;
		}
		if (vector.y < minY)
		{
			minY = vector.y;
		}
		if (vector.y > maxY)
		{
			maxY = vector.y;
		}
	}

	private static void CalculateMaxDistance(Vector3 point)
	{
		Vector3 vector = projectionPlaneHorizontal.ClosestPointOnPlane(point);
		float num = Mathf.Max(b: projectionPlaneHorizontal.GetDistanceToPoint(point) / aspect, a: projectionPlaneVertical.GetDistanceToPoint(point)) / Mathf.Tan(renderCamera.fieldOfView * 0.5f * ((float)Math.PI / 180f));
		float sqrMagnitude = (vector - m_previewDirection * num - boundsCenter).sqrMagnitude;
		if (sqrMagnitude > maxDistance)
		{
			maxDistance = sqrMagnitude;
		}
	}

	private static bool IsStatic(Transform obj)
	{
		if (obj.gameObject.isStatic)
		{
			return true;
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			if (IsStatic(obj.GetChild(i)))
			{
				return true;
			}
		}
		return false;
	}

	private static void SetLayerRecursively(Transform obj)
	{
		obj.gameObject.layer = 22;
		for (int i = 0; i < obj.childCount; i++)
		{
			SetLayerRecursively(obj.GetChild(i));
		}
	}

	private static void GetLayerRecursively(Transform obj)
	{
		layersList.Add(obj.gameObject.layer);
		for (int i = 0; i < obj.childCount; i++)
		{
			GetLayerRecursively(obj.GetChild(i));
		}
	}

	private static void SetLayerRecursively(Transform obj, ref int index)
	{
		obj.gameObject.layer = layersList[index++];
		for (int i = 0; i < obj.childCount; i++)
		{
			SetLayerRecursively(obj.GetChild(i), ref index);
		}
	}
}
