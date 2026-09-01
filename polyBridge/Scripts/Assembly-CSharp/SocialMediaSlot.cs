using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediaSlot : MonoBehaviour
{
	public delegate void OnAuthorizeDelegate();

	public OnAuthorizeDelegate m_OnAuthorizeDelegate;

	public TextMeshProUGUI m_UsernameText;

	public Text m_UsernameTextSystemFont;

	public TextMeshProUGUI m_StatusText;

	public TextMeshProUGUI m_AuthorizeButtonText;

	public Button m_AuthorizeButton;

	private bool m_Authorized;

	public void OnEnable()
	{
		m_AuthorizeButton.onClick.AddListener(OnAuthorize);
	}

	public void OnDisable()
	{
		m_AuthorizeButton.onClick.RemoveAllListeners();
	}

	public void Init(OnAuthorizeDelegate authorizeDelegate)
	{
		m_OnAuthorizeDelegate = authorizeDelegate;
		UnAuthorize();
	}

	public void UnAuthorize()
	{
		m_Authorized = false;
		m_AuthorizeButtonText.text = GameUI.MarkupForGreen("Authorize");
		m_UsernameText.text = "Username: ";
		m_StatusText.text = string.Format("Status: {0}", GameUI.MarkupForGold("Not Authorized"));
	}

	public void Authorize()
	{
		if (m_OnAuthorizeDelegate != null)
		{
			m_OnAuthorizeDelegate();
		}
	}

	private void OnAuthorize()
	{
		if (m_Authorized)
		{
			UnAuthorize();
		}
		else
		{
			Authorize();
		}
	}
}
