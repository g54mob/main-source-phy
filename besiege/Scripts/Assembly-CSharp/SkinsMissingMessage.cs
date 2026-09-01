using System.Collections.Generic;
using UnityEngine;

public class SkinsMissingMessage : MissingMessageBase
{
	public GameObject entry;

	public Transform bgOffsetter;

	public MissingMessageBase push;

	protected List<BlockSkinLoader.SkinPack> packsToFindInWorkshop;

	private Vector3 startPos;

	protected override void Awake()
	{
		XmlLoader.OnLoad += MachineLoaded;
		base.Awake();
	}

	protected override void Start()
	{
		startPos = parentObj.localPosition;
		base.Start();
	}

	private void OnDestroy()
	{
		XmlLoader.OnLoad -= MachineLoaded;
	}

	private void MachineLoaded(MachineInfo info)
	{
		if (!SteamManager.Initialized || SingleInstance<StatMaster>.Instance.LowViolence)
		{
			return;
		}
		extend = 0f;
		for (int i = 0; i < listContainer.transform.childCount; i++)
		{
			Object.Destroy(listContainer.transform.GetChild(i).gameObject);
		}
		entryRens.Clear();
		entryTexts.Clear();
		int num = 0;
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
		if (packsToFindInWorkshop.Count <= 0)
		{
			return;
		}
		float y = entry.transform.localScale.y;
		foreach (BlockSkinLoader.SkinPack item2 in packsToFindInWorkshop)
		{
			GameObject gameObject = Object.Instantiate(entry);
			gameObject.transform.position = listContainer.transform.position + Vector3.down * y * num;
			gameObject.transform.parent = listContainer.transform;
			SkinsMissingMessageEntry component = gameObject.GetComponent<SkinsMissingMessageEntry>();
			component.pack = item2;
			entryRens.Add(component.icon);
			entryTexts.Add(component.text.GetComponent<MeshRenderer>());
			component.Setup();
			num++;
		}
		BG.transform.localScale = bgStartSize + Vector3.up * packsToFindInWorkshop.Count * y;
		push.Push(true);
		Push(bgOffsetter.gameObject.activeInHierarchy);
		StopAllCoroutines();
		StartCoroutine(DoIt());
		if (playAudio)
		{
			GetComponent<AudioSource>().Play();
		}
	}

	public override bool Push(bool push)
	{
		float y = ((!push) ? startPos.y : (startPos.y - bgOffsetter.localScale.y - 0.1f));
		parentObj.localPosition = new Vector3(parentObj.localPosition.x, y, parentObj.localPosition.z);
		parentObjStartPos = parentObj.localPosition;
		return parentObj.gameObject.activeSelf;
	}
}
