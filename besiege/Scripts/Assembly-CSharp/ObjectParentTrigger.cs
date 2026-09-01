using UnityEngine;

public class ObjectParentTrigger : MonoBehaviour
{
	public Transform myTransform;

	private Collider coll;

	protected void Start()
	{
		if (!StatMaster._customLevelSimulating)
		{
			return;
		}
		coll = GetComponent<Collider>();
		Bounds bounds = coll.bounds;
		LayerMask layerMask = 285212673;
		Collider[] array = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore);
		bool flag = false;
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			if (!(coll == collider) && !(collider.transform == myTransform) && !(collider.transform.parent == myTransform) && !(collider.name == "BoundingBox") && TriggerEnter(collider))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Object.Destroy(this);
		}
	}

	private bool TriggerEnter(Collider other)
	{
		LevelEntity levelEntity = other.gameObject.GetComponentInParent<LevelEntity>();
		if ((bool)levelEntity && levelEntity.isSimulating)
		{
			if (levelEntity.hasBase)
			{
				levelEntity = levelEntity.baseEntity as LevelEntity;
			}
			if (!levelEntity.isStatic && levelEntity.behaviour.prefab.showPhysicsToggle)
			{
				Transform parent = other.transform;
				Transform parent2 = other.transform;
				while (parent != null)
				{
					parent2 = CreateHelperToCounter(parent, parent2);
					parent = parent.parent;
				}
				myTransform.SetParent(parent2, false);
				return true;
			}
		}
		return false;
	}

	private Transform CreateHelperToCounter(Transform toCounter, Transform parent)
	{
		Matrix4x4 matrix4x = Matrix4x4.TRS(toCounter.localPosition, toCounter.localRotation, toCounter.localScale);
		if (matrix4x == Matrix4x4.identity)
		{
			return parent;
		}
		Matrix4x4 inverse = matrix4x.inverse;
		Vector3 localPosition = inverse.GetColumn(3);
		Quaternion localRotation = Quaternion.LookRotation(inverse.GetColumn(2), inverse.GetColumn(1));
		Vector3 localScale = new Vector3(inverse.GetColumn(0).magnitude, inverse.GetColumn(1).magnitude, inverse.GetColumn(2).magnitude);
		Transform transform = new GameObject("ObjectParentTrigger Counter for " + toCounter.name).transform;
		transform.parent = parent;
		transform.localScale = localScale;
		transform.localRotation = localRotation;
		transform.localPosition = localPosition;
		return transform;
	}
}
