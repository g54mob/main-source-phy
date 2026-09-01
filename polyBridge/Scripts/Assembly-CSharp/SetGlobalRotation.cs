using UnityEngine;

public class SetGlobalRotation : MonoBehaviour
{
	public Vector3 m_EulerAngles;

	private void Start()
	{
		base.gameObject.transform.eulerAngles = m_EulerAngles;
	}

	private void Update()
	{
		base.gameObject.transform.eulerAngles = m_EulerAngles;
	}
}
