using UnityEngine;

public class ToggleObjectOnClick : ClickBehaviour
{
	public Transform objToDisable;

	public bool useEscapeKey;

	public bool onDisableDefaultHandle = true;

	private bool def;

	private void Awake()
	{
		def = objToDisable.gameObject.activeSelf;
		releaseOnlyOver = true;
	}

	private void OnEnable()
	{
		if (useEscapeKey)
		{
			InputManager.AddAsNextToClose(Toggle);
		}
	}

	public override void OnClickReleased()
	{
		Toggle();
	}

	private void Toggle()
	{
		objToDisable.gameObject.SetActive(!objToDisable.gameObject.activeSelf);
		InputManager.RemoveAsNextToClose(Toggle);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (onDisableDefaultHandle)
		{
			objToDisable.gameObject.SetActive(def);
		}
	}
}
