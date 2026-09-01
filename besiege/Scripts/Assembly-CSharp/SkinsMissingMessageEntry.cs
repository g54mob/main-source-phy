using Steamworks;
using UnityEngine;

public class SkinsMissingMessageEntry : ClickBehaviour
{
	public BlockSkinLoader.SkinPack pack;

	public MeshRenderer icon;

	public TextMesh text;

	public void Setup()
	{
		text.text = pack.name;
		CenterText();
	}

	public override void OnClicked()
	{
		if (!OptionsMaster.skinsEnabled)
		{
			OptionsMaster.skinsEnabled = true;
		}
		if (!string.IsNullOrEmpty(pack.id) && !char.IsLetter(pack.id[0]))
		{
			if (SteamManager.Initialized)
			{
				SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/sharedfiles/filedetails/?id=" + pack.id);
			}
			else
			{
				Application.OpenURL("http://steamcommunity.com/sharedfiles/filedetails/?id=" + pack.id);
			}
		}
		else if (SteamManager.Initialized)
		{
			SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/workshop/browse/?appid=346010&searchtext=" + pack.name.Replace(" ", "+") + "&childpublishedfileid=0&browsesort=trend&section=readytouseitems&requiredtags%5B%5D=Skin+Packs");
		}
		else
		{
			Application.OpenURL("http://steamcommunity.com/workshop/browse/?appid=346010&searchtext=" + pack.name.Replace(" ", "+") + "&childpublishedfileid=0&browsesort=trend&section=readytouseitems&requiredtags%5B%5D=Skin+Packs");
		}
	}

	private void CenterText()
	{
		MeshRenderer component = text.GetComponent<MeshRenderer>();
		float x = component.bounds.min.x;
		float x2 = icon.bounds.max.x;
		float num = (x + x2) / 2f;
		float num2 = component.transform.parent.position.x - num;
		component.transform.position += Vector3.right * num2;
		icon.transform.position += Vector3.right * num2;
	}
}
