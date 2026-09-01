using UnityEngine;

public class ReturnToMenuScreen : MonoBehaviour
{
	public UIButton disconnectButton;

	private void OnEnable()
	{
		StatMaster.SetInMenu(true);
		if (disconnectButton != null)
		{
			disconnectButton.gameObject.SetActive(!StatMaster.IsLevelEditorOnly);
		}
	}

	private void Awake()
	{
		if (disconnectButton != null)
		{
			disconnectButton.Click += OnDisconnect;
		}
	}

	private void OnDisable()
	{
		StatMaster.SetInMenu(false);
	}

	private void Close()
	{
		base.gameObject.SetActive(false);
	}

	private void OnDisconnect()
	{
		Close();
		NetworkScene.Instance.ManualStop();
	}
}
