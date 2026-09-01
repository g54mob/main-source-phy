using UnityEngine;

public class OpenWorkshopAgreement : ClickBehaviour
{
	public override void OnClicked()
	{
		Application.OpenURL("http://steamcommunity.com/sharedfiles/workshoplegalagreement");
	}
}
