using ModIO.UI;
using UnityEngine;

public class ModIOWorkshopManager : DestroySelf
{
	[SerializeField]
	protected ModBrowser modBrowser;

	[SerializeField]
	protected GameObject userLostError;

	protected ModBrowserContainer browserContainer;
}
