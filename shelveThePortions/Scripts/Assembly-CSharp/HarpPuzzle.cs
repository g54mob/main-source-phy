using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class HarpPuzzle : PuzzleController
{
	[Space(10f)]
	[Header("Harp Puzzle")]
	[Space(5f)]
	[SerializeField]
	[MinMaxSlider(-5f, 5f)]
	private Vector2 spawnWidthRange;

	[SerializeField]
	private float timeBetweenSpawns;

	[SerializeField]
	private Vector3 musicNoteMoveTarget;

	[SerializeField]
	private Transform spawnCenter;

	[SerializeField]
	private float musicNoteMovementDuration;

	[SerializeField]
	private AnimationCurve musicNoteMovementCurve;

	[SerializeField]
	private AnimationCurve musicNoteFadingCurve;

	[SerializeField]
	private List<Sprite> musicNoteSpries;

	[SerializeField]
	private Color invisableColor;

	[SerializeField]
	[ReadOnly]
	private bool isSpawning;

	public override void InteractWithPuzzle()
	{
		if (!isSpawning)
		{
			base.InteractWithPuzzle();
			StartCoroutine(ISpawnNotes());
		}
	}

	private IEnumerator ISpawnNotes()
	{
		isSpawning = true;
		for (int x = 0; x < musicNoteSpries.Count; x++)
		{
			Vector3 vector = spawnCenter.position + new Vector3(0f, 0f, Random.Range(spawnWidthRange.x, spawnWidthRange.y));
			SpriteRenderer component = MasterPool.Get(PrefabTypes.MusicNote, vector).GetComponent<SpriteRenderer>();
			component.sprite = musicNoteSpries[x];
			component.color = Color.white;
			TweenController.DOMove(component.transform, vector + musicNoteMoveTarget, musicNoteMovementDuration, musicNoteMovementCurve);
			TweenController.ChangeColor(component, Color.white, invisableColor, musicNoteMovementDuration, musicNoteFadingCurve);
			yield return new WaitForSeconds(timeBetweenSpawns);
		}
		isSpawning = false;
	}
}
