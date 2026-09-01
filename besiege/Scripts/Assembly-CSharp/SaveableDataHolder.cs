using System;
using System.Collections.Generic;
using System.Linq;
using Localisation;
using Modding.Mapper;
using UnityEngine;

public abstract class SaveableDataHolder : BasicInfo, ILocalisationAware
{
	[NonSerialized]
	public List<MKey> KeyList = new List<MKey>();

	private readonly List<MapperType> mapperTypes = new List<MapperType>();

	private byte[] saveData;

	private bool isModifying;

	[NonSerialized]
	public bool hasLastState;

	[NonSerialized]
	public bool isBMAction;

	public List<MapperType> MapperTypes
	{
		get
		{
			return new List<MapperType>(mapperTypes);
		}
	}

	public IEnumerable<MValue> Values
	{
		get
		{
			return mapperTypes.OfType<MValue>();
		}
	}

	public IEnumerable<MSlider> Sliders
	{
		get
		{
			return mapperTypes.OfType<MSlider>();
		}
	}

	public IEnumerable<MKey> Keys
	{
		get
		{
			return mapperTypes.OfType<MKey>();
		}
	}

	public IEnumerable<MToggle> Toggles
	{
		get
		{
			return mapperTypes.OfType<MToggle>();
		}
	}

	public IEnumerable<MLimits> Limits
	{
		get
		{
			return mapperTypes.OfType<MLimits>();
		}
	}

	public virtual XDataHolder InitialState { get; private set; }

	public XDataHolder LastState { get; protected set; }

	public bool IsModifying
	{
		get
		{
			return isModifying;
		}
	}

	public void SaveInitialData()
	{
		InitialState = new XDataHolder();
		OnSave(InitialState);
		isModifying = false;
	}

	public MKey AddKey(int nameLocalisationId, string key, ControlScheme.BlockControls defaultControl, int controlIndex, KeyCode defaultKey)
	{
		MKey key2 = new MKey(nameLocalisationId, key, InputManager.GetFirstControl(InputManager.Scheme.Blocks, (int)defaultControl, controlIndex, defaultKey));
		return AddKey(key2);
	}

	public MKey AddKey(int nameLocalisationId, string key, KeyCode defaultKey)
	{
		MKey key2 = new MKey(nameLocalisationId, key, defaultKey);
		return AddKey(key2);
	}

	public MKey AddKey(string displayName, string key, KeyCode defaultKey)
	{
		MKey key2 = new MKey(displayName, key, defaultKey);
		return AddKey(key2);
	}

	public MKey AddEmulatorKey(int nameLocalisationId, string key, ControlScheme.BlockControls defaultControl, int controlIndex, KeyCode defaultKey)
	{
		MKey key2 = new MKey(nameLocalisationId, key, InputManager.GetFirstControl(InputManager.Scheme.Blocks, (int)defaultControl, controlIndex, defaultKey), true);
		return AddKey(key2);
	}

	public MKey AddEmulatorKey(int nameLocalisationId, string key, KeyCode defaultKey)
	{
		MKey key2 = new MKey(nameLocalisationId, key, defaultKey, true);
		return AddKey(key2);
	}

	public MKey AddEmulatorKey(string displayName, string key, KeyCode defaultKey)
	{
		MKey key2 = new MKey(displayName, key, defaultKey, true);
		return AddKey(key2);
	}

	public virtual MKey AddKey(MKey key)
	{
		mapperTypes.Add(key);
		KeyList.Add(key);
		return key;
	}

	public MTeam AddTeam(int nameLocalisationId, string key, MPTeam defaultTeam)
	{
		MTeam team = new MTeam(nameLocalisationId, key, defaultTeam);
		return AddTeam(team);
	}

	public MTeam AddTeam(string displayName, string key, MPTeam defaultTeam)
	{
		MTeam team = new MTeam(displayName, key, defaultTeam);
		return AddTeam(team);
	}

	public MTeam AddTeam(MTeam team)
	{
		mapperTypes.Add(team);
		return team;
	}

	public MHealthType AddHealthRange(string displayName, string key, HealthRange defaultTeam)
	{
		MHealthType team = new MHealthType(displayName, key, defaultTeam);
		return AddRange(team);
	}

	public MHealthType AddRange(MHealthType team)
	{
		mapperTypes.Add(team);
		return team;
	}

	public MText AddText(int nameLocalisationId, string key, string defaultText)
	{
		MText key2 = new MText(nameLocalisationId, key, defaultText);
		return AddText(key2);
	}

	public MText AddText(string displayName, string key, string defaultText)
	{
		MText key2 = new MText(displayName, key, defaultText);
		return AddText(key2);
	}

	public MText AddText(MText key)
	{
		mapperTypes.Add(key);
		return key;
	}

