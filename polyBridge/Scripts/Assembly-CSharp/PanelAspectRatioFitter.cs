using UnityEngine;

public class PanelAspectRatioFitter : MonoBehaviour
{
	[SerializeField]
	private Transform m_Transform;

	[SerializeField]
	public float m_NarrowScreenAspectRatioThreshold = 1.3f;

	[SerializeField]
	public float m_WidescreenAspectRatioThreshold = 1.55f;

	[SerializeField]
	public Vector3 m_NarrowScreenScale = new Vector3(0.9f, 0.9f, 1f);

	[SerializeField]
	public Vector3 m_WidescreenScale = new Vector3(1.2f, 1.2f, 1f);

	private float m_PreviousAspectRatio;

	private void Update()
	{
		float num = (float)Screen.width / (float)Screen.height;
		if (!num.Equals(m_PreviousAspectRatio))
		{
			if (num > m_WidescreenAspectRatioThreshold)
			{
				m_Transform.localScale = m_WidescreenScale;
			}
			else if (num < m_NarrowScreenAspectRatioThreshold)
			{
				m_Transform.localScale = m_NarrowScreenScale;
			}
			else
			{
				m_Transform.localScale = Vector3.one;
			}
			m_PreviousAspectRatio = num;
		}
	}
}
