using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Tutorial/Tutorial Step")]
public class TutorialStep : MonoBehaviour
{
	public enum Type
	{
		ButtonPress = 0,
		KeyboardPress = 1,
		BlockPlacement = 2,
		BlockTypePick = 3,
		BlockRemove = 4,
		CategoryChange = 5,
		ParameterEdit = 6,
		SelectionChange = 7,
		GhostChange = 8,
		BlockHover = 9,
		MouseScroll = 10,
		KeyCombination = 11,
		KeyButtonCombination = 12,
		Any = 64
	}

	public enum ParameterType
	{
		Any = 0,
		Key = 1,
		Slider = 2,
		Toggle = 3
	}

	public UIManager.UIMode modes;

	public Type type;

	public ParameterType parameterType;

	[SerializeField]
	protected ClickBehaviour button;

	[SerializeField]
	private KeyCode key;

	[SerializeField]
	private KeyCode key2;

	[SerializeField]
	private BlockType block;

	private HashSet<BlockType> blockmapperBlockTypes = new HashSet<BlockType>();

	[SerializeField]
	private bool onFirstPoint;

	[SerializeField]
	private int amount = 1;

	[SerializeField]
	public bool followBM;

	public GameObject box;

	public Button closeButton;

	[SerializeField]
	public GameObject[] content = new GameObject[0];

	protected bool isOpen;

	protected int toggleFrame;

	protected bool activeStep;

	protected bool assigned;

	[HideInInspector]
	public TutorialBaseContainer container;

	private int current;

	private Canvas canvas;

	[SerializeField]
	protected float displayDelay;

	[SerializeField]
	protected float disappearDelay;

	protected float timeElapsed;

	[SerializeField]
	protected bool persistIfTimeLeft;

	private int state;

	private bool waitingForComplete;

	public int index { get; private set; }

	public bool Active
	{
		get
		{
			return activeStep && (isOpen || Time.frameCount == toggleFrame);
		}
	}

	public bool JustOpened
	{
		get
		{
			return isOpen && Time.frameCount == toggleFrame;
		}
	}

