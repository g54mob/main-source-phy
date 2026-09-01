using Landfall.TABS;
using UnityEngine;

public class DebugModalPanel : MonoBehaviour
{
	[SerializeField]
	private string message = "This is a test message";

	[SerializeField]
	private string yesText = "Yes";

	[SerializeField]
	private string noText = "No";

	[SerializeField]
	private string customText = "Custom Text";

	[SerializeField]
	private KeyCode testKey = KeyCode.F1;

	private ModalPanel modalPanel;

	private void Start()
	{
		modalPanel = ServiceLocator.GetService<ModalPanel>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(testKey))
		{
			TestPopup();
		}
	}

	[ContextMenu("Test Popup Ok/Cancel")]
	public void TestPopup()
	{
		modalPanel.PopUp(message, yesText, noText, customText, OnYesPressed, OnNoPressed);
	}

	private void OnYesPressed()
	{
		Debug.Log("YES Button was pressed on test popup.");
	}

	private void OnNoPressed()
	{
		Debug.Log("NO Button was pressed on test popup.");
	}
}
