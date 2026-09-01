using UnityEngine;
using UnityEngine.UI;

public class SandboxPropertiesHeader : MonoBehaviour
{
	public Button m_DeleteButton;

	public Button m_FlipButton;

	public Button m_DuplicateButton;

	public void DeactivateAllButtons()
	{
		m_DeleteButton.gameObject.SetActive(value: false);
		m_FlipButton.gameObject.SetActive(value: false);
		m_DuplicateButton.gameObject.SetActive(value: false);
	}
}
