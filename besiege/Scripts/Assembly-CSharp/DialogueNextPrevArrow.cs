using UnityEngine;

public class DialogueNextPrevArrow : ClickBehaviour
{
	public DialoguePageController pageController;

	public bool prevArrow;

	public override void OnClicked()
	{
		if (!prevArrow)
		{
			pageController.NextPage();
		}
		else
		{
			pageController.PrevPage();
		}
		GetComponent<AudioSource>().Play();
	}
}
