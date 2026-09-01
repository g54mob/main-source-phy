using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("DlcMissingEntry")]
public class DlcMissingEntry : MonoBehaviour
{
	[Header("References")]
	[SerializeField]
	private MeshRenderer iconRenderer;

	[SerializeField]
	private DynamicText nameTextMesh;

	[SerializeField]
	private SimpleUIButton getDlcButton;

	private DlcManager.DlcType dlcType;

	private DlcManager dlcManager;

	public void Setup(uint dlcTypeInt)
	{
		dlcType = (DlcManager.DlcType)dlcTypeInt;
		dlcManager = DlcManager.Instance;
		SetupDlcIcon();
		SetupDlcName();
		SetupGetDlcButton();
	}

	internal void Setup(DlcManager.DlcStatus issue)
	{
		Setup((uint)issue.type);
		Debug.Log(string.Concat("DLC issue found, ", dlcManager.GetDlcName(issue.type), " (", issue.type, "): ", issue.status));
	}

	private void SetupDlcName()
	{
		nameTextMesh.SetText(dlcManager.GetDlcName(dlcType));
	}

	private void SetupGetDlcButton()
	{
		getDlcButton.Click += OnGetDlcClicked;
	}

	private void SetupDlcIcon()
	{
		iconRenderer.material.mainTexture = dlcManager.GetDlcTexture(dlcType);
	}

	private void OnGetDlcClicked()
	{
		dlcManager.OpenDlcStore(dlcType);
	}

	private void OnDestroy()
	{
		if (getDlcButton != null)
		{
			getDlcButton.Click -= OnGetDlcClicked;
		}
	}
}
