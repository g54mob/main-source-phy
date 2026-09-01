using UnityEngine;

public class FireTrigger : PrefabTransform
{
	public FireController fireController;

	public override void Apply(BlockPrefab prefab)
	{
		base.Apply(prefab);
		Transform transform = prefab.gameObject.transform;
		fireController.overlapRadius *= base.transform.localScale.x;
		fireController.overlapCenter = transform.InverseTransformPoint(base.transform.TransformPoint(fireController.overlapCenter));
		fireController.overlapSize = transform.InverseTransformVector(base.transform.TransformVector(fireController.overlapSize));
	}

	public void OnDrawGizmosSelected()
	{
		if (!(fireController == null))
		{
			Gizmos.color = new Color(1f, 0.6f, 0f);
			switch (fireController.overlapType)
			{
			case FireController.OverlapType.Sphere:
			{
				float magnitude = base.transform.TransformVector(Vector3.up * fireController.overlapRadius).magnitude;
				Gizmos.DrawWireSphere(base.transform.position, magnitude);
				break;
			}
			case FireController.OverlapType.Box:
			{
				Matrix4x4 matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, base.transform.localScale);
				Gizmos.matrix = matrix;
				Gizmos.DrawWireCube(fireController.overlapCenter, fireController.overlapSize);
				Gizmos.matrix = Matrix4x4.identity;
				break;
			}
			}
			Gizmos.color = Color.white;
		}
	}
}
