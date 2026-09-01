using UnityEngine;

[AddComponentMenu("UI/UI Button Scaler")]
public class UIButtonScaler : UIButtonComponent
{
	private Vector3 startScale;

	private Vector3 upScale;

	protected override void Awake()
	{
		base.Awake();
		startScale = base.transform.localScale.Absolute();
		upScale = (startScale * 1.25f).Absolute();
	}

	protected override void OnButtonMouseExit()
	{
		base.transform.localScale = startScale;
	}

	protected override void OnButtonMouseEnter()
	{
		base.transform.localScale = upScale;
	}

	protected override void OnButtonClicked()
	{
	}
}
