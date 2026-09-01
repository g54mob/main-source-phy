using BlockMapperInternal;
using UnityEngine;

public class SettingsWidget : ParameterWidget
{
	public UIButton copy;

	public UIButton paste;

	public UIButton reset;

	public AudioSource audioSource;

	protected LevelEntity entity;

	public void Awake()
	{
		copy.Down += Copy;
		paste.Down += Paste;
		reset.Down += Reset;
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		GenericEntity genericEntity = parameter as GenericEntity;
		entity = genericEntity.entity;
	}

	public void Copy()
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if (!instance)
		{
			BlockMapper.clipboard = new XDataHolder();
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.Current.OnSave(BlockMapper.clipboard);
				if (currentInstance.IsBlock)
				{
					XDataHolder xDataHolder = BlockMapper.clipboard.Clone();
					xDataHolder.EraseCustomBlockData();
					BlockMapper.clipboard = xDataHolder;
				}
			}
		}
		BlockMapper.entityClipboard = BlockMapper.CurrentInstance.Entity;
		audioSource.Play();
	}

	public void Paste()
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if ((bool)instance)
		{
			if (BlockMapper.entityClipboard != null)
			{
				instance.OnPaste(BlockMapper.entityClipboard, CopyMode.Parameters);
			}
		}
		else if (BlockMapper.clipboard != null)
		{
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.Current.isBMAction = true;
				currentInstance.Current.OnLoad(BlockMapper.clipboard);
				currentInstance.Current.isBMAction = false;
				currentInstance.Refresh();
			}
		}
		audioSource.Play();
	}

	public void Reset()
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if ((bool)instance)
		{
			instance.OnReset();
		}
		else
		{
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.Current.isBMAction = true;
				currentInstance.Current.OnLoad(currentInstance.Current.InitialState);
				currentInstance.Current.OnReset();
				currentInstance.Current.isBMAction = false;
				currentInstance.Refresh();
			}
		}
		audioSource.Play();
	}
}
