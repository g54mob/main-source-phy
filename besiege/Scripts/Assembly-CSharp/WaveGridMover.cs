using UnityEngine;

[AddComponentMenu("VFX/WaveGridMover")]
public class WaveGridMover : MonoBehaviour
{
	public bool moveInIntervals = true;

	public float cellSize = 20f;

	public float offset = 1f;

	protected Camera camera;

	protected Transform cam;

	private Vector3 down = Vector3.down;

	private float deg45 = 0.8f;

	private float deg45Scale = 5f;

	private Plane plane = new Plane(Vector3.up, Vector3.zero);

	protected virtual void Start()
	{
		if (!camera)
		{
			camera = Camera.main;
		}
		cam = camera.transform;
		deg45Scale = 1f / (1f - deg45);
		CreatePlane();
	}

	private void GetWaterHeight()
	{
		if (WaterController.waterTransformHeight == 0f || WaterController.waterTransformHeight < -900f)
		{
			WaterController waterController = Object.FindObjectOfType<WaterController>();
			if ((bool)waterController)
			{
				WaterController.waterTransform = waterController.transform;
				WaterController.waterTransformHeight = WaterController.waterTransform.position.y;
			}
		}
	}

	protected virtual void LateUpdate()
	{
		MoveGrid();
	}

	protected void MoveGrid()
	{
		if (!StatMaster.isHeadless && !SingleInstanceFindOnly<MouseOrbit>.Instance.IsOrthographic)
		{
			Vector3 position = cam.position;
			Vector3 forward = cam.forward;
			float f = Vector3.Dot(forward, down);
			Vector3 position2 = ClosestPointOnPlane(plane, camera.transform.position);
			position2 = camera.WorldToViewportPoint(position2);
			Vector2 vector = new Vector2(0.5f, 0.5f);
			Vector2 vector2 = (Vector2)position2 - vector;
			float num = Mathf.Abs(vector2.x);
			float num2 = Mathf.Abs(vector2.y);
			if (num > num2)
			{
				vector2.x /= num;
				vector2.y /= num;
			}
			else
			{
				vector2.y /= num2;
				vector2.x /= num2;
			}
			position2 = vector + vector2 * 0.5f;
			Ray ray = camera.ViewportPointToRay(position2);
			float enter;
			Vector3 a;
			if (plane.Raycast(ray, out enter))
			{
				a = position + ray.direction * enter * offset;
			}
			else
			{
				a = position;
				a.y = 0f;
			}
			ray = new Ray(position, forward);
			if (plane.Raycast(ray, out enter))
			{
				position += forward * enter * offset;
			}
			position.y = 0f;
			forward.y = 0f;
			float num3 = Offset();
			a += forward.normalized * num3;
			a = Vector3.Lerp(a, position, (Mathf.Abs(f) - deg45) * deg45Scale);
			if (moveInIntervals)
			{
				a.x = Mathf.Round(a.x / cellSize) * cellSize;
				a.z = Mathf.Round(a.z / cellSize) * cellSize;
			}
			ResetPos(ref a);
			SetPosition(a);
		}
	}

	public static Vector3 ClosestPointOnPlane(Plane plane, Vector3 point)
	{
		return point + plane.GetDistanceToPoint(point) * plane.normal;
	}

	protected virtual float Offset()
	{
		return cellSize * 0.499f;
	}

	protected virtual void ResetPos(ref Vector3 pos)
	{
		pos.y = base.transform.position.y;
	}

	protected virtual void SetPosition(Vector3 pos)
	{
		if (base.transform.position != pos)
		{
			base.transform.position = pos;
		}
	}

	protected Vector3 RaycastOnPlane(Vector2 v)
	{
		Vector3 position = cam.position;
		Ray ray = camera.ViewportPointToRay(v);
		Vector3 direction = ray.direction;
		Ray ray2 = new Ray(position, direction);
		Vector3 vector = ray.origin + ray.direction * 10000000f;
		float enter;
		if (plane.Raycast(ray2, out enter))
		{
			return position + direction * enter;
		}
		vector = Vector3.ProjectOnPlane(vector, plane.normal);
		return position + (vector - position).normalized * 3000f;
	}

	private void CreatePlane()
	{
		GetWaterHeight();
		float num = WaterController.waterTransformHeight;
		if (num < -900f)
		{
			num = 0f;
		}
		plane.SetNormalAndPosition(plane.normal, Vector3.up * num);
	}
}
