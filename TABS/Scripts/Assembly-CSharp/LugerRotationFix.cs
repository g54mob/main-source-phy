using UnityEngine;

public class LugerRotationFix : MonoBehaviour
{
	public Transform root;

	public float multiplier = -2f;

	public float offset;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		base.transform.localEulerAngles = new Vector3((root.localEulerAngles.x + offset) * (0f - multiplier), 0f, 0f);
	}
}
