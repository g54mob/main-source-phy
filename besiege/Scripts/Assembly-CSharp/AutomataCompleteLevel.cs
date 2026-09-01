using System.Collections;
using UnityEngine;

public class AutomataCompleteLevel : MonoBehaviour
{
	public Transform graveStoneParent;

	public float graveBreakTimer = 0.15f;

	private bool haveGravesExploded;

	private void Update()
	{
		if (WinCondition.hasWon && !haveGravesExploded)
		{
			StartCoroutine(ExplodeGraves());
		}
	}

	private IEnumerator ExplodeGraves()
	{
		haveGravesExploded = true;
		yield return new WaitForSeconds(0.4f);
		for (int i = 0; i < graveStoneParent.childCount; i++)
		{
			yield return StartCoroutine(ExplodeSingleGrave(graveStoneParent.GetChild(i), (float)i * graveBreakTimer));
		}
	}

	private IEnumerator ExplodeSingleGrave(Transform obj, float timer)
	{
		yield return new WaitForSeconds(timer);
		obj.GetComponent<BreakOnForce>().ExternalBreak();
	}
}
