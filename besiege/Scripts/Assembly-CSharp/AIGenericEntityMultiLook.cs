using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

public class AIGenericEntityMultiLook : AIGenericEntity, ILocalisationAware
{
	[Serializable]
	public class EnvironmentLook
	{
		public LevelSettings.LevelEnvironment env;

		public int locId;
	}

	public GameObject[] entityLooks;

	public int[] nameLocalisations;

	public EnvironmentLook[] envLooks;

	protected MMenu lookMenu;

	public override bool IsMultiLook
	{
		get
		{
			return true;
		}
	}

	public override void Init()
	{
		if (!isInitialized)
		{
			if (entityLooks.Length > 0)
			{
				lookMenu = AddMenu("look", 0, GetLocalisedLookNames());
				lookMenu.ValueChanged += UpdateLook;
				UpdateLook(lookMenu.Value);
			}
			base.Init();
		}
	}

	public override void SetupDefault()
	{
		base.SetupDefault();
		if (envLooks == null)
		{
			return;
		}
		for (int i = 0; i < envLooks.Length; i++)
		{
			EnvironmentLook environmentLook = envLooks[i];
			if (environmentLook.env != LevelEditor.Instance.Settings.Environment)
			{
				continue;
			}
			for (int j = 0; j < nameLocalisations.Length; j++)
			{
				if (environmentLook.locId == nameLocalisations[j])
				{
					lookMenu.SetValue(j);
					lookMenu.ApplyValue();
				}
			}
			break;
		}
	}

	private List<string> GetLocalisedLookNames()
	{
		List<string> list = new List<string>();
		int[] array = nameLocalisations;
		foreach (int id in array)
		{
			list.Add(LocalisationManager.GetTranslation(id));
		}
		return list;
	}

	private void UpdateLook(int index)
	{
		visualController.Restore();
		index = ((index < entityLooks.Length) ? index : 0);
		for (int i = 0; i < entityLooks.Length; i++)
		{
			entityLooks[i].SetActive(index == i);
		}
		if (entityLooks.Length > 0)
		{
			GameObject entityGO = entityLooks[index];
			visualController.Init(entityGO);
		}
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		if (lookMenu != null)
		{
			lookMenu.Items = GetLocalisedLookNames();
		}
	}
}
