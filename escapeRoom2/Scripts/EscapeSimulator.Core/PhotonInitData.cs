using ExitGames.Client.Photon;
using Photon.Realtime;

public class PhotonInitData
{
	public AuthenticationValues auth = new AuthenticationValues();

	public Hashtable customProps = new Hashtable();

	public string nickname;
}
