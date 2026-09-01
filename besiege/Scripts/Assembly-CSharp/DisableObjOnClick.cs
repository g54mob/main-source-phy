using UnityEngine;

public class DisableObjOnClick : ClickBehaviour
{
	public Transform menuObject;

	public bool blurCam;

	public bool allowInPlayMode = true;

	public bool allowWithoutMachine;

	public bool sendMessageInstead;

	public bool useOnDownInstead;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (useOnDownInstead)
		{
			Clicked();
		}
	}

	public override void OnClickReleased()
	{
		if (!useOnDownInstead)
		{
			Clicked();
		}
	}

	protected void Clicked()
	{
		if (allowInPlayMode)
		{
			Close();
			return;
		}
		Machine machine = Machine.Active();
		bool flag = machine != null;
		if ((allowWithoutMachine && !flag) || (flag && !machine.isSimulating))
		{
			Close();
		}
	}

	private void Close()
	{
		if (!sendMessageInstead)
		{
			menuObject.gameObject.SetActive(false);
		}
		else
		{
			menuObject.gameObject.SendMessage("Disable");
		}
		if (blurCam)
		{
			Camera main = Camera.main;
			Blur component = main.GetComponent<Blur>();
			BlurEffect component2 = main.GetComponent<BlurEffect>();
			if (component != null)
			{
				component.enabled = false;
			}
			if (component2 != null)
			{
				component2.enabled = false;
			}
		}
	}
}
