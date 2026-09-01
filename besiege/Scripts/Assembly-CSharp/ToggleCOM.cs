using UnityEngine;

public class ToggleCOM : ClickBehaviour
{
	public MachineCenterOfMass ComCode;

	public Material redMaterial;

	private Renderer myRender;

	private void Awake()
	{
		myRender = GetComponent<Renderer>();
		myRender.material = redMaterial;
		myRender.enabled = false;
	}

	public override void OnClicked()
	{
		Set();
	}

	private void Set()
	{
		if (ComCode.showCOM)
		{
			ComCode.ToggleCOM(false);
			myRender.enabled = false;
		}
		else
		{
			ComCode.ToggleCOM(true);
			myRender.enabled = true;
		}
	}
}
