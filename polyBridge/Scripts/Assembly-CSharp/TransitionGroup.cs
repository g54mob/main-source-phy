using UnityEngine;

public class TransitionGroup
{
	public GameObject m_Start;

	public GameObject m_End;

	public GameObject m_Moving;

	public TransitionGroup(GameObject start, GameObject end)
	{
		m_Start = start;
		m_End = end;
		m_Moving = new GameObject();
		m_Moving.transform.position = m_Start.transform.position;
		m_Moving.name = $"{start.name} to {end.name}";
		for (int num = m_Start.transform.childCount - 1; num >= 0; num--)
		{
			m_Start.transform.GetChild(num).SetParent(m_Moving.transform);
		}
	}

	public void Interpolate(float t)
	{
		m_Moving.transform.position = Vector3.Lerp(m_Start.transform.position, m_End.transform.position, t);
	}

	public void End()
	{
		m_Moving.transform.position = m_End.transform.position;
		for (int num = m_Moving.transform.childCount - 1; num >= 0; num--)
		{
			m_Moving.transform.GetChild(num).SetParent(m_End.transform);
		}
		Object.Destroy(m_Moving);
	}
}
