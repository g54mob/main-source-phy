using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

[RequireComponent(typeof(Canvas))]
[ExecuteAlways]
public class OVROverlayCanvas : OVRRayTransformer
{
	public enum DrawMode
	{
		Opaque = 0,
		OpaqueWithClip = 1,
		TransparentDefaultAlpha = 2,
		TransparentCorrectAlpha = 3,
		AlphaToMask = 4
	}

	public enum CanvasShape
	{
		Flat = 0,
		Curved = 1
	}

	private const float kOptimalResolutionScale = 2f;

	private RectTransform _rectTransform;

	private Canvas _canvas;

	private Camera _camera;

	private OVROverlay _overlay;

	private MeshRenderer _meshRenderer;

	private OVROverlayMeshGenerator _meshGenerator;

	private RenderTexture _renderTexture;

	private Material _imposterMaterial;

	private bool _optimalResolutionInitialized;

	private float _optimalResolutionWidth;

	private float _optimalResolutionHeight;

	private int _lastPixelWidth;

	private int _lastPixelHeight;

	private Vector2 _imposterTextureOffset;

	private Vector2 _imposterTextureScale;

	private bool _hasRenderedFirstFrame;

	private bool _useTempRT;

	private readonly bool _scaleViewport = Application.isMobilePlatform;

	[FormerlySerializedAs("MaxTextureSize")]
	public int maxTextureSize = 2048;

	[FormerlySerializedAs("DrawRate")]
	public int renderInterval = 1;

	[FormerlySerializedAs("DrawFrameOffset")]
	public int renderIntervalFrameOffset;

	[FormerlySerializedAs("Expensive")]
	public bool expensive;

	[FormerlySerializedAs("Layer")]
	public int layer = 5;

	[FormerlySerializedAs("Opacity")]
	public DrawMode opacity = DrawMode.TransparentDefaultAlpha;

	public CanvasShape shape;

	public float curveRadius = 1f;

	public bool overlapMask;

	[SerializeField]
	private bool _overlayEnabled = true;

	private static readonly Plane[] _FrustumPlanes = new Plane[6];

	private static readonly Vector3[] _Corners = new Vector3[4];

	public bool overlayEnabled
	{
		get
		{
			return _overlayEnabled;
		}
		set
		{
			if ((bool)_overlay && Application.isPlaying)
			{
				_overlay.enabled = value;
				_imposterMaterial.color = (value ? Color.black : Color.white);
			}
			_overlayEnabled = value;
		}
	}

	private void Start()
	{
		_canvas = GetComponent<Canvas>();
		_rectTransform = _canvas.GetComponent<RectTransform>();
		HideFlags hideFlags = HideFlags.HideAndDontSave;
		GameObject gameObject = new GameObject(base.name + " Overlay Camera")
		{
			hideFlags = hideFlags
		};
		gameObject.transform.SetParent(base.transform, worldPositionStays: false);
		_camera = gameObject.AddComponent<Camera>();
		_camera.stereoTargetEye = StereoTargetEyeMask.None;
		_camera.transform.position = base.transform.position - base.transform.forward;
		_camera.orthographic = true;
		_camera.enabled = false;
		_camera.clearFlags = CameraClearFlags.Color;
		_camera.backgroundColor = Color.clear;
		_camera.nearClipPlane = 0.99f;
		_camera.farClipPlane = 1.01f;
		GameObject gameObject2 = new GameObject(base.name + " Imposter")
		{
			hideFlags = hideFlags
		};
		gameObject2.transform.SetParent(base.transform, worldPositionStays: false);
		gameObject2.AddComponent<MeshFilter>();
		_meshRenderer = gameObject2.AddComponent<MeshRenderer>();
		_meshGenerator = gameObject2.AddComponent<OVROverlayMeshGenerator>();
		GameObject gameObject3 = new GameObject(base.name + " Overlay")
		{
			hideFlags = hideFlags
		};
		gameObject3.transform.SetParent(base.transform, worldPositionStays: false);
		_overlay = gameObject3.AddComponent<OVROverlay>();
		_overlay.enabled = false;
		_overlay.isDynamic = true;
		_overlay.noDepthBufferTesting = true;
		_overlay.isAlphaPremultiplied = true;
		_overlay.currentOverlayType = OVROverlay.OverlayType.Underlay;
		_useTempRT = Application.isMobilePlatform;
		InitializeRenderTexture();
	}

