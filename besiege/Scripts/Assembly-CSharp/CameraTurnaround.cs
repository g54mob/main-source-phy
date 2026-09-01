using UnityEngine;

public class CameraTurnaround : MonoBehaviour
{
	public Transform target;

	public float orbitSpeed;

	private MouseOrbit orbit;

	private bool orbitDisable;

	private void Start()
	{
		orbit = GetComponent<MouseOrbit>();
	}

	private void Update()
	{
		orbit.enabled = false;
		base.transform.LookAt(target);
		base.transform.Translate(Vector3.right * orbitSpeed);
	}
}
