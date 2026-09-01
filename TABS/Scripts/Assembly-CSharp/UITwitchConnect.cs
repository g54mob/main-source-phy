using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITwitchConnect : MonoBehaviour
{
	public TextMeshProUGUI Text;

	public TMP_InputField InputField;

	public Button Button;

	private bool oldPlayerActionsEnabled;

	private bool oldInputManagerEnabled;

	public void SelectText()
	{
		oldPlayerActionsEnabled = PlayerActions.Instance.Enabled;
		oldInputManagerEnabled = InputManager.Enabled;
		PlayerActions.Instance.Enabled = false;
		InputManager.Enabled = false;
	}

	public void DeselectText()
	{
		PlayerActions.Instance.Enabled = oldPlayerActionsEnabled;
		InputManager.Enabled = oldInputManagerEnabled;
	}

	public void TryConnect()
	{
		if (Text.text != "")
		{
			TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
			if ((bool)service)
			{
				service.ConnectToStream(Text.text.ToLower());
			}
		}
	}
}
