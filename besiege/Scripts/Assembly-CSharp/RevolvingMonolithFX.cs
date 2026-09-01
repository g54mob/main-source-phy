using System.Collections;
using UnityEngine;

public class RevolvingMonolithFX : MonoBehaviour
{
	private bool isPlaying;

	public FireController[] beaconKeys;

	public MeshRenderer rendo;

	public float waitDuration = 4.5f;

	public float animDuration = 2f;

	public Color targetColor;

	public GameObject enableOnDone;

	private void Update()
	{
		if (isPlaying)
		{
			return;
		}
		for (int i = 0; i < beaconKeys.Length; i++)
		{
			if (!beaconKeys[i].onFire)
			{
				return;
			}
		}
		isPlaying = true;
		StartCoroutine(AnimateKeyStone());
	}

	private IEnumerator AnimateKeyStone()
	{
		yield return new WaitForSeconds(waitDuration);
		Color s = rendo.material.GetColor("_EmissCol");
		for (float t = 0f; t < animDuration; t += Time.deltaTime)
		{
			float pct = t / animDuration;
			rendo.material.SetColor("_EmissCol", Color.Lerp(s, targetColor, pct));
			yield return null;
		}
		rendo.material.SetColor("_EmissCol", targetColor);
		enableOnDone.SetActive(true);
		Object.Destroy(this);
	}
}
