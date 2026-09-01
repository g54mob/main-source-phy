using BesiegeDlc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Dlc Mismatch Entry")]
public class DlcMismatchEntry : MonoBehaviour
{
	public Image Icon;

	public Text NameText;

	public Button GetButton;

	public Image DisabledImage;

	private DlcManager.DlcStatus issue;

	public void Awake()
	{
		GetButton.onClick.AddListener(GetClicked);
	}

	internal void Init(DlcManager.DlcStatus issue)
	{
		this.issue = issue;
		NameText.text = DlcManager.Instance.GetDlcName(issue.type);
		Icon.sprite = DlcManager.Instance.GetDlcIcon(issue.type);
		GetButton.gameObject.SetActive(false);
		DisabledImage.gameObject.SetActive(false);
		switch (issue.status)
		{
		case DlcManager.DlcStatusType.MissingDlc:
			GetButton.gameObject.SetActive(true);
			break;
		case DlcManager.DlcStatusType.DisabledOnServer:
			DisabledImage.gameObject.SetActive(true);
			break;
		}
	}

	private void GetClicked()
	{
		DlcManager.Instance.OpenDlcStore(issue.type);
	}
}
