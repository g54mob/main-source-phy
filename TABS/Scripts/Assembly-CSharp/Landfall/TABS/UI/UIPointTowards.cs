using UnityEngine;

namespace Landfall.TABS.UI
{
	public class UIPointTowards : MonoBehaviour
	{
		[SerializeField]
		private Transform m_target;

		private float m_len;

		private RectTransform m_trans;

		private void Awake()
		{
			m_trans = base.transform as RectTransform;
			m_len = m_trans.anchoredPosition.magnitude;
		}

		private void Start()
		{
			if (m_target == null)
			{
				m_target = m_trans.parent.parent;
			}
		}

		private void Update()
		{
			if (!(m_target == null))
			{
				m_trans.anchoredPosition = (m_target.position - m_trans.parent.position).normalized * m_len;
			}
		}
	}
}
