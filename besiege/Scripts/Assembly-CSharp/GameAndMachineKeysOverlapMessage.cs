using System.Collections.Generic;
using UnityEngine;

public class GameAndMachineKeysOverlapMessage : MissingMessageBase
{
	public MissingMessageBase push;

	private bool onlyExactMatches = true;

	protected override void Awake()
	{
		XmlLoader.OnLoad += MachineLoaded;
		base.Awake();
	}

	private void OnDestroy()
	{
		XmlLoader.OnLoad -= MachineLoaded;
	}

	private void MachineLoaded(MachineInfo info)
	{
		RemoveMessage();
		for (int i = 0; i < info.Blocks.Count; i++)
		{
			BlockInfo blockInfo = info.Blocks[i];
			XDataHolder blockData = blockInfo.BlockData;
			HashSet<XData> hashSet = blockData.ReadAll();
			foreach (XData item in hashSet)
			{
				if (item.Type == "StringArray" && DeSerialize(item))
				{
					return;
				}
			}
		}
	}

	protected void RemoveMessage()
	{
		on = false;
		StopAllCoroutines();
		SetAllRenderersOff();
		parentObj.gameObject.SetActive(false);
		if (hasCollider)
		{
			boxCollider.enabled = false;
		}
	}

	public bool DeSerialize(XData raw)
	{
		string[] array = (string[])(XStringArray)raw;
		foreach (string key in array)
		{
			KeyCode keyCode;
			if (KeyCodeConverter.GetKey(key, out keyCode) && CheckOverlap(keyCode))
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckOverlap(KeyCode keyCode)
	{
		ControlScheme.ControlEntry[] general = OptionsMaster.CustomControls.General;
		int i = 1;
		if (general[1].Options[0].Keys.Length == 1 && general[1].Options[0].Keys[0] == KeyCode.Space)
		{
			i = 2;
		}
		for (; i < general.Length; i++)
		{
			ControlScheme.ControlOption[] options = general[i].Options;
			for (int j = 0; j < options.Length; j++)
			{
				KeyCode[] keys = options[j].Keys;
				if (onlyExactMatches)
				{
					if (keys.Length == 1 && keys[0] == keyCode)
					{
						DisplayMessage();
						return true;
					}
					continue;
				}
				for (int k = 0; k < keys.Length; k++)
				{
					if (keys[k] == keyCode)
					{
						DisplayMessage();
						return true;
					}
				}
			}
		}
		return false;
	}

	public void DisplayMessage()
	{
		push.Push(true);
		StopCoroutine(DoIt());
		StartCoroutine(DoIt());
		if (playAudio)
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
