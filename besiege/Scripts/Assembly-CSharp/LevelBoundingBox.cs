using System.Collections.Generic;
using UnityEngine;

public class LevelBoundingBox : MonoBehaviour
{
	public class GroundResult
	{
		public bool hasHit;

		public Collider hitCollider;

		public float hitDistance;

		public GroundResult(bool hit, Collider coll, float dist)
		{
			hasHit = hit;
			hitCollider = coll;
			hitDistance = dist;
		}
	}

	[SerializeField]
	public BoxCollider boxCollider;

	[SerializeField]
	public Rigidbody body;

	[SerializeField]
	private bool StayActiveInBuildMode;

	private Vector3 topPos;

	private Vector3 bottomPos;

	private Vector3 centerPos;

	public void Init(LevelPrefab prefab)
	{
		boxCollider = GetComponent<BoxCollider>();
		body = GetComponent<Rigidbody>();
		body.isKinematic = true;
		boxCollider.center = (centerPos = prefab.boundObjPos);
		boxCollider.size = prefab.boundObjSize;
		float num = prefab.boundObjSize.y * 0.5f;
		bottomPos = new Vector3(centerPos.x, centerPos.y - num, centerPos.z);
		topPos = new Vector3(centerPos.x, centerPos.y + num, centerPos.z);
	}

	public Vector2 GetSize()
	{
		return base.transform.TransformVector(boxCollider.size);
	}

	public Vector3 GetTop()
	{
		return base.transform.TransformPoint(topPos);
	}

	public Vector3 GetWorldTop()
	{
		return new Vector3(base.transform.position.x, boxCollider.bounds.max.y, base.transform.position.z);
	}

	public Vector3 GetBottom()
	{
		return base.transform.TransformPoint(bottomPos);
	}

	public Vector3 GetCenter()
	{
		return base.transform.TransformPoint(centerPos);
	}

	public void Toggle(bool toggle)
	{
		if (StayActiveInBuildMode)
		{
			Machine machine = Machine.Active();
			if ((bool)machine)
			{
				toggle = !machine.isSimulating;
			}
		}
		if (base.gameObject == null)
		{
			Debug.LogError("[LevelBoundingBox.Toggle]: Missing source gameObject");
		}
		base.gameObject.SetActive(toggle);
	}

	public void UpdateBounds()
	{
	}

	public static void Calculate(LevelEntity entity, out Vector3 center, out Vector3 size)
	{
		entity.transform.rotation = Quaternion.identity;
		int num = 0;
		MeshRenderer[] componentsInChildren = entity.GetComponentsInChildren<MeshRenderer>();
		List<MeshRenderer> list = new List<MeshRenderer>();
		for (num = 0; num < componentsInChildren.Length; num++)
		{
			MeshRenderer meshRenderer = componentsInChildren[num];
			if (meshRenderer.enabled)
			{
				string text = componentsInChildren[num].name.ToLower();
				if (!text.Contains("particle") && !text.Contains("blood") && !(text == "cube_cube_001"))
				{
					list.Add(meshRenderer);
				}
			}
		}
		Vector3 vector;
		Vector3 vector2;
		if (list.Count == 0)
		{
			Collider[] componentsInChildren2 = entity.GetComponentsInChildren<Collider>();
			if (componentsInChildren2.Length == 0)
			{
				Debug.LogWarning("No legal renderers and colliders on " + entity.name + "!");
				center = (size = Vector3.zero);
				return;
			}
			Bounds bounds = componentsInChildren2[0].bounds;
			for (int i = 1; i < componentsInChildren2.Length; i++)
			{
				bounds.Encapsulate(componentsInChildren2[i].bounds);
			}
			vector = bounds.min;
			vector2 = bounds.max;
		}
		else
		{
			vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (num = 0; num < list.Count; num++)
			{
				Renderer renderer = list[num];
				Bounds bounds2 = renderer.bounds;
				vector = new Vector3(Mathf.Min(vector.x, bounds2.min.x), Mathf.Min(vector.y, bounds2.min.y), Mathf.Min(vector.z, bounds2.min.z));
				vector2 = new Vector3(Mathf.Max(vector2.x, bounds2.max.x), Mathf.Max(vector2.y, bounds2.max.y), Mathf.Max(vector2.z, bounds2.max.z));
			}
		}
		size = vector2 - vector;
		center = vector + size * 0.5f;
	}

	public static GroundResult Ground(Rigidbody rBody)
	{
		RaycastHit[] array = rBody.SweepTestAll(Vector3.down, float.PositiveInfinity, QueryTriggerInteraction.Ignore);
		float num = 0f;
		int num2 = 0;
		bool flag = false;
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				RaycastHit raycastHit = array[i];
				if (i == 0 || raycastHit.distance < num)
				{
					num = raycastHit.distance;
					num2 = i;
				}
			}
			flag = true;
		}
		return new GroundResult(flag, (!flag) ? null : array[num2].collider, num);
	}

	public static GroundResult Ground(BlockBehaviour b, LayerMask layerMask)
	{
		if (b.Prefab.hasMyBounds && WaterController.Exist)
		{
			float y = b.myBounds.GetBounds(false).min.y;
			bool isWater;
			float ground = MachineGround.GetGround(y, b.ParentMachine, out isWater);
			GroundResult groundResult = new GroundResult(true, b.ParentMachine.boundingBoxController.colliders[0], y - ground);
			GroundResult groundResult2 = Ground(b.Rigidbody, layerMask);
			if (groundResult.hitDistance < groundResult2.hitDistance)
			{
				return groundResult;
			}
			return groundResult2;
		}
		return Ground(b.Rigidbody, layerMask);
	}

	public static GroundResult Ground(Rigidbody rBody, LayerMask layerMask)
	{
		RaycastHit[] array = rBody.SweepTestAll(Vector3.down, float.PositiveInfinity, QueryTriggerInteraction.Ignore);
		float num = 0f;
		int num2 = 0;
		bool flag = false;
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				RaycastHit raycastHit = array[i];
				if ((int)layerMask == ((int)layerMask | (1 << raycastHit.collider.gameObject.layer)))
				{
					if (!flag || raycastHit.distance < num)
					{
						num = raycastHit.distance;
						num2 = i;
					}
					flag = true;
				}
			}
		}
		return new GroundResult(flag, (!flag) ? null : array[num2].collider, num);
	}

	public GroundResult Ground()
	{
		bool activeSelf = base.gameObject.activeSelf;
		if (!activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		GroundResult result = Ground(body);
		if (!activeSelf)
		{
			base.gameObject.SetActive(false);
		}
		return result;
	}
}
