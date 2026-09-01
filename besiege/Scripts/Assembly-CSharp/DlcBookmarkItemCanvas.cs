using BesiegeDlc;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Besiege/FileBrowserView/Dlc/DlcBookmarkItemCanvas")]
public class DlcBookmarkItemCanvas : MonoBehaviour
{
	[SerializeField]
	private Image iconImage;

	[SerializeField]
	private Text tooltipTextfield;

	[SerializeField]
	private GameObject warningBadge;

	[SerializeField]
	private Button dlcButton;

	private DlcManager.DlcType dlcType;

	internal void Setup(DlcManager.DlcType dlcType, string dlcName, Sprite dlcIconSprite, bool markAsMissing = false)
	{
		this.dlcType = dlcType;
		iconImage.sprite = dlcIconSprite;
		string text = dlcName;
		if (markAsMissing)
		{
			string translation = LocalisationManager.GetTranslation(4477);
			text = string.Format(translation, dlcName);
		}
		tooltipTextfield.text = text;
		if (warningBadge != null)
		{
			warningBadge.SetActive(markAsMissing);
		}
		if (dlcButton != null)
		{
			dlcButton.onClick.AddListener(OpenDlcStore);
		}
	}

	private void OpenDlcStore()
	{
		DlcManager.Instance.OpenDlcStore(dlcType);
	}
}
