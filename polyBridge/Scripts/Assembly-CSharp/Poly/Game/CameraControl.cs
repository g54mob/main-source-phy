using System.Linq;
using Poly.Collide;
using Poly.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.Game
{
	public class CameraControl : MonoBehaviour
	{
		public Camera cam;

		public float cameraDistanceZ = -200f;

		public bool autoControlZDistanceAndShadows;

		[Range(0f, 100f)]
		public float minCameraDistance = 30f;

		[Range(0.01f, 100f)]
		public float cameraDistanceDiscretization = 10f;

		[Range(0.01f, 50f)]
		public float shadowDistancePastPivotPoint = 50f;

		public bool autoUpdate = true;

		public bool handleZoom = true;

		public Vec2 rotScale = new Vec2(0.25f, -0.25f);

		public float minAllowedOffsetLength = 10f;

		public bool drawGizmos = true;

		public bool drawBounds = true;

		public MeshRenderer waterPlane;

		private const bool createBoundingColliderForMeshRenderers = true;

		public string layerNameToCreateBoundingColliderIn = "NoRender";

		public Canvas canvas;

		public Text text;

		private float lastCameraDist;

		private Bounds2 focusBounds;

		private Bounds renderingBounds;

		private BoxCollider renderingBoundingCollider;

		private BoxCollider waterPlaneBoundingCollider;

		private int boundingColliderLayer;

		private Vec2 anchorPos;

		private Vec2 offsetFromAnchor;

		private bool mousePanDown;

		private bool mouseRotDown;

		private Vec2 prevMouseScreenPos;

		private Vec2 mouseYawPitch;

		private static CameraControl _instance;

		public bool isSimActive { get; set; }

		public static CameraControl instance => _instance ?? (_instance = Object.FindObjectOfType<CameraControl>());

		public CameraControl()
		{
			focusBounds.min.x = -50f;
			focusBounds.max.x = 50f;
			focusBounds.min.y = 0f;
			focusBounds.max.y = 50f;
		}

		private void OnEnable()
		{
			_instance = this;
		}

		private void OnDisable()
		{
			if (instance == this)
			{
				_instance = null;
			}
			GameRenderSettings.ResetSetShadowsOnExit();
		}

		private void Start()
		{
			RegisterTransformUpdate();
		}

		public void Init(Bounds2 bounds, Bounds renderingBounds)
		{
			lastCameraDist = -100f;
			boundingColliderLayer = LayerMask.NameToLayer(layerNameToCreateBoundingColliderIn);
			Object.Destroy(renderingBoundingCollider?.gameObject);
			Bounds bounds2 = waterPlane.bounds;
			(BoxCollider, BoxCollider) tuple = CreateBoudingCollider(renderingBounds, boundingColliderLayer, bounds2);
			renderingBoundingCollider = tuple.Item1;
			waterPlaneBoundingCollider = tuple.Item2;
			focusBounds = bounds;
			this.renderingBounds = renderingBounds;
			RegisterTransformUpdate();
		}

		private static (BoxCollider, BoxCollider) CreateBoudingCollider(Bounds renderingBounds, int layer, Bounds extraBounds)
		{
			GameObject gameObject = new GameObject("CameraControl's Rendering Bounding Box");
			gameObject.layer = layer;
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.isTrigger = true;
			boxCollider.center = renderingBounds.center;
			boxCollider.size = renderingBounds.size;
			BoxCollider boxCollider2 = null;
			boxCollider2 = gameObject.AddComponent<BoxCollider>();
			boxCollider2.isTrigger = true;
			boxCollider2.center = extraBounds.center;
			boxCollider2.size = extraBounds.size;
			return (boxCollider, boxCollider2);
		}

		private void Update()
		{
			if (autoUpdate)
			{
				mousePanDown = Input.GetMouseButton(0) || Input.GetMouseButton(2);
				mouseRotDown = Input.GetMouseButton(1);
				Update_Manual(Vec2.zero, mousePanDown, mouseRotDown, handleZoom);
			}
		}

		private void LateUpdate()
		{
			prevMouseScreenPos = (Vec2)Input.mousePosition;
		}

		public void Update_Manual(Vec2 mouseDelta, bool doPan = false, bool doRotate = false, bool doZoom = false)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
			{
				prevMouseScreenPos = (Vec2)Input.mousePosition;
			}
			if (mouseDelta == Vec2.zero)
			{
				mouseDelta = (Vec2)Input.mousePosition - prevMouseScreenPos;
			}
			if (doPan)
			{
				if (cam.orthographic)
				{
					mouseDelta *= 2f * cam.orthographicSize / (float)Screen.height;
				}
				Vec2 displacement = -mouseDelta;
				PanMouse(displacement);
			}
			if (doRotate)
			{
				Vector2 vector = Vec2.Scale(mouseDelta, rotScale);
				RotMouse(vector);
			}
			if (doZoom)
			{
				ZoomMouse();
			}
		}

		public void Update_Manual_AfterExternalTranslation_InScreenSpace(Vec2 displacement)
		{
			PanMouse(displacement, force: true);
		}

		public static void RegisterTransformUpdate()
		{
			if ((bool)instance && instance.isSimActive)
			{
				instance.Update_Manual_AfterExternalTransformations();
			}
		}

		public void Update_Manual_AfterExternalTransformations()
		{
			Vector3 vector = anchorPos + GetCameraZDistance() * cam.transform.forward + cam.transform.rotation * offsetFromAnchor;
			Vector3 position = cam.transform.position;
			Vec2 displacement = (Vec2)(Quaternion.Inverse(cam.transform.rotation) * (position - vector));
			Vector3 lhs = cam.transform.forward;
			float num = Vector3.Dot(lhs, Vector3.up);
			if (0.48999998f < num * num)
			{
				lhs = cam.transform.up;
			}
			lhs.y = 0f;
			lhs.Normalize();
			float num2 = Mathf.Atan2(0f - lhs.x, lhs.z) * 57.29578f;
			Vector3 vector2 = Quaternion.Euler(0f, num2, 0f) * cam.transform.forward;
			float y = Mathf.Atan2(vector2.y, vector2.z) * 57.29578f;
			mouseYawPitch = -1f * new Vec2(num2, y);
			PanMouse(displacement, force: true);
		}

		private void PanMouse(Vec2 displacement, bool force = false)
		{
			if (displacement.sqrMagnitude == 0f && !force)
			{
				return;
			}
			float num = 10f;
			if (cam.orthographic)
			{
				num = cam.orthographicSize;
			}
			offsetFromAnchor += displacement;
			RecalcCamPosition();
			Vec2 b = ((cam.transform.forward.z < 0f) ? new Vec2(-1f, 1f) : Vec2.one);
			PolygonShape polygonShape = PolygonShape.FromRect(focusBounds.center, Vec2.Scale(focusBounds.size, b));
			polygonShape.radius = 0f;
			for (int i = 0; i < polygonShape.verts.Length; i++)
			{
				polygonShape.verts[i] = (Vec2)cam.transform.InverseTransformPoint(polygonShape.verts[i]);
			}
			polygonShape.CacheLengths();
			float num2 = polygonShape.invLengths.Max() + 5.877472E-39f;
			if (1f / num2 < 0.0001f)
			{
				Debug.Log("Handling parallel projections for camera controller");
				Vec2[] fromPointCloud = new Vec2[2]
				{
					polygonShape.verts[0],
					polygonShape.verts[2]
				};
				polygonShape.SetFromPointCloud(fromPointCloud);
				polygonShape.CacheLengths();
				num2 = polygonShape.invLengths.Max() + 5.877472E-39f;
			}
			Vec2 closestPoint = GetClosestPoint(polygonShape, Vec2.zero);
			Vector3 origin = cam.transform.TransformPoint(closestPoint);
			Plane plane = new Plane(Vector3.forward, Vector3.zero);
			Ray ray = new Ray(origin, cam.transform.forward);
			ray.origin -= ray.direction;
			Vec2 vec = anchorPos;
			float num3 = Vector3.Dot(ray.direction, Vector3.forward);
			if (num3 * num3 < 1E-10f)
			{
				Vector3 direction = ray.direction;
				direction.z = 0f;
				plane = new Plane(direction, focusBounds.min.y * Vector3.up);
			}
			if (plane.Raycast(ray, out var enter))
			{
				vec = (Vec2)ray.GetPoint(enter);
			}
			else
			{
				ray.origin += 2f * ray.direction;
				ray.direction *= -1f;
				if (plane.Raycast(ray, out enter))
				{
					vec = (Vec2)ray.GetPoint(enter);
				}
			}
			anchorPos = vec;
			offsetFromAnchor = -closestPoint;
			float num4 = 0.9f * num + minAllowedOffsetLength;
			float magnitude = offsetFromAnchor.magnitude;
			if (num4 < magnitude)
			{
				offsetFromAnchor *= num4 / magnitude;
			}
			RecalcCamPosition();
		}

		private void RecalcCamPosition()
		{
			float num = GetCameraZDistance();
			Vector3 one = Vector3.one;
			if (autoControlZDistanceAndShadows)
			{
				float num2 = -400f;
				Vector3 center = anchorPos + num2 * cam.transform.forward + cam.transform.rotation * offsetFromAnchor;
				float orthographicSize = cam.orthographicSize;
				float x = (float)Screen.width / ((float)Screen.height + 5.877472E-39f) * orthographicSize;
				num = ((!UnityEngine.Physics.BoxCast(halfExtents: new Vector3(x, orthographicSize, 0.1f), layerMask: (LayerMask)(1 << boundingColliderLayer), center: center, direction: cam.transform.forward, hitInfo: out var hitInfo, orientation: cam.transform.rotation, maxDistance: float.MaxValue, queryTriggerInteraction: QueryTriggerInteraction.Collide)) ? (0f - minCameraDistance) : Mathf.Min(num2 + hitInfo.distance, 0f - minCameraDistance));
				float num3 = 0f - num;
				num3 = Mathf.Ceil(num3 / cameraDistanceDiscretization) * cameraDistanceDiscretization;
				if (lastCameraDist != num3)
				{
					bool num4 = Profiles.m_ActiveProfile.m_ShadowResolution != ShadowResolution.OFF;
					float cameraZDistance = num3 + shadowDistancePastPivotPoint - 50f;
					GameRenderSettings.SetShadows_OverrideDistance(num4, cameraZDistance);
					lastCameraDist = num3;
				}
				num = 0f - num3;
			}
			else if (lastCameraDist != GameSettings.CamDistFromPivot())
			{
				GameRenderSettings.SetShadows(Profiles.m_ActiveProfile.m_ShadowResolution != ShadowResolution.OFF);
				lastCameraDist = GameSettings.CamDistFromPivot();
			}
			cam.transform.position = anchorPos + num * cam.transform.forward + cam.transform.rotation * offsetFromAnchor;
		}

		public static Vec2 GetClosestPoint(PolygonShape testShape, Vec2 pos)
		{
			PolygonShape polyB = PolygonShape.FromCircle(pos, 0f);
			PolygonCollisionProcess.Init(ref testShape, ref Transform2.identity, ref polyB, ref Transform2.identity, out var process);
			PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint, doAveragePointPositions: false);
			Vec2 pointInLocalA = closestPoint.pointInLocalA;
			if (closestPoint.distance < 0f)
			{
				pointInLocalA += closestPoint.distance * closestPoint.normalInLocalA;
			}
			return pointInLocalA;
		}

		public void RotMouse(Vec2 deltaYawPitch)
		{
			mouseYawPitch += deltaYawPitch;
			mouseYawPitch.y = Mathf.Clamp(mouseYawPitch.y, Cameras.GetMinPitch(), Cameras.GetMaxPitch());
			if (mouseYawPitch.x < 0f)
			{
				mouseYawPitch.x += 360f;
			}
			if (360f <= mouseYawPitch.x)
			{
				mouseYawPitch.x -= 360f;
			}
			mouseYawPitch.x = Mathf.Clamp(mouseYawPitch.x, 0f, 360f);
			cam.transform.eulerAngles = new Vector3(mouseYawPitch.y, mouseYawPitch.x, 0f);
			RecalcCamPosition();
		}

		public void ZoomUpdate()
		{
			RecalcCamPosition();
		}

		private void ZoomMouse()
		{
			float y = Input.mouseScrollDelta.y;
			if (y != 0f)
			{
				if (cam.orthographic)
				{
					cam.orthographicSize = Mathf.Max(0.1f, cam.orthographicSize + y);
				}
				else
				{
					Vector3 localPosition = cam.transform.localPosition;
					localPosition.z = Mathf.Min(0f, localPosition.z + y);
					cam.transform.localPosition = localPosition;
				}
				PanMouse(Vec2.zero, force: true);
			}
		}

		private void OnDrawGizmos()
		{
			if (drawGizmos)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(anchorPos, anchorPos + cam.transform.rotation * offsetFromAnchor);
				if (drawBounds)
				{
					Vec2 min = focusBounds.min;
					min.x = focusBounds.max.x;
					Vec2 max = focusBounds.max;
					max.x = focusBounds.min.x;
					Gizmos.color = Color.white;
					Gizmos.DrawLine(focusBounds.min, min);
					Gizmos.DrawLine(min, focusBounds.max);
					Gizmos.DrawLine(focusBounds.max, max);
					Gizmos.DrawLine(max, focusBounds.min);
				}
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(anchorPos, 0.5f);
				Gizmos.color = Color.red;
				Gizmos.DrawWireCube(renderingBounds.center, renderingBounds.size);
				if ((bool)waterPlaneBoundingCollider)
				{
					Gizmos.DrawWireCube(waterPlaneBoundingCollider.center, waterPlaneBoundingCollider.size);
				}
			}
		}

		private float GetCameraZDistance()
		{
			if (!(GameSettings.m_Instance != null))
			{
				return cameraDistanceZ;
			}
			return 0f - GameSettings.CamDistFromPivot();
		}
	}
}
