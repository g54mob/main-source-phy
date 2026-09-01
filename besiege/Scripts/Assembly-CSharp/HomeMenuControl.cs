using UnityEngine;

public class HomeMenuControl : ClickBehaviour
{
	public Transform menuObject;

	protected bool lastCheck;

	protected void Awake()
	{
		releaseOnlyOver = true;
	}

	public void Update()
	{
		if (StatMaster.isMP && !NetworkAuxAddPiece.Instance.receivedGameState)
		{
			lastCheck = false;
			return;
		}
		bool flag = InputManager.ToCloseCount <= 0 && !StatMaster.inMenu && (!LevelEditor.Instance || LevelEditor.Instance.selectionController.Count <= 0) && (!AdvancedBlockEditor.Instance || AdvancedBlockEditor.Instance.selectionController.Count <= 0);
		if (InputManager.CloseKey() && flag && lastCheck)
		{
			Open();
		}
		lastCheck = flag;
	}

	public override void OnClickReleased()
	{
		Open();
	}

	private void Open()
	{
		menuObject.gameObject.SetActive(true);
		BlockMapper.Close();
		OverviewBlockMapper.Close();
	}
}
