using BlockMapperInternal;
using Selectors;
using UnityEngine;

public class PrefabNameWidget : ParameterWidget
{
	public UIButton activeButton;

	public Renderer buttonIcon;

	public Texture2D activeIcon;

	public Texture2D inactiveIcon;

	public TextHolder nameHolder;

	private GenericEntity entity;

	public void Awake()
	{
		activeButton.Click += OnActiveChange;
		nameHolder.TextChanged += OnManualInput;
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		entity = parameter as GenericEntity;
		if (entity.activeOnStart == null)
		{
			entity.Init();
		}
		entity.activeOnStart.Toggled += OnVisibleToggle;
		entity.logicName.TextChanged += OnNameChanged;
		UpdateVisual();
	}

	protected void OnDisable()
	{
		if (entity != null)
		{
			entity.activeOnStart.Toggled -= OnVisibleToggle;
			entity.logicName.TextChanged -= OnNameChanged;
		}
	}

	protected void OnDestroy()
	{
		activeButton.Click -= OnActiveChange;
		nameHolder.TextChanged -= OnManualInput;
	}

	protected void UpdateVisual()
	{
		UpdateButton();
		UpdateText();
	}

	protected void OnActiveChange()
	{
		entity.activeOnStart.IsActive = !entity.activeOnStart.IsActive;
		OnEdit(entity.activeOnStart);
	}

	private void OnManualInput(string newValue)
	{
		if (isEditing)
		{
			entity.logicName.SetValue(newValue);
			OnEdit(entity.logicName);
		}
	}

	private void OnNameChanged(string newText)
	{
		if (isEditing)
		{
			UpdateText();
		}
	}

	protected void OnVisibleToggle(bool toggle)
	{
		UpdateButton();
	}

	private void UpdateButton()
	{
		buttonIcon.material.mainTexture = ((!entity.ActiveOnStart()) ? inactiveIcon : activeIcon);
	}

	private void UpdateText()
	{
		string text = entity.LogicName();
		if (entity.prefab.name.Equals(text))
		{
			nameHolder.SetText(text);
			return;
		}
		WorkshopManager.VerifyString(text, delegate(WorkshopManager.VerifyStringResult res, string str)
		{
			if (nameHolder != null)
			{
				nameHolder.SetText(str);
			}
		});
	}

	private void OnEdit(MapperType mapperType)
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if (instance != null)
		{
			instance.OnEditField(entity, mapperType);
		}
		else
		{
			mapperType.ApplyValue();
		}
	}
}