	public MValue AddValue(int nameLocalisationId, string key, float defaultValue)
	{
		MValue valueHolder = new MValue(nameLocalisationId, key, defaultValue);
		return AddValue(valueHolder);
	}

	public MValue AddValue(string displayName, string key, float defaultValue)
	{
		MValue valueHolder = new MValue(displayName, key, defaultValue);
		return AddValue(valueHolder);
	}

	public MValue AddValue(int nameLocalisationId, string key, float defaultValue, float min, float max)
	{
		MValue valueHolder = new MValue(nameLocalisationId, key, defaultValue, min, max);
		return AddValue(valueHolder);
	}

	public MValue AddValue(string displayName, string key, float defaultValue, float min, float max)
	{
		MValue valueHolder = new MValue(displayName, key, defaultValue, min, max);
		return AddValue(valueHolder);
	}

	public MValue AddValue(MValue valueHolder)
	{
		mapperTypes.Add(valueHolder);
		return valueHolder;
	}

	public MSlider AddSlider(int nameLocalisationId, string key, float defaultValue, float min, float max, string prefix = "", string suffix = "x")
	{
		MSlider slider = new MSlider(nameLocalisationId, key, defaultValue, min, max, prefix, suffix, false, false);
		return AddSlider(slider);
	}

	public MSlider AddSlider(string displayName, string key, float defaultValue, float min, float max, string prefix = "", string suffix = "x")
	{
		MSlider slider = new MSlider(displayName, key, defaultValue, min, max, prefix, suffix, false, false);
		return AddSlider(slider);
	}

	public MSlider AddSliderUnclamped(int nameLocalisationId, string key, float defaultValue, float min, float max, string prefix = "", string suffix = "x", bool onlyPositive = false)
	{
		MSlider slider = new MSlider(nameLocalisationId, key, defaultValue, min, max, prefix, suffix, true, false, onlyPositive);
		return AddSlider(slider);
	}

	public MSlider AddSliderLooped(int nameLocalisationId, string key, float defaultValue, float min, float max, string prefix = "", string suffix = "x")
	{
		MSlider slider = new MSlider(nameLocalisationId, key, defaultValue, min, max, prefix, suffix, true, true);
		return AddSlider(slider);
	}

	public MSlider AddSliderUnclamped(string displayName, string key, float defaultValue, float min, float max, string prefix = "", string suffix = "x", bool onlyPositive = false)
	{
		MSlider slider = new MSlider(displayName, key, defaultValue, min, max, prefix, suffix, true, false, onlyPositive);
		return AddSlider(slider);
	}

	public MSlider AddSlider(MSlider slider)
	{
		mapperTypes.Add(slider);
		return slider;
	}

	public MColourSlider AddColourSlider(int nameLocalisationId, string key, Color defaultValue, bool snapColors, bool useHue = false)
	{
		MColourSlider slider = new MColourSlider(nameLocalisationId, key, defaultValue, snapColors, useHue);
		return AddColourSlider(slider);
	}

	public MColourSlider AddColourSlider(string displayName, string key, Color defaultValue, bool snapColors, bool useHue = false)
	{
		MColourSlider slider = new MColourSlider(displayName, key, defaultValue, snapColors, useHue);
		return AddColourSlider(slider);
	}

	public MColourSlider AddColourSlider(MColourSlider slider)
	{
		mapperTypes.Add(slider);
		return slider;
	}

	public MMenu AddMenu(string key, int defaultIndex, List<string> items, bool footerMenu = false)
	{
		MMenu menu = new MMenu(key, defaultIndex, items, footerMenu);
		return AddMenu(menu);
	}

	public MMenu AddMenu(MMenu menu)
	{
		mapperTypes.Add(menu);
		return menu;
	}

	public MToggle AddToggle(int nameLocalisationid, string key, bool defaultValue)
	{
		MToggle toggle = new MToggle(nameLocalisationid, key, defaultValue);
		return AddToggle(toggle);
	}

	public MToggle AddToggle(string displayName, string key, bool defaultValue)
	{
		MToggle toggle = new MToggle(displayName, key, defaultValue);
		return AddToggle(toggle);
	}

	public MToggle AddToggle(int nameLocalisationid, string key, string tooltipText, bool defaultValue)
	{
		MToggle toggle = new MToggle(nameLocalisationid, key, defaultValue);
		return AddToggle(toggle);
	}

	public MToggle AddToggle(string displayName, string key, string tooltipText, bool defaultValue)
	{
		MToggle toggle = new MToggle(displayName, key, defaultValue);
		return AddToggle(toggle);
	}

	public MToggle AddToggle(MToggle toggle)
	{
		mapperTypes.Add(toggle);
		return toggle;
	}

