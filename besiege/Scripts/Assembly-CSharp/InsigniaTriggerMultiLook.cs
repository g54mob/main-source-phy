using System.Collections.Generic;
using System.Linq;
using Localisation;
using UnityEngine;

public class InsigniaTriggerMultiLook : InsigniaTrigger, ILocalisationAware
{
	public GameObject[] entityLooks;

	public int[] lookLocIds;

	protected MMenu lookMenu;

	public override void Init()
	{
		base.Init();
		if (entityLooks.Length > 0)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < lookLocIds.Length; i++)
			{
				list.Add(LocalisationManager.GetTranslation(lookLocIds[i]));
			}
			lookMenu = AddMenu("look", 0, list);
			lookMenu.ValueChanged += UpdateLook;
			UpdateLook(lookMenu.Value);
		}
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		if (lookMenu != null)
		{
			lookMenu.Items = lookLocIds.Select((int x) => LocalisationManager.GetTranslation(x)).ToList();
		}
	}

	private void UpdateLook(int index)
	{
		index = ((index < entityLooks.Length) ? index : 0);
		for (int i = 0; i < entityLooks.Length; i++)
		{
			entityLooks[i].SetActive(index == i);
		}
	}
}
