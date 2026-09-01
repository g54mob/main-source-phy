using UnityEngine;

public class ToggleDropdownBox : ClickBehaviour
{
	public GameObject item;

	public TextMesh resText;

	public bool forRes = true;

	public override void OnClicked()
	{
		item.SetActive(!item.activeSelf);
		if (forRes)
		{
			ConfirmResolutionChange.AwaitingConfirmation = false;
			resText.text = Screen.width + " x " + Screen.height;
		}
		else
		{
			resText.text = QualitySettings.anisotropicFiltering.ToString();
		}
		AlignToScreenPoint[] array = Object.FindObjectsOfType<AlignToScreenPoint>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].AlignObject();
		}
	}
}
