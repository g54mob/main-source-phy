using System.Collections;
using UnityEngine;

public class AnimationRandomDelayer : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine(DelayAnimation());
	}

	private IEnumerator DelayAnimation()
	{
		GetComponent<Animator>().enabled = false;
		yield return new WaitForSeconds(Random.Range(0f, 0.75f));
		GetComponent<Animator>().enabled = true;
	}
}