	public virtual void Prepare(TutorialBaseContainer container, int index, bool isSandbox)
	{
		if (assigned)
		{
			return;
		}
		this.container = container;
		this.index = index;
		assigned = true;
		if (!isSandbox)
		{
			if ((bool)closeButton)
			{
				closeButton.gameObject.SetActive(false);
			}
		}
		else if ((bool)closeButton)
		{
			closeButton.onClick.AddListener(TutorialSystem.Close);
		}
		canvas = base.transform.root.GetComponent<Canvas>();
		UIManager.onUIModeChanged = (Action<UIManager.UIMode>)Delegate.Combine(UIManager.onUIModeChanged, new Action<UIManager.UIMode>(DisplayUIChanged));
		switch (type)
		{
		case Type.BlockPlacement:
			if (Machine.IsDraggedBlock(block))
			{
				if (onFirstPoint)
				{
					ReferenceMaster.onDraggedBlockPlacement = (Action<BlockBehaviour>)Delegate.Combine(ReferenceMaster.onDraggedBlockPlacement, new Action<BlockBehaviour>(OnBlockPlaced));
				}
				else
				{
					ReferenceMaster.onDraggedBlockPlaced = (Action<BlockBehaviour>)Delegate.Combine(ReferenceMaster.onDraggedBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
				}
			}
			else
			{
				ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Combine(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
			}
			break;
		case Type.BlockTypePick:
			StatMaster.SelectedBlockChanged += OnBlockPicked;
			break;
		case Type.ButtonPress:
		case Type.KeyButtonCombination:
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
			if (container.blockTabController != null)
			{
				BlockTabController blockTabController = container.blockTabController;
				blockTabController.OnCategoryChange = (Action)Delegate.Combine(blockTabController.OnCategoryChange, new Action(OnCategoryChanged));
			}
			break;
		case Type.ParameterEdit:
			BlockMapper.OnParameterChange = (Action<MapperType>)Delegate.Combine(BlockMapper.OnParameterChange, new Action<MapperType>(OnParameterChanged));
			break;
		case Type.SelectionChange:
			StatMaster.Mode.SelectionChanged = (Action)Delegate.Combine(StatMaster.Mode.SelectionChanged, new Action(OnGenericComplete));
			break;
		case Type.GhostChange:
			ReferenceMaster.onGhostTransformed = (Action)Delegate.Combine(ReferenceMaster.onGhostTransformed, new Action(OnGhostTransformed));
			break;
		case Type.BlockHover:
			ReferenceMaster.onBlockHover = (Action<bool>)Delegate.Combine(ReferenceMaster.onBlockHover, new Action<bool>(BlockHovered));
			break;
		case Type.Any:
			block = BlockType.StartingBlock;
			StatMaster.SelectedBlockChanged += OnBlockPicked;
			ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Combine(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
			ReferenceMaster.onBlockRemoved = (Action<int>)Delegate.Combine(ReferenceMaster.onBlockRemoved, new Action<int>(OnBlockRemoved));
			StatMaster.Mode.SelectionChanged = (Action)Delegate.Combine(StatMaster.Mode.SelectionChanged, new Action(OnGenericComplete));
			break;
		}
		blockmapperBlockTypes.Add(block);
	}

	public void OnGenericComplete()
	{
		if (Active)
		{
			Complete();
		}
	}

	public void BlockHovered(bool b)
	{
		if (b)
		{
			OnGenericComplete();
		}
	}

	public void OnParameterChanged(MapperType m)
	{
		switch (parameterType)
		{
		case ParameterType.Any:
			OnGenericComplete();
			break;
		case ParameterType.Key:
			if (m is MKey)
			{
				OnGenericComplete();
			}
			break;
		case ParameterType.Slider:
			if (m is MSlider)
			{
				OnGenericComplete();
			}
			break;
		case ParameterType.Toggle:
			if (m is MToggle)
			{
				OnGenericComplete();
			}
			break;
		}
	}

	public void OnCategoryChanged()
	{
		if (Active)
		{
			Complete();
		}
	}

	public void OnGhostTransformed()
	{
		if (!(SingleInstanceFindOnly<AddPiece>.Instance.CurrentGhost != null) || SingleInstanceFindOnly<AddPiece>.Instance.CurrentGhost.gameObject.activeSelf)
		{
			OnGenericComplete();
		}
	}

	public void OnBlockPlaced(BlockBehaviour b)
	{
		if (Active && b.BlockID != 0 && (block == BlockType.StartingBlock || b.BlockID == (int)block))
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
		if (Active && (block == BlockType.StartingBlock || id == (int)block))
		{
			current++;
			if (current >= amount)
			{
				Complete();
			}
		}
	}

	public void OnBlockPicked(BlockType b)
	{
		if (Active && b != BlockType.StartingBlock && (block == BlockType.StartingBlock || b == block))
		{
			Complete();
		}
	}

	public void OnButtonPressed()
	{
		OnGenericComplete();
	}

	public virtual bool SupportsMode(UIManager.UIMode mode)
	{
		if (mode == UIManager.UIMode.BlockMapper && BlockMapper.CurrentInstance.IsBlock)
		{
			BlockType blockID = (BlockType)(BlockMapper.CurrentInstance.Current as BlockBehaviour).BlockID;
			return (modes & mode) != 0 && blockmapperBlockTypes.Contains(blockID);
		}
		return (modes & mode) != 0;
	}

	public void Open()
	{
		if (displayDelay == 0f)
		{
			activeStep = true;
			Display(UIManager.CurrentUIMode);
		}
		else
		{
			StartCoroutine(OpenDelayed(displayDelay));
		}
	}

	private IEnumerator OpenDelayed(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		activeStep = true;
		Display(UIManager.CurrentUIMode);
	}

	public void Close()
	{
		activeStep = false;
		Display(UIManager.CurrentUIMode);
	}

	private void DisplayUIChanged(UIManager.UIMode mode)
	{
		if (displayDelay == 0f || activeStep)
		{
			Display(mode);
		}
	}

	public void Display(UIManager.UIMode mode)
	{
		bool flag = base.isActiveAndEnabled;
		bool flag2 = flag && activeStep && SupportsMode(mode) && canvas.enabled;
		if (!flag2 && waitingForComplete && !persistIfTimeLeft)
		{
			Next();
		}
		if (StatMaster.isMP && flag2 != isOpen && state == (flag2 ? 3 : 0))
		{
			StopAllCoroutines();
			flag = false;
		}
		if (flag)
		{
			if (flag2 != isOpen)
			{
				toggleFrame = Time.frameCount;
				StopAllCoroutines();
				StartCoroutine(Animate(flag2));
			}
		}
		else
		{
			if (flag2 != isOpen)
			{
				toggleFrame = Time.frameCount;
			}
			isOpen = flag2;
			state = (flag2 ? 3 : 0);
			box.SetActive(flag2);
			box.transform.localScale = Vector3.one * ((!flag2) ? 1f : 0f);
		}
		for (int i = 0; i < content.Length; i++)
		{
			content[i].SetActive(flag2);
		}
		if (flag2 && waitingForComplete && persistIfTimeLeft)
		{
			StartCoroutine(CompleteDelayed(Mathf.Clamp(disappearDelay - timeElapsed, 0f, disappearDelay)));
		}
	}

	public void Update()
	{
		if (!Active || !base.isActiveAndEnabled)
		{
			return;
		}
		switch (type)
		{
		case Type.ButtonPress:
		case Type.KeyboardPress:
			if (Input.GetKeyDown(key))
			{
				Complete();
			}
			break;
		case Type.KeyCombination:
		case Type.KeyButtonCombination:
			if (Input.GetKey(key) && Input.GetKey(key2))
			{
				Complete();
			}
			break;
		case Type.MouseScroll:
			if (Input.mouseScrollDelta.y != 0f)
			{
				Complete();
			}
			break;
		case Type.Any:
			if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse1))
			{
				Complete();
			}
			break;
		}
		if (followBM && !StatMaster.levelSimulating && BlockMapper.IsOpen && isOpen && activeStep)
		{
			Vector3 vector = SingleInstanceFindOnly<AddPiece>.Instance.hudCam.WorldToViewportPoint(BlockMapper.CurrentInstance.transform.position);
			Vector3 a = vector - new Vector3(0.5f, 0.5f, 0f);
			RectTransform component = TutorialSystem.Instance.transform.parent.GetComponent<RectTransform>();
			Vector2 sizeDelta = component.sizeDelta;
			(base.transform as RectTransform).anchoredPosition = Vector3.Scale(a, sizeDelta);
		}
	}

	public virtual void Complete()
	{
		if (JustOpened || TutorialBaseContainer.Reloading)
		{
			return;
		}
		if (disappearDelay > 0f)
		{
			if (!waitingForComplete)
			{
				StartCoroutine(CompleteDelayed(disappearDelay));
			}
		}
		else
		{
			Next();
		}
	}

	private IEnumerator CompleteDelayed(float delayTime)
	{
		waitingForComplete = true;
		float t = 0f;
		while (waitingForComplete && t < delayTime)
		{
			t += Time.unscaledDeltaTime;
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		Next();
	}

	public virtual void Next()
	{
		Cleanup();
		container.Next(this);
	}

	public void AddBlockmapperBlockType(BlockType blockType)
	{
		blockmapperBlockTypes.Add(blockType);
	}

	protected virtual void OnDisable()
	{
		Cleanup();
	}

	private void Cleanup()
	{
		if (!assigned)
		{
			return;
		}
		waitingForComplete = false;
		switch (type)
		{
		case Type.BlockPlacement:
			if (Machine.IsDraggedBlock(block))
			{
				if (onFirstPoint)
				{
					ReferenceMaster.onDraggedBlockPlacement = (Action<BlockBehaviour>)Delegate.Remove(ReferenceMaster.onDraggedBlockPlacement, new Action<BlockBehaviour>(OnBlockPlaced));
				}
				else
				{
					ReferenceMaster.onDraggedBlockPlaced = (Action<BlockBehaviour>)Delegate.Remove(ReferenceMaster.onDraggedBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
				}
			}
			else
			{
				ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Remove(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
			}
			break;
		case Type.BlockTypePick:
			StatMaster.SelectedBlockChanged -= OnBlockPicked;
			break;
		case Type.ButtonPress:
		case Type.KeyButtonCombination:
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
			if (container.blockTabController != null)
			{
				BlockTabController blockTabController = container.blockTabController;
				blockTabController.OnCategoryChange = (Action)Delegate.Remove(blockTabController.OnCategoryChange, new Action(OnCategoryChanged));
			}
			break;
		case Type.ParameterEdit:
			BlockMapper.OnParameterChange = (Action<MapperType>)Delegate.Remove(BlockMapper.OnParameterChange, new Action<MapperType>(OnParameterChanged));
			break;
		case Type.SelectionChange:
			StatMaster.Mode.SelectionChanged = (Action)Delegate.Remove(StatMaster.Mode.SelectionChanged, new Action(OnGenericComplete));
			break;
		case Type.GhostChange:
			ReferenceMaster.onGhostTransformed = (Action)Delegate.Remove(ReferenceMaster.onGhostTransformed, new Action(OnGhostTransformed));
			break;
		case Type.Any:
			StatMaster.SelectedBlockChanged -= OnBlockPicked;
			ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Remove(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(OnBlockPlaced));
			ReferenceMaster.onBlockRemoved = (Action<int>)Delegate.Remove(ReferenceMaster.onBlockRemoved, new Action<int>(OnBlockRemoved));
			StatMaster.Mode.SelectionChanged = (Action)Delegate.Remove(StatMaster.Mode.SelectionChanged, new Action(OnGenericComplete));
			break;
		}
		for (int i = 0; i < content.Length; i++)
		{
			if (content[i] != null)
			{
				content[i].SetActive(false);
			}
		}
		if (!base.enabled && box != null)
		{
			box.SetActive(false);
		}
		UIManager.onUIModeChanged = (Action<UIManager.UIMode>)Delegate.Remove(UIManager.onUIModeChanged, new Action<UIManager.UIMode>(DisplayUIChanged));
		assigned = false;
	}

	private IEnumerator Animate(bool open)
	{
		isOpen = open;
		box.transform.localScale = Vector3.one * ((!open) ? 1f : 0f);
		if (open)
		{
			box.SetActive(true);
			yield return new WaitForSecondsRealtime(TutorialSystem.OpenAnimWait);
		}
		float d = TutorialSystem.AnimDuration;
		if (open)
		{
			yield return StartCoroutine(AnimateBox(0f, 1.05f, d * 0.65f));
			state = 0;
			yield return StartCoroutine(AnimateBox(1.05f, 0.97f, d * 0.2f));
			state = 1;
			yield return StartCoroutine(AnimateBox(0.97f, 1.01f, d * 0.1f));
			state = 2;
			yield return StartCoroutine(AnimateBox(1.01f, 1f, d * 0.05f));
			state = 3;
		}
		else
		{
			yield return StartCoroutine(AnimateBox(1f, 1.05f, d * 0.1f));
			state = 2;
			yield return StartCoroutine(AnimateBox(1.05f, 0f, d * 0.4f));
			state = 0;
		}
		if (!open)
		{
			box.SetActive(false);
			if (followBM)
			{
				(base.transform as RectTransform).anchoredPosition = Vector3.zero;
			}
		}
	}

	private IEnumerator AnimateBox(float start, float end, float duration)
	{
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			box.transform.localScale = Vector3.one * Mathf.Lerp(start, end, pct);
			yield return null;
		}
		box.transform.localScale = Vector3.one * end;
	}

	private void OnDestroy()
	{
		UIManager.onUIModeChanged = (Action<UIManager.UIMode>)Delegate.Remove(UIManager.onUIModeChanged, new Action<UIManager.UIMode>(DisplayUIChanged));
	}
}
