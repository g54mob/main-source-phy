using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	public class RipplePostProcessing : MonoBehaviour
	{
		public const float MIN_SIZE = 60f;

		private const float MAX_RANGE = 200f;

		public GameObject border;

		public bool moveInIntervals = true;

		public static RipplePostProcessing Instance;

		private static Camera _source;

		private static Camera _camera;

		private static Transform _border;

		private RenderTexture _rendertex;

		private int _texID;

		private int _posID;

		private bool setupDone;

		private Plane plane = new Plane(Vector3.up, Vector3.zero);

		private float deg45 = 0.8f;

		private float deg45Scale = 5f;

		private Vector3 down = Vector3.down;

		private Vector4 shaderPos;

		public static bool Active;

		private ParticleSystem[] foamParticles;

		private ParticleSystem[] rippleParticles;

		private HashSet<ParticleSystem> extra = new HashSet<ParticleSystem>();

		public bool lockOnToRipples = true;

		private Bounds b;

		private WaterLod water;

		private Matrix4x4 VP;

		private ParticleSystem.Particle[] currentParticles = new ParticleSystem.Particle[10000];

		public void AddExtraParticles(ParticleSystem p)
		{
			if (!extra.Contains(p))
			{
				extra.Add(p);
			}
		}

		public void RemoveExtraParticles(ParticleSystem p)
		{
			if (extra.Contains(p))
			{
				extra.Remove(p);
			}
		}

		private void OnEnable()
		{
			if (!WaterController.Exist && !StatMaster.isMP)
			{
				OnDisable();
				base.enabled = false;
				return;
			}
			Active = true;
			b = new Bounds(Vector3.zero, Vector3.zero);
			plane.SetNormalAndPosition(plane.normal, Vector3.up * WaterController.waterTransformHeight);
			if (!Shader.IsKeywordEnabled("DisplayRipples"))
			{
				Shader.EnableKeyword("DisplayRipples");
			}
			if (setupDone && (bool)water && (bool)water.collisionPlane)
			{
				water.collisionPlane.enabled = true;
			}
		}

		private void OnDisable()
		{
			Active = false;
			if (Shader.IsKeywordEnabled("DisplayRipples"))
			{
				Shader.DisableKeyword("DisplayRipples");
			}
			if (setupDone && (bool)water && (bool)water.collisionPlane)
			{
				water.collisionPlane.enabled = false;
			}
			Cleanup();
		}

		private void OnDestroy()
		{
			Cleanup();
		}

		public void Awake()
		{
			if (setupDone)
			{
				return;
			}
			if (WaterController.Exist || StatMaster.isMP)
			{
				if (WaterController.waterTransformHeight == 0f)
				{
					WaterController waterController = Object.FindObjectOfType<WaterController>();
					if (!waterController && StatMaster.isMP)
					{
						LevelEditor levelEditor = LevelEditor.Instance;
						if (!levelEditor)
						{
							levelEditor = Object.FindObjectOfType<LevelEditor>();
						}
						waterController = levelEditor.environmentManager.GetEnv(LevelSettings.LevelEnvironment.Water).localGoalObj.GetComponentInChildren<WaterController>(true);
					}
					WaterController.waterTransform = waterController.transform;
					WaterController.waterTransformHeight = WaterController.waterTransform.position.y;
				}
				water = Object.FindObjectOfType<WaterLod>();
				if ((bool)water && (bool)water.collisionPlane)
				{
					water.collisionPlane.enabled = base.enabled;
				}
			}
			if (!base.enabled && Shader.IsKeywordEnabled("DisplayRipples"))
			{
				Shader.DisableKeyword("DisplayRipples");
			}
			setupDone = true;
			Instance = this;
			_source = Camera.main;
			deg45Scale = 1f / (1f - deg45);
			_texID = Shader.PropertyToID("_RippleTexture");
			_posID = Shader.PropertyToID("_RipplePosition");
			LayerMask layerMask = _source.cullingMask;
			if ((int)layerMask == ((int)layerMask | 8))
			{
				layerMask = (int)layerMask & -9;
				_source.cullingMask = layerMask;
			}
		}

		private void Cleanup()
		{
			if ((bool)_camera)
			{
				_camera.targetTexture = null;
				SafeDestroy(_camera.gameObject);
				_camera = null;
			}
			if ((bool)_rendertex)
			{
				_rendertex.DiscardContents();
				RenderTexture.ReleaseTemporary(_rendertex);
				_rendertex = null;
			}
		}

		private static void SafeDestroy(Object obj)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(obj);
			}
			else
			{
				Object.Destroy(obj);
			}
		}

		private Camera CreateRippleCam()
		{
			GameObject gameObject = new GameObject("Ripple Camera", typeof(Camera));
			Camera component = gameObject.GetComponent<Camera>();
			component.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
			component.depth = -11f;
			component.enabled = false;
			component.orthographic = true;
			component.orthographicSize = 30f;
			component.nearClipPlane = 1f;
			component.farClipPlane = 40f;
			component.cullingMask = AddPiece.CreateLayerMask(default(LayerMask), 3);
			component.backgroundColor = Color.black;
			component.clearFlags = CameraClearFlags.Color;
			component.renderingPath = RenderingPath.Forward;
			component.useOcclusionCulling = false;
			component.depthTextureMode = DepthTextureMode.Depth;
			if (component.orthographic)
			{
				_border = ((GameObject)Object.Instantiate(border, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform)).transform;
				_border.transform.localPosition = new Vector3(0f, 0f, 1f);
			}
			if (!GlobalParticles.GetParticleSystem(14, out foamParticles))
			{
				Debug.LogError("No Foam Particles found");
			}
			if (!GlobalParticles.GetParticleSystem(15, out rippleParticles))
			{
				Debug.LogError("No Foam Particles found");
			}
			return component;
		}

		private void GetRenderTexture(int size)
		{
			if (_rendertex == null)
			{
				Vector2 res = GetRes(_camera, size);
				if (SystemInfo.graphicsShaderLevel > 30)
				{
					_rendertex = RenderTexture.GetTemporary((int)res.x, (int)res.y, 0, RenderTextureFormat.RGB111110Float);
				}
				else
				{
					_rendertex = RenderTexture.GetTemporary((int)res.x / 2, (int)res.y / 2, 0, RenderTextureFormat.Default);
				}
				_rendertex.wrapMode = TextureWrapMode.Clamp;
				_rendertex.filterMode = FilterMode.Trilinear;
				_rendertex.anisoLevel = 16;
				_rendertex.antiAliasing = 8;
			}
			_camera.targetTexture = _rendertex;
		}

		private Vector2 GetRes(Camera cam, float size)
		{
			float x = ((!cam.orthographic) ? ((float)(int)(size * (float)cam.pixelWidth / (float)cam.pixelHeight)) : size);
			return new Vector2(x, size);
		}

		private void LateUpdate()
		{
			if (WaterController.Exist && SystemInfo.supportsRenderTextures)
			{
				if (_camera == null)
				{
					_camera = CreateRippleCam();
				}
				GetRenderTexture(512);
				if (lockOnToRipples)
				{
					ScaleAndMoveCam();
				}
				else
				{
					ScaleCam();
					MoveCam();
				}
				_camera.Render();
				Shader.SetGlobalTexture(_texID, _rendertex);
			}
		}

		protected void ScaleCam()
		{
			if (_camera.orthographic)
			{
				Vector3 forward = _source.transform.forward;
				forward.y = 0f;
				forward = forward.normalized;
				forward *= _source.farClipPlane;
				Vector3 position = _source.transform.position;
				position.y = WaterController.waterTransformHeight;
				position += forward;
				position = _source.WorldToViewportPoint(position);
				position.y = Mathf.Clamp01(position.y);
				Vector3 vector = RaycastOnPlane(new Vector2(0f, 0f));
				Vector3 vector2 = RaycastOnPlane(new Vector2(1f, 0f));
				Vector3 vector3 = RaycastOnPlane(new Vector2(0f, 1f));
				Vector3 vector4 = RaycastOnPlane(new Vector2(1f, 1f));
				float num = Vector3.Distance(vector3, vector4);
				float num2 = Vector3.Distance(vector, vector2);
				float num3 = (1f - Mathf.Abs(Vector3.Dot(base.transform.forward, down))) * 4f;
				if (num > num2)
				{
					Vector3 vector5 = RaycastOnPlane(new Vector2(0.5f, (0f + position.y) * 0.5f));
					Vector3 vector6 = (vector + vector2) * 0.5f;
					Vector3 vector7 = vector6 - vector5;
					vector7 = Vector3.ClampMagnitude(-vector7 * (2f + num3), _source.farClipPlane);
					Vector3 position2 = _source.WorldToViewportPoint(vector + vector7);
					position2.x = 0f;
					Vector3 position3 = _source.WorldToViewportPoint(vector2 + vector7);
					position3.x = 1f;
					vector3 = _source.ViewportToWorldPoint(position2);
					vector4 = _source.ViewportToWorldPoint(position3);
				}
				else
				{
					Vector3 vector5 = RaycastOnPlane(new Vector2(0.5f, (1f + position.y) * 0.5f));
					Vector3 vector8 = (vector3 + vector4) * 0.5f;
					Vector3 vector7 = vector8 - vector5;
					vector7 = Vector3.ClampMagnitude(-vector7 * (2f + num3), _source.farClipPlane);
					Vector3 position4 = _source.WorldToViewportPoint(vector3 + vector7);
					position4.x = 0f;
					Vector3 position5 = _source.WorldToViewportPoint(vector4 + vector7);
					position5.x = 1f;
					vector = _source.ViewportToWorldPoint(position4);
					vector2 = _source.ViewportToWorldPoint(position5);
				}
				num = Vector3.Distance(vector3, vector4);
				num2 = Vector3.Distance(vector, vector2);
				float num4 = Mathf.Max(num, num2);
				if (moveInIntervals)
				{
					num4 = Mathf.Round(num4 * 0.2f) * 5f;
				}
				num4 = Mathf.Max(num4, 60f);
				_camera.orthographicSize = num4 * 0.5f;
				_border.localScale = Vector3.one * num4;
			}
		}

		private void ScaleAndMoveCam()
		{
			GetBounds();
			float num = Mathf.Max(b.extents.x, b.extents.z);
			Vector3 center = b.center;
			center.y = 20f + WaterController.waterTransformHeight;
			if (moveInIntervals)
			{
				num = Mathf.Round(num * 0.2f) * 5f;
				float num2 = _camera.orthographicSize * 2f / (float)_rendertex.width;
				center.x = Mathf.Round(center.x / num2) * num2;
				center.z = Mathf.Round(center.z / num2) * num2;
			}
			num = Mathf.Max(num, 60f);
			if (num != _camera.orthographicSize || _camera.transform.position != center)
			{
				_camera.orthographicSize = num;
				_border.localScale = Vector3.one * num * 2f;
				_camera.transform.position = center;
				shaderPos = center;
				shaderPos.w = num * 2f;
				Shader.SetGlobalVector(_posID, shaderPos);
			}
		}

		private void GetBounds()
		{
			b.center = Vector3.zero;
			b.extents = Vector3.zero;
			Matrix4x4 worldToCameraMatrix = _source.worldToCameraMatrix;
			Matrix4x4 projectionMatrix = _source.projectionMatrix;
			VP = projectionMatrix * worldToCameraMatrix;
			Vector3 min = new Vector3(float.MaxValue, 0f, float.MaxValue);
			Vector3 max = new Vector3(float.MinValue, 0f, float.MinValue);
			Vector3 avg = Vector3.zero;
			float n = 0f;
			EncapsulateParticles(foamParticles[0], ref min, ref max, ref avg, ref n);
			EncapsulateParticles(rippleParticles[0], ref min, ref max, ref avg, ref n);
			foreach (ParticleSystem item in extra)
			{
				EncapsulateParticles(item, ref min, ref max, ref avg, ref n);
			}
			if (max.x > float.MinValue)
			{
				Vector3 vector = (min + max) * 0.5f;
				Vector3 vector2 = Vector3.zero;
				if (n > 0f)
				{
					avg /= n;
					vector2 = vector - avg;
					vector2.x = Math.Abs(vector2.x);
					vector2.z = Math.Abs(vector2.z);
					vector2.y = 0f;
					vector = (avg + vector) * 0.5f;
				}
				b.center = vector;
				min -= b.center;
				max -= b.center;
				max.x = Mathf.Max(Mathf.Abs(min.x), Mathf.Abs(max.x));
				max.z = Mathf.Max(Mathf.Abs(min.z), Mathf.Abs(max.z));
				b.extents = max - vector2 * 0.5f;
				b.extents += 10f * Vector3.one;
			}
		}

		private void EncapsulateParticles(ParticleSystem p, ref Vector3 min, ref Vector3 max, ref Vector3 avg, ref float n)
		{
			int particles = p.GetParticles(currentParticles);
			Vector3 position = _source.transform.position;
			position.y -= WaterController.waterTransformHeight;
			float num = Mathf.Abs(position.y) + 200f;
			for (int i = 0; i < particles; i++)
			{
				Vector3 position2 = currentParticles[i].position;
				Vector3 vector = VP.MultiplyPoint3x4(position2);
				float z = vector.z;
				vector /= z;
				if (z < num && z > 0f && vector.x >= -1f && vector.x <= 1f && vector.y >= -1f && vector.y <= 1f)
				{
					if (position2.x < min.x)
					{
						min.x = position2.x;
					}
					if (position2.x > max.x)
					{
						max.x = position2.x;
					}
					if (position2.z < min.z)
					{
						min.z = position2.z;
					}
					if (position2.z > max.z)
					{
						max.z = position2.z;
					}
					float num2 = num - z;
					avg += position2 * num2;
					n += num2;
				}
			}
		}

		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			if (num < Mathf.Epsilon)
			{
				return Vector3.zero;
			}
			float num2 = Vector3.Dot(vector, onNormal);
			return new Vector3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
		}

		protected void MoveCam()
		{
			if (StatMaster.isHeadless)
			{
				return;
			}
			if (!_camera.orthographic)
			{
				_camera.transform.position = _source.transform.position;
				_camera.transform.rotation = _source.transform.rotation;
				return;
			}
			float fieldOfView = _source.fieldOfView;
			Vector3 position = _source.transform.position;
			Vector3 forward = _source.transform.forward;
			float f = Vector3.Dot(forward, down);
			Vector3 vector = Quaternion.AngleAxis(fieldOfView * 0.5f * ((!WaterFogController.overWater && Application.isPlaying) ? (-1f) : 1f), _source.transform.right) * forward;
			Ray ray = new Ray(position, vector);
			float enter;
			Vector3 a;
			if (plane.Raycast(ray, out enter))
			{
				a = position + vector * enter;
			}
			else
			{
				a = position;
				a.y = 0f;
			}
			ray = new Ray(position, forward);
			if (plane.Raycast(ray, out enter))
			{
				position += forward * enter;
			}
			position.y = 0f;
			forward.y = 0f;
			a += forward * _camera.orthographicSize * 0.8f;
			a = Vector3.Lerp(a, position, (Mathf.Abs(f) - deg45) * deg45Scale);
			a.y = 20f;
			if (moveInIntervals)
			{
				float num = _camera.orthographicSize * 2f / (float)_rendertex.width;
				a.x = Mathf.Round(a.x / num) * num;
				a.z = Mathf.Round(a.z / num) * num;
			}
			if (_camera.transform.position != a)
			{
				_camera.transform.position = a;
				shaderPos = a;
				shaderPos.w = _border.localScale.x;
				Shader.SetGlobalVector(_posID, shaderPos);
			}
		}

		protected Vector3 RaycastOnPlane(Vector2 v)
		{
			Vector3 position = _source.transform.position;
			Ray ray = _source.ViewportPointToRay(v);
			Vector3 direction = ray.direction;
			Ray ray2 = new Ray(position, direction);
			Vector3 vector = ray.origin + ray.direction * 10000000f;
			float enter;
			if (plane.Raycast(ray2, out enter))
			{
				return position + direction * enter;
			}
			vector = Vector3.ProjectOnPlane(vector, plane.normal);
			return position + (vector - position).normalized * 10000f;
		}
	}
}
