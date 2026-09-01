using System.Text;
using UnityEngine;

[AddComponentMenu("UI/Multiplayer/Server Password Dialog")]
public class ServerPasswordDialog : MonoBehaviour
{
	public TextMesh passwordText;

	public UIButton sendButton;

	public UIButton cancelButton;

	protected void Awake()
	{
		sendButton.Click += OnSend;
		cancelButton.Click += OnCancel;
	}

	private void OnSend()
	{
		NetworkAuxAddPiece.Instance.SendServerMessage(RPCMessageType.ServerPassword, Encoding.UTF8.GetBytes(passwordText.text));
		base.gameObject.SetActive(false);
	}

	private void OnCancel()
	{
		BesiegeNetworkManager.Instance.Stop();
		base.gameObject.SetActive(false);
	}
}