	private void InitializeRenderTexture()
	{
		float width = _rectTransform.rect.width;
		float height = _rectTransform.rect.height;
		float num = ((width >= height) ? 1f : (width / height));
		float num2 = ((height >= width) ? 1f : (height / width));
		int num3 = ((!_scaleViewport) ? 8 : 0);
		int num4 = Mathf.CeilToInt(num * (float)(maxTextureSize - num3 * 2));
		int num5 = Mathf.CeilToInt(num2 * (float)(maxTextureSize - num3 * 2));
		int num6 = num4 + num3 * 2;
		int num7 = num5 + num3 * 2;
		float x = width * ((float)num6 / (float)num4);
		float num8 = height * ((float)num7 / (float)num5);
		float num9 = (float)num4 / (float)num6;
		float num10 = (float)num5 / (float)num7;
		_imposterTextureOffset = new Vector2(0.5f - 0.5f * num9, 0.5f - 0.5f * num10);
		_imposterTextureScale = new Vector2(num9, num10);
		if (_renderTexture == null || _renderTexture.width != num6 || _renderTexture.height != num7)
		{
			if (_renderTexture != null)
			{
				UnityEngine.Object.DestroyImmediate(_renderTexture);
			}
			_renderTexture = new RenderTexture(num6, num7, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			_renderTexture.useMipMap = !_scaleViewport;
			_renderTexture.filterMode = FilterMode.Trilinear;
		}
		_camera.orthographicSize = 0.5f * num8 * _rectTransform.localScale.y;
		_camera.targetTexture = _renderTexture;
		_camera.cullingMask = 1 << base.gameObject.layer;
		Shader shader = OVROverlayCanvasSettings.Instance.GetShader(opacity);
		if (_imposterMaterial == null)
		{
			_imposterMaterial = new Material(shader);
		}
		else
		{
			_imposterMaterial.shader = shader;
		}
		if (opacity == DrawMode.OpaqueWithClip)
		{
			_imposterMaterial.EnableKeyword("WITH_CLIP");
		}
		else
		{
			_imposterMaterial.DisableKeyword("WITH_CLIP");
		}
		if (opacity == DrawMode.TransparentDefaultAlpha)
		{
			_imposterMaterial.EnableKeyword("ALPHA_SQUARED");
		}
		else
		{
			_imposterMaterial.DisableKeyword("ALPHA_SQUARED");
		}
		if (expensive)
		{
			_imposterMaterial.EnableKeyword("EXPENSIVE");
		}
		else
		{
			_imposterMaterial.DisableKeyword("EXPENSIVE");
		}
		if (opacity == DrawMode.AlphaToMask)
		{
			_imposterMaterial.EnableKeyword("ALPHA_TO_MASK");
			_imposterMaterial.SetInt("_AlphaToMask", 1);
		}
		else
		{
			_imposterMaterial.DisableKeyword("ALPHA_TO_MASK");
			_imposterMaterial.SetInt("_AlphaToMask", 0);
		}
		if (overlapMask)
		{
			_imposterMaterial.EnableKeyword("OVERLAP_MASK");
		}
		else
		{
			_imposterMaterial.DisableKeyword("OVERLAP_MASK");
		}
		_imposterMaterial.mainTexture = _renderTexture;
		_imposterMaterial.color = Color.black;
		_imposterMaterial.mainTextureOffset = _imposterTextureOffset;
		_imposterMaterial.mainTextureScale = _imposterTextureScale;
		_meshRenderer.sharedMaterial = _imposterMaterial;
		_meshRenderer.gameObject.layer = layer;
		if (shape == CanvasShape.Flat)
		{
			Transform obj = _meshRenderer.transform;
			Vector3 localPosition = (_overlay.transform.localPosition = Vector3.zero);
			obj.localPosition = localPosition;
			_meshRenderer.transform.localScale = new Vector3(width, height, 1f);
			_overlay.transform.localScale = new Vector3(x, num8, 1f);
		}
		else
		{
			Transform obj2 = _meshRenderer.transform;
			Vector3 localPosition = (_overlay.transform.localPosition = new Vector3(0f, 0f, (0f - curveRadius) / base.transform.lossyScale.z));
			obj2.localPosition = localPosition;
			_meshRenderer.transform.localScale = new Vector3(width, height, curveRadius / base.transform.lossyScale.z);
			_overlay.transform.localScale = new Vector3(x, num8, curveRadius / base.transform.lossyScale.z);
		}
		_overlay.textures[0] = _renderTexture;
		_overlay.currentOverlayShape = ((shape != CanvasShape.Flat) ? OVROverlay.OverlayShape.Cylinder : OVROverlay.OverlayShape.Quad);
		_overlay.useExpensiveSuperSample = expensive;
		_overlay.enabled = Application.isPlaying && _overlayEnabled;
		_meshGenerator.SetOverlay(_overlay);
		OVROverlayCanvasSettings.Instance.ApplyGlobalSettings();
	}

	private void OnDestroy()
	{
		if (Application.isPlaying)
		{
			UnityEngine.Object.Destroy(_imposterMaterial);
			UnityEngine.Object.Destroy(_renderTexture);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(_imposterMaterial);
			UnityEngine.Object.DestroyImmediate(_renderTexture);
		}
	}

	private void OnEnable()
	{
		if ((bool)_overlay)
		{
			_meshRenderer.enabled = true;
			_overlay.enabled = Application.isPlaying && _overlayEnabled;
		}
	}

	private void OnDisable()
	{
		if ((bool)_overlay)
		{
			_overlay.enabled = false;
			_meshRenderer.enabled = false;
		}
	}

	protected virtual bool ShouldRender()
	{
		if (renderInterval > 1 && Time.frameCount % renderInterval != renderIntervalFrameOffset % renderInterval && _hasRenderedFirstFrame)
		{
			return false;
		}
		if (Application.isEditor)
		{
			return true;
		}
		Camera camera = OVRManager.FindMainCamera();
		if (camera != null)
		{
			if (camera.stereoEnabled)
			{
				for (int i = 0; i < 2; i++)
				{
					Camera.StereoscopicEye eye = (Camera.StereoscopicEye)i;
					GeometryUtility.CalculateFrustumPlanes(camera.GetStereoProjectionMatrix(eye) * camera.GetStereoViewMatrix(eye), _FrustumPlanes);
					if (GeometryUtility.TestPlanesAABB(_FrustumPlanes, _meshRenderer.bounds))
					{
						return true;
					}
				}
			}
			else
			{
				GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix, _FrustumPlanes);
				if (GeometryUtility.TestPlanesAABB(_FrustumPlanes, _meshRenderer.bounds))
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	private void Update()
	{
		if (ShouldRender())
		{
			ApplyViewportScale();
			_hasRenderedFirstFrame = true;
			RenderCamera();
		}
	}

	private void LateUpdate()
	{
		_imposterMaterial.color = ((_overlay.enabled && _overlay.isOverlayVisible) ? Color.black : Color.white);
		_imposterMaterial.mainTextureScale = _imposterTextureScale;
		_imposterMaterial.mainTextureOffset = _imposterTextureOffset;
	}

	private void OnValidate()
	{
	}

	private void ApplyViewportScale()
	{
		if (!_scaleViewport)
		{
			return;
		}
		Camera camera = OVRManager.FindMainCamera();
		if (!(camera == null))
		{
			if (!_optimalResolutionInitialized && XRSettings.isDeviceActive)
			{
				_optimalResolutionWidth = (float)XRSettings.eyeTextureWidth * 2f / XRSettings.eyeTextureResolutionScale;
				_optimalResolutionHeight = (float)XRSettings.eyeTextureHeight * 2f / XRSettings.eyeTextureResolutionScale;
				_optimalResolutionInitialized = _optimalResolutionWidth > 0f && _optimalResolutionHeight > 0f;
			}
			_rectTransform.GetLocalCorners(_Corners);
			Matrix4x4 localToWorldMatrix = _rectTransform.localToWorldMatrix;
			if (shape == CanvasShape.Curved)
			{
				localToWorldMatrix *= CalculateCurveViewBillboardMatrix(camera);
			}
			Matrix4x4 matrix4x = camera.projectionMatrix * camera.worldToCameraMatrix;
			Matrix4x4 matrix4x2 = Matrix4x4.Scale(new Vector3(0.5f * _optimalResolutionWidth, 0.5f * _optimalResolutionHeight, 0f)) * matrix4x * localToWorldMatrix;
			for (int i = 0; i < 4; i++)
			{
				_Corners[i] = matrix4x2.MultiplyPoint(_Corners[i]);
			}
			int num = Mathf.RoundToInt(Mathf.Max((_Corners[1] - _Corners[0]).magnitude, (_Corners[3] - _Corners[2]).magnitude));
			int num2 = Mathf.RoundToInt(Mathf.Max((_Corners[2] - _Corners[1]).magnitude, (_Corners[3] - _Corners[0]).magnitude));
			int a = (num + 1) / 2 * 2 * ((!expensive) ? 1 : 2) + 4;
			int a2 = (num2 + 1) / 2 * 2 * ((!expensive) ? 1 : 2) + 4;
			a = Mathf.Min(a, _renderTexture.height);
			a2 = Mathf.Min(a2, _renderTexture.width);
			if (Math.Abs(a - _lastPixelHeight) < 4 && Math.Abs(a2 - _lastPixelWidth) < 4)
			{
				a2 = _lastPixelWidth;
				a = _lastPixelHeight;
			}
			else
			{
				_lastPixelHeight = a;
				_lastPixelWidth = a2;
			}
			int num3 = a - 4;
			int num4 = a2 - 4;
			float num5 = _rectTransform.rect.height * _rectTransform.localScale.y * (float)a / (float)num3;
			float num6 = _rectTransform.rect.width * _rectTransform.localScale.x * (float)a2 / (float)num4;
			_camera.orthographicSize = 0.5f * num5;
			_camera.aspect = num6 / num5;
			float num7 = (float)a2 / (float)_renderTexture.width;
			float num8 = (float)a / (float)_renderTexture.height;
			float num9 = (float)num4 / (float)_renderTexture.width;
			float num10 = (float)num3 / (float)_renderTexture.height;
			_camera.rect = new Rect((1f - num7) / 2f, (1f - num8) / 2f, num7, num8);
			Rect rect = new Rect(0.5f - 0.5f * num9, 0.5f - 0.5f * num10, num9, num10);
			Rect rect2 = new Rect(0f, 0f, 1f, 1f);
			_overlay.overrideTextureRectMatrix = true;
			_overlay.SetSrcDestRects(rect, rect, rect2, rect2);
			_imposterTextureOffset = rect.min;
			_imposterTextureScale = rect.size;
		}
	}

	private void RenderCamera()
	{
		Rect rect = _camera.rect;
		int num = (int)(rect.width * (float)_renderTexture.width);
		int num2 = (int)(rect.height * (float)_renderTexture.height);
		if (_useTempRT && (num < _renderTexture.width || num2 < _renderTexture.height))
		{
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			_camera.targetTexture = temporary;
			_camera.rect = new Rect(0f, 0f, 1f, 1f);
			_camera.Render();
			Graphics.CopyTexture(temporary, 0, 0, 0, 0, num, num2, _renderTexture, 0, 0, (int)(rect.x * (float)_renderTexture.width), (int)(rect.y * (float)_renderTexture.height));
			RenderTexture.ReleaseTemporary(temporary);
			_camera.rect = rect;
			_camera.targetTexture = _renderTexture;
		}
		else
		{
			_camera.Render();
		}
	}

	private Matrix4x4 CalculateCurveViewBillboardMatrix(Camera mainCamera)
	{
		Vector3 vector = Quaternion.Inverse(_rectTransform.rotation) * (mainCamera.transform.position - _rectTransform.position);
		float value = Mathf.Atan2(0f - vector.x, 0f - vector.z);
		Vector3 lossyScale = _rectTransform.lossyScale;
		float num = _rectTransform.rect.width * lossyScale.x / curveRadius;
		value = Mathf.Clamp(value, -0.5f * num, 0.5f * num);
		Vector3 vector2 = new Vector3(value * curveRadius, 0f, 0f);
		Vector3 vector3 = new Vector3(0f, 0f, curveRadius);
		return Matrix4x4.Scale(new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z)) * Matrix4x4.Translate(-vector3) * Matrix4x4.Rotate(Quaternion.AngleAxis(57.29578f * value, Vector3.up)) * Matrix4x4.Translate(vector3 - vector2) * Matrix4x4.Scale(new Vector3(lossyScale.x, lossyScale.y, 1f));
	}

	public override Ray TransformRay(Ray ray)
	{
		if (shape != CanvasShape.Curved)
		{
			return ray;
		}
		Vector3 vector = base.transform.InverseTransformPoint(ray.origin);
		Vector3 vector2 = base.transform.InverseTransformDirection(ray.direction);
		float num = curveRadius / base.transform.lossyScale.z;
		Vector3 vector3 = new Vector3(0f, 0f, 0f - num);
		if (!LineCircleIntersection(new Vector2(vector.x, vector.z), new Vector2(vector2.x, vector2.z), new Vector2(vector3.x, vector3.z), num, out var distance))
		{
			return new Ray(ray.origin, base.transform.right);
		}
		Vector3 vector4 = vector + vector2 * distance;
		float x = Mathf.Atan2(vector4.x, vector4.z + num) * num;
		float y = vector4.y;
		return new Ray(base.transform.TransformPoint(new Vector3(x, y, -1f)), base.transform.forward);
	}

	private static bool LineCircleIntersection(Vector2 p1, Vector2 dp, Vector2 center, float radius, out float distance)
	{
		float sqrMagnitude = dp.sqrMagnitude;
		float num = 2f * Vector2.Dot(dp, p1 - center);
		float sqrMagnitude2 = center.sqrMagnitude;
		sqrMagnitude2 += p1.sqrMagnitude;
		sqrMagnitude2 -= 2f * Vector2.Dot(center, p1);
		sqrMagnitude2 -= radius * radius;
		float num2 = num * num - 4f * sqrMagnitude * sqrMagnitude2;
		if (Mathf.Abs(sqrMagnitude) < float.Epsilon || num2 < 0f)
		{
			distance = 0f;
			return false;
		}
		float num3 = (0f - num - Mathf.Sqrt(num2)) / (2f * sqrMagnitude);
		float num4 = (0f - num + Mathf.Sqrt(num2)) / (2f * sqrMagnitude);
		distance = ((num3 >= 0f) ? num3 : num4);
		return true;
	}
}
