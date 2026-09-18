using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VInspector;

public class PuzzleController : MonoBehaviour, IHighlight
{
	[SerializeField]
	private Outline outline;

	[SerializeField]
	private bool disableOutlineAfterCompletion;

	[SerializeField]
	private bool activatedOnce;

	[SerializeField]
	private bool activateOnCompletion;

	[SerializeField]
	private bool isActivateSolutionAfterLoad;

	[SerializeField]
	private bool disablePuzzleControllerOnComplitation;

	[SerializeField]
	private ParticleSystem interactionParticleSystem;

	[SerializeField]
	[ReadOnly]
	private bool hasBeenActivated;

	public TutorialStepType completionTutorialStepType;

	public AchievementTypes completionAchievementType;

	[Space]
	[Foldout("Camera mover")]
	[SerializeField]
	private Transform cameraTarget;

	[SerializeField]
	private bool moveCameraOnInteraction;

	[SerializeField]
	[ReadOnly]
	protected bool isCameraZoomed;

	[EndFoldout]
	[Space(5f)]
	[SerializeField]
	private PuzzleTextTypes puzzleTextType;

	[Space]
	public SortingPuzzleType interactionSortingPuzzleType;

	[SerializeField]
	public List<ItemPlacement> itemPlacementsList = new List<ItemPlacement>();

	[Space]
	[SerializeField]
	private UnityEvent onInteraction;

	[Space]
	[SerializeField]
	[ReadOnly]
	private bool isHighlighted;

	[SerializeField]
	[ReadOnly]
	private bool isHintHighlighted;

	[Space]
	[SerializeField]
	private bool isDebugOn;

	[Space(10f)]
	[Header("Audio")]
	[SerializeField]
	private AudioClipTypes itemPlacedAudioClipType = AudioClipTypes.Puzzle_Item_Placed;

	[SerializeField]
	private AudioClipTypes interactionAudioClipType;

	private void Start()
	{
		foreach (ItemPlacement itemPlacements in itemPlacementsList)
		{
			itemPlacements.isItemAvilable = false;
			itemPlacements.itemGameObject.SetActive(value: false);
			itemPlacements.itemHologramGameObject.SetActive(value: true);
		}
		SetHighlight(flag: false);
		outline.enabled = false;
	}

	public void SetHintHighlight(bool flag)
	{
		isHintHighlighted = flag;
		UpdateOutline();
	}

	public void SetHighlight(bool flag)
	{
		isHighlighted = flag;
		UpdateOutline();
	}

	public void SetInteractionHighlight(bool flag)
	{
		if (flag)
		{
			interactionParticleSystem.Play();
		}
		else
		{
			interactionParticleSystem.Stop();
		}
	}

	private void UpdateOutline()
	{
		outline.enabled = (isHighlighted && (!disableOutlineAfterCompletion || !IsPuzzleComplete())) || isHintHighlighted;
		if (isHintHighlighted)
		{
			outline.OutlineColor = Color.purple;
		}
		else
		{
			outline.OutlineColor = Color.white;
		}
	}

	public void LoadPuzzleItem(PrefabTypes prefabType)
	{
		foreach (ItemPlacement itemPlacements in itemPlacementsList)
		{
			if (itemPlacements.prefabType == prefabType)
			{
				ActivateItemPlacement(itemPlacements);
			}
		}
		if (isActivateSolutionAfterLoad && CanInteractWithPuzzle())
		{
			InteractWithPuzzle();
			if (disablePuzzleControllerOnComplitation)
			{
				DisablePuzzleController();
			}
		}
	}

	public bool CanInteractWithPuzzle()
	{
		if (activatedOnce && hasBeenActivated)
		{
			return false;
		}
		return IsPuzzleComplete();
	}

	public bool IsPuzzleComplete()
	{
		foreach (ItemPlacement itemPlacements in itemPlacementsList)
		{
			if (!itemPlacements.isItemAvilable)
			{
				return false;
			}
		}
		return true;
	}

