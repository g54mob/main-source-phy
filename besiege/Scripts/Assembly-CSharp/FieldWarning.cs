using System.Collections;
using UnityEngine;

public class FieldWarning : MonoBehaviour
{
	public LerpAndFade lerpCode;

	public float warningDuration = 0.4f;

	public void Warning()
	{
		StopAllCoroutines();
		GetComponent<AudioSource>().Play();
		StartCoroutine(CoWarning());
	}

	private IEnumerator CoWarning()
	{
		lerpCode.LerpIn();
		yield return new WaitForSeconds(warningDuration);
		lerpCode.LerpOut();
	}
}
