using UnityEngine;

[AddComponentMenu("UI/Drop Down Button")]
public class DropDownButton : ClickBehaviour
{
	public Transform dropper;

	public float startY;

	public float endY;

	public bool activey;

	protected void Update()
	{
		if (Machine.Active().isSimulating && activey)
		{
			ReturnHidden();
		}
	}

	public override void OnClicked()
	{
		if (OnActivation != null)
		{
			OnActivation();
		}
		if (!activey)
		{
			DropDown();
		}
		else
		{
			ReturnHidden();
		}
	}

	public void CloseAll()
	{
		ReturnHidden();
	}

	protected void DropDown()
	{
		activey = true;
		dropper.localPosition = new Vector3(dropper.localPosition.x, endY, dropper.localPosition.z);
	}

	protected void ReturnHidden()
	{
		activey = false;
		dropper.localPosition = new Vector3(dropper.localPosition.x, startY, dropper.localPosition.z);
	}
}