	public virtual void InteractWithPuzzle()
	{
		if (isDebugOn)
		{
			Debug.Log("Interact With Holder");
		}
		hasBeenActivated = true;
		onInteraction?.Invoke();
		if (moveCameraOnInteraction)
		{
			MoveCameraToPuzzle();
		}
		if (puzzleTextType != PuzzleTextTypes.None)
		{
			SceneSingleton<UIManager>.Instance.ShowPuzzleTextPanel(puzzleTextType);
		}
		if (interactionAudioClipType != AudioClipTypes.None)
		{
			Singleton<AudioManager>.Instance.PlayClip(interactionAudioClipType, base.transform.position);
		}
	}

	public void DisablePuzzleController()
	{
		SetHighlight(flag: false);
		GetComponent<Collider>().enabled = false;
	}

	private void MoveCameraToPuzzle()
	{
		if (isDebugOn)
		{
			Debug.Log($"MoveCameraToPuzzle - isCameraZoomed {isCameraZoomed}");
		}
		if (!isCameraZoomed)
		{
			isCameraZoomed = true;
			SceneSingleton<CharacterCamerasController>.Instance.SetMovingPuzzleCameraTarget(cameraTarget, delegate
			{
				SceneSingleton<UIManager>.Instance.ShowPuzzleCameraZoom(ReturnCamera);
			});
		}
	}

	public virtual void ReturnCamera()
	{
		if (isDebugOn)
		{
			Debug.Log($"ReturnCamera - isCameraZoomed {isCameraZoomed}");
		}
		if (isCameraZoomed)
		{
			isCameraZoomed = false;
			SceneSingleton<CharacterCamerasController>.Instance.ResetMovingCamera();
		}
	}

	public bool IsCameraZoomed()
	{
		return isCameraZoomed;
	}

	public bool CanAddItem(ItemController itemController)
	{
		return GetItemPlacement(itemController.prefabType) != null;
	}

	public void AddItem(ItemController itemController)
	{
		ItemPlacement itemPlacement = GetItemPlacement(itemController.prefabType);
		if (itemPlacement != null)
		{
			itemController.MoveItem(itemPlacement.itemGameObject.transform, delegate
			{
				ActivateItemPlacement(itemPlacement);
				itemController.gameObject.SetActive(value: false);
				Singleton<AudioManager>.Instance.PlayClip(itemPlacedAudioClipType);
				if (activateOnCompletion && CanInteractWithPuzzle())
				{
					InteractWithPuzzle();
					if (disablePuzzleControllerOnComplitation)
					{
						DisablePuzzleController();
					}
				}
			});
		}
		else
		{
			Debug.LogError("itemPlacement != null, prefabType: " + itemController.prefabType);
		}
	}

	private void ActivateItemPlacement(ItemPlacement itemPlacement)
	{
		itemPlacement.isItemAvilable = true;
		itemPlacement.itemHologramGameObject.SetActive(value: false);
		itemPlacement.itemGameObject.SetActive(value: true);
		if (IsPuzzleComplete())
		{
			if (completionTutorialStepType != TutorialStepType.None)
			{
				SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(completionTutorialStepType);
			}
			if (completionAchievementType != AchievementTypes.None)
			{
				Singleton<GameAchievementsManager>.Instance.ActivateAchievement(completionAchievementType);
			}
			isHintHighlighted = false;
			UpdateOutline();
		}
	}

	private ItemPlacement GetItemPlacement(PrefabTypes prefabType)
	{
		foreach (ItemPlacement itemPlacements in itemPlacementsList)
		{
			if (itemPlacements.prefabType == prefabType)
			{
				return itemPlacements;
			}
		}
		return null;
	}

	public bool IsItemAlreadyPlaced(PrefabTypes prefabType)
	{
		foreach (ItemPlacement itemPlacements in itemPlacementsList)
		{
			if (itemPlacements.prefabType == prefabType)
			{
				return itemPlacements.isItemAvilable;
			}
		}
		return false;
	}

	public void PlayTickSound()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Puzzle_ClockTick, base.transform.position);
	}
}
