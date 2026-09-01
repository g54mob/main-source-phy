using System;
using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial Step (Alternative)")]
public class TutorialStepAlternative : MonoBehaviour
{
	public enum Type
	{
		ButtonPress = 0,
		KeyboardPress = 1,
		BlockPlacement = 2,
		BlockTypePick = 3,
		BlockRemove = 4,
		CategoryChange = 5,
		ParameterEdit = 6
	}

	public TutorialStep baseStep;

	public Type type;

	[SerializeField]
	protected ClickBehaviour button;

	[SerializeField]
	private KeyCode key;

	[SerializeField]
	private BlockType block;

	[SerializeField]
	private int amount = 1;

	[SerializeField]
	private TutorialStep.ParameterType parameterType;

	private int current;

	private void Start()
	{
		switch (type)
		{
		case Type.BlockPlacement:
			ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Combine(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
			break;
		case Type.BlockTypePick:
			StatMaster.SelectedBlockChanged += OnBlockPicked;
			break;
		case Type.ButtonPress:
			if (button != null)
			{
				ClickBehaviour clickBehaviour = button;
				clickBehaviour.OnActivation = (Action)Delegate.Combine(clickBehaviour.OnActivation, new Action(OnButtonPressed));
			}
			break;
		case Type.BlockRemove:
			ReferenceMaster.onBlockRemoved = (Action<int>)Delegate.Combine(ReferenceMaster.onBlockRemoved, new Action<int>(OnBlockRemoved));
			break;
		case Type.CategoryChange:
			if (baseStep.container.blockTabController != null)
			{
				BlockTabController blockTabController = baseStep.container.blockTabController;
				blockTabController.OnCategoryChange = (Action)Delegate.Combine(blockTabController.OnCategoryChange, new Action(OnActionComplete));
			}
			break;
		case Type.ParameterEdit:
			BlockMapper.OnParameterChange = (Action<MapperType>)Delegate.Combine(BlockMapper.OnParameterChange, new Action<MapperType>(OnParameterChanged));
			baseStep.AddBlockmapperBlockType(block);
			break;
		case Type.KeyboardPress:
			break;
		}
	}

	public void Update()
	{
		if (baseStep.Active && base.isActiveAndEnabled)
		{
			Type type = this.type;
			if ((type == Type.ButtonPress || type == Type.KeyboardPress) && Input.GetKeyDown(key))
			{
				Complete();
			}
		}
	}

	public void OnBlockPlaced(BlockBehaviour b)
	{
		if (baseStep.Active && b.BlockID != 0 && (block == BlockType.StartingBlock || b.BlockID == (int)block))
		{
			current++;
			if (current >= amount)
			{
				Complete();
			}
		}
	}

	public void OnBlockRemoved(int id)
	{
		if (baseStep.Active && (block == BlockType.StartingBlock || id == (int)block))
		{
			current++;
			if (current >= amount)
			{
				Complete();
			}
		}
	}

	public void OnParameterChanged(MapperType m)
	{
		switch (parameterType)
		{
		case TutorialStep.ParameterType.Any:
			Complete();
			break;
		case TutorialStep.ParameterType.Key:
			if (m is MKey)
			{
				Complete();
			}
			break;
		case TutorialStep.ParameterType.Slider:
			if (m is MSlider)
			{
				Complete();
			}
			break;
		case TutorialStep.ParameterType.Toggle:
			if (m is MToggle)
			{
				Complete();
			}
			break;
		}
	}

	public void OnBlockPicked(BlockType b)
	{
		if (baseStep.Active && b != BlockType.StartingBlock && (block == BlockType.StartingBlock || b == block))
		{
			Complete();
		}
	}

	public void OnButtonPressed()
	{
		if (baseStep.Active)
		{
			Complete();
		}
	}

	public void OnActionComplete()
	{
		if (baseStep.Active)
		{
			Complete();
		}
	}

	public void Complete()
	{
		baseStep.Complete();
	}

	protected virtual void OnDisable()
	{
		switch (type)
		{
		case Type.BlockPlacement:
			ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Remove(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
			break;
		case Type.BlockTypePick:
			StatMaster.SelectedBlockChanged -= OnBlockPicked;
			break;
		case Type.ButtonPress:
			if (button != null)
			{
				ClickBehaviour clickBehaviour = button;
				clickBehaviour.OnActivation = (Action)Delegate.Remove(clickBehaviour.OnActivation, new Action(OnButtonPressed));
			}
			break;
		case Type.BlockRemove:
			ReferenceMaster.onBlockRemoved = (Action<int>)Delegate.Remove(ReferenceMaster.onBlockRemoved, new Action<int>(OnBlockRemoved));
			break;
		case Type.CategoryChange:
			if (baseStep.container.blockTabController != null)
			{
				BlockTabController blockTabController = baseStep.container.blockTabController;
				blockTabController.OnCategoryChange = (Action)Delegate.Remove(blockTabController.OnCategoryChange, new Action(OnActionComplete));
			}
			break;
		case Type.ParameterEdit:
			BlockMapper.OnParameterChange = (Action<MapperType>)Delegate.Remove(BlockMapper.OnParameterChange, new Action<MapperType>(OnParameterChanged));
			break;
		case Type.KeyboardPress:
			break;
		}
	}
}
