using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SkinsMissingDialog : WarningPopupBase
{
	protected List<BlockSkinLoader.SkinPack> packsToFindInWorkshop;

	private IEnumerator openMissingSkinPacksInWorkshopCoroutine;

	protected override void Awake()
	{
		XmlLoader.OnLoad += MachineLoaded;
		base.Awake();
	}

	private void MachineLoaded(MachineInfo info)
	{
		packsToFindInWorkshop = info.SkinPacks;
		foreach (BlockSkinLoader.SkinPack skinPack2 in BlockSkinLoader.SkinPacks)
		{
			BlockSkinLoader.SkinPack skinPack = null;
			foreach (BlockSkinLoader.SkinPack item in packsToFindInWorkshop)
			{
				if (!string.IsNullOrEmpty(item.id) && !char.IsLetter(item.id[0]))
				{
					if (item.id == skinPack2.id)
					{
						skinPack = item;
						break;
					}
				}
				else if (item.name == skinPack2.name)
				{
					if (item.id == skinPack2.id)
					{
						skinPack = item;
						break;
					}
					skinPack = item;
				}
			}
			if (skinPack != null)
			{
				packsToFindInWorkshop.Remove(skinPack);
			}
		}
		if (packsToFindInWorkshop.Count > 0)
		{
			StopAllCoroutines();
			StartCoroutine(DoIt());
			if (playAudio)
			{
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public override void OnClicked()
	{
		if (openMissingSkinPacksInWorkshopCoroutine != null)
		{
			StopCoroutine(openMissingSkinPacksInWorkshopCoroutine);
		}
		openMissingSkinPacksInWorkshopCoroutine = OpenMissingSkinPacksInWorkshop();
		StartCoroutine(openMissingSkinPacksInWorkshopCoroutine);
	}

	private IEnumerator OpenMissingSkinPacksInWorkshop()
	{
		if (!OptionsMaster.skinsEnabled)
		{
			OptionsMaster.skinsEnabled = true;
		}
		foreach (BlockSkinLoader.SkinPack pack in packsToFindInWorkshop)
		{
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
			yield return new WaitForSeconds(0.1f);
		}
	}
}
