using System;
using System.Collections.Generic;
using System.Linq;
using BlockMapperInternal;
using Localisation;
using Selectors;
using UnityEngine;

public class InputGroupWidget : InputBaseWidget, IWidgetContainer
{
	[Header("Cosmetic Settings")]
	[SerializeField]
	private GameObject hoverBG;

	[SerializeField]
	private TextHolder groupName;

	[SerializeField]
	private Renderer highlightIcon;

	[SerializeField]
	private Material highlightGroupMat;

	[SerializeField]
	private Material oldGroupMat;

	[SerializeField]
	private Renderer dropdownIcon;

	[SerializeField]
	private DynamicText groupCount;

	[SerializeField]
	private GameObject countBG;

	[SerializeField]
	private FilterRendererPair centerBlock;

	[SerializeField]
	private GameObject noThirdPlaceholder;

	[SerializeField]
	private UIButtonExtended hideGroup;

	[SerializeField]
	private Texture2D hide;

	[SerializeField]
	private Texture2D unhide;

	public WidgetController childController;

	private string defaultName;

	private Transform hoverBGTransform;

	private List<InputGroup.BlockEntry> filteredList;

	private float widgetHeight = 1f;

	private InputGroupKeySelector inputSelector;

	public bool isTruelyHovered { get; private set; }

	public float TopValue()
	{
		return base.transform.position.y;
	}

	public float HeightValue()
	{
		return container.Background.localScale.y;
	}

	public float ZValue()
	{
		return base.transform.position.z;
	}

	protected void Awake()
	{
		string text = "Prefabs/BlockMapper/Input/";
		childController = new WidgetController(text + "InputChildContainer");
		inputSelector = keySelector as InputGroupKeySelector;
		groupName.TextChanged += OnGroupNameChanged;
		hideGroup.Down += ToggleIgnored;
		hoverBGTransform = hoverBG.transform;
		inputSelector.KeyModified += OnKeyModified;
		inputSelector.KeysChanged += OnKeysChanged;
		InputGroupKeySelector inputGroupKeySelector = inputSelector;
		inputGroupKeySelector.OnChangeOther = (Action<int, KeyCode>)Delegate.Combine(inputGroupKeySelector.OnChangeOther, new Action<int, KeyCode>(OnChangeOther));
	}

	protected override void UpdateBlockVis(List<BlockType> types)
	{
		GameObject gameObject = centerBlock.filter.transform.parent.gameObject;
		if (types.Count > 1)
		{
			countBG.SetActive(true);
			gameObject.SetActive(false);
			if (types.Count < 3)
			{
				noThirdPlaceholder.SetActive(true);
			}
			base.UpdateBlockVis(types);
		}
		else
		{
			countBG.SetActive(false);
			noThirdPlaceholder.SetActive(false);
			if (types.Count > 0)
			{
				gameObject.SetActive(true);
				SetIconTo(types[0], centerBlock);
			}
			for (int i = 0; i < blockPairs.Length; i++)
			{
				blockPairs[i].filter.transform.parent.gameObject.SetActive(false);
			}
		}
		if (filteredList.Count > 1)
		{
			groupCount.gameObject.SetActive(true);
			if (groupCount.serializedText != string.Empty + filteredList.Count)
			{
				ReferenceMaster.SetDynamicText(groupCount, string.Empty + filteredList.Count);
			}
		}
		else
		{
			groupCount.gameObject.SetActive(false);
		}
		hideGroup.icon.material.mainTexture = ((OverviewBlockMapper.Filter != OverviewBlockMapper.BlockFilter.Hidden) ? hide : unhide);
	}

	private void OnGroupNameChanged(string value)
	{
		if (isEditing && !value.Equals(defaultName) && (string.IsNullOrEmpty(group.CustomName) || !group.CustomName.Equals(value)))
		{
			OverviewBlockMapper currentInstance = OverviewBlockMapper.CurrentInstance;
			Machine current = currentInstance.Current;
			UndoActionGroupName action = new UndoActionGroupName(current, group.key, group.CustomName, value);
			group.CustomName = value;
			current.UndoSystem.AddAction(action);
			OverviewBlockMapper.SaveInputGroups(current, currentInstance.inputGroups);
		}
	}

