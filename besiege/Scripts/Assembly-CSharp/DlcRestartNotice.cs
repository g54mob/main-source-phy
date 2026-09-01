using UnityEngine;

[AddComponentMenu("Besiege/UI/Canvas/DlcRestartNotice")]
public class DlcRestartNotice : SingleInstanceFindOnly<DlcRestartNotice>
{
	[SerializeField]
	private GameObject containerObject;

	public override string Name
	{
		get
		{
			return "DlcRestartNotice";
		}
	}

	public override void SetUp()
	{
		containerObject.SetActive(false);
	}

	[ContextMenu("ToggleRestartNotice")]
	private void ToggleRestartNotice()
	{
		if (containerObject.activeSelf)
		{
			Close();
		}
		else
		{
			Open();
		}
	}

	public void Close()
	{
		containerObject.SetActive(false);
		UIHelper.ToggleCanvasBackCollider(false);
	}

	public void Open()
	{
		containerObject.SetActive(true);
		UIHelper.ToggleCanvasBackCollider(true);
	}
}
