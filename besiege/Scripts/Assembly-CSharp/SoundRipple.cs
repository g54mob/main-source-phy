using System.Collections;
using UnityEngine;

public class SoundRipple : MonoBehaviour
{
	public Vector3 maxSize = Vector3.one;

	public float maxDestortion = 6f;

	public float lerpTime = 1f;

	private float currentTime;

	private float timeRatio;

	private Renderer rend;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		StartCoroutine(Scale());
	}

	private IEnumerator Scale()
	{
		while (lerpTime >= currentTime)
		{
			currentTime += Time.deltaTime;
			timeRatio = currentTime / lerpTime;
			base.transform.localScale = Vector3.Lerp(Vector3.zero, maxSize, timeRatio);
			rend.material.SetFloat("_BumpAmt", (1f - timeRatio) * maxDestortion);
			yield return null;
		}
		Object.Destroy(base.gameObject);
	}
}
