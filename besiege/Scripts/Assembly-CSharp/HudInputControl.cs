using UnityEngine;

public class HudInputControl : MonoBehaviour
{
	public Behaviour[] toDisable;

	public GameObject[] toDeactivate;

	private NetworkAuxAddPiece auxAddPiece;

	public void Awake()
	{
		StatMaster.hudHidden = false;
	}

	public void Start()
	{
		if (StatMaster.isMP)
		{
			auxAddPiece = NetworkAuxAddPiece.Instance;
		}
	}

	public void ToggleHUD(bool active)
	{
		if (!StatMaster.isMP || (auxAddPiece.receivedGameState && StatMaster.networkActive))
		{
			StatMaster.hudHidden = active;
			StatMaster.InvokeHudHiddenChanged();
			Behaviour[] array = toDisable;
			foreach (Behaviour behaviour in array)
			{
				behaviour.enabled = !StatMaster.hudHidden;
			}
			GameObject[] array2 = toDeactivate;
			foreach (GameObject gameObject in array2)
			{
				gameObject.SetActive(!StatMaster.hudHidden);
			}
		}
	}

	private void Update()
	{
		if (InputManager.ToggleHUDKey())
		{
			ToggleHUD(!StatMaster.hudHidden);
		}
	}
}
