using UnityEngine;

public class DMDisableByPlatform : MonoBehaviour
{
	[SerializeField]
	private bool m_disableOnDesktop;

	[SerializeField]
	private bool m_disableOnXbox;

	[SerializeField]
	private bool m_disableOnPlayStation;

	[SerializeField]
	private bool m_disableOnSwitch;

	private void Awake()
	{
		base.gameObject.SetActive(!m_disableOnDesktop);
	}
}