	private string GetDefaultName(List<InputGroup.BlockEntry> entries)
	{
		MKey firstKey = entries[0].key;
		BlockBehaviour firstBlock = entries[0].block;
		bool flag = entries.FindIndex((InputGroup.BlockEntry x) => x.block.Prefab.Type != firstBlock.Prefab.Type) == -1;
		bool flag2 = false;
		if (flag && firstBlock.Keys.Count() > 1)
		{
			bool flag3 = true;
			if (firstBlock is SpringCode)
			{
				flag3 = (firstBlock as SpringCode).winchMode;
			}
			else if (firstBlock is CogMotorControllerHinge)
			{
				flag3 = (firstBlock as CogMotorControllerHinge).allowControl;
			}
			flag2 = flag3 && entries.FindIndex((InputGroup.BlockEntry x) => !x.key.Key.Equals(firstKey.Key)) == -1;
		}
		string text = ReferenceMaster.TranslateBlockName(firstBlock.Prefab.Type).ToUpper();
		string systemLanguage = SingleInstance<LocalisationManager>.Instance.CurrentTranslationFile.SystemLanguage;
		if (flag)
		{
			if (flag2)
			{
				text = text + " " + firstKey.DisplayName.ToUpper();
			}
			else if (entries.Count > 1 && systemLanguage.Equals("English"))
			{
				text += "S";
			}
		}
		else if (!flag2)
		{
			text = string.Format(LocalisationManager.GetTranslation(3421), index);
		}
		return text;
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		index = i;
		group = (InputGroup)parameter;
		inputSelector.Key = group.key;
		inputSelector.Init(group.otherKeys);
		Rebuild();
		ToggleHover(false);
	}

	public float WidgetHeight()
	{
		float y = hoverBGTransform.lossyScale.y;
		return widgetHeight * y;
	}

	private void Rebuild()
	{
		Clear();
		OverviewBlockMapper obm = OverviewBlockMapper.CurrentInstance;
		filteredList = group.blockList.FindAll((InputGroup.BlockEntry x) => obm.FilterContains(x.block.Prefab.Type));
		defaultName = GetDefaultName(filteredList);
		if (filteredList.Count > 1)
		{
			dropdownIcon.gameObject.SetActive(true);
			int num = 270;
			if (group.dropdownOpen)
			{
				for (int num2 = 0; num2 < filteredList.Count; num2++)
				{
					InputGroup.BlockEntry item = filteredList[num2];
					childController.RegisterToggle(group, group.blockList.IndexOf(item));
				}
				num = 90;
			}
			dropdownIcon.transform.localRotation = Quaternion.Euler(0f, 0f, num);
		}
		else
		{
			dropdownIcon.gameObject.SetActive(false);
		}
		groupName.SetText((!string.IsNullOrEmpty(group.CustomName)) ? group.CustomName : defaultName);
		UpdateBlockVis(GetFirstBlockTypes());
		childController.Display(this, WidgetHeight());
		float num3 = 1f / base.transform.lossyScale.y;
		float y = childController.EndPosition * num3;
		Transform transform = container.Background.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	private List<BlockType> GetFirstBlockTypes()
	{
		List<BlockType> list = new List<BlockType>();
		for (int i = 0; i < filteredList.Count; i++)
		{
			BlockBehaviour block = filteredList[i].block;
			BlockType type = block.Prefab.Type;
			if (!list.Contains(type))
			{
				list.Add(type);
			}
		}
		return list;
	}

	private void Clear()
	{
		childController.Clear();
	}

	public override void ResetToPool()
	{
		Clear();
		for (int i = 0; i < childController.ContainerCount; i++)
		{
			childController.containers[i].widget.ResetToPool();
		}
		base.ResetToPool();
	}

	private void ToggleIgnored()
	{
		MKey key = keySelector.Key;
		key.Ignored = !key.Ignored;
		OverviewBlockMapper.CurrentInstance.OnEditGroupKey(group, false);
	}

	private void OnKeysChanged()
	{
		if (isEditing)
		{
			OverviewBlockMapper.CurrentInstance.OnEditGroupKey(group, false);
		}
	}

	private void OnChangeOther(int index, KeyCode key)
	{
		if (isEditing)
		{
			OverviewBlockMapper.CurrentInstance.OnEditOtherKey(group, index, key);
		}
	}

	private void OnKeyModified(int index, KeyCode keyCode)
	{
		if (isEditing)
		{
			group.key.AddOrReplaceKey(index, keyCode);
			OverviewBlockMapper.CurrentInstance.OnEditGroupKey(group, false);
		}
	}

	public void ToggleHover(bool over, int index = -1)
	{
		bool flag = over && index == -1;
		if (hideGroup.icon.enabled != flag)
		{
			hideGroup.icon.enabled = flag;
		}
		for (int i = 0; i < childController.ContainerCount; i++)
		{
			InputChildWidget inputChildWidget = childController.containers[i].widget as InputChildWidget;
			if (inputChildWidget != null)
			{
				inputChildWidget.ToggleHover(over && (index == -1 || index == inputChildWidget.Index));
			}
		}
	}
}
