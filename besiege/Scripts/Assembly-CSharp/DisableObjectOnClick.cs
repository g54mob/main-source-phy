using UnityEngine;

public class DisableObjectOnClick : ClickBehaviour
{
	public Transform objToDisable;

	public bool disableBlur = true;

	public bool useEscapeKey;

	public bool writeToTutorialFile;

	public Transform sendQuitMessage;

	public GameObject canvasObj;

	public bool removeCloseOnDisable;

	private bool addedAsNextToClose;

	private void OnEnable()
	{
		if (useEscapeKey)
		{
			InputManager.AddAsNextToClose(Quit);
			addedAsNextToClose = true;
		}
		releaseOnlyOver = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (addedAsNextToClose && removeCloseOnDisable)
		{
			InputManager.RemoveAsNextToClose(Quit);
			addedAsNextToClose = false;
		}
	}

	public override void OnClickReleased()
	{
		Quit();
	}

	private void Quit()
	{
		if (objToDisable != null)
		{
			objToDisable.gameObject.SetActive(false);
		}
		if (writeToTutorialFile)
		{
			TutorialFileManager.SetTutorialState(objToDisable.name, 0);
		}
		if (addedAsNextToClose)
		{
			InputManager.RemoveAsNextToClose(Quit);
			addedAsNextToClose = false;
		}
		if (sendQuitMessage != null)
		{
			sendQuitMessage.SendMessage("Quit");
		}
		if (canvasObj != null)
		{
			canvasObj.SetActive(true);
		}
		if (disableBlur)
		{
			Camera main = Camera.main;
			Blur component = main.GetComponent<Blur>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}
}
