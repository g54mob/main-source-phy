using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenFileBrowserButton : SimpleUIButton
{
	[SerializeField]
	protected FileBrowserType fileBrowserType;

	[SerializeField]
	protected bool isSaveMenu;

	[SerializeField]
	protected FileBrowserView fileBrowserView;

	protected override void Awake()
	{
		base.Awake();
		if (fileBrowserView == null)
		{
			fileBrowserView = GameObject.Find("HUD").transform.FindChild("FileBrowserView").GetComponent<FileBrowserView>();
			if (fileBrowserView == null)
			{
				Debug.LogWarning("FileBrowserView could not be found, deactivating...");
				base.gameObject.SetActive(false);
			}
		}
	}

	protected override bool _InvokeOnClick()
	{
		OpenFileBrowser();
		return true;
	}

	private void OpenFileBrowser()
	{
		if (fileBrowserType == FileBrowserType.LocalMachines)
		{
			OpenMachineFileBrowser();
		}
		else
		{
			fileBrowserView.Open(fileBrowserType, isSaveMenu, true);
		}
	}

	private void OpenMachineFileBrowser()
	{
		Machine machine = null;
		if (StatMaster.isMP)
		{
			if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
			{
				machine = PlayerData.localPlayer.machine;
			}
		}
		else
		{
			machine = Machine.Active();
		}
		bool flag = SceneManager.GetActiveScene().name.Equals("2DHudTest");
		if ((!(machine == null) && !machine.isSimulating) || flag)
		{
			if (StatMaster.isMP && StatMaster.limitMachines)
			{
				NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
				instance.hud.ShowAllowedMachines(machine as ServerMachine);
			}
			else
			{
				fileBrowserView.Open(fileBrowserType, isSaveMenu, true);
			}
		}
	}
}
