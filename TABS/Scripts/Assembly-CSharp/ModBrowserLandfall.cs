using UnityEngine;
using UnityEngine.UI;

public class ModBrowserLandfall : MonoBehaviour
{
	[SerializeField]
	private Button m_ExitButton;

	private void Awake()
	{
		if ((bool)m_ExitButton)
		{
			m_ExitButton.onClick.AddListener(Close);
		}
	}

	private void Close()
	{
		base.transform.parent.gameObject.SetActive(value: false);
	}
}
