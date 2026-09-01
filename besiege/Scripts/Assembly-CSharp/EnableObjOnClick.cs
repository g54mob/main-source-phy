using UnityEngine;

public class EnableObjOnClick : ClickBehaviour
{
	public Transform menuObject;

	public bool blurCam;

	public bool allowInPlayMode = true;

	public bool closeLoadScreen;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClickReleased()
	{
		if (allowInPlayMode)
		{
			Open();
			return;
		}
		Machine machine = Machine.Active();
		if (!machine || machine.isSimulating)
		{
			return;
		}
		if (closeLoadScreen)
		{
			GameObject gameObject = GameObject.Find("LOAD MACHINE WINDOW");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			gameObject = GameObject.Find("SAVE LOAD LEVEL WINDOW");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		Open();
	}

	private void Open()
	{
		menuObject.gameObject.SetActive(true);
		BlockMapper.Close();
		OverviewBlockMapper.Close();
		if (blurCam)
		{
			Camera main = Camera.main;
			Blur component = main.GetComponent<Blur>();
			BlurEffect component2 = main.GetComponent<BlurEffect>();
			if (component != null)
			{
				component.enabled = true;
			}
			if (component2 != null)
			{
				component2.enabled = true;
			}
		}
	}
}
