using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSaveData
{
	[SerializeField]
	public string saveFileName;

	[SerializeField]
	public bool isGameEnded;

	[SerializeField]
	public int catsPettedCount;

	[SerializeField]
	public int abilitiesUsed;

	[SerializeField]
	public int currentGameHours;

	[SerializeField]
	public int currentGameMinutes;

	[SerializeField]
	public int currentGameSeconds;

	[SerializeField]
	public string gameTimerString;

	[SerializeField]
	public List<bool> isCatsPettedList;

	[SerializeField]
	public PuzzleSaveData puzzleSaveData;

	[SerializeField]
	public List<PotionSaveData> potionSaveDatasList = new List<PotionSaveData>();

	[SerializeField]
	public List<SortingPuzzleType> solvedSortingPuzzleTypesList = new List<SortingPuzzleType>();

	[SerializeField]
	public int inventoryUpgradeIndex;

	[SerializeField]
	public int speedUpgradeIndex;

	[SerializeField]
	public int currentHighlightIndex;

	[SerializeField]
	public int currentShelvesHighlightIndex;

	[SerializeField]
	public int currentAssembleIndex;

	[SerializeField]
	public bool isSprintUpgraded;

	[SerializeField]
	public float currentHighlightTimer;

	[SerializeField]
	public float currentAssembleTimer;

	[SerializeField]
	public float currentShelveHighlightMaxTimer;

	[Space]
	[SerializeField]
	public float highlightPuzzleTimer;

	[SerializeField]
	public float revealSolutionTimer;
}
