public class DisconnectWarning : WarningPopupBase
{
	public void Disconnected(string message)
	{
		textMeshy.text = message;
		ShowWarning();
	}
}