	public MLimits AddLimits(int nameLocalisationId, string key, float defaultMin, float defaultMax, float highestAngle, ILimitsDisplay limitsDisplay, bool enabled = true)
	{
		FauxTransform iconInfo = new FauxTransform(Vector3.zero, Quaternion.identity, Vector3.one);
		return AddLimits(nameLocalisationId, key, defaultMin, defaultMax, highestAngle, iconInfo, limitsDisplay, enabled);
	}

	public MLimits AddLimits(string displayName, string key, float defaultMin, float defaultMax, float highestAngle, ILimitsDisplay limitsDisplay, bool enabled = true)
	{
		FauxTransform iconInfo = new FauxTransform(Vector3.zero, Quaternion.identity, Vector3.one);
		return AddLimits(displayName, key, defaultMin, defaultMax, highestAngle, iconInfo, limitsDisplay, enabled);
	}

	public MLimits AddLimits(int nameLocalisationId, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform iconInfo, ILimitsDisplay limitsDisplay, bool enabled = true)
	{
		MLimits limits = new MLimits(nameLocalisationId, key, defaultMin, defaultMax, highestAngle, iconInfo, limitsDisplay);
		return AddLimits(limits, enabled);
	}

	public MLimits AddLimits(string displayName, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform iconInfo, ILimitsDisplay limitsDisplay, bool enabled = true)
	{
		MLimits limits = new MLimits(displayName, key, defaultMin, defaultMax, highestAngle, iconInfo, limitsDisplay);
		return AddLimits(limits, enabled);
	}

	public MLimits AddLimits(MLimits limits, bool enabled)
	{
		MToggle mToggle = AddToggle(2501, "uselimits", enabled);
		mToggle.Toggled += delegate(bool isActive)
		{
			limits.DisplayInMapper = isActive;
		};
		limits.UseLimitsToggle = mToggle;
		mapperTypes.Add(limits);
		return limits;
	}

	public MCustom<T> AddCustom<T>(MCustom<T> custom)
	{
		mapperTypes.Add(custom);
		return custom;
	}

	protected void LoadMapperValues(XDataHolder data)
	{
		List<MapperType> list = MapperTypes;
		for (int i = 0; i < list.Count; i++)
		{
			MapperType mapperType = list[i];
			string key = "bmt-" + mapperType.Key;
			XData xData = data.Read(key);
			if (xData != null || !StatMaster.isPaste)
			{
				mapperType.DeSerialize((xData == null) ? mapperType.defaultData : xData);
			}
		}
	}

	public MapperType GetMapperType(string key)
	{
		foreach (MapperType mapperType in mapperTypes)
		{
			string text = "bmt-" + mapperType.Key;
			if (text.Equals(key))
			{
				return mapperType;
			}
		}
		return null;
	}

	public XData GetData(string key)
	{
		MapperType mapperType = GetMapperType(key);
		return (mapperType == null) ? null : mapperType.Serialize();
	}

	public XData GetLoadData(string key)
	{
		MapperType mapperType = GetMapperType(key);
		return (mapperType == null) ? null : mapperType.SerializeLoadValue();
	}

	public bool Load(XData xData)
	{
		MapperType mapperType = GetMapperType(xData.Key);
		if (mapperType != null)
		{
			mapperType.DeSerialize(xData);
			return true;
		}
		return false;
	}

	protected void SaveMapperValues(XDataHolder data)
	{
		bool flag = StatMaster.SavingXML && OptionsMaster.BesiegeConfig.ExcludeDefaultSaveData;
		for (int i = 0; i < mapperTypes.Count; i++)
		{
			MapperType mapperType = mapperTypes[i];
			if (!flag || !mapperType.isDefaultValue)
			{
				data.Write(mapperType.Serialize());
			}
		}
	}

	public virtual void OnReset()
	{
	}

	public virtual void OnSave(XDataHolder data)
	{
		LastState = data;
		hasLastState = true;
	}

	public virtual void OnSave(XDataHolder data, CopyMode mode)
	{
	}

	public virtual void OnLoad(XDataHolder data, CopyMode mode)
	{
	}

	public virtual void OnLoad(XDataHolder data)
	{
		LastState = data;
		hasLastState = true;
	}

	public virtual void OnMapperOpen()
	{
		isModifying = true;
	}

	public virtual void OnMapperClose()
	{
		isModifying = false;
	}

	public virtual void ResetHolder()
	{
		OnLoad(GetInitialHolder());
		OnReset();
	}

	protected virtual XDataHolder GetInitialHolder()
	{
		return InitialState.Clone();
	}

	public virtual void OnLocalisationChange()
	{
		foreach (MapperType mapperType in mapperTypes)
		{
			mapperType.ResetLocalisation();
		}
	}
}
