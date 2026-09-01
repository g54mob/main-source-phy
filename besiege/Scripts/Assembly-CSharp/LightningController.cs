using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
	public Light lightSource;

	public int flashCount = 4;

	public float lerpSpeed = 0.1f;

	public float lightRangeRandom = 5f;

	public Renderer lightningRenderer;

	public float minTimeBetweenLightning = 6f;

	public float randomTime = 16f;

	public RandomSoundController sfxController;

	public Transform raycastSource;

	public float rayLength = 20f;

	public float forceToAddPower = 1000f;

	public bool disablePhys;

	public Color defaultColor = Color.white;

	private float startLightRange;

	private IEnumerator Start()
	{
		startLightRange = lightSource.range;
		lightningRenderer.enabled = false;
		lightSource.enabled = false;
		while (true)
		{
			yield return new WaitForSeconds(minTimeBetweenLightning);
			yield return new WaitForSeconds(Random.Range(0f, randomTime));
			StartCoroutine(Lightning());
		}
	}

	private void IgniteRaycast(Transform obj)
	{
		RaycastHit hitInfo;
		if (Physics.SphereCast(obj.position, 1f, -Vector3.up, out hitInfo, rayLength) && (bool)hitInfo.collider.attachedRigidbody)
		{
			Machine componentInParent = hitInfo.collider.GetComponentInParent<Machine>();
			if (componentInParent != null)
			{
				AchievementHelper.Increment(6, 1);
			}
			hitInfo.collider.attachedRigidbody.AddForce((Vector3.up + Random.insideUnitSphere) * 1000f);
			FireTag component = hitInfo.collider.attachedRigidbody.GetComponent<FireTag>();
			if (component != null)
			{
				component.Ignite(1f);
			}
		}
	}

	private IEnumerator Lightning()
	{
		if (StatMaster.levelSimulating && !disablePhys)
		{
			IgniteRaycast(raycastSource);
		}
		int i = 0;
		while (i < flashCount)
		{
			i++;
			lightSource.enabled = true;
			lightningRenderer.enabled = true;
			yield return StartCoroutine(LerpLight());
			lightSource.enabled = false;
			lightningRenderer.enabled = false;
			yield return new WaitForSeconds(Random.Range(0.02f, 0.06f));
		}
		sfxController.Play();
	}

	private IEnumerator LerpLight()
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		lightSource.range = startLightRange + Random.Range(0f - lightRangeRandom, lightRangeRandom);
		Color col = defaultColor;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			lightSource.intensity = Mathf.Lerp(8f, 0f, cTime);
			col.a = defaultColor.a * Mathf.Lerp(1f, 0f, cTime);
			lightningRenderer.material.SetColor("_TintColor", col);
			yield return null;
		}
	}
}
