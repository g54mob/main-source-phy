using UnityEngine;

public class MirrorGizmoHandler : MonoBehaviour
{
	public Transform x;

	public Transform y;

	public Transform z;

	public float displacement = 0.75f;

	protected Vector3 xPos = new Vector3(0f, 1f, 1f);

	protected Vector3 yPos = new Vector3(1f, 0f, 1f);

	protected Vector3 zPos = new Vector3(1f, 1f, 0f);

	private void Update()
	{
		if (!StatMaster.ToolActive)
		{
			Vector3 vector = base.transform.InverseTransformPoint(Camera.main.transform.position) - base.transform.InverseTransformPoint(base.transform.position);
			Vector3 vector2 = new Vector3((!(vector.x > 0f)) ? (-1f) : 1f, (!(vector.y > 0f)) ? (-1f) : 1f, (!(vector.z > 0f)) ? (-1f) : 1f);
			x.localPosition = new Vector3(xPos.x * vector2.x, xPos.y * vector2.y, xPos.z * vector2.z) * displacement;
			y.localPosition = new Vector3(yPos.x * vector2.x, yPos.y * vector2.y, yPos.z * vector2.z) * displacement;
			z.localPosition = new Vector3(zPos.x * vector2.x, zPos.y * vector2.y, zPos.z * vector2.z) * displacement;
		}
	}
}
