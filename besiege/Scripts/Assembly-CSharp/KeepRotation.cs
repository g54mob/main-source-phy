using UnityEngine;

public class KeepRotation : MonoBehaviour
{
	private Quaternion startRot;

	private void Awake()
	{
		startRot = base.transform.rotation;
	}

	private void OnEnable()
	{
		base.transform.rotation = startRot;
	}

	private void Update()
	{
		base.transform.rotation = startRot;
	}
}
