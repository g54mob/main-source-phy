using System.Collections.Generic;
using UnityEngine;

public class RequisitionConsoleManager : MonoBehaviour
{
	public static RequisitionConsoleManager Instance;

	[Header("Scene References")]
	[Tooltip("The DragSurface (formerly DesktopArea) that cards live on when pulled from the deck.")]
	public DragSurface DragSurface;

	[Tooltip("The DraggableItemDeckArea (formerly PunchcardDeckArea) that cards are dealt into.")]
	public DraggableItemDeckArea DeckArea;

	[Tooltip("The RequisitionSlot that cards are dragged into for requisition.")]
	public RequisitionSlot RequisitionSlot;

	private bool initialized;

	[Tooltip("All PunchcardDefinitionV2 assets loaded from Resources at runtime. Keyed by ID.")]
	[HideInInspector]
	public Dictionary<string, PunchcardDefinitionV2> AllDefinitions { get; private set; }

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void InitializeConsole()
	{
	}

	private bool ValidateRefs()
	{
		return false;
	}

	public void EnsureCards(List<PunchcardDefinitionV2> cards)
	{
	}

	public void AddNewCardsToDeck(List<PunchcardDefinitionV2> newCards)
	{
	}

	public void AddSetCardsToDeck(List<PunchcardDefinitionV2> newCards)
	{
	}

	public void RebuildDeck(List<PunchcardDefinitionV2> exactCards)
	{
	}

	public PunchcardRuntime[] GetAllCards()
	{
		return null;
	}

	public void ClearAllCards()
	{
	}

	private List<PunchcardDefinitionV2> FilterNewDefinitions(List<PunchcardDefinitionV2> source)
	{
		return null;
	}

	private void SpawnIntoDeck(List<PunchcardDefinitionV2> defs)
	{
	}
}
