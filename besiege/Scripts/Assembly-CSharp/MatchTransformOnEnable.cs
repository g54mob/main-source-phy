using System.Collections;
using UnityEngine;

public class MatchTransformOnEnable : MonoBehaviour
{
	public Transform toMatch;

	public void OnEnable()
	{
		StartCoroutine(IEOnEnable());
	}

	public IEnumerator IEOnEnable()
	{
		yield return new WaitForEndOfFrame();
		base.transform.position = toMatch.position;
		base.transform.rotation = toMatch.rotation;
		base.transform.localScale = toMatch.localScale;
	}
}
