using UnityEngine;

public class EnableComponentAfterSeconds : MonoBehaviour
{
	public MonoBehaviour m_Component;

	public Collider m_Collider;

	public float seconds;

	private void Start()
	{
	}

	private void Update()
	{
		seconds -= Time.deltaTime;
		if (seconds < 0f)
		{
			if ((bool)m_Component)
			{
				m_Component.enabled = true;
			}
			if ((bool)m_Collider)
			{
				m_Collider.enabled = true;
			}
			Object.Destroy(this);
		}
	}
}
