using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

public class MultiLevelBlock : ColorLevelBlock, ILocalisationAware
{
	[Serializable]
	public class EnvironmentLook
	{
		public LevelSettings.LevelEnvironment env;

		public bool applicable = true;

		public Color color = Color.white;

		[Range(0f, 1f)]
		public float brightnessModifier = 1f;
	}

	public Renderer[] entityLooks;

	public int[] nameLocalisations;

	public EnvironmentLook[] envLooks;

	public int version = 1;

	public bool useVersion;

	protected MMenu lookMenu;

	private int currentLook;

	private Color currentColor = Color.magenta;

	public override void Init()
	{
		if (!isInitialized)
		{
			if (entityLooks.Length > 0)
			{
				lookMenu = AddMenu("look", 0, GetLocalisedLookNames());
				lookMenu.ValueChanged += UpdateLook;
			}
			base.Init();
			if (entityLooks.Length > 0)
			{
				UpdateLook(lookMenu.Value);
			}
			colourSlider.ValueChanged += ColourChanged;
			correctColour = true;
		}
	}

	protected override void FinalizeInit()
	{
		SetBlockColor(currentColor);
	}

	private List<string> GetLocalisedLookNames()
	{
		List<string> list = new List<string>();
		if (nameLocalisations.Length == 0)
		{
			return list;
		}
		int[] array = nameLocalisations;
		foreach (int id in array)
		{
			list.Add(LocalisationManager.GetTranslation(id));
		}
		return list;
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
			if (environmentLook.env == levelEditor.Settings.Environment && environmentLook.applicable)
			{
				lookMenu.SetValue(i);
				lookMenu.ApplyValue();
				break;
			}
		}
	}

	private void UpdateLook(int index)
	{
		EntityVisualController component = GetComponent<EntityVisualController>();
		bool flag = component != null;
		if (flag)
		{
			component.Restore();
		}
		index = ((index < entityLooks.Length) ? index : 0);
		for (int i = 0; i < entityLooks.Length; i++)
		{
			entityLooks[i].gameObject.SetActive(index == i);
		}
		if (flag && entityLooks.Length > 0)
		{
			GameObject entityGO = entityLooks[index].gameObject;
			component.Init(entityGO);
		}
		if (currentLook != index)
		{
			if (useVersion)
			{
				version = 1;
			}
			SetBlockColor(envLooks[currentLook].color);
			currentLook = index;
			ResetToDefaultColor();
		}
		SetCurrentColor(currentColor);
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		if (lookMenu != null)
		{
			lookMenu.Items = GetLocalisedLookNames();
		}
	}

	protected override void ResetToDefaultColor()
	{
		if (defaultToMaterialColour)
		{
			currentColor = envLooks[currentLook].color;
			def = ColorToVector3(currentColor);
			colourSlider.Value = currentColor;
		}
	}

	protected override void SetCurrentColor(Color value)
	{
		entityLooks[currentLook].material.SetColor("_Color", value);
		currentColor = value;
	}

	protected override float GetBrightness()
	{
		return envLooks[currentLook].brightnessModifier * brightnessModifier;
	}

	public override void OnSave(XDataHolder data)
	{
		if (useVersion)
		{
			data.Write("bmt-version", version);
		}
		base.OnSave(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		if (useVersion)
		{
			if (!data.HasKey("bmt-version"))
			{
				version = 0;
				data.Write("bmt-version", version);
			}
			else
			{
				int num = data.ReadInt("bmt-version");
				version = num;
			}
			if (version < 1)
			{
				data.Write("bmt-colour", currentColor);
			}
		}
		base.OnLoad(data);
	}
}
